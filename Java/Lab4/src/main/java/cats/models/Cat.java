package cats.models;

import jakarta.persistence.*;
import lombok.Getter;
import lombok.Setter;

import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;


@Getter
@Setter
@Entity @Table(name = "pet")
public class Cat {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long ID;

    @Column(name="name", length = 255)
    private String Name;

    @Column(name="birthdate")
    private LocalDateTime Birthdate;

    @Column(name="breed")
    private String Breed;

    @Column(name="tail_length")
    private int TailLength;

    @Column(name="color")
    @Enumerated(EnumType.STRING)
    private Color Color;

    @ManyToOne
    @JoinColumn(name = "owner_id")
    private Owner owner;


    @OneToMany(cascade = CascadeType.ALL)
    @JoinTable(
            name = "pet_friends",
            joinColumns = @JoinColumn(name = "pet_id"),
            inverseJoinColumns = @JoinColumn(name = "friend_id")
    )
    private List<Cat> friends = new ArrayList<Cat>();

    public void addFriend(Cat friend) {
        this.friends.add(friend);
        friend.getFriends().add(this);
    }
}
