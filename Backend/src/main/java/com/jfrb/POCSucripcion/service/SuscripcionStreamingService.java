package com.jfrb.POCSucripcion.service;

import com.jfrb.POCSucripcion.model.SuscripcionStreaming;
import com.jfrb.POCSucripcion.repositories.SuscripcionStreamingRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class SuscripcionStreamingService implements ISuscripcionStreamingService {

    @Autowired
    private SuscripcionStreamingRepository streamingRepository;

    // ── Reglas de negocio ─────────────────────────────────────────────

    @Override
    public void validarDispositivos(int cantidad) {
        if (cantidad < 1 || cantidad > 6) {
            throw new RuntimeException("La cantidad de pantallas debe estar entre 1 y 6");
        }
    }

    @Override
    public double calcularCosto(SuscripcionStreaming suscripcion) {
        final double PRECIO_BASE = 16000.0;
        final double RECARGO_POR_PANTALLA = 2000.0;

        int dispositivos = suscripcion.getDispositivosSimultaneos();
        validarDispositivos(dispositivos); // refuerzo de la regla en capa de dominio

        double recargo = 0;
        int cont = 1;

        while (cont < dispositivos) {
            recargo += RECARGO_POR_PANTALLA;
            cont++;
        }

        return PRECIO_BASE + recargo;
    }

    // ── CRUD sobre la BD (usando JPA) ─────────────────────────────────

    @Override
    public SuscripcionStreaming addSuscripcion(SuscripcionStreaming suscripcion) {
        // Aquí podrías usar calcularCosto si luego decides guardar el costo total
        return streamingRepository.save(suscripcion);
    }

    @Override
    public SuscripcionStreaming buscarSuscripcionPorId(int id) {
        return streamingRepository.findById(id).orElse(null);
    }

    @Override
    public boolean eliminarSuscripcionPorId(int id) {
        if (!streamingRepository.existsById(id)) {
            return false;
        }
        streamingRepository.deleteById(id);
        return true;
    }

    @Override
    public boolean actualizarSuscripcion(SuscripcionStreaming suscripcion) {
        if (!streamingRepository.existsById(suscripcion.getId())) {
            return false;
        }
        streamingRepository.save(suscripcion);
        return true;
    }

    @Override
    public List<SuscripcionStreaming> getSuscripciones() {
        return streamingRepository.findAll();
    }
}