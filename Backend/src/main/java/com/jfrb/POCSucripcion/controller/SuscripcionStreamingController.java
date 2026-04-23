package com.jfrb.POCSucripcion.controller;

import com.jfrb.POCSucripcion.model.Suscripcion;
import com.jfrb.POCSucripcion.model.SuscripcionStreaming;
import com.jfrb.POCSucripcion.service.ISuscripcionService;
import com.jfrb.POCSucripcion.service.ISuscripcionStreamingService;

import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.Map;
import java.util.List;
import java.time.LocalDate;
import java.time.LocalDateTime;

@RestController
@RequestMapping("/streaming")
@CrossOrigin(origins = "*")
public class SuscripcionStreamingController {

    private final ISuscripcionService service;
    private final ISuscripcionStreamingService streamingService;

    public SuscripcionStreamingController(
            ISuscripcionService service,
            ISuscripcionStreamingService streamingService) {

        this.service = service;
        this.streamingService = streamingService;
    }

    @GetMapping("/health")
    public String healthCheck() {
        return "Servicio Suscripciones OK";
    }

    
    private void validarFecha(LocalDateTime fecha) {

        if (fecha == null) {
            throw new RuntimeException("La fecha es obligatoria");
        }

        LocalDate hoy = LocalDate.now();
        LocalDate fechaUsuario = fecha.toLocalDate();


        if (fechaUsuario.isBefore(hoy)) {
            throw new RuntimeException("La fecha no puede ser anterior a hoy");
        }


        LocalDate limite = hoy.plusMonths(1);

        if (fechaUsuario.isAfter(limite)) {
            throw new RuntimeException("La fecha no puede ser mayor a un mes");
        }
    }

    @PostMapping
    public ResponseEntity<?> add(@RequestBody SuscripcionStreaming sus) {
        try {

            if (sus == null) {
                return ResponseEntity.badRequest().body("La suscripción no puede ser null");
            }

            if (sus.getId() <= 0) {
                return ResponseEntity.badRequest().body("El ID debe ser mayor a 0");
            }

            Suscripcion existente = service.buscarSuscripcionPorId(sus.getId());
            if (existente != null) {
                return ResponseEntity.badRequest().body("Ya existe una suscripción con ese ID");
            }

            if (sus.getNombreUsuario() == null || sus.getNombreUsuario().trim().isEmpty()) {
                return ResponseEntity.badRequest().body("El nombre de usuario es obligatorio");
            }

            if (sus.getFechaInicio() == null) {
                return ResponseEntity.badRequest().body("La fecha es obligatoria");
            }

            if (sus.getDispositivosSimultaneos() < 1) {
                return ResponseEntity.badRequest().body("Debe ingresar dispositivos");
            }

            validarFecha(sus.getFechaInicio());
            streamingService.validarDispositivos(sus.getDispositivosSimultaneos());

            service.addSuscripcion(sus);

            return ResponseEntity.status(201).body(sus);

        } catch (Exception e) {
            return ResponseEntity.badRequest().body(e.getMessage());
        }
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

        return ResponseEntity.ok(Map.of("mensaje", "Eliminado correctamente"));
    }


    @PutMapping("/{id}")
    public ResponseEntity<?> update(@PathVariable int id, @RequestBody SuscripcionStreaming sus) {

        try {

            Suscripcion existente = service.buscarSuscripcionPorId(id);

            if (existente == null) {
                return ResponseEntity.status(404).body("No existe el registro");
            }

            if (sus.getNombreUsuario() == null || sus.getNombreUsuario().trim().isEmpty()) {
                return ResponseEntity.badRequest().body("El nombre de usuario es obligatorio");
            }

            if (sus.getFechaInicio() == null) {
                return ResponseEntity.badRequest().body("La fecha es obligatoria");
            }

            if (sus.getDispositivosSimultaneos() < 1) {
                return ResponseEntity.badRequest().body("Debe ingresar dispositivos");
            }


            validarFecha(sus.getFechaInicio());
            streamingService.validarDispositivos(sus.getDispositivosSimultaneos());

            sus.setId(id);
            boolean actualizado = service.actualizarSuscripcion(sus);

            if (!actualizado) {
                return ResponseEntity.badRequest().body("No se pudo actualizar");
            }

            return ResponseEntity.ok(Map.of("mensaje", "Actualizado correctamente"));

        } catch (Exception e) {
            return ResponseEntity.badRequest().body(e.getMessage());
        }
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