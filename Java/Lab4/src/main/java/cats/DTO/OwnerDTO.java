package cats.DTO;

import cats.models.Role;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.time.LocalDateTime;
import java.util.Collection;
import java.util.List;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class OwnerDTO {
    private Long id;
    private String name;
    private String login;
    private String password;
    private Role role;
    private LocalDateTime birthdate;
    private List<Long> CatIds;
}
