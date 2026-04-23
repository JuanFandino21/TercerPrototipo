package com.jfrb.POCSucripcion.service;

import com.jfrb.POCSucripcion.model.Suscripcion;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;

@Service
public class SuscripcionService implements ISuscripcionService {

    private List<Suscripcion> suscripciones = new ArrayList<>();

    @Override
    public Suscripcion addSuscripcion(Suscripcion sus) {
        if (sus != null) {
            suscripciones.add(sus);
        }
        return sus;
    }

    @Override
    public List<Suscripcion> getSuscripciones() {
        return List.copyOf(suscripciones);
    }

    @Override
    public Suscripcion buscarSuscripcionPorId(int id) {
        return suscripciones.stream()
                .filter(s -> s.getId() == id)
                .findFirst()
                .orElse(null);
    }

    @Override
    public boolean eliminarSuscripcionPorId(int id) {
        return suscripciones.removeIf(s -> s.getId() == id);
    }

    @Override
    public boolean actualizarSuscripcion(Suscripcion susActualizada) {
        for (int i = 0; i < suscripciones.size(); i++) {
            if (suscripciones.get(i).getId() == susActualizada.getId()) {
                suscripciones.set(i, susActualizada);
                return true;
            }
        }
        return false;
    }
}