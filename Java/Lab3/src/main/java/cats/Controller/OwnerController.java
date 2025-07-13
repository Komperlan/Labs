package cats.Controller;

import cats.DTO.OwnerDTO;
import cats.service.OwnerService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.data.crossstore.ChangeSetPersister;
import org.springframework.http.HttpStatus;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;


@RestController
@Controller
@RequestMapping("/owners")
@RequiredArgsConstructor
@Tag(name = "Owners", description = "API for owners")
public class OwnerController {
    private final OwnerService ownerService;

    @ExceptionHandler(ChangeSetPersister.NotFoundException.class)
    @PostMapping("/add")
    @ResponseStatus(HttpStatus.CREATED)
    public OwnerDTO addOwner(@Valid @RequestBody OwnerDTO ownerDTO) {
        return ownerService.createOwner(ownerDTO);
    }

    @GetMapping("/all")
    @Operation(summary = "Get all owners with pagination")
    Page<OwnerDTO> getAllOwners(Pageable pageable) {
        return ownerService.getAllOwners(pageable);
    }

    @GetMapping("/name")
    @Operation(summary = "Get all owners with name")
    Page<OwnerDTO> getAllOwnersByName(Pageable pageable, String name) {
        return ownerService.getOwnerWithName(name, pageable);
    }
}