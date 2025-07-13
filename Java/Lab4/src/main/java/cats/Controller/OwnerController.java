package cats.Controller;

import cats.service.OwnerService;
import cats.DTO.OwnerDTO;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;

@RestController
@Controller
@RequestMapping("/api/owners")
@RequiredArgsConstructor
@Tag(name = "Owners", description = "API for owners")
public class OwnerController {
    private final OwnerService ownerService;

    @ExceptionHandler
    @PostMapping("/add")
    @PreAuthorize("hasAnyRole('ROLE_USER', 'ROLE_ADMIN')")
    @ResponseStatus(HttpStatus.CREATED)
    public OwnerDTO addOwner(@Valid @RequestBody OwnerDTO ownerDTO) {
        return ownerService.createOwner(ownerDTO);
    }

    @ExceptionHandler
    @GetMapping("/all")
    @PreAuthorize("hasAnyRole('ROLE_USER', 'ROLE_ADMIN')")
    @Operation(summary = "Get all owners with pagination")
    Page<OwnerDTO> getAllOwners(Pageable pageable) {
        return ownerService.getAllOwners(pageable);
    }

    @ExceptionHandler
    @GetMapping("/name")
    @PreAuthorize("hasAnyRole('ROLE_USER', 'ROLE_ADMIN')")
    @Operation(summary = "Get all owners with color")
    Page<OwnerDTO> getAllCatsByName(Pageable pageable, String name) {
        return ownerService.getOwnerWithName(name, pageable);
    }
}