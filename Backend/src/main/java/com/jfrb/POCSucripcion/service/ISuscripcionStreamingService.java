package com.jfrb.POCSucripcion.service;

import com.jfrb.POCSucripcion.model.SuscripcionStreaming;

import java.util.List;

public interface ISuscripcionStreamingService {

    // Reglas de negocio que ya tenías
    void validarDispositivos(int cantidad);

    double calcularCosto(SuscripcionStreaming suscripcion);

    // CRUD contra la base de datos
    SuscripcionStreaming addSuscripcion(SuscripcionStreaming suscripcion);

    SuscripcionStreaming buscarSuscripcionPorId(int id);

    boolean eliminarSuscripcionPorId(int id);

    boolean actualizarSuscripcion(SuscripcionStreaming suscripcion);

    List<SuscripcionStreaming> getSuscripciones();
}