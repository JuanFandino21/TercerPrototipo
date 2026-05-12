import React, { useEffect, useState } from "react";
import { getUsuarios } from "../services/api";

const ListarUsuario = () => {
  const [lista, setLista] = useState([]);
  const [criterio, setCriterio] = useState("nombre");
  const [busqueda, setBusqueda] = useState("");
  const [estado, setEstado] = useState("");
  const [alerta, setAlerta] = useState(null);
  const [cargando, setCargando] = useState(true);

  const cargarUsuarios = async () => {
    setCargando(true);
    setAlerta(null);

    const res = await getUsuarios();

    if (res.ok) {
      const usuarios = Array.isArray(res.data) ? res.data : [];
      setLista(usuarios);

      if (usuarios.length === 0) {
        setAlerta({
          tipo: "warning",
          mensaje: "Aún no hay usuarios registrados. Primero agrega un usuario.",
        });
      }
    } else {
      setLista([]);
      setAlerta({
        tipo: "error",
        mensaje: res.mensaje || "No se pudieron cargar los usuarios.",
      });
    }

    setCargando(false);
  };

  useEffect(() => {
    cargarUsuarios();
  }, []);

  const limpiarFiltros = () => {
    setCriterio("nombre");
    setBusqueda("");
    setEstado("");
  };

  const filtrados = lista.filter((u) => {
    const texto = busqueda.trim().toLowerCase();

    let coincideBusqueda = true;

    if (texto !== "") {
      if (criterio === "codigo") {
        coincideBusqueda = String(u.codigo) === texto;
      }

      if (criterio === "documento") {
        coincideBusqueda = String(u.numDocumento || "").includes(texto);
      }

      if (criterio === "nombre") {
        coincideBusqueda = String(u.nombre || "").toLowerCase().includes(texto);
      }

      if (criterio === "email") {
        coincideBusqueda = String(u.email || "").toLowerCase().includes(texto);
      }
    }

    const coincideEstado = estado === "" ? true : u.estado === estado;

    return coincideBusqueda && coincideEstado;
  });

  const formatearFecha = (fecha) => {
    if (!fecha) return "Sin fecha";
    return String(fecha).replace("T", " ").slice(0, 19);
  };

  const textoAyuda = {
    codigo: "Escribe el código exacto del usuario. Ejemplo: 1",
    documento: "Escribe el número de documento completo o una parte.",
    nombre: "Escribe el nombre o una parte del nombre.",
    email: "Escribe el email o una parte del email.",
  };

  return (
    <div className="page-container">
      <h2 className="page-title">👥 Lista de Usuarios</h2>

      <p className="page-subtitle">
        Aquí puedes ver los usuarios registrados y filtrar de forma clara por código,
        documento, nombre, email o estado.
      </p>

      <div className="card">
        <div className="form-grid">
          <div className="form-group">
            <label>Buscar por</label>
            <span className="form-help">
              Selecciona el dato que quieres usar para buscar.
            </span>

            <select
              className="select"
              value={criterio}
              onChange={(e) => {
                setCriterio(e.target.value);
                setBusqueda("");
              }}
            >
              <option value="codigo">Código</option>
              <option value="documento">Documento</option>
              <option value="nombre">Nombre</option>
              <option value="email">Email</option>
            </select>
          </div>

          <div className="form-group">
            <label>Dato a buscar</label>
            <span className="form-help">{textoAyuda[criterio]}</span>

            <input
              className="input"
              type={criterio === "codigo" || criterio === "documento" ? "number" : "text"}
              placeholder={
                criterio === "codigo"
                  ? "Ej: 1"
                  : criterio === "documento"
                  ? "Ej: 1193115029"
                  : criterio === "nombre"
                  ? "Ej: Juan"
                  : "Ej: juan@test.com"
              }
              value={busqueda}
              onChange={(e) => setBusqueda(e.target.value)}
            />
          </div>

          <div className="form-group">
            <label>Estado</label>
            <span className="form-help">
              Puedes ver todos, solo activos o solo inactivos.
            </span>

            <select
              className="select"
              value={estado}
              onChange={(e) => setEstado(e.target.value)}
            >
              <option value="">Todos</option>
              <option value="AC">Activos</option>
              <option value="IN">Inactivos</option>
            </select>
          </div>

          <div className="form-group">
            <label>Acciones</label>
            <span className="form-help">
              Recarga la información o limpia los filtros.
            </span>

            <div className="btn-row">
              <button className="btn btn-search" type="button" onClick={cargarUsuarios}>
                🔄 Recargar
              </button>

              <button className="btn btn-secondary" type="button" onClick={limpiarFiltros}>
                Limpiar
              </button>
            </div>
          </div>
        </div>

        {alerta && <div className={`alert alert-${alerta.tipo}`}>{alerta.mensaje}</div>}

        {cargando ? (
          <div className="alert alert-warning">⏳ Cargando usuarios...</div>
        ) : lista.length === 0 ? (
          <div className="alert alert-warning">
            Aún no hay usuarios registrados.
          </div>
        ) : filtrados.length === 0 ? (
          <div className="alert alert-warning">
            No hay usuarios que coincidan con los filtros seleccionados.
          </div>
        ) : (
          <div className="table-wrapper">
            <table className="table">
              <thead>
                <tr>
                  <th>Código</th>
                  <th>N° Documento</th>
                  <th>Tipo Doc</th>
                  <th>Nombre</th>
                  <th>Email</th>
                  <th>Estado</th>
                  <th>Fecha Registro</th>
                </tr>
              </thead>

              <tbody>
                {filtrados.map((u) => (
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

                    <td>{formatearFecha(u.fechaRegistro)}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
      </div>

      {lista.length > 0 && (
        <p className="page-subtitle" style={{ marginTop: "12px" }}>
          Mostrando {filtrados.length} de {lista.length} usuario(s).
        </p>
      )}
    </div>
  );
};

export default ListarUsuario;