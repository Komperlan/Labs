package cats.AccessingData;

import cats.models.Cat;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.time.LocalDate;
import java.util.List;

@Repository
public interface CatRepository extends JpaRepository<Cat, Long> {
    Page<Cat> findAllByName(String name, Pageable pageable);
}