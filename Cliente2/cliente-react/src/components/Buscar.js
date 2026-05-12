import React, { useState } from "react";
import { buscar, getAll } from "../services/api";

const Buscar = () => {
  const [id, setId] = useState("");
  const [codigoUsuario, setCodigoUsuario] = useState("");
  const [resultados, setResultados] = useState([]);
  const [alerta, setAlerta] = useState(null);
  const [cargando, setCargando] = useState(false);

  const buscarPorId = async () => {
    setAlerta(null);
    setResultados([]);

    if (!id || Number(id) <= 0) {
      setAlerta({ tipo: "warning", mensaje: "Ingresa un ID válido mayor a 0." });
      return;
    }

    setCargando(true);
    const res = await buscar(id);

    if (res.ok) {
      setResultados([res.data]);
    } else {
      setAlerta({ tipo: "error", mensaje: res.mensaje });
    }

    setCargando(false);
  };

  const buscarPorCodigoUsuario = async () => {
    setAlerta(null);
    setResultados([]);

    if (!codigoUsuario || Number(codigoUsuario) <= 0) {
      setAlerta({ tipo: "warning", mensaje: "Ingresa un código de usuario válido mayor a 0." });
      return;
    }

    setCargando(true);
    const res = await getAll({ codigoUsuario });

    if (res.ok && Array.isArray(res.data) && res.data.length > 0) {
      setResultados(res.data);
    } else {
      setAlerta({ tipo: "error", mensaje: "No se encontraron suscripciones para ese usuario." });
    }

    setCargando(false);
  };

  const formatearFecha = (fecha) => {
    if (!fecha) return "Sin fecha";
    return String(fecha).replace("T", " ").slice(0, 19);
  };

  return (
    <div className="page-container">
      <h2 className="page-title">🔎 Buscar Suscripción</h2>
      <p className="page-subtitle">
        Busca una suscripción por ID o lista suscripciones por código del usuario.
      </p>

      {alerta && <div className={`alert alert-${alerta.tipo}`}>{alerta.mensaje}</div>}

      <div className="card">
        <div className="form-group">
          <label>ID de la suscripción</label>
          <span className="form-help">Consulta individual por llave primaria.</span>
          <div className="search-two">
            <input
              className="input"
              type="number"
              placeholder="Ej: 1"
              value={id}
              onChange={(e) => setId(e.target.value)}
            />
            <button className="btn btn-search" onClick={buscarPorId} disabled={cargando}>
              Buscar por ID
            </button>
          </div>
        </div>

        <div className="form-group">
          <label>Código del usuario</label>
          <span className="form-help">Consulta personalizada: suscripciones asociadas a un usuario.</span>
          <div className="search-two">
            <input
              className="input"
              type="number"
              placeholder="Ej: 1"
              value={codigoUsuario}
              onChange={(e) => setCodigoUsuario(e.target.value)}
            />
            <button className="btn btn-search" onClick={buscarPorCodigoUsuario} disabled={cargando}>
              Buscar por usuario
            </button>
          </div>
        </div>
      </div>

      {cargando && <div className="alert alert-warning">⏳ Buscando suscripción...</div>}

      {resultados.length > 0 && (
        <div className="card">
          <h3 style={{ marginTop: 0 }}>Resultados encontrados</h3>
          <div className="table-wrapper">
            <table className="table">
              <thead>
                <tr>
                  <th>ID</th>
                  <th>Usuario</th>
                  <th>Código Usuario</th>
                  <th>Plataforma</th>
                  <th>Activa</th>
                  <th>Dispositivos</th>
                  <th>Costo</th>
                  <th>Fecha inicio</th>
                </tr>
              </thead>
              <tbody>
                {resultados.map((s) => (
                  <tr key={s.id}>
                    <td><strong>{s.id}</strong></td>
                    <td>{s.usuario?.nombre || "Sin usuario"}</td>
                    <td>{s.usuario?.codigo || "N/A"}</td>
                    <td><span className="badge badge-info">{s.plataforma}</span></td>
                    <td>
                      {s.activa ? (
                        <span className="badge badge-success">Activa</span>
                      ) : (
                        <span className="badge badge-danger">Inactiva</span>
                      )}
                    </td>
                    <td>{s.dispositivosSimultaneos}</td>
                    <td>${Number(s.costoMensual || 0).toLocaleString()}</td>
                    <td>{formatearFecha(s.fechaInicio)}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      )}
    </div>
  );
};

export default Buscar;