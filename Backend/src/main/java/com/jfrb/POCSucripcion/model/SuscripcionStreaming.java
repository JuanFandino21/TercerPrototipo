package com.jfrb.POCSucripcion.model;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import jakarta.persistence.Entity;
import jakarta.persistence.FetchType;
import jakarta.persistence.Id;
import jakarta.persistence.JoinColumn;
import jakarta.persistence.ManyToOne;
import jakarta.persistence.Table;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.time.LocalDateTime;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Entity
@Table(name = "SUSCRIPCION_STREAMING")
public class SuscripcionStreaming {

    @Id
    private int id;

    private LocalDateTime fechaInicio;
    private boolean activa;
    private int dispositivosSimultaneos;
    private double costoMensual;
    private String plataforma;

    @ManyToOne(fetch = FetchType.EAGER)
    @JoinColumn(name = "CODIGO_USUARIO", nullable = false)
    @JsonIgnoreProperties({"hibernateLazyInitializer", "handler"})
    private Usuario usuario;
}