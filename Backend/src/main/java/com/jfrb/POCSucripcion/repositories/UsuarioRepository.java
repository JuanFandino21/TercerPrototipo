package com.jfrb.POCSucripcion.repositories;

import com.jfrb.POCSucripcion.model.Usuario;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;
import java.util.Optional;

public interface UsuarioRepository extends JpaRepository<Usuario, Integer> {

    // Buscar por nombre (contiene, ignore case)
    List<Usuario> findByNombreContainingIgnoreCase(String nombre);

    // Buscar por email exacto
    Optional<Usuario> findByEmail(String email);
}