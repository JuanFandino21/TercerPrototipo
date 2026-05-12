package com.jfrb.POCSucripcion.repositories;

import com.jfrb.POCSucripcion.model.Usuario;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;
import java.util.Optional;

public interface UsuarioRepository extends JpaRepository<Usuario, Integer> {

    List<Usuario> findByNombreContainingIgnoreCase(String nombre);

    Optional<Usuario> findByEmail(String email);

    Optional<Usuario> findByNumDocumento(Long numDocumento);

    boolean existsByEmail(String email);

    boolean existsByNumDocumento(Long numDocumento);
}