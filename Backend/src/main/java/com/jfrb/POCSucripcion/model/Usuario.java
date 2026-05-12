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
@Table(name = "USUARIO")
public class Usuario {

    @Id
    private int codigo;

    private Long numDocumento;
    private String tipoDoc;
    private String nombre;
    private String email;
    private LocalDateTime fechaRegistro;
    private String estado;
}