package com.jfrb.POCSucripcion.controller;

import com.jfrb.POCSucripcion.model.Suscripcion;
import com.jfrb.POCSucripcion.model.SuscripcionStreaming;
import com.jfrb.POCSucripcion.service.SuscripcionService;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/streaming")
public class SuscripcionStreamingController {

    private final SuscripcionService service = SuscripcionService.getInstance();

    @GetMapping("/health")
    public String healthCheck() {
        return "Servicio Suscripciones OK";
    }


    @PostMapping
    public ResponseEntity<?> add(@RequestBody SuscripcionStreaming sus) {

        if (sus == null) {
            return ResponseEntity.badRequest().body("La suscripción no puede ser null");
        }

        service.addSuscripcion(sus);

        return ResponseEntity.status(201).body(sus);
    }

    @GetMapping("/{id}")
    public ResponseEntity<?> getById(@PathVariable int id) {
        Suscripcion s = service.buscarSuscripcionPorId(id);

        if (s == null) {
            return ResponseEntity.status(404).body("No existe suscripción con id: " + id);
        }

        return ResponseEntity.ok(s);
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<?> delete(@PathVariable int id) {

        Suscripcion s = service.buscarSuscripcionPorId(id);

        if (s == null) {
            return ResponseEntity.status(404).body("No existe el registro");
        }

        service.eliminarSuscripcionPorId(id);
        return ResponseEntity.ok("Eliminado correctamente");
    }

    @PutMapping("/{id}")
    public ResponseEntity<?> update(@PathVariable int id, @RequestBody SuscripcionStreaming sus) {

        Suscripcion existente = service.buscarSuscripcionPorId(id);

        if (existente == null) {
            return ResponseEntity.status(404).body("No existe el registro");
        }

        sus.setId(id);
        boolean actualizado = service.actualizarSuscripcion(sus);

        if (!actualizado) {
            return ResponseEntity.status(400).body("No se pudo actualizar");
        }

        return ResponseEntity.ok("Actualizado correctamente");
    }

    @GetMapping
    public ResponseEntity<?> listar(
            @RequestParam(required = false) String nombre,
            @RequestParam(required = false) Boolean activa
    ) {

        List<Suscripcion> lista = service.getSuscripciones();

        List<Suscripcion> resultado = lista.stream()
                .filter(s -> nombre == null || s.getNombreUsuario().equalsIgnoreCase(nombre))
                .filter(s -> activa == null || s.isActiva() == activa)
                .toList();

        return ResponseEntity.ok(resultado);
    }
}