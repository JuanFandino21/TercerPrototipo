package com.jfrb.POCSucripcion.repositories;

import com.jfrb.POCSucripcion.model.SuscripcionStreaming;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

import java.util.List;

public interface SuscripcionStreamingRepository extends JpaRepository<SuscripcionStreaming, Integer> {

    List<SuscripcionStreaming> findByActiva(boolean activa);

    List<SuscripcionStreaming> findByPlataformaContainingIgnoreCase(String plataforma);

    @Query("SELECT s FROM SuscripcionStreaming s JOIN FETCH s.usuario WHERE s.usuario.codigo = :codigo")
    List<SuscripcionStreaming> buscarPorCodigoUsuario(@Param("codigo") int codigo);

    @Query("SELECT s FROM SuscripcionStreaming s WHERE s.activa = true AND s.costoMensual <= :costo")
    List<SuscripcionStreaming> activasConCostoMenorA(@Param("costo") double costo);
}