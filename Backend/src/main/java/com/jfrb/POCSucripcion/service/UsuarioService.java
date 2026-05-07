package com.jfrb.POCSucripcion.service;

import com.jfrb.POCSucripcion.model.Usuario;
import com.jfrb.POCSucripcion.repositories.UsuarioRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class UsuarioService implements IUsuarioService {

    @Autowired
    private UsuarioRepository usuarioRepository;

    @Override
    public Usuario addUsuario(Usuario usuario) {
        return usuarioRepository.save(usuario);
    }

    @Override
    public Usuario buscarPorCodigo(int codigo) {
        return usuarioRepository.findById(codigo).orElse(null);
    }

    @Override
    public List<Usuario> getUsuarios() {
        return usuarioRepository.findAll();
    }

    @Override
    public boolean eliminarUsuario(int codigo) {
        if (!usuarioRepository.existsById(codigo)) {
            return false;
        }
        usuarioRepository.deleteById(codigo);
        return true;
    }

    @Override
    public Usuario actualizarUsuario(int codigo, Usuario usuario) {
        if (!usuarioRepository.existsById(codigo)) {
            return null;
        }
        usuario.setCodigo(codigo);
        return usuarioRepository.save(usuario);
    }

    @Override
    public List<Usuario> buscarPorNombre(String nombre) {
        return usuarioRepository.findByNombreContainingIgnoreCase(nombre);
    }

    @Override
    public Usuario buscarPorEmail(String email) {
        return usuarioRepository.findByEmail(email).orElse(null);
    }
}