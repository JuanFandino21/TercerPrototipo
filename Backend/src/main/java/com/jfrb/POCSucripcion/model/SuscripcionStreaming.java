package com.jfrb.POCSucripcion.model;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.time.LocalDate;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;

@Data
@AllArgsConstructor
@NoArgsConstructor
public class SuscripcionStreaming extends Suscripcion {

    private int dispositivosSimultaneos;
    private List<DispositivoPrincipal> dispositivos = new ArrayList<>();

    public SuscripcionStreaming(int id, double precioMensual, LocalDate fechaInicio,
                                LocalDate fechaVencimiento, boolean activa,
                                int dispositivosSimultaneos, List<DispositivoPrincipal> dispositivos) {

        super(id, precioMensual,
                fechaInicio.atStartOfDay(),
                fechaVencimiento.atStartOfDay(),
                activa);

        this.dispositivosSimultaneos = dispositivosSimultaneos;
        this.dispositivos = dispositivos;
    }

    public SuscripcionStreaming(int id, int dispositivosSimultaneos,
                                LocalDate fechaInicio, String nombreUsuario) throws Exception {

        super(id, 16000.0,
                fechaInicio.atStartOfDay(),
                nombreUsuario);

        setDispositivosSimultaneos(dispositivosSimultaneos);
    }

    public void setDispositivosSimultaneos(int dispositivosSimultaneos) {
        if (dispositivosSimultaneos < 1 || dispositivosSimultaneos > 6) {
            throw new RuntimeException("Dispositivos debe ser entre 1 y 6!");
        }
        this.dispositivosSimultaneos = dispositivosSimultaneos;
    }

    public void agregarDispositivo(DispositivoPrincipal dispositivo) {
        dispositivos.add(dispositivo);
    }

    @Override
    public double calcularCosto() {
        double recargo = 0;
        int cont = 1;

        while (cont < dispositivosSimultaneos) {
            recargo += 2000;
            cont++;
        }

        return getPrecioMensual() + recargo;
    }
}