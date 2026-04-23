import React from "react";

const Menu = ({ setVista }) => {
  return (
    <div>
      <h3>Opciones</h3>

      <button onClick={() => setVista("agregar")}>Agregar</button>
      <button onClick={() => setVista("listar")}>Listar</button>
      <button onClick={() => setVista("buscar")}>Buscar</button>
      <button onClick={() => setVista("actualizar")}>Actualizar</button>
      <button onClick={() => setVista("eliminar")}>Eliminar</button>
      <button onClick={() => setVista("acerca")}>Acerca de</button>
    </div>
  );
};

export default Menu;