import React, { useState } from "react";
import { buscar, eliminar } from "../services/api";

const Eliminar = () => {
  const [id, setId] = useState("");
  const [data, setData] = useState(null);

  const handleBuscar = async () => {
    const res = await buscar(id);

    if (!res || res.id === undefined) {
      alert("No encontrado");
      return;
    }

    setData(res);
  };

  const handleDelete = async () => {
    if (!window.confirm("¿Seguro que desea eliminar?")) return;

    await eliminar(id);
    alert("Eliminado correctamente");
    setData(null);
  };

  return (
    <div>
      <h3>Eliminar Suscripción</h3>

      <input placeholder="ID" onChange={(e) => setId(e.target.value)} />
      <button onClick={handleBuscar}>Buscar</button>

      {data && (
        <div>
          <h4>Información:</h4>
          <p><b>ID:</b> {data.id}</p>
          <p><b>Usuario:</b> {data.nombreUsuario}</p>
          <p><b>Activa:</b> {data.activa ? "Sí" : "No"}</p>
          <p><b>Dispositivos:</b> {data.dispositivosSimultaneos}</p>
          <p><b>Fecha:</b> {data.fechaInicio}</p>

          <button className="danger" onClick={handleDelete}>
            Eliminar
          </button>
        </div>
      )}
    </div>
  );
};

export default Eliminar;