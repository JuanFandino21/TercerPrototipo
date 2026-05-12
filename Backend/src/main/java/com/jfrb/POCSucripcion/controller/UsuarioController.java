package com.jfrb.POCSucripcion.controller;

import com.jfrb.POCSucripcion.model.Usuario;
import com.jfrb.POCSucripcion.service.IUsuarioService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.time.LocalDateTime;
import java.util.List;
import java.util.Map;

@RestController
@RequestMapping(value = "/usuarios")
@CrossOrigin(origins = "http://localhost:3000")
public class UsuarioController {

    @Autowired
    private IUsuarioService usuarioService;

    @GetMapping(value = "/healthCheck")
    public ResponseEntity<String> healthCheck() {
        return ResponseEntity.ok("Ok!");
    }

    @PostMapping(value = "/")
    public ResponseEntity<?> addUsuario(@RequestBody Usuario usuario) {
        try {
            validarUsuarioParaCrear(usuario);

            if (usuario.getFechaRegistro() == null) {
                usuario.setFechaRegistro(LocalDateTime.now());
            }

            Usuario responseUsuario = usuarioService.addUsuario(usuario);
            return ResponseEntity.ok(responseUsuario);

        } catch (RuntimeException e) {
            String mensaje = e.getMessage();

            if (mensaje != null && mensaje.startsWith("Ya existe")) {
                return ResponseEntity.status(409).body(mensaje);
            }

            return ResponseEntity.badRequest().body(mensaje);
        }
    }

    @GetMapping(value = "/all")
    public ResponseEntity<List<Usuario>> getUsuarios() {
        List<Usuario> usuarios = usuarioService.getUsuarios();

        /*
         * Importante:
         * Si no hay usuarios, se retorna una lista vacía [] con 200 OK.
         * No se debe retornar 404, porque la ruta sí existe.
         */
        return ResponseEntity.ok(usuarios);
    }

    @GetMapping(value = "/find/{codigo}")
    public ResponseEntity<?> getUsuarioByCodigo(@PathVariable int codigo) {
        Usuario usuario = usuarioService.buscarPorCodigo(codigo);

        if (usuario == null) {
            return ResponseEntity.status(404).body("No existe usuario con código: " + codigo);
        }

        return ResponseEntity.ok(usuario);
    }

    @PutMapping(value = "/update/{codigo}")
    public ResponseEntity<?> updateUsuario(@PathVariable int codigo, @RequestBody Usuario usuario) {
        try {
            Usuario existente = usuarioService.buscarPorCodigo(codigo);

            if (existente == null) {
                return ResponseEntity.status(404).body("No existe usuario con código: " + codigo);
            }

            validarUsuarioParaActualizar(usuario);

            Usuario responseUsuario = usuarioService.actualizarUsuario(codigo, usuario);
            return ResponseEntity.ok(responseUsuario);

        } catch (RuntimeException e) {
            String mensaje = e.getMessage();

            if (mensaje != null && mensaje.startsWith("Ya existe")) {
                return ResponseEntity.status(409).body(mensaje);
            }

            return ResponseEntity.badRequest().body(mensaje);
        }
    }

    @DeleteMapping(value = "/delete/{codigo}")
    public ResponseEntity<?> deleteUsuario(@PathVariable int codigo) {
        try {
            Usuario usuario = usuarioService.buscarPorCodigo(codigo);

            if (usuario == null) {
                return ResponseEntity.status(404).body("No existe usuario con código: " + codigo);
            }

            boolean eliminado = usuarioService.eliminarUsuario(codigo);

            if (!eliminado) {
                return ResponseEntity.badRequest().body("No se pudo eliminar el usuario");
            }

            return ResponseEntity.ok(Map.of("mensaje", "Usuario eliminado correctamente"));

        } catch (Exception e) {
            return ResponseEntity.badRequest().body(
                    "No se puede eliminar el usuario porque puede tener suscripciones asociadas"
            );
        }
    }

    @GetMapping(value = "/buscar")
    public ResponseEntity<?> buscarUsuario(
            @RequestParam(required = false) String nombre,
            @RequestParam(required = false) String email) {

        if (nombre != null && !nombre.isBlank()) {
            List<Usuario> usuarios = usuarioService.buscarPorNombre(nombre);
            return ResponseEntity.ok(usuarios);
        }

        if (email != null && !email.isBlank()) {
            Usuario usuario = usuarioService.buscarPorEmail(email);

            if (usuario == null) {
                return ResponseEntity.status(404).body("No existe usuario con email: " + email);
            }

            return ResponseEntity.ok(usuario);
        }

        return ResponseEntity.badRequest().body("Debe enviar nombre o email como parámetro");
    }

    private void validarUsuarioParaCrear(Usuario usuario) {
        if (usuario == null) {
            throw new RuntimeException("El usuario no puede ser null");
        }

        if (usuario.getCodigo() <= 0) {
            throw new RuntimeException("El código debe ser mayor a 0");
        }

        validarUsuarioParaActualizar(usuario);
    }

    private void validarUsuarioParaActualizar(Usuario usuario) {
        if (usuario == null) {
            throw new RuntimeException("El usuario no puede ser null");
        }

        if (usuario.getNumDocumento() == null || usuario.getNumDocumento() <= 0) {
            throw new RuntimeException("El número de documento es obligatorio");
        }

        String documento = String.valueOf(usuario.getNumDocumento());

        if (!documento.matches("\\d{6,15}")) {
            throw new RuntimeException("El documento debe tener entre 6 y 15 dígitos");
        }

        if (usuario.getTipoDoc() == null || usuario.getTipoDoc().isBlank()) {
            throw new RuntimeException("El tipo de documento es obligatorio");
        }

        if (!usuario.getTipoDoc().matches("CC|TI|PAS|CE")) {
            throw new RuntimeException("El tipo de documento debe ser CC, TI, PAS o CE");
        }

        if (usuario.getNombre() == null || usuario.getNombre().trim().length() < 3) {
            throw new RuntimeException("El nombre debe tener mínimo 3 caracteres");
        }

        if (usuario.getEmail() == null || !usuario.getEmail().matches("^[^\\s@]+@[^\\s@]+\\.[^\\s@]+$")) {
            throw new RuntimeException("El email no tiene un formato válido");
        }

        if (usuario.getEstado() == null || !usuario.getEstado().matches("AC|IN")) {
            throw new RuntimeException("El estado debe ser AC o IN");
        }
    }
}