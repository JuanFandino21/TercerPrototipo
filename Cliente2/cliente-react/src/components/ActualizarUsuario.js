import React, { useState } from "react";
import { getUsuarioByCodigo, actualizarUsuario } from "../services/api";

const REGLAS = [
  {
    campo: "numDocumento",
    label: "N° Documento",
    tipo: "text",
    ayuda: "Solo dígitos, entre 6 y 15 caracteres",
  },
  {
    campo: "tipoDoc",
    label: "Tipo Documento",
    tipo: "select",
    opciones: ["CC", "TI", "PAS", "CE"],
    ayuda: "CC=Cédula, TI=Tarjeta Identidad, PAS=Pasaporte, CE=Cédula Extranjería",
  },
  {
    campo: "nombre",
    label: "Nombre Completo",
    tipo: "text",
    ayuda: "Mínimo 3 caracteres, máximo 80",
  },
  {
    campo: "email",
    label: "Email",
    tipo: "email",
    ayuda: "Formato válido: ejemplo@dominio.com",
  },
  {
    campo: "estado",
    label: "Estado",
    tipo: "select",
    opciones: ["AC", "IN"],
    ayuda: "AC = Activo | IN = Inactivo",
  },
];

function validar(form) {
  const errores = {};

  if (!form.numDocumento || !/^\d{6,15}$/.test(String(form.numDocumento))) {
    errores.numDocumento = "Solo dígitos, entre 6 y 15 caracteres";
  }

  if (!form.tipoDoc) {
    errores.tipoDoc = "Selecciona un tipo de documento";
  }

  if (!form.nombre || form.nombre.trim().length < 3) {
    errores.nombre = "El nombre debe tener al menos 3 caracteres";
  }

  if (!form.email || !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(form.email)) {
    errores.email = "Ingresa un email válido";
  }

  if (!form.estado) {
    errores.estado = "Selecciona un estado";
  }

  return errores;
}

function ActualizarUsuario() {
  const [codigo, setCodigo] = useState("");
  const [form, setForm] = useState(null);
  const [errores, setErrores] = useState({});
  const [alerta, setAlerta] = useState(null);
  const [cargando, setCargando] = useState(false);

  const handleBuscar = async () => {
    setAlerta(null);
    setErrores({});
    setForm(null);

    if (!codigo || Number(codigo) <= 0) {
      setAlerta({
        tipo: "warning",
        mensaje: "Ingresa un código válido mayor a 0.",
      });
      return;
    }

    setCargando(true);

    const res = await getUsuarioByCodigo(codigo);

    if (res.ok && res.data?.codigo) {
      setForm({
        codigo: res.data.codigo,
        numDocumento: String(res.data.numDocumento || ""),
        tipoDoc: res.data.tipoDoc || "",
        nombre: res.data.nombre || "",
        email: res.data.email || "",
        estado: res.data.estado || "AC",
        fechaRegistro: res.data.fechaRegistro,
      });

      setAlerta({
        tipo: "success",
        mensaje: "Usuario encontrado. Ahora puedes actualizar sus datos.",
      });
    } else {
      setAlerta({
        tipo: "error",
        mensaje: res.mensaje || "No se encontró el usuario.",
      });
    }

    setCargando(false);
  };

  const handleChange = (e) => {
    const { name, value } = e.target;

    setForm((prev) => ({
      ...prev,
      [name]: value,
    }));

    if (errores[name]) {
      setErrores((prev) => ({
        ...prev,
        [name]: null,
      }));
    }
  };

  const handleUpdate = async (e) => {
    e.preventDefault();
    setAlerta(null);

    const erroresValidacion = validar(form);

    if (Object.keys(erroresValidacion).length > 0) {
      setErrores(erroresValidacion);
      setAlerta({
        tipo: "warning",
        mensaje: "Corrige los errores antes de actualizar.",
      });
      return;
    }

    setCargando(true);

    const payload = {
      codigo: parseInt(form.codigo),
      numDocumento: parseInt(form.numDocumento),
      tipoDoc: form.tipoDoc,
      nombre: form.nombre.trim(),
      email: form.email.trim().toLowerCase(),
      estado: form.estado,
      fechaRegistro: form.fechaRegistro,
    };

    const res = await actualizarUsuario(form.codigo, payload);

    if (res.ok) {
      setAlerta({
        tipo: "success",
        mensaje: "Usuario actualizado correctamente.",
      });
    } else {
      setAlerta({
        tipo: "error",
        mensaje: res.mensaje || "No se pudo actualizar el usuario.",
      });
    }

    setCargando(false);
  };

  return (
    <div style={{ maxWidth: "680px", margin: "0 auto" }}>
      <h2 className="page-title">✏️ Actualizar Usuario</h2>

      <p className="page-subtitle">
        Primero busca el usuario por código. Luego modifica los campos necesarios.
      </p>

      {alerta && (
        <div className={`alert alert-${alerta.tipo}`}>
          {alerta.mensaje}
        </div>
      )}

      <div className="card">
        <div className="form-group">
          <label>Código del usuario</label>
          <span className="form-help">
            Ingresa el código del usuario que deseas actualizar.
          </span>

          <div className="search-two">
            <input
              className="input"
              type="number"
              placeholder="Ej: 1"
              value={codigo}
              onChange={(e) => setCodigo(e.target.value)}
            />

            <button
              className="btn btn-search"
              type="button"
              onClick={handleBuscar}
              disabled={cargando}
            >
              {cargando ? "Buscando..." : "Buscar"}
            </button>
          </div>
        </div>
      </div>

      {form && (
        <form className="card" onSubmit={handleUpdate} noValidate>
          <div className="alert alert-warning">
            Revisa toda la información antes de actualizar. El código no se modifica.
          </div>

          <div className="form-group">
            <label>Código</label>
            <span className="form-help">Llave primaria del usuario.</span>
            <input
              className="input"
              value={form.codigo}
              disabled
            />
          </div>

          {REGLAS.map((r) => (
            <div className="form-group" key={r.campo}>
              <label>
                {r.label} <span style={{ color: "#e94560" }}>*</span>
              </label>

              <span className="form-help">{r.ayuda}</span>

              {r.tipo === "select" ? (
                <select
                  className={`select ${errores[r.campo] ? "input-error" : ""}`}
                  name={r.campo}
                  value={form[r.campo]}
                  onChange={handleChange}
                >
                  <option value="">-- Selecciona --</option>

                  {r.opciones.map((op) => (
                    <option key={op} value={op}>
                      {r.campo === "tipoDoc"
                        ? {
                            CC: "CC - Cédula de Ciudadanía",
                            TI: "TI - Tarjeta de Identidad",
                            PAS: "PAS - Pasaporte",
                            CE: "CE - Cédula Extranjería",
                          }[op]
                        : op === "AC"
                        ? "AC - Activo"
                        : "IN - Inactivo"}
                    </option>
                  ))}
                </select>
              ) : (
                <input
                  className={`input ${errores[r.campo] ? "input-error" : ""}`}
                  type={r.tipo}
                  name={r.campo}
                  value={form[r.campo]}
                  onChange={handleChange}
                />
              )}

              {errores[r.campo] && (
                <span className="error-field">⚠ {errores[r.campo]}</span>
              )}
            </div>
          ))}

          <button
            className="btn btn-primary"
            type="submit"
            disabled={cargando}
            style={{ width: "100%" }}
          >
            {cargando ? "⏳ Actualizando..." : "💾 Actualizar Usuario"}
          </button>
        </form>
      )}
    </div>
  );
}

export default ActualizarUsuario;