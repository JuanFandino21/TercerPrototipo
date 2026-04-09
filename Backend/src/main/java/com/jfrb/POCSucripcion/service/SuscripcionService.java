package com.jfrb.POCSucripcion.service;

import com.jfrb.POCSucripcion.model.Suscripcion;
import java.util.ArrayList;
import java.util.List;

public class SuscripcionService {

    // singleton
    private static SuscripcionService suscripcionService;

    private List<Suscripcion> suscripciones = new ArrayList<>();

    private int contadorIdStreaming = 1;
    private int contadorIdNube = 1;

    private SuscripcionService() {
    }

    public static SuscripcionService getInstance() {
        if (suscripcionService == null) {
            suscripcionService = new SuscripcionService();
        }
        return suscripcionService;
    }

    public Suscripcion addSuscripcion(Suscripcion sus) {
        if (sus != null) {
            suscripciones.add(sus);
        }
        return sus;
    }

    public int generarId(String tipo) {
        if (tipo.equals("streaming")) {
            return contadorIdStreaming++;
        }
        if (tipo.equals("nube")) {
            return contadorIdNube++;
        }
        return -1;
    }

    public List<Suscripcion> getSuscripciones() {
        return List.copyOf(suscripciones);
    }

    public Suscripcion buscarSuscripcionPorId(int id) {
        return suscripciones.stream()
                .filter(s -> s.getId() == id)
                .findFirst()
                .orElse(null);
    }

    public boolean eliminarSuscripcionPorId(int id) {
        return suscripciones.removeIf(s -> s.getId() == id);
    }

    public boolean actualizarSuscripcion(Suscripcion susActualizada) {
        for (int i = 0; i < suscripciones.size(); i++) {
            if (suscripciones.get(i).getId() == susActualizada.getId()) {
                suscripciones.set(i, susActualizada);
                return true;
            }
        }
        return false;
    }

    public List<Suscripcion> buscarPorNombre(String nombre) {
        return suscripciones.stream()
                .filter(s -> s.getNombreUsuario() != null &&
                        s.getNombreUsuario().toLowerCase().contains(nombre.toLowerCase()))
                .toList();
    }

    public double calcularCostoTotal() {
        return suscripciones.stream()
                .mapToDouble(Suscripcion::calcularCosto)
                .sum();
    }

}