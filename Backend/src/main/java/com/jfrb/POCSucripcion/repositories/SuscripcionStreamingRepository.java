package com.jfrb.POCSucripcion.repositories;

import com.jfrb.POCSucripcion.model.SuscripcionStreaming;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SuscripcionStreamingRepository
        extends JpaRepository<SuscripcionStreaming, Integer> {
}