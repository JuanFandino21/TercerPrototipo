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

        if (usuarioRepository.existsById(usuario.getCodigo())) {
            throw new RuntimeException("Ya existe un usuario con el código: " + usuario.getCodigo());
        }

        if (usuarioRepository.existsByNumDocumento(usuario.getNumDocumento())) {
            throw new RuntimeException("Ya existe un usuario con el documento: " + usuario.getNumDocumento());
        }

        if (usuarioRepository.existsByEmail(usuario.getEmail())) {
            throw new RuntimeException("Ya existe un usuario con el email: " + usuario.getEmail());
        }

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
        Usuario existente = usuarioRepository.findById(codigo).orElse(null);

        if (existente == null) {
            return null;
        }

        Usuario usuarioDocumento = usuarioRepository.findByNumDocumento(usuario.getNumDocumento()).orElse(null);

        if (usuarioDocumento != null && usuarioDocumento.getCodigo() != codigo) {
            throw new RuntimeException("Ya existe otro usuario con el documento: " + usuario.getNumDocumento());
        }

        Usuario usuarioEmail = usuarioRepository.findByEmail(usuario.getEmail()).orElse(null);

        if (usuarioEmail != null && usuarioEmail.getCodigo() != codigo) {
            throw new RuntimeException("Ya existe otro usuario con el email: " + usuario.getEmail());
        }

        existente.setNumDocumento(usuario.getNumDocumento());
        existente.setTipoDoc(usuario.getTipoDoc());
        existente.setNombre(usuario.getNombre());
        existente.setEmail(usuario.getEmail());
        existente.setEstado(usuario.getEstado());

        return usuarioRepository.save(existente);
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