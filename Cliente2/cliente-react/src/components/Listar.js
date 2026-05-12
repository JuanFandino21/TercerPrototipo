import React, { useEffect, useState } from "react";
import { getAll } from "../services/api";

const Listar = () => {
  const [lista, setLista] = useState([]);
  const [filtro, setFiltro] = useState("");
  const [filtroEstado, setFiltroEstado] = useState("");
  const [alerta, setAlerta] = useState(null);
  const [cargando, setCargando] = useState(true);

  const cargarSuscripciones = async (params = {}) => {
    setCargando(true);
    setAlerta(null);

    const res = await getAll(params);

    if (res.ok) {
      setLista(Array.isArray(res.data) ? res.data : []);

      if (!res.data || res.data.length === 0) {
        setAlerta({
          tipo: "warning",
          mensaje: "No hay suscripciones para mostrar con este criterio.",
        });
      }
    } else {
      setLista([]);
      setAlerta({
        tipo: "error",
        mensaje: res.mensaje || "No se pudieron cargar las suscripciones.",
      });
    }

    setCargando(false);
  };

  useEffect(() => {
    cargarSuscripciones();
  }, []);

  const aplicarFiltroEstado = async (valor) => {
    setFiltroEstado(valor);

    if (valor === "") {
      await cargarSuscripciones();
      return;
    }

    await cargarSuscripciones({
      activa: valor === "true",
    });
  };

  const filtrados = lista.filter((s) => {
    const texto = `${s.id} ${s.plataforma || ""} ${s.usuario?.nombre || ""} ${s.usuario?.codigo || ""}`
      .toLowerCase();

    const coincideTexto = texto.includes(filtro.toLowerCase());

    const coincideEstado =
      filtroEstado === ""
        ? true
        : filtroEstado === "true"
        ? s.activa === true
        : s.activa === false;

    return coincideTexto && coincideEstado;
  });

  const formatearFecha = (fecha) => {
    if (!fecha) return "Sin fecha";
    return String(fecha).replace("T", " ").slice(0, 19);
  };

  return (
    <div className="page-container">
      <h2 className="page-title">🎬 Lista de Suscripciones Streaming</h2>

      <p className="page-subtitle">
        Aquí puedes ver las suscripciones registradas y filtrar por usuario, plataforma o estado.
      </p>

      <div className="card">
        <div className="form-grid">
          <div className="form-group">
            <label>Buscar en la lista</label>
            <span className="form-help">
              Puedes escribir el nombre del usuario, la plataforma o el número de suscripción.
            </span>

            <input
              className="input"
              placeholder="Ej: Netflix, Juan, 1"
              value={filtro}
              onChange={(e) => setFiltro(e.target.value)}
            />
          </div>

          <div className="form-group">
            <label>Estado de la suscripción</label>
            <span className="form-help">
              Filtra las suscripciones activas o inactivas.
            </span>

            <select
              className="select"
              value={filtroEstado}
              onChange={(e) => aplicarFiltroEstado(e.target.value)}
            >
              <option value="">Todas</option>
              <option value="true">Activas</option>
              <option value="false">Inactivas</option>
            </select>
          </div>
        </div>

        <div className="btn-row">
          <button
            className="btn btn-search"
            onClick={() => {
              setFiltro("");
              setFiltroEstado("");
              cargarSuscripciones();
            }}
          >
            🔄 Recargar todo
          </button>
        </div>

        {alerta && <div className={`alert alert-${alerta.tipo}`}>{alerta.mensaje}</div>}
      </div>

      {cargando ? (
        <div className="alert alert-warning">⏳ Cargando suscripciones...</div>
      ) : filtrados.length === 0 && lista.length > 0 ? (
        <div className="alert alert-warning">
          No hay suscripciones que coincidan con la búsqueda.
        </div>
      ) : filtrados.length > 0 ? (
        <div className="table-wrapper">
          <table className="table">
            <thead>
              <tr>
                <th>N° Suscripción</th>
                <th>Usuario</th>
                <th>Código Usuario</th>
                <th>Plataforma</th>
                <th>Estado</th>
                <th>Dispositivos</th>
                <th>Costo mensual</th>
                <th>Fecha inicio</th>
              </tr>
            </thead>

            <tbody>
              {filtrados.map((s) => (
                <tr key={s.id}>
                  <td>
                    <strong>{s.id}</strong>
                  </td>
                  <td>{s.usuario?.nombre || "Sin usuario"}</td>
                  <td>{s.usuario?.codigo || "N/A"}</td>
                  <td>
                    <span className="badge badge-info">{s.plataforma}</span>
                  </td>
                  <td>
                    {s.activa ? (
                      <span className="badge badge-success">Activa</span>
                    ) : (
                      <span className="badge badge-danger">Inactiva</span>
                    )}
                  </td>
                  <td>{s.dispositivosSimultaneos}</td>
                  <td>${Number(s.costoMensual || 0).toLocaleString("es-CO")}</td>
                  <td>{formatearFecha(s.fechaInicio)}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      ) : null}
    </div>
  );
};

export default Listar;