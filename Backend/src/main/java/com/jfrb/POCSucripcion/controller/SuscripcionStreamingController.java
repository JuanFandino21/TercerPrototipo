package com.jfrb.POCSucripcion.controller;

import com.jfrb.POCSucripcion.model.SuscripcionStreaming;
import com.jfrb.POCSucripcion.model.Usuario;
import com.jfrb.POCSucripcion.service.ISuscripcionStreamingService;
import com.jfrb.POCSucripcion.service.IUsuarioService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.time.LocalDate;
import java.time.LocalDateTime;
import java.util.List;
import java.util.Map;
import java.util.stream.Collectors;

@RestController
@RequestMapping(value = "/streaming")
@CrossOrigin(origins = "http://localhost:3000")
public class SuscripcionStreamingController {

    @Autowired
    private ISuscripcionStreamingService streamingService;

    @Autowired
    private IUsuarioService usuarioService;

    @GetMapping(value = "/healthCheck")
    public ResponseEntity<String> healthCheck() {
        return ResponseEntity.ok("Ok!");
    }

    /*
     * Acepta:
     * POST http://localhost:8090/streaming
     * POST http://localhost:8090/streaming/
     *
     * Esto evita el error 404 que te salía desde Visual Studio.
     */
    @PostMapping(value = {"", "/"})
    public ResponseEntity<?> addSuscripcion(@RequestBody SuscripcionStreaming suscripcion) {
        try {
            validarSuscripcionParaCrear(suscripcion);

            Usuario usuarioReal = obtenerUsuarioReal(suscripcion);
            suscripcion.setUsuario(usuarioReal);

            SuscripcionStreaming responseSuscripcion = streamingService.addSuscripcion(suscripcion);
            return ResponseEntity.ok(responseSuscripcion);

        } catch (RuntimeException e) {
            String mensaje = e.getMessage();

            if (mensaje != null && mensaje.startsWith("Ya existe")) {
                return ResponseEntity.status(409).body(mensaje);
            }

            return ResponseEntity.badRequest().body(mensaje);
        }
    }

    @GetMapping(value = "/all")
    public ResponseEntity<List<SuscripcionStreaming>> getSuscripciones(
            @RequestParam(required = false) String plataforma,
            @RequestParam(required = false) Boolean activa,
            @RequestParam(required = false) Integer codigoUsuario,
            @RequestParam(required = false) Double costoMax) {

        List<SuscripcionStreaming> suscripciones = streamingService.getSuscripciones();

        if (codigoUsuario != null) {
            suscripciones = suscripciones.stream()
                    .filter(s -> s.getUsuario() != null && s.getUsuario().getCodigo() == codigoUsuario)
                    .collect(Collectors.toList());
        }

        if (activa != null) {
            suscripciones = suscripciones.stream()
                    .filter(s -> s.isActiva() == activa)
                    .collect(Collectors.toList());
        }

        if (plataforma != null && !plataforma.isBlank()) {
            String filtroPlataforma = plataforma.toLowerCase();

            suscripciones = suscripciones.stream()
                    .filter(s -> s.getPlataforma() != null
                            && s.getPlataforma().toLowerCase().contains(filtroPlataforma))
                    .collect(Collectors.toList());
        }

        if (costoMax != null) {
            suscripciones = suscripciones.stream()
                    .filter(s -> s.getCostoMensual() <= costoMax)
                    .collect(Collectors.toList());
        }

        return ResponseEntity.ok(suscripciones);
    }

    @GetMapping(value = {"/find/{id}", "/{id}"})
    public ResponseEntity<?> getSuscripcionById(@PathVariable int id) {
        SuscripcionStreaming suscripcion = streamingService.buscarSuscripcionPorId(id);

        if (suscripcion == null) {
            return ResponseEntity.status(404).body("No existe suscripción con ID: " + id);
        }

        return ResponseEntity.ok(suscripcion);
    }

    @PutMapping(value = {"/update/{id}", "/{id}"})
    public ResponseEntity<?> updateSuscripcion(@PathVariable int id, @RequestBody SuscripcionStreaming suscripcion) {
        try {
            SuscripcionStreaming existente = streamingService.buscarSuscripcionPorId(id);

            if (existente == null) {
                return ResponseEntity.status(404).body("No existe suscripción con ID: " + id);
            }

            validarSuscripcionParaActualizar(suscripcion);

            Usuario usuarioReal = obtenerUsuarioReal(suscripcion);

            suscripcion.setId(id);
            suscripcion.setUsuario(usuarioReal);

            boolean actualizado = streamingService.actualizarSuscripcion(suscripcion);

            if (!actualizado) {
                return ResponseEntity.badRequest().body("No se pudo actualizar la suscripción");
            }

            return ResponseEntity.ok(Map.of("mensaje", "Suscripción actualizada correctamente"));

        } catch (RuntimeException e) {
            return ResponseEntity.badRequest().body(e.getMessage());
        }
    }

    @DeleteMapping(value = {"/delete/{id}", "/{id}"})
    public ResponseEntity<?> deleteSuscripcion(@PathVariable int id) {
        SuscripcionStreaming suscripcion = streamingService.buscarSuscripcionPorId(id);

        if (suscripcion == null) {
            return ResponseEntity.status(404).body("No existe suscripción con ID: " + id);
        }

        boolean eliminado = streamingService.eliminarSuscripcionPorId(id);

        if (!eliminado) {
            return ResponseEntity.badRequest().body("No se pudo eliminar la suscripción");
        }

        return ResponseEntity.ok(Map.of("mensaje", "Suscripción eliminada correctamente"));
    }

    private void validarSuscripcionParaCrear(SuscripcionStreaming suscripcion) {
        if (suscripcion == null) {
            throw new RuntimeException("La suscripción no puede ser null");
        }

        if (suscripcion.getId() <= 0) {
            throw new RuntimeException("El número de suscripción debe ser mayor a 0");
        }

        if (streamingService.buscarSuscripcionPorId(suscripcion.getId()) != null) {
            throw new RuntimeException("Ya existe una suscripción con el número: " + suscripcion.getId());
        }

        validarSuscripcionParaActualizar(suscripcion);
    }

    private void validarSuscripcionParaActualizar(SuscripcionStreaming suscripcion) {
        if (suscripcion == null) {
            throw new RuntimeException("La suscripción no puede ser null");
        }

        if (suscripcion.getUsuario() == null || suscripcion.getUsuario().getCodigo() <= 0) {
            throw new RuntimeException("Debe seleccionar un usuario válido");
        }

        if (suscripcion.getPlataforma() == null || suscripcion.getPlataforma().isBlank()) {
            throw new RuntimeException("Debe seleccionar una plataforma");
        }

        if (!plataformaValida(suscripcion.getPlataforma())) {
            throw new RuntimeException("La plataforma seleccionada no es válida");
        }

        if (suscripcion.getFechaInicio() == null) {
            throw new RuntimeException("La fecha de inicio es obligatoria");
        }

        validarFechaFlexible(suscripcion.getFechaInicio());
        streamingService.validarDispositivos(suscripcion.getDispositivosSimultaneos());
    }

    private Usuario obtenerUsuarioReal(SuscripcionStreaming suscripcion) {
        Usuario usuarioReal = usuarioService.buscarPorCodigo(suscripcion.getUsuario().getCodigo());

        if (usuarioReal == null) {
            throw new RuntimeException("No existe usuario con código: " + suscripcion.getUsuario().getCodigo());
        }

        return usuarioReal;
    }

    private void validarFechaFlexible(LocalDateTime fecha) {
        LocalDate hoy = LocalDate.now();
        LocalDate fechaIngresada = fecha.toLocalDate();

        if (fechaIngresada.isBefore(hoy)) {
            throw new RuntimeException("La fecha no puede ser anterior al día de hoy");
        }
    }

    private boolean plataformaValida(String plataforma) {
        return plataforma.equals("Netflix")
                || plataforma.equals("Disney+")
                || plataforma.equals("HBO Max")
                || plataforma.equals("Amazon Prime Video")
                || plataforma.equals("Paramount+")
                || plataforma.equals("Apple TV+")
                || plataforma.equals("Crunchyroll")
                || plataforma.equals("Star+");
    }
}