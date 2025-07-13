package cats.DTO;

import lombok.AllArgsConstructor;
import lombok.Data;
import cats.models.Color;
import lombok.NoArgsConstructor;

import java.time.LocalDateTime;
import java.util.List;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class CatDTO {
    private Long id;
    private String name;
    private Color color;
    private Integer tailLength;
    private Long ownerId;
    private LocalDateTime birthdate;
    private List<Long> friendIds;
    private String ownerName;
}