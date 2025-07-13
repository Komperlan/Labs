package cats.service;

import cats.AccessingData.CatRepository;
import cats.AccessingData.OwnerRepository;
import cats.DTO.OwnerDTO;
import jakarta.persistence.EntityNotFoundException;
import lombok.RequiredArgsConstructor;
import cats.models.Cat;
import cats.models.Owner;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;

@Service
@RequiredArgsConstructor
public class OwnerService {
    private final OwnerRepository ownerRepository;
    private final CatRepository catRepository;

    public Page<OwnerDTO> getAllOwners(Pageable pageable) {
        return ownerRepository.findAll(pageable).map(this::convertToDTO);
    }

    public OwnerDTO getOwnerById(Long id, Pageable pageable) {
        return convertToDTO(ownerRepository.findById(id).orElseThrow(() -> new EntityNotFoundException("owner not found")));
    }

    public OwnerDTO getOwnerByLogin(String login) {
        return convertToDTO(ownerRepository.findByLogin(login).orElseThrow(() -> new EntityNotFoundException("owner not found")));
    }

    public OwnerDTO createOwner(OwnerDTO ownerDTO) {
        Owner owner = new Owner();
        owner.setName(ownerDTO.getName());
        owner.setBirthDate(ownerDTO.getBirthdate());
        List<Cat> cats = new ArrayList<Cat>();

        List<Long> catsId = ownerDTO.getCatIds();

        if (catsId != null) {
            for (Long catId : catsId) {
                cats.add(catRepository.findById(catId).orElseThrow(() -> new EntityNotFoundException("owner not found")));
            }
        }

        owner.setLogin(ownerDTO.getLogin());
        owner.setPassword(new BCryptPasswordEncoder().encode(ownerDTO.getPassword()));

        owner.setCats(cats);

        Owner savedOwner = ownerRepository.save(owner);
        return convertToDTO(savedOwner);
    }

    public Page<OwnerDTO> getOwnerWithName(String name, Pageable pageable) {
        return ownerRepository.findAllByName(name, pageable)
                .map(this::convertToDTO);
    }

    private OwnerDTO convertToDTO(Owner owner) {
        OwnerDTO dto = new OwnerDTO();
        dto.setId(owner.getID());
        dto.setName(owner.getName());
        dto.setLogin(owner.getLogin());
        dto.setRole(owner.getRole());
        dto.setBirthdate(owner.getBirthDate());
        dto.setCatIds(owner.getCats().stream().map(Cat::getID).collect(Collectors.toList()));
        return dto;
    }
}