package com.jfrb.POCSucripcion.service;

import com.jfrb.POCSucripcion.model.SuscripcionStreaming;

public interface ISuscripcionStreamingService {

    void validarDispositivos(int cantidad);

    double calcularCosto(SuscripcionStreaming suscripcion);
}