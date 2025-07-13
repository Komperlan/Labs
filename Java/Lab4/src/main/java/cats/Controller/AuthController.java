package cats.Controller;

import cats.DTO.OwnerDTO;
import cats.service.OwnerService;
import io.swagger.v3.oas.annotations.tags.Tag;
import lombok.RequiredArgsConstructor;
import org.springframework.http.ResponseEntity;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

@RestController
@Controller
@RequestMapping("/api/auth")
@RequiredArgsConstructor
@Tag(name = "Authorization", description = "API for authorization")
public class AuthController {
    private final AuthenticationManager authenticationManager;

    private final OwnerService ownerService;

    @ExceptionHandler
    @PostMapping("/login")
    public ResponseEntity<String> login(@RequestBody OwnerDTO request) {
        Authentication authentication = authenticationManager.authenticate(
                new UsernamePasswordAuthenticationToken(
                        request.getLogin(),
                        request.getPassword()
                )
        );

        SecurityContextHolder.getContext().setAuthentication(authentication);
        return ResponseEntity.ok("Login successful");
    }

    @ExceptionHandler
    @PostMapping("/register")
    public ResponseEntity<OwnerDTO> register(@RequestBody OwnerDTO request) {
        OwnerDTO user = ownerService.createOwner(request);
        return ResponseEntity.ok(user);
    }

    @ExceptionHandler
    @PostMapping("admin/register")
    public ResponseEntity<OwnerDTO> adminRegister(@RequestBody OwnerDTO request) {
        OwnerDTO user = ownerService.createOwner(request);
        return ResponseEntity.ok(user);
    }

    @ExceptionHandler
    @GetMapping("/logout")
    public ResponseEntity<String> logout() {
        SecurityContextHolder.clearContext();
        return ResponseEntity.ok("Logout successful");
    }
}
