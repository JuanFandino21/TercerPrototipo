package com.jfrb.POCSucripcion.model;

import jakarta.persistence.Entity;
import jakarta.persistence.Id;
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
    private int id;                        // PK

    private String nombreUsuario;          // quién es el dueño
    private LocalDateTime fechaInicio;     // inicio de la suscripción
    private boolean activa;                // true / false
    private int dispositivosSimultaneos;   // 1..6

}