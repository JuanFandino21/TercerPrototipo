import React, { useState } from "react";
import { getUsuarioByCodigo, eliminarUsuario } from "../services/api";

const EliminarUsuario = () => {
  const [codigo, setCodigo] = useState("");
  const [data, setData] = useState(null);
  const [confirmar, setConfirmar] = useState(false);
  const [alerta, setAlerta] = useState(null);
  const [cargando, setCargando] = useState(false);

  const handleBuscar = async () => {
    setData(null);
    setConfirmar(false);
    setAlerta(null);

    if (!codigo || Number(codigo) <= 0) {
      setAlerta({
        tipo: "warning",
        mensaje: "Escribe un código de usuario válido. Ejemplo: 1",
      });
      return;
    }

    setCargando(true);

    const res = await getUsuarioByCodigo(codigo);

    if (res.ok && res.data?.codigo) {
      setData(res.data);
      setAlerta({
        tipo: "success",
        mensaje: `Usuario ${codigo} encontrado. Revisa la información antes de eliminar.`,
      });
    } else {
      setAlerta({
        tipo: "warning",
        mensaje: `No existe un usuario con el código ${codigo}.`,
      });
    }

    setCargando(false);
  };

  const handleDelete = async () => {
    setAlerta(null);

    if (!data) {
      setAlerta({
        tipo: "warning",
        mensaje: "Primero busca un usuario existente.",
      });
      return;
    }

    if (!confirmar) {
      setAlerta({
        tipo: "warning",
        mensaje: "Marca la confirmación antes de eliminar.",
      });
      return;
    }

    setCargando(true);

    const res = await eliminarUsuario(data.codigo);

    if (res.ok) {
      setAlerta({
        tipo: "success",
        mensaje: "Usuario eliminado correctamente.",
      });

      setData(null);
      setCodigo("");
      setConfirmar(false);
    } else {
      setAlerta({
        tipo: "error",
        mensaje:
          res.mensaje ||
          "No se pudo eliminar el usuario. Revisa si tiene suscripciones asociadas.",
      });
    }

    setCargando(false);
  };

  return (
    <div className="page-container">
      <h2 className="page-title">🗑️ Eliminar Usuario</h2>

      <p className="page-subtitle">
        Busca el usuario por su código, revisa los datos y confirma la eliminación.
      </p>

      <div className="card">
        <div className="form-group">
          <label>Código del usuario</label>
          <span className="form-help">
            Escribe el código exacto del usuario que deseas eliminar.
          </span>

          <div className="search-two">
            <input
              className="input"
              type="number"
              placeholder="Ej: 1"
              value={codigo}
              onChange={(e) => {
                setCodigo(e.target.value);
                setData(null);
                setConfirmar(false);
                setAlerta(null);
              }}
            />

            <button className="btn btn-search" onClick={handleBuscar} disabled={cargando}>
              Buscar usuario
            </button>
          </div>
        </div>

        {alerta && <div className={`alert alert-${alerta.tipo}`}>{alerta.mensaje}</div>}
      </div>

      {data && (
        <div className="card">
          <h3 style={{ marginTop: 0 }}>Usuario encontrado</h3>

          <div className="detail-grid">
            <div className="detail-label">Código</div>
            <div className="detail-value">{data.codigo}</div>

            <div className="detail-label">Nombre</div>
            <div className="detail-value">{data.nombre}</div>

            <div className="detail-label">Email</div>
            <div className="detail-value">{data.email}</div>

            <div className="detail-label">Documento</div>
            <div className="detail-value">
              {data.tipoDoc} {data.numDocumento}
            </div>

            <div className="detail-label">Estado</div>
            <div className="detail-value">
              {data.estado === "AC" ? (
                <span className="badge badge-success">Activo</span>
              ) : (
                <span className="badge badge-danger">Inactivo</span>
              )}
            </div>

            <div className="detail-label">Fecha registro</div>
            <div className="detail-value">
              {String(data.fechaRegistro || "Sin fecha").replace("T", " ").slice(0, 19)}
            </div>
          </div>

          <div className="alert alert-warning" style={{ marginTop: "18px" }}>
            Esta acción eliminará el usuario seleccionado. Si tiene suscripciones asociadas,
            primero debes eliminar esas suscripciones.
          </div>

          <label style={{ display: "block", marginBottom: "14px", fontWeight: 700 }}>
            <input
              type="checkbox"
              checked={confirmar}
              onChange={(e) => setConfirmar(e.target.checked)}
              style={{ marginRight: "8px" }}
            />
            Confirmo que deseo eliminar este usuario.
          </label>

          <button className="btn btn-danger" onClick={handleDelete} disabled={cargando}>
            🗑️ Eliminar usuario
          </button>
        </div>
      )}
    </div>
  );
};

export default EliminarUsuario;