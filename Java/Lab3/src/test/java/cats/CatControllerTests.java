package cats;

import cats.Controller.CatController;
import cats.DTO.CatDTO;

import cats.models.Color;
import cats.service.CatService;

import org.junit.jupiter.api.Test;

import org.skyscreamer.jsonassert.JSONAssert;
import org.skyscreamer.jsonassert.JSONCompareMode;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.WebMvcTest;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.http.MediaType;
import org.springframework.test.context.bean.override.mockito.MockitoBean;
import org.springframework.test.web.servlet.MockMvc;

import java.time.LocalDateTime;

import static org.mockito.Mockito.*;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.*;

@WebMvcTest(CatController.class)
@AutoConfigureMockMvc
class CatControllerTest {

    @Autowired
    private MockMvc mockMvc;

    @MockitoBean
    private CatService catService;

    @Test
    void addCat_WithValidColor_ShouldReturnCreated() throws Exception {
        CatDTO response = new CatDTO(1L, "Mers", Color.GRAY, 20, null, LocalDateTime.of(2014, 5, 10, 0, 0), null, null);
        when(catService.createCat(any())).thenReturn(response);

        String resultJson = mockMvc.perform(post("/api/cats/cat")
                .contentType(MediaType.APPLICATION_JSON)
                .content("""
                    {
                      "name": "Mers",
                      "color": "GRAY",
                      "tailLength": 20,
                      "bithdate": "2014-05-10T00:00:00"
                    }
                    """)).andReturn().getResponse().getContentAsString();

        JSONAssert.assertEquals("{id: 1, name: \"Mers\", color: \"GRAY\", tailLength: 20, ownerId: null, birthdate: \"2014-05-10T00:00:00\", friendIds: null, ownerName: null}", resultJson, JSONCompareMode.NON_EXTENSIBLE);
    }
}