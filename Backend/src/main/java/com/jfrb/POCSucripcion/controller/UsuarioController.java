package com.jfrb.POCSucripcion.controller;

import com.jfrb.POCSucripcion.model.Usuario;
import com.jfrb.POCSucripcion.service.IUsuarioService;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.time.LocalDateTime;
import java.util.List;
import java.util.Map;

@RestController
@RequestMapping("/usuarios")
@CrossOrigin(origins = "*")
public class UsuarioController {

    private final IUsuarioService usuarioService;

    public UsuarioController(IUsuarioService usuarioService) {
        this.usuarioService = usuarioService;
    }

    @GetMapping("/health")
    public String healthCheck() {
        return "Servicio Usuarios OK";
    }

    // POST /usuarios  → insertar nuevo usuario
    @PostMapping
    public ResponseEntity<?> add(@RequestBody Usuario usuario) {
        try {
            if (usuario == null) {
                return ResponseEntity.badRequest().body("El usuario no puede ser null");
            }

            if (usuario.getCodigo() <= 0) {
                return ResponseEntity.badRequest().body("El código debe ser mayor a 0");
            }

            if (usuario.getNombre() == null || usuario.getNombre().trim().isEmpty()) {
                return ResponseEntity.badRequest().body("El nombre es obligatorio");
            }

            if (usuario.getEmail() == null || usuario.getEmail().trim().isEmpty()) {
                return ResponseEntity.badRequest().body("El email es obligatorio");
            }

            if (usuario.getFechaRegistro() == null) {
                usuario.setFechaRegistro(LocalDateTime.now());
            }

            Usuario creado = usuarioService.addUsuario(usuario);
            return ResponseEntity.status(201).body(creado);

        } catch (Exception e) {
            return ResponseEntity.badRequest().body(e.getMessage());
        }
    }

    // GET /usuarios/{codigo} → buscar por código
    @GetMapping("/{codigo}")
    public ResponseEntity<?> getByCodigo(@PathVariable int codigo) {
        Usuario u = usuarioService.buscarPorCodigo(codigo);

        if (u == null) {
            return ResponseEntity.status(404).body("No existe usuario con código: " + codigo);
        }

        return ResponseEntity.ok(u);
    }

    // GET /usuarios → listar todos
    @GetMapping
    public ResponseEntity<List<Usuario>> listarTodos() {
        List<Usuario> usuarios = usuarioService.getUsuarios();
        return ResponseEntity.ok(usuarios);
    }

    // DELETE /usuarios/{codigo} → eliminar (previa búsqueda)
    @DeleteMapping("/{codigo}")
    public ResponseEntity<?> delete(@PathVariable int codigo) {
        Usuario u = usuarioService.buscarPorCodigo(codigo);

        if (u == null) {
            return ResponseEntity.status(404).body("No existe el usuario");
        }

        boolean eliminado = usuarioService.eliminarUsuario(codigo);

        if (!eliminado) {
            return ResponseEntity.badRequest().body("No se pudo eliminar");
        }

        return ResponseEntity.ok(Map.of("mensaje", "Eliminado correctamente"));
    }

    // PUT /usuarios/{codigo} → actualizar (previa búsqueda)
    @PutMapping("/{codigo}")
    public ResponseEntity<?> update(@PathVariable int codigo, @RequestBody Usuario usuario) {
        try {
            Usuario existente = usuarioService.buscarPorCodigo(codigo);

            if (existente == null) {
                return ResponseEntity.status(404).body("No existe el usuario");
            }

            if (usuario.getNombre() == null || usuario.getNombre().trim().isEmpty()) {
                return ResponseEntity.badRequest().body("El nombre es obligatorio");
            }

            if (usuario.getEmail() == null || usuario.getEmail().trim().isEmpty()) {
                return ResponseEntity.badRequest().body("El email es obligatorio");
            }

            Usuario actualizado = usuarioService.actualizarUsuario(codigo, usuario);
            return ResponseEntity.ok(actualizado);

        } catch (Exception e) {
            return ResponseEntity.badRequest().body(e.getMessage());
        }
    }

    // GET /usuarios/buscar?nombre=...  o  /usuarios/buscar?email=...
    @GetMapping("/buscar")
    public ResponseEntity<?> buscar(
            @RequestParam(required = false) String nombre,
            @RequestParam(required = false) String email) {

        if (nombre != null && !nombre.isBlank()) {
            List<Usuario> lista = usuarioService.buscarPorNombre(nombre);
            return ResponseEntity.ok(lista);
        }

        if (email != null && !email.isBlank()) {
            Usuario u = usuarioService.buscarPorEmail(email);
            if (u == null) {
                return ResponseEntity.status(404).body("No existe usuario con email: " + email);
            }
            return ResponseEntity.ok(u);
        }

        return ResponseEntity.badRequest().body("Debe enviar nombre o email como parámetro");
    }
}