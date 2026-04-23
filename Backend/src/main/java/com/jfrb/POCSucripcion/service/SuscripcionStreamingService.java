package com.jfrb.POCSucripcion.service;

import com.jfrb.POCSucripcion.model.SuscripcionStreaming;
import org.springframework.stereotype.Service;

@Service
public class SuscripcionStreamingService implements ISuscripcionStreamingService {

    @Override
    public void validarDispositivos(int cantidad) {
        if (cantidad < 1 || cantidad > 6) {
            throw new RuntimeException("Dispositivos debe estar entre 1 y 6");
        }
    }

    @Override
    public double calcularCosto(SuscripcionStreaming suscripcion) {
        double recargo = 0;
        int cont = 1;

        while (cont < suscripcion.getDispositivosSimultaneos()) {
            recargo += 2000;
            cont++;
        }

        return suscripcion.getPrecioMensual() + recargo;
    }
}