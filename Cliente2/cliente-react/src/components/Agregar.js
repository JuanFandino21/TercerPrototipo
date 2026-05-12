import React, { useState } from "react";
import { crear, getUsuarioByCodigo } from "../services/api";

const PLATAFORMAS = [
  "Netflix",
  "Disney+",
  "HBO Max",
  "Amazon Prime Video",
  "Paramount+",
  "Apple TV+",
  "Crunchyroll",
  "Star+",
];

const PRECIOS = {
  1: 16000,
  2: 18000,
  3: 20000,
  4: 22000,
  5: 24000,
  6: 26000,
};

function hoyLocalDateTime() {
  const ahora = new Date();
  ahora.setMinutes(ahora.getMinutes() - ahora.getTimezoneOffset());
  return ahora.toISOString().slice(0, 16);
}

function validar(form) {
  const errores = {};

  if (!form.id || Number(form.id) <= 0) {
    errores.id = "El ID debe ser mayor a 0";
  }

  if (!form.codigoUsuario || Number(form.codigoUsuario) <= 0) {
    errores.codigoUsuario = "Debes ingresar el código de un usuario existente";
  }

  if (!form.plataforma) {
    errores.plataforma = "Selecciona una plataforma";
  }

  if (!form.dispositivosSimultaneos || Number(form.dispositivosSimultaneos) < 1 || Number(form.dispositivosSimultaneos) > 6) {
    errores.dispositivosSimultaneos = "Selecciona entre 1 y 6 dispositivos";
  }

  if (!form.fechaInicio) {
    errores.fechaInicio = "Debes ingresar la fecha de inicio";
  } else if (form.fechaInicio < hoyLocalDateTime()) {
    errores.fechaInicio = "La fecha no puede ser anterior a hoy";
  }

  return errores;
}

function Agregar() {
  const [form, setForm] = useState({
    id: "",
    codigoUsuario: "",
    plataforma: "",
    dispositivosSimultaneos: "",
    costoMensual: "",
    fechaInicio: "",
    activa: true,
  });

  const [usuarioEncontrado, setUsuarioEncontrado] = useState(null);
  const [errores, setErrores] = useState({});
  const [alerta, setAlerta] = useState(null);
  const [cargando, setCargando] = useState(false);
  const [verificandoUsuario, setVerificandoUsuario] = useState(false);

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;

    if (name === "dispositivosSimultaneos") {
      const cantidad = Number(value);

      setForm((prev) => ({
        ...prev,
        dispositivosSimultaneos: value,
        costoMensual: PRECIOS[cantidad] || "",
      }));
    } else if (name === "codigoUsuario") {
      setUsuarioEncontrado(null);

      setForm((prev) => ({
        ...prev,
        [name]: value,
      }));
    } else {
      setForm((prev) => ({
        ...prev,
        [name]: type === "checkbox" ? checked : value,
      }));
    }

    if (errores[name]) {
      setErrores((prev) => ({
        ...prev,
        [name]: null,
      }));
    }
  };

  const verificarUsuario = async () => {
    setAlerta(null);
    setUsuarioEncontrado(null);

    if (!form.codigoUsuario || Number(form.codigoUsuario) <= 0) {
      setErrores((prev) => ({
        ...prev,
        codigoUsuario: "Ingresa un código válido mayor a 0",
      }));
      return false;
    }

    setVerificandoUsuario(true);

    const res = await getUsuarioByCodigo(form.codigoUsuario);

    if (res.ok && res.data?.codigo) {
      setUsuarioEncontrado(res.data);
      setVerificandoUsuario(false);
      return true;
    }

    setAlerta({
      tipo: "error",
      mensaje: res.mensaje || "No existe un usuario con ese código. Primero debes crear el usuario.",
    });

    setVerificandoUsuario(false);
    return false;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setAlerta(null);

    const erroresValidacion = validar(form);

    if (Object.keys(erroresValidacion).length > 0) {
      setErrores(erroresValidacion);
      setAlerta({
        tipo: "warning",
        mensaje: "Corrige los errores antes de guardar la suscripción.",
      });
      return;
    }

    const existeUsuario = usuarioEncontrado || await verificarUsuario();

    if (!existeUsuario) {
      return;
    }

    setCargando(true);

    const payload = {
      id: parseInt(form.id),
      plataforma: form.plataforma,
      dispositivosSimultaneos: parseInt(form.dispositivosSimultaneos),
      costoMensual: Number(form.costoMensual),
      fechaInicio: `${form.fechaInicio}:00`,
      activa: form.activa,
      usuario: {
        codigo: parseInt(form.codigoUsuario),
      },
    };

    const res = await crear(payload);

    if (res.ok) {
      setAlerta({
        tipo: "success",
        mensaje: "Suscripción creada correctamente.",
      });

      setForm({
        id: "",
        codigoUsuario: "",
        plataforma: "",
        dispositivosSimultaneos: "",
        costoMensual: "",
        fechaInicio: "",
        activa: true,
      });

      setUsuarioEncontrado(null);
      setErrores({});
    } else {
      setAlerta({
        tipo: "error",
        mensaje: res.mensaje || "No se pudo crear la suscripción.",
      });
    }

    setCargando(false);
  };

  return (
    <div style={{ maxWidth: "680px", margin: "0 auto" }}>
      <h2 className="page-title">➕ Agregar Suscripción</h2>

      <p className="page-subtitle">
        Primero debe existir el usuario. Luego puedes registrar la suscripción streaming.
      </p>

      {alerta && (
        <div className={`alert alert-${alerta.tipo}`}>
          {alerta.mensaje}
        </div>
      )}

      <form className="card" onSubmit={handleSubmit} noValidate>
        <div className="form-group">
          <label>
            ID Suscripción <span style={{ color: "#e94560" }}>*</span>
          </label>
          <span className="form-help">
            Número entero positivo, no puede repetirse.
          </span>
          <input
            className={`input ${errores.id ? "input-error" : ""}`}
            type="number"
            name="id"
            value={form.id}
            onChange={handleChange}
            placeholder="Ej: 1"
          />
          {errores.id && <span className="error-field">⚠ {errores.id}</span>}
        </div>

        <div className="form-group">
          <label>
            Código del Usuario <span style={{ color: "#e94560" }}>*</span>
          </label>
          <span className="form-help">
            La suscripción debe estar vinculada a un usuario existente.
          </span>

          <div className="search-two">
            <input
              className={`input ${errores.codigoUsuario ? "input-error" : ""}`}
              type="number"
              name="codigoUsuario"
              value={form.codigoUsuario}
              onChange={handleChange}
              placeholder="Ej: 1"
            />

            <button
              className="btn btn-search"
              type="button"
              onClick={verificarUsuario}
              disabled={verificandoUsuario}
            >
              {verificandoUsuario ? "Verificando..." : "Verificar"}
            </button>
          </div>

          {errores.codigoUsuario && (
            <span className="error-field">⚠ {errores.codigoUsuario}</span>
          )}

          {usuarioEncontrado && (
            <div className="alert alert-success" style={{ marginTop: "12px" }}>
              Usuario encontrado: <strong>{usuarioEncontrado.nombre}</strong> — Código:{" "}
              <strong>{usuarioEncontrado.codigo}</strong>
            </div>
          )}
        </div>

        <div className="form-group">
          <label>
            Plataforma <span style={{ color: "#e94560" }}>*</span>
          </label>
          <span className="form-help">
            Selecciona la plataforma para evitar errores de escritura.
          </span>
          <select
            className={`select ${errores.plataforma ? "input-error" : ""}`}
            name="plataforma"
            value={form.plataforma}
            onChange={handleChange}
          >
            <option value="">-- Selecciona una plataforma --</option>
            {PLATAFORMAS.map((p) => (
              <option key={p} value={p}>
                {p}
              </option>
            ))}
          </select>
          {errores.plataforma && (
            <span className="error-field">⚠ {errores.plataforma}</span>
          )}
        </div>

        <div className="form-group">
          <label>
            Dispositivos Simultáneos <span style={{ color: "#e94560" }}>*</span>
          </label>
          <span className="form-help">
            El costo mensual se calcula automáticamente según la cantidad de dispositivos.
          </span>
          <select
            className={`select ${errores.dispositivosSimultaneos ? "input-error" : ""}`}
            name="dispositivosSimultaneos"
            value={form.dispositivosSimultaneos}
            onChange={handleChange}
          >
            <option value="">-- Selecciona --</option>
            <option value="1">1 dispositivo — $16.000 COP</option>
            <option value="2">2 dispositivos — $18.000 COP</option>
            <option value="3">3 dispositivos — $20.000 COP</option>
            <option value="4">4 dispositivos — $22.000 COP</option>
            <option value="5">5 dispositivos — $24.000 COP</option>
            <option value="6">6 dispositivos — $26.000 COP</option>
          </select>
          {errores.dispositivosSimultaneos && (
            <span className="error-field">⚠ {errores.dispositivosSimultaneos}</span>
          )}
        </div>

        <div className="form-group">
          <label>Costo Mensual</label>
          <span className="form-help">
            Este valor se completa automáticamente. No debes escribirlo manualmente.
          </span>
          <input
            className="input"
            type="text"
            value={
              form.costoMensual
                ? `$${Number(form.costoMensual).toLocaleString("es-CO")} COP`
                : "Selecciona los dispositivos"
            }
            disabled
          />
        </div>

        <div className="form-group">
          <label>
            Fecha Inicio <span style={{ color: "#e94560" }}>*</span>
          </label>
          <span className="form-help">
            La fecha no puede ser anterior a hoy.
          </span>
          <input
            className={`input ${errores.fechaInicio ? "input-error" : ""}`}
            type="datetime-local"
            name="fechaInicio"
            value={form.fechaInicio}
            min={hoyLocalDateTime()}
            onChange={handleChange}
          />
          {errores.fechaInicio && (
            <span className="error-field">⚠ {errores.fechaInicio}</span>
          )}
        </div>

        <div className="form-group">
          <label>Estado de la suscripción</label>
          <span className="form-help">
            Marca si la suscripción queda activa desde el registro.
          </span>

          <label style={{ display: "flex", gap: "8px", alignItems: "center", fontWeight: 700 }}>
            <input
              type="checkbox"
              name="activa"
              checked={form.activa}
              onChange={handleChange}
            />
            Activa
          </label>
        </div>

        <button
          className="btn btn-primary"
          type="submit"
          disabled={cargando}
          style={{ width: "100%" }}
        >
          {cargando ? "⏳ Guardando..." : "💾 Guardar Suscripción"}
        </button>
      </form>
    </div>
  );
}

export default Agregar;