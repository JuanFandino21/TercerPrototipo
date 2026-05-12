import React, { useState } from "react";
import { crearUsuario } from "../services/api";

const REGLAS = [
  {
    campo: "codigo",
    label: "Código",
    tipo: "number",
    ayuda: "Número entero positivo, no puede repetirse (ej: 1, 2, 101)",
  },
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

  if (!form.codigo || isNaN(form.codigo) || Number(form.codigo) < 1) {
    errores.codigo = "El código debe ser un número entero mayor a 0";
  }

  if (!form.numDocumento || !/^\d{6,15}$/.test(form.numDocumento)) {
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

function AgregarUsuario() {
  const [form, setForm] = useState({
    codigo: "",
    numDocumento: "",
    tipoDoc: "",
    nombre: "",
    email: "",
    estado: "AC",
  });

  const [errores, setErrores] = useState({});
  const [alerta, setAlerta] = useState(null);
  const [cargando, setCargando] = useState(false);

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

  const handleSubmit = async (e) => {
    e.preventDefault();
    setAlerta(null);

    const erroresValidacion = validar(form);

    if (Object.keys(erroresValidacion).length > 0) {
      setErrores(erroresValidacion);
      setAlerta({
        tipo: "warning",
        mensaje: "Corrige los errores antes de guardar.",
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
    };

    const res = await crearUsuario(payload);

    if (res.ok) {
      setAlerta({
        tipo: "success",
        mensaje: "Usuario creado correctamente.",
      });

      setForm({
        codigo: "",
        numDocumento: "",
        tipoDoc: "",
        nombre: "",
        email: "",
        estado: "AC",
      });

      setErrores({});
    } else {
      setAlerta({
        tipo: "error",
        mensaje: res.mensaje || "No se pudo guardar el usuario.",
      });
    }

    setCargando(false);
  };

  return (
    <div style={{ maxWidth: "560px", margin: "0 auto" }}>
      <h2 className="page-title">➕ Agregar Usuario</h2>

      <p className="page-subtitle">
        Todos los campos marcados con{" "}
        <span style={{ color: "#e94560" }}>*</span> son obligatorios.
      </p>

      {alerta && (
        <div className={`alert alert-${alerta.tipo}`}>
          {alerta.mensaje}
        </div>
      )}

      <form className="card" onSubmit={handleSubmit} noValidate>
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
                placeholder={
                  r.campo === "codigo"
                    ? "Ej: 1"
                    : r.campo === "email"
                    ? "Ej: juan@gmail.com"
                    : ""
                }
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
          {cargando ? "⏳ Guardando..." : "💾 Guardar Usuario"}
        </button>
      </form>
    </div>
  );
}

export default AgregarUsuario;