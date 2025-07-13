package cats.AccessingData;

import cats.models.Cat;
import cats.models.Owner;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;
import java.util.Optional;

public interface OwnerRepository extends JpaRepository<Owner, Long> {
    Page<Owner> findAllByName(String name, Pageable pageable);
    Optional<Owner> findByLogin(String login);
}