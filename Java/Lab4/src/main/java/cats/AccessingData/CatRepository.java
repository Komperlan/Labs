package cats.AccessingData;

import cats.models.Cat;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.repository.JpaRepository;

import java.time.LocalDate;
import java.util.List;

public interface CatRepository extends JpaRepository<Cat, Long> {
    Page<Cat> findAllByName(String name, Pageable pageable);
}