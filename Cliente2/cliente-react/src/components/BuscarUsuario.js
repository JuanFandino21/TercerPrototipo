import React, { useState } from "react";
import { getUsuarioByCodigo, buscarUsuarioPorNombre } from "../services/api";

const BuscarUsuario = () => {
  const [codigo, setCodigo] = useState("");
  const [nombre, setNombre] = useState("");
  const [resultados, setResultados] = useState([]);
  const [alerta, setAlerta] = useState(null);
  const [cargando, setCargando] = useState(false);

  const limpiarResultados = () => {
    setResultados([]);
    setAlerta(null);
  };

  const buscarPorCodigo = async () => {
    limpiarResultados();

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
      setResultados([res.data]);
    } else {
      setAlerta({
        tipo: "warning",
        mensaje: `No se encontró ningún usuario con el código ${codigo}.`,
      });
    }

    setCargando(false);
  };

  const buscarPorNombre = async () => {
    limpiarResultados();

    if (!nombre.trim() || nombre.trim().length < 2) {
      setAlerta({
        tipo: "warning",
        mensaje: "Escribe al menos 2 letras del nombre del usuario.",
      });
      return;
    }

    setCargando(true);

    const res = await buscarUsuarioPorNombre(nombre.trim());

    if (res.ok && Array.isArray(res.data) && res.data.length > 0) {
      setResultados(res.data);
    } else {
      setAlerta({
        tipo: "warning",
        mensaje: `No se encontraron usuarios con el nombre "${nombre}".`,
      });
    }

    setCargando(false);
  };

  return (
    <div className="page-container">
      <h2 className="page-title">🔎 Buscar Usuario</h2>

      <p className="page-subtitle">
        Busca un usuario por su código o por coincidencias en el nombre.
      </p>

      <div className="card">
        <div className="form-group">
          <label>Código del usuario</label>
          <span className="form-help">
            Usa esta opción si conoces el código exacto del usuario.
          </span>

          <div className="search-two">
            <input
              className="input"
              type="number"
              placeholder="Ej: 1"
              value={codigo}
              onChange={(e) => setCodigo(e.target.value)}
            />

            <button className="btn btn-search" onClick={buscarPorCodigo} disabled={cargando}>
              Buscar por código
            </button>
          </div>
        </div>

        <div className="form-group">
          <label>Nombre del usuario</label>
          <span className="form-help">
            Usa esta opción si recuerdas el nombre o parte del nombre.
          </span>

          <div className="search-two">
            <input
              className="input"
              type="text"
              placeholder="Ej: Juan"
              value={nombre}
              onChange={(e) => setNombre(e.target.value)}
            />

            <button className="btn btn-search" onClick={buscarPorNombre} disabled={cargando}>
              Buscar por nombre
            </button>
          </div>
        </div>

        {cargando && <div className="alert alert-warning">⏳ Buscando...</div>}

        {alerta && <div className={`alert alert-${alerta.tipo}`}>{alerta.mensaje}</div>}
      </div>

      {resultados.length > 0 && (
        <div className="card">
          <h3 style={{ marginTop: 0 }}>Resultado de la búsqueda</h3>

          <div className="table-wrapper">
            <table className="table">
              <thead>
                <tr>
                  <th>Código</th>
                  <th>Documento</th>
                  <th>Tipo</th>
                  <th>Nombre</th>
                  <th>Email</th>
                  <th>Estado</th>
                </tr>
              </thead>

              <tbody>
                {resultados.map((u) => (
                  <tr key={u.codigo}>
                    <td>
                      <strong>{u.codigo}</strong>
                    </td>
                    <td>{u.numDocumento}</td>
                    <td>
                      <span className="badge badge-info">{u.tipoDoc}</span>
                    </td>
                    <td>{u.nombre}</td>
                    <td>{u.email}</td>
                    <td>
                      {u.estado === "AC" ? (
                        <span className="badge badge-success">Activo</span>
                      ) : (
                        <span className="badge badge-danger">Inactivo</span>
                      )}
                    </td>
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

export default BuscarUsuario;