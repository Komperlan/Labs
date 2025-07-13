package cats;

import cats.Controller.AuthController;
import cats.Controller.CatController;
import cats.DTO.CatDTO;
import cats.DTO.OwnerDTO;
import cats.Security.OwnerDetailService;
import cats.Security.SecurityConfig;
import cats.models.Color;
import cats.models.Role;
import cats.service.CatService;
import cats.service.OwnerService;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.junit.jupiter.api.Test;
import org.skyscreamer.jsonassert.JSONAssert;
import org.skyscreamer.jsonassert.JSONCompareMode;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.WebMvcTest;
import org.springframework.context.annotation.Import;
import org.springframework.data.domain.PageImpl;
import org.springframework.http.MediaType;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.authority.SimpleGrantedAuthority;
import org.springframework.security.test.context.support.WithMockUser;
import org.springframework.test.context.bean.override.mockito.MockitoBean;
import org.springframework.test.web.servlet.MockMvc;

import java.time.LocalDateTime;
import java.util.Collections;
import java.util.List;

import static org.mockito.ArgumentMatchers.any;
import static org.mockito.Mockito.when;
import static org.springframework.security.test.web.servlet.request.SecurityMockMvcRequestPostProcessors.csrf;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.*;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

@WebMvcTest(AuthController.class)
@Import(SecurityConfig.class)
class AuthControllerTest {

    @Autowired
    private MockMvc mockMvc;

    @Autowired
    private ObjectMapper objectMapper;

    @MockitoBean
    private AuthenticationManager authenticationManager;

    @MockitoBean
    private OwnerDetailService ownerDetailService;

    @MockitoBean
    private OwnerService ownerService;

    @MockitoBean
    private CatService catService;

    @Test
    void unauthenticatedRequest_shouldReturn401() throws Exception {
        mockMvc.perform(get("/api/cats/all"))
                .andExpect(status().isUnauthorized());
    }

    @Test
    @WithMockUser
    void login_WithValidCredentials_ReturnsOk() throws Exception {
        when(authenticationManager.authenticate(any()))
                .thenReturn(new UsernamePasswordAuthenticationToken(
                        "Andrew",
                        null,
                        List.of(new SimpleGrantedAuthority("USER_ROLE")))
                );

        OwnerDTO loginRequest = new OwnerDTO();
        loginRequest.setLogin("Andrew");
        loginRequest.setPassword("encodedPass");

        mockMvc.perform(post("/api/auth/login")
                        .with(csrf())
                        .contentType(MediaType.APPLICATION_JSON)
                        .content(objectMapper.writeValueAsString(loginRequest)))
                .andExpect(status().isOk());
    }

    @Test
    @WithMockUser
    void register_NewUser_ReturnsCreatedUser() throws Exception {
        OwnerDTO request = new OwnerDTO();
        request.setName("Andrew");
        request.setLogin("Dryu");
        request.setPassword("encodedPass");
        request.setRole(Role.USER_ROLE);

        OwnerDTO response = new OwnerDTO();
        response.setId(1L);
        response.setName("Andrew");
        response.setLogin("Dryu");
        response.setRole(Role.USER_ROLE);

        when(ownerService.createOwner(any(OwnerDTO.class)))
                .thenReturn(response);

        mockMvc.perform(post("/api/auth/register")
                        .with(csrf())
                        .contentType(MediaType.APPLICATION_JSON)
                        .content(objectMapper.writeValueAsString(request)))
                .andExpect(status().isOk());
    }

}