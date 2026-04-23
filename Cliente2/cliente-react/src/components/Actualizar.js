import React, { useState } from "react";
import { buscar, actualizar } from "../services/api";

const Actualizar = () => {

  const [id, setId] = useState("");
  const [form, setForm] = useState(null);


  const handleBuscar = async () => {
    try {
      const data = await buscar(id);

      data.fechaInicio = data.fechaInicio.slice(0, 16);

      setForm(data);

    } catch {
      alert("No encontrado");
    }
  };

  
  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;

    setForm({
      ...form,
      [name]: type === "checkbox" ? checked : value,
    });
  };

 
  const handleUpdate = async (e) => {
    e.preventDefault();

    if (!form.fechaInicio) {
      alert("Debe ingresar fecha");
      return;
    }

    const data = {
      ...form,
      dispositivosSimultaneos: parseInt(form.dispositivosSimultaneos),

      // 🔥 FORMATO QUE SPRING ENTIENDE
      fechaInicio: form.fechaInicio + ":00"
    };

    try {
      const res = await actualizar(form.id, data);

      if (typeof res === "string") {
        alert(res);
      } else {
        alert("Actualizado correctamente");
      }

    } catch {
      alert("Error al actualizar");
    }
  };

  return (
    <div className="card">
      <h3>Actualizar Suscripción</h3>

      <input
        placeholder="ID"
        onChange={(e) => setId(e.target.value)}
      />

      <button onClick={handleBuscar}>Buscar</button>

      {form && (
        <form onSubmit={handleUpdate}>

          <label>Usuario</label>
          <input
            name="nombreUsuario"
            value={form.nombreUsuario}
            onChange={handleChange}
          />

          <label>Dispositivos</label>
          <input
            type="number"
            name="dispositivosSimultaneos"
            value={form.dispositivosSimultaneos}
            onChange={handleChange}
          />

          <label>Fecha inicio</label>
          <input
            type="datetime-local"
            name="fechaInicio"
            value={form.fechaInicio}
            onChange={handleChange}
          />

          <label>
            Activa
            <input
              type="checkbox"
              name="activa"
              checked={form.activa}
              onChange={handleChange}
            />
          </label>

          <button className="primary">Actualizar</button>
        </form>
      )}
    </div>
  );
};

export default Actualizar;