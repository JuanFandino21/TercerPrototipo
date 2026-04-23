package com.jfrb.POCSucripcion.service;

import com.jfrb.POCSucripcion.model.Suscripcion;
import java.util.List;

public interface ISuscripcionService {

    Suscripcion addSuscripcion(Suscripcion sus);

    List<Suscripcion> getSuscripciones();

    Suscripcion buscarSuscripcionPorId(int id);

    boolean eliminarSuscripcionPorId(int id);

    boolean actualizarSuscripcion(Suscripcion susActualizada);
}