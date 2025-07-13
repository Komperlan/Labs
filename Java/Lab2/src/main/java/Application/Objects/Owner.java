package Application.Objects;

import jakarta.persistence.*;
import lombok.Getter;
import lombok.Setter;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

@Getter
@Setter
@Entity @Table(name = "owner")
public class Owner {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private long id;

    @Column(name="name", length = 255)
    @Getter
    private String Name;

    @Column(name="birthdate")
    @Getter
    private Date BirthDate;

    @OneToMany(mappedBy = "owner", cascade = CascadeType.ALL)
    @Getter
    private List<Cat> cats;
}