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
    private int codigo;               // NUMBER(10)

    private Long numDocumento;        // NUMBER(15)
    private String tipoDoc;           // CC, TI, PAS, CE
    private String nombre;            // VARCHAR2(255)
    private String email;             // VARCHAR2(255)
    private LocalDateTime fechaRegistro;
    private String estado;            // AC / IN
}