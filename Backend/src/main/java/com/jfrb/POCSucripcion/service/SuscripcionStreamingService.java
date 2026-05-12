package com.jfrb.POCSucripcion.service;

import com.jfrb.POCSucripcion.model.SuscripcionStreaming;
import com.jfrb.POCSucripcion.repositories.SuscripcionStreamingRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class SuscripcionStreamingService implements ISuscripcionStreamingService {

    @Autowired
    private SuscripcionStreamingRepository repo;

    @Override
    public SuscripcionStreaming addSuscripcion(SuscripcionStreaming suscripcion) {

        if (repo.existsById(suscripcion.getId())) {
            throw new RuntimeException("Ya existe una suscripción con el ID: " + suscripcion.getId());
        }

        suscripcion.setCostoMensual(calcularCostoMensual(suscripcion.getDispositivosSimultaneos()));

        return repo.save(suscripcion);
    }

    @Override
    public SuscripcionStreaming buscarSuscripcionPorId(int id) {
        return repo.findById(id).orElse(null);
    }

    @Override
    public List<SuscripcionStreaming> getSuscripciones() {
        return repo.findAll();
    }

    @Override
    public List<SuscripcionStreaming> filtrarPorActiva(boolean activa) {
        return repo.findByActiva(activa);
    }

    @Override
    public List<SuscripcionStreaming> filtrarPorPlataforma(String plataforma) {
        return repo.findByPlataformaContainingIgnoreCase(plataforma);
    }

    @Override
    public List<SuscripcionStreaming> buscarPorCodigoUsuario(int codigo) {
        return repo.buscarPorCodigoUsuario(codigo);
    }

    @Override
    public List<SuscripcionStreaming> activasConCostoMenorA(double costo) {
        return repo.activasConCostoMenorA(costo);
    }

    @Override
    public boolean eliminarSuscripcionPorId(int id) {
        if (!repo.existsById(id)) {
            return false;
        }

        repo.deleteById(id);
        return true;
    }

    @Override
    public boolean actualizarSuscripcion(SuscripcionStreaming suscripcion) {
        if (!repo.existsById(suscripcion.getId())) {
            return false;
        }

        suscripcion.setCostoMensual(calcularCostoMensual(suscripcion.getDispositivosSimultaneos()));
        repo.save(suscripcion);

        return true;
    }

    @Override
    public void validarDispositivos(int dispositivos) {
        if (dispositivos < 1 || dispositivos > 6) {
            throw new RuntimeException("Los dispositivos simultáneos deben estar entre 1 y 6");
        }
    }

    private double calcularCostoMensual(int dispositivos) {
        return switch (dispositivos) {
            case 1 -> 16000;
            case 2 -> 18000;
            case 3 -> 20000;
            case 4 -> 22000;
            case 5 -> 24000;
            case 6 -> 26000;
            default -> throw new RuntimeException("Cantidad de dispositivos inválida");
        };
    }
}