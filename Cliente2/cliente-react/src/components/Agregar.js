import React, { useState } from "react";
import { crear } from "../services/api";

const Agregar = () => {

  const [form, setForm] = useState({
    id: "",
    nombreUsuario: "",
    activa: false,
    dispositivosSimultaneos: "",
    fechaInicio: "",
  });

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;

    setForm({
      ...form,
      [name]: type === "checkbox" ? checked : value,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

   
    if (!form.id || parseInt(form.id) <= 0) {
      alert("El ID debe ser mayor a 0");
      return;
    }

    if (!form.nombreUsuario.trim()) {
      alert("El usuario es obligatorio");
      return;
    }

    if (!form.dispositivosSimultaneos) {
      alert("Debe ingresar dispositivos");
      return;
    }

    const dispositivos = parseInt(form.dispositivosSimultaneos);

    if (dispositivos < 1 || dispositivos > 6) {
      alert("Los dispositivos deben estar entre 1 y 6");
      return;
    }

    if (!form.fechaInicio) {
      alert("Debe ingresar fecha");
      return;
    }

    
    const fechaFormateada = form.fechaInicio + ":00";

    const data = {
      id: parseInt(form.id),
      nombreUsuario: form.nombreUsuario.trim(),
      activa: form.activa,
      dispositivosSimultaneos: dispositivos,
      fechaInicio: fechaFormateada,
    };

    try {
      console.log("Enviando:", data);

      const res = await crear(data);

      if (typeof res === "string") {
        alert(res); // mensaje backend
      } else {
        alert("Guardado correctamente");

        setForm({
          id: "",
          nombreUsuario: "",
          activa: false,
          dispositivosSimultaneos: "",
          fechaInicio: "",
        });
      }

    } catch (error) {
      console.error(error);
      alert("Error al conectar con el servidor");
    }
  };

  return (
    <div className="card">
      <h3>Agregar Suscripción</h3>

      <form onSubmit={handleSubmit}>

        <input
          type="number"
          name="id"
          placeholder="ID"
          value={form.id}
          onChange={handleChange}
        />

        <input
          type="text"
          name="nombreUsuario"
          placeholder="Usuario"
          value={form.nombreUsuario}
          onChange={handleChange}
        />

        <label style={{ display: "block", margin: "10px 0" }}>
          Activa
          <input
            type="checkbox"
            name="activa"
            checked={form.activa}
            onChange={handleChange}
            style={{ marginLeft: "10px" }}
          />
        </label>

        <input
          type="number"
          name="dispositivosSimultaneos"
          placeholder="Dispositivos (1-6)"
          value={form.dispositivosSimultaneos}
          onChange={handleChange}
        />

        <input
          type="datetime-local"
          name="fechaInicio"
          value={form.fechaInicio}
          onChange={handleChange}
        />

        <button className="primary">Guardar</button>
      </form>
    </div>
  );
};

export default Agregar;