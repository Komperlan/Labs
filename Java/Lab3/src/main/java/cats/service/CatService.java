package cats.service;

import cats.AccessingData.CatRepository;
import cats.AccessingData.OwnerRepository;
import cats.DTO.CatDTO;
import cats.models.Owner;
import jakarta.persistence.EntityNotFoundException;
import lombok.RequiredArgsConstructor;
import cats.models.Cat;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.stream.Collectors;

@Service
@RequiredArgsConstructor
public class CatService {
    private final CatRepository catRepository;
    private final OwnerRepository ownerRepository;


    public Page<CatDTO> getAllCats(Pageable pageable) {
        return catRepository.findAll(pageable).map(this::convertToDTO);
    }

    public CatDTO getCatById(Long id) {
        return convertToDTO(catRepository.findById(id).orElseThrow(() -> new EntityNotFoundException("cat not found")));
    }

    @Transactional
    public void addFriendship(Long cat1Id, Long cat2Id) {
        Cat cat1 = catRepository.findById(cat1Id).orElseThrow(() -> new EntityNotFoundException("cat not found"));
        Cat cat2 = catRepository.findById(cat2Id).orElseThrow(() -> new EntityNotFoundException("cat not found"));
        cat1.addFriend(cat2);
    }

    public Page<CatDTO> getCatWithName(String name, Pageable pageable) {
        return catRepository.findAllByName(name, pageable)
                .map(this::convertToDTO);
    }

    public CatDTO createCat(CatDTO catDTO) {
        Cat cat = new Cat();
        cat.setName(catDTO.getName());
        cat.setColor(catDTO.getColor());
        cat.setTailLength(catDTO.getTailLength());
        cat.setBirthdate(catDTO.getBirthdate());

        if (catDTO.getOwnerId() != null) {
            Owner owner = ownerRepository.findById(catDTO.getOwnerId()).orElseThrow(() -> new EntityNotFoundException("owner not found"));
            cat.setOwner(owner);
        }

        Cat savedCat = catRepository.save(cat);
        return convertToDTO(savedCat);
    }

    private CatDTO convertToDTO(Cat cat) {
        CatDTO dto = new CatDTO();
        dto.setId(cat.getID());
        dto.setName(cat.getName());
        dto.setColor(cat.getColor());
        dto.setTailLength(cat.getTailLength());
        dto.setFriendIds(cat.getFriends().stream().map(Cat::getID).collect(Collectors.toList()));
        dto.setBirthdate(cat.getBirthdate());

        if (cat.getOwner() != null) {
            dto.setOwnerId(cat.getOwner().getID());
            dto.setOwnerName(cat.getOwner().getName());
        }
        return dto;
    }
}