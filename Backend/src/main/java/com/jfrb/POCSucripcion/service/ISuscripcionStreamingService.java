package com.jfrb.POCSucripcion.service;

import com.jfrb.POCSucripcion.model.SuscripcionStreaming;

import java.util.List;

public interface ISuscripcionStreamingService {

    SuscripcionStreaming addSuscripcion(SuscripcionStreaming suscripcion);

    SuscripcionStreaming buscarSuscripcionPorId(int id);

    List<SuscripcionStreaming> getSuscripciones();

    List<SuscripcionStreaming> filtrarPorActiva(boolean activa);

    List<SuscripcionStreaming> filtrarPorPlataforma(String plataforma);

    List<SuscripcionStreaming> buscarPorCodigoUsuario(int codigo);

    List<SuscripcionStreaming> activasConCostoMenorA(double costo);

    boolean eliminarSuscripcionPorId(int id);

    boolean actualizarSuscripcion(SuscripcionStreaming suscripcion);

    void validarDispositivos(int dispositivos);
}