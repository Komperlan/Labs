package cats.Controller;

import cats.service.CatService;
import cats.DTO.CatDTO;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import io.swagger.v3.oas.annotations.Parameter;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;

@RestController
@Controller
@RequestMapping("/api/cats")
@RequiredArgsConstructor
@Tag(name = "Cats", description = "API for cats and their friendships")
public class CatController {
    private final CatService catService;

    @ExceptionHandler
    @PostMapping("/{catId}/friends/{friendId}")
    @Operation(summary = "Make friends")
    public void addFriend(
            @Parameter(description = "First cat ID") @PathVariable Long catId,
            @Parameter(description = "Second cat ID") @PathVariable Long friendId) {
        catService.addFriendship(catId, friendId);
    }

    @ExceptionHandler
    @PostMapping("/cat")
    public CatDTO addCat(@Valid @RequestBody CatDTO catDTO) {
        return catService.createCat(catDTO);
    }

    @ExceptionHandler
    @GetMapping("/all")
    @Operation(summary = "Get all cats with pagination")
    Page<CatDTO> getAllCats(Pageable pageable) {
        return catService.getAllCats(pageable);
    }

    @ExceptionHandler
    @GetMapping("/name")
    @Operation(summary = "Get all cats with color")
    Page<CatDTO> getAllCatsByName(Pageable pageable, String name) {
        return catService.getCatWithName(name, pageable);
    }
}