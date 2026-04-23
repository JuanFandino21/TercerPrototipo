import React, { useState } from "react";
import { buscar } from "../services/api";

const Buscar = () => {
  const [id, setId] = useState("");
  const [data, setData] = useState(null);

  const handleBuscar = async () => {
    try {
      const res = await buscar(id);
      setData(res);
    } catch {
      alert("No encontrado");
    }
  };

  return (
    <div>
      <h3>Buscar Suscripción</h3>

      <input placeholder="ID" onChange={(e) => setId(e.target.value)} />
      <button onClick={handleBuscar}>Buscar</button>

      {data && (
        <div>
          <p><strong>ID:</strong> {data.id}</p>
          <p><strong>Usuario:</strong> {data.nombreUsuario}</p>
          <p><strong>Activa:</strong> {data.activa ? "Sí" : "No"}</p>
          <p><strong>Dispositivos:</strong> {data.dispositivosSimultaneos}</p>
          <p><strong>Fecha inicio:</strong> {data.fechaInicio}</p>
          <p><strong>Fecha vencimiento:</strong> {data.fechaVencimiento}</p>
        </div>
      )}
    </div>
  );
};

export default Buscar;