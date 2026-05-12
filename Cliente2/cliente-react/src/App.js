import React from 'react';
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import Menu from './components/Menu';
import Inicio from './components/Inicio';
import "./styles.css";
import Listar from './components/Listar';
import Agregar from './components/Agregar';
import Buscar from './components/Buscar';
import Actualizar from './components/Actualizar';
import Eliminar from './components/Eliminar';

import ListarUsuario from './components/ListarUsuario';
import AgregarUsuario from './components/AgregarUsuario';
import BuscarUsuario from './components/BuscarUsuario';
import ActualizarUsuario from './components/ActualizarUsuario';
import EliminarUsuario from './components/EliminarUsuario';
import AcercaDe from './components/AcercaDe';

function App() {
    return (
        <BrowserRouter>
            <Menu />
            <div style={{ padding: '30px', maxWidth: '1100px', margin: '0 auto' }}>
                <Routes>
                    <Route path="/" element={<Navigate to="/inicio" />} />
                    <Route path="/inicio"              element={<Inicio />} />
                    <Route path="/usuarios/listar"     element={<ListarUsuario />} />
                    <Route path="/usuarios/agregar"    element={<AgregarUsuario />} />
                    <Route path="/usuarios/buscar"     element={<BuscarUsuario />} />
                    <Route path="/usuarios/actualizar" element={<ActualizarUsuario />} />
                    <Route path="/usuarios/eliminar"   element={<EliminarUsuario />} />
                    <Route path="/streaming/listar"     element={<Listar />} />
                    <Route path="/streaming/agregar"    element={<Agregar />} />
                    <Route path="/streaming/buscar"     element={<Buscar />} />
                    <Route path="/streaming/actualizar" element={<Actualizar />} />
                    <Route path="/streaming/eliminar"   element={<Eliminar />} />
                    <Route path="/acerca"               element={<AcercaDe />} />
                </Routes>
            </div>
        </BrowserRouter>
    );
}

export default App;