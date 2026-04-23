import React, { useState } from "react";
import "./styles.css";

import Menu from "./components/Menu";
import Agregar from "./components/Agregar";
import Listar from "./components/Listar";
import Buscar from "./components/Buscar";
import Eliminar from "./components/Eliminar";
import Actualizar from "./components/Actualizar";
import AcercaDe from "./components/AcercaDe";

function App() {
  const [vista, setVista] = useState("menu");

  return (
    <div className="container">
      <h1>Sistema de Suscripciones</h1>

      <div className="panel">
        <Menu setVista={setVista} />
      </div>

      <div className="panel">
        {vista === "menu" && <p>Seleccione una opción del menú</p>}
        {vista === "agregar" && <Agregar />}
        {vista === "listar" && <Listar />}
        {vista === "buscar" && <Buscar />}
        {vista === "eliminar" && <Eliminar />}
        {vista === "actualizar" && <Actualizar />}
        {vista === "acerca" && <AcercaDe />}
      </div>
    </div>
  );
}

export default App;