package cats.models;

import jakarta.persistence.*;
import lombok.Getter;
import lombok.Setter;

import java.time.LocalDateTime;
import java.util.List;

@Getter
@Setter
@Entity @Table(name = "owner")
public class Owner {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long ID;

    @Column(name="name", length = 255)
    @Getter
    private String Name;

    @Column(name="birthdate")
    @Getter
    private LocalDateTime BirthDate;

    @OneToMany(mappedBy = "owner", cascade = CascadeType.ALL)
    @Getter
    private List<Cat> cats;
}