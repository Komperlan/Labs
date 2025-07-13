package cats;

import cats.Controller.CatController;
import cats.Controller.OwnerController;
import cats.DTO.OwnerDTO;

import cats.service.OwnerService;

import org.junit.jupiter.api.Test;

import org.skyscreamer.jsonassert.JSONAssert;
import org.skyscreamer.jsonassert.JSONCompareMode;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.autoconfigure.web.servlet.WebMvcTest;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.http.MediaType;
import org.springframework.test.context.bean.override.mockito.MockitoBean;
import org.springframework.test.web.servlet.MockMvc;
import java.time.LocalDateTime;

import static org.mockito.ArgumentMatchers.any;
import static org.mockito.Mockito.when;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.*;

@WebMvcTest(OwnerController.class)
@AutoConfigureMockMvc
class OwnerControllerTest {

    @Autowired
    private MockMvc mockMvc;

    @MockitoBean
    private OwnerService ownerService;


    @Test
    void addOwner_WithValidData_ShouldReturnCreated() throws Exception {
        OwnerDTO response = new OwnerDTO(1L, "Andrew", LocalDateTime.of(2005, 9, 7, 0, 0), null);
        when(ownerService.createOwner(any())).thenReturn(response);

        String resultJson = mockMvc.perform(post("/owners/add").contentType(MediaType.APPLICATION_JSON).content("""
                    {
                      "id": 1,
                      "name": "Andrew",
                      "birthdate": "2005-09-07T00:00:00",
                      "catIds": []
                    }
                    """))
                .andReturn().getResponse().getContentAsString();

        JSONAssert.assertEquals("{id: 1, name: \"Andrew\", birthdate: \"2005-09-07T00:00:00\", catIds: null}", resultJson, JSONCompareMode.NON_EXTENSIBLE);
    }
}