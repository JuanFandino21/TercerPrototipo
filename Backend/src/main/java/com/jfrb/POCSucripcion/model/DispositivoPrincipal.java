package com.jfrb.POCSucripcion.model;

import java.util.Objects;

public class DispositivoPrincipal {

    private static int contadorId = 1;
    private int id;
    private String marca;
    private String modelo;

    public DispositivoPrincipal() {
    }

    public DispositivoPrincipal(String marca, String modelo) {
        this.id = contadorId++;
        this.marca = marca;
        this.modelo = modelo;
    }

    public int getId() {
        return id;
    }

    public String getMarca() {
        return marca;
    }

    public String getModelo() {
        return modelo;
    }

    public void setMarca(String marca) {
        this.marca = marca;
    }

    public void setModelo(String modelo) {
        this.modelo = modelo;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) {
            return true;
        }
        if (o == null || getClass() != o.getClass()) {
            return false;
        }
        DispositivoPrincipal that = (DispositivoPrincipal) o;
        return Objects.equals(marca, that.marca)
                && Objects.equals(modelo, that.modelo);
    }

    @Override
    public int hashCode() {
        return Objects.hash(marca, modelo);
    }

    @Override
    public String toString() {
        return id + " - " + marca + " " + modelo;
    }
}
