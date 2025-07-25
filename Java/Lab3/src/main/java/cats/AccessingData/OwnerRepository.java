package cats.AccessingData;

import cats.models.Cat;
import cats.models.Owner;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface OwnerRepository extends JpaRepository<Owner, Long> {
    Page<Owner> findAllByName(String name, Pageable pageable);
}