package com.jfrb.POCSucripcion.service;

import com.jfrb.POCSucripcion.model.Usuario;

import java.util.List;

public interface IUsuarioService {

    Usuario addUsuario(Usuario usuario);

    Usuario buscarPorCodigo(int codigo);

    List<Usuario> getUsuarios();

    boolean eliminarUsuario(int codigo);

    Usuario actualizarUsuario(int codigo, Usuario usuario);

    // métodos que está usando el controller
    List<Usuario> buscarPorNombre(String nombre);

    Usuario buscarPorEmail(String email);
}