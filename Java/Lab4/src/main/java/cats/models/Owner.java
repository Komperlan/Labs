package cats.models;

import jakarta.persistence.*;
import lombok.Getter;
import lombok.Setter;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.authority.SimpleGrantedAuthority;
import org.springframework.security.core.GrantedAuthority;

import java.time.LocalDateTime;
import java.util.Collection;
import java.util.List;
import java.util.stream.Collectors;

@Getter
@Setter
@Entity
@Table(name = "owner")
public class Owner implements UserDetails {
    @Id
    @Column(name="id")
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long ID;

    @Column(name="name", length = 255)
    @Getter
    private String Name;

    @Column(name="password", length = 255)
    private  String Password;

    @Column(name="login", length = 255, unique = true)
    private  String login;

    @Column(name="birthdate")
    @Getter
    private LocalDateTime BirthDate;

    @OneToMany(mappedBy = "owner", cascade = CascadeType.ALL)
    @Getter
    private List<Cat> cats;

    private Role role;

    @Override
    public Collection<? extends GrantedAuthority> getAuthorities() {
        return List.of(new SimpleGrantedAuthority(role.toString()));
    }

    private boolean enabled;
    private boolean tokenExpired;

    @Override
    public String getUsername() {
        return Name;
    }

    @Override
    public boolean isAccountNonExpired() {
        return tokenExpired;
    }

    @Override
    public boolean isAccountNonLocked() {
        return enabled;
    }

    @Override
    public boolean isCredentialsNonExpired() {
        return tokenExpired;
    }

    @Override
    public boolean isEnabled() {
        return enabled;
    }
}