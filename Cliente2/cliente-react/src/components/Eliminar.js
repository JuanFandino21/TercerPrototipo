import React, { useState } from "react";
import { buscar, eliminar } from "../services/api";

const Eliminar = () => {
  const [id, setId] = useState("");
  const [data, setData] = useState(null);
  const [confirmar, setConfirmar] = useState(false);
  const [alerta, setAlerta] = useState(null);
  const [cargando, setCargando] = useState(false);

  const limpiarResultado = () => {
    setData(null);
    setConfirmar(false);
    setAlerta(null);
  };

  const handleBuscar = async () => {
    limpiarResultado();

    if (!id || Number(id) <= 0) {
      setAlerta({
        tipo: "warning",
        mensaje: "Escribe un número de suscripción válido. Ejemplo: 1",
      });
      return;
    }

    setCargando(true);

    const res = await buscar(id);

    if (res.ok && res.data?.id) {
      setData(res.data);
      setAlerta({
        tipo: "success",
        mensaje: `Suscripción ${id} encontrada. Revisa la información antes de eliminar.`,
      });
    } else {
      setAlerta({
        tipo: "warning",
        mensaje: `No existe una suscripción con el número ${id}.`,
      });
    }

    setCargando(false);
  };

  const handleDelete = async () => {
    setAlerta(null);

    if (!data) {
      setAlerta({
        tipo: "warning",
        mensaje: "Primero busca una suscripción existente.",
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

    const res = await eliminar(data.id);

    if (res.ok) {
      setAlerta({
        tipo: "success",
        mensaje: "Suscripción eliminada correctamente.",
      });

      setData(null);
      setId("");
      setConfirmar(false);
    } else {
      setAlerta({
        tipo: "error",
        mensaje: res.mensaje || "No se pudo eliminar la suscripción.",
      });
    }

    setCargando(false);
  };

  return (
    <div className="page-container">
      <h2 className="page-title">🗑️ Eliminar Suscripción</h2>

      <p className="page-subtitle">
        Busca la suscripción por su número, revisa los datos y confirma la eliminación.
      </p>

      <div className="card">
        <div className="form-group">
          <label>Número de suscripción</label>
          <span className="form-help">
            Escribe el número exacto de la suscripción que deseas eliminar.
          </span>

          <div className="search-two">
            <input
              className="input"
              type="number"
              placeholder="Ej: 1"
              value={id}
              onChange={(e) => {
                setId(e.target.value);
                setData(null);
                setConfirmar(false);
                setAlerta(null);
              }}
            />

            <button className="btn btn-search" onClick={handleBuscar} disabled={cargando}>
              Buscar suscripción
            </button>
          </div>
        </div>

        {alerta && <div className={`alert alert-${alerta.tipo}`}>{alerta.mensaje}</div>}
      </div>

      {data && (
        <div className="card">
          <h3 style={{ marginTop: 0 }}>Suscripción encontrada</h3>

          <div className="detail-grid">
            <div className="detail-label">N° Suscripción</div>
            <div className="detail-value">{data.id}</div>

            <div className="detail-label">Usuario</div>
            <div className="detail-value">{data.usuario?.nombre || "Sin usuario"}</div>

            <div className="detail-label">Código usuario</div>
            <div className="detail-value">{data.usuario?.codigo || "N/A"}</div>

            <div className="detail-label">Plataforma</div>
            <div className="detail-value">{data.plataforma}</div>

            <div className="detail-label">Estado</div>
            <div className="detail-value">
              {data.activa ? (
                <span className="badge badge-success">Activa</span>
              ) : (
                <span className="badge badge-danger">Inactiva</span>
              )}
            </div>

            <div className="detail-label">Dispositivos</div>
            <div className="detail-value">{data.dispositivosSimultaneos}</div>

            <div className="detail-label">Costo mensual</div>
            <div className="detail-value">
              ${Number(data.costoMensual || 0).toLocaleString("es-CO")}
            </div>

            <div className="detail-label">Fecha inicio</div>
            <div className="detail-value">
              {String(data.fechaInicio || "Sin fecha").replace("T", " ").slice(0, 19)}
            </div>
          </div>

          <div className="alert alert-warning" style={{ marginTop: "18px" }}>
            Esta acción eliminará la suscripción seleccionada. Verifica que sea la correcta.
          </div>

          <label style={{ display: "block", marginBottom: "14px", fontWeight: 700 }}>
            <input
              type="checkbox"
              checked={confirmar}
              onChange={(e) => setConfirmar(e.target.checked)}
              style={{ marginRight: "8px" }}
            />
            Confirmo que deseo eliminar esta suscripción.
          </label>

          <button className="btn btn-danger" onClick={handleDelete} disabled={cargando}>
            🗑️ Eliminar suscripción
          </button>
        </div>
      )}
    </div>
  );
};

export default Eliminar;