package Application.Objects;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import jakarta.persistence.*;
import lombok.Getter;
import lombok.Setter;


@Getter
@Setter
@Entity @Table(name = "pet")
public class Cat {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private long ID;

    @Column(name="name", length = 255)
    private String Name;

    @Column(name="birthdate")
    private Date Birthdate;

    @Column(name="breed")
    private String Breed;

    @Column(name="color")
    @Enumerated(EnumType.STRING)
    private Color Color;

    @ManyToOne
    @JoinColumn(name = "owner_id")
    private Application.Objects.Owner owner;


    @OneToMany(cascade = CascadeType.ALL)
    @JoinTable(
            name = "pet_friends",
            joinColumns = @JoinColumn(name = "pet_id"),
            inverseJoinColumns = @JoinColumn(name = "friend_id")
    )
    private List<Cat> friends = new ArrayList<Cat>();
}
