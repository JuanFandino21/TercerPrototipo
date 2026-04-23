import React, { useEffect, useState } from "react";
import { getAll } from "../services/api";

const Listar = () => {
  const [lista, setLista] = useState([]);
  const [filtro, setFiltro] = useState("");

  useEffect(() => {
    getAll().then(setLista);
  }, []);

  const filtrados = lista.filter((s) =>
    s.nombreUsuario?.toLowerCase().includes(filtro.toLowerCase())
  );

  return (
    <div>
      <h3>Lista de Suscripciones</h3>

      <input
        placeholder="Filtrar por usuario"
        onChange={(e) => setFiltro(e.target.value)}
      />

      <ul>
        {filtrados.map((s) => (
          <li key={s.id}>
            <strong>ID:</strong> {s.id} <br />
            <strong>Usuario:</strong> {s.nombreUsuario} <br />
            <strong>Activa:</strong> {s.activa ? "Sí" : "No"} <br />
            <strong>Dispositivos:</strong> {s.dispositivosSimultaneos} <br />
            <strong>Fecha inicio:</strong> {s.fechaInicio} <br />
            <strong>Fecha vencimiento:</strong> {s.fechaVencimiento}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default Listar;