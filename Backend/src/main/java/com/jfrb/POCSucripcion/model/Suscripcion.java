package com.jfrb.POCSucripcion.model;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.time.LocalDateTime;

@Data
@AllArgsConstructor
@NoArgsConstructor
public abstract class Suscripcion {

    private int id;
    private double precioMensual;
    private LocalDateTime fechaInicio;
    private LocalDateTime fechaVencimiento;
    private boolean activa;
    private String nombreUsuario;

    public Suscripcion(int id, double precioMensual, LocalDateTime fechaInicio,
                       LocalDateTime fechaVencimiento, boolean activa) {
        this.id = id;
        this.precioMensual = precioMensual;
        this.fechaInicio = fechaInicio;
        this.fechaVencimiento = fechaVencimiento;
        this.activa = activa;
    }

    public Suscripcion(int id, double precioMensual, LocalDateTime fechaInicio, String nombreUsuario) {
        this.id = id;
        this.precioMensual = precioMensual;
        this.fechaInicio = fechaInicio;
        this.fechaVencimiento = fechaInicio.plusMonths(1);
        this.activa = true;
        this.nombreUsuario = nombreUsuario;
    }

    public void validarId(int id) throws Exception {
        if (id < 1) {
            throw new Exception("ID no válido!");
        }
    }

    public void validarFechaInicio(LocalDateTime fechaInicio) throws Exception {
        if (fechaInicio.isBefore(LocalDateTime.now())) {
            throw new Exception("La fecha no puede ser anterior a hoy ");
        }
        if (fechaInicio.isAfter(LocalDateTime.now().plusMonths(1))) {
            throw new Exception("La fecha no puede ser mayor a un mes desde hoy ");
        }
    }

    public double calcularCosto() {
        return precioMensual;
    }
}