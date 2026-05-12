import React, { useState } from 'react';
import { Link, useLocation } from 'react-router-dom';

function Menu() {
    const [openMenu, setOpenMenu] = useState(null);
    const location = useLocation();

    const toggle = (menu) => setOpenMenu(openMenu === menu ? null : menu);

    const linkStyle = (path) => ({
        display: 'block', padding: '10px 18px', color: '#cdd6f4',
        textDecoration: 'none', fontSize: '0.9rem',
        background: location.pathname === path ? 'rgba(233,69,96,0.15)' : 'transparent',
        borderLeft: location.pathname === path ? '3px solid #e94560' : '3px solid transparent',
        transition: 'all 0.2s'
    });

    const dropdown = (label, icon, menuKey, links) => (
        <div style={{ position: 'relative' }}>
            <button
                onClick={() => toggle(menuKey)}
                style={{
                    background: openMenu === menuKey ? 'rgba(233,69,96,0.2)' : 'transparent',
                    border: 'none', color: '#fff', padding: '8px 16px',
                    borderRadius: '8px', cursor: 'pointer', fontSize: '0.95rem',
                    fontWeight: '600', display: 'flex', alignItems: 'center', gap: '6px'
                }}
            >
                {icon} {label} <span style={{ fontSize: '0.7rem' }}>{openMenu === menuKey ? '▲' : '▼'}</span>
            </button>
            {openMenu === menuKey && (
                <div style={{
                    position: 'absolute', top: '110%', left: 0,
                    background: '#16213e', borderRadius: '10px',
                    boxShadow: '0 8px 32px rgba(0,0,0,0.3)',
                    border: '1px solid rgba(255,255,255,0.08)',
                    minWidth: '180px', zIndex: 1000, overflow: 'hidden'
                }}>
                    {links.map(([to, label]) => (
                        <Link key={to} to={to} style={linkStyle(to)} onClick={() => setOpenMenu(null)}>
                            {label}
                        </Link>
                    ))}
                </div>
            )}
        </div>
    );

    return (
        <nav style={{
            background: 'linear-gradient(90deg, #1a1a2e, #0f3460)',
            padding: '0 24px', display: 'flex', alignItems: 'center',
            gap: '8px', height: '60px', boxShadow: '0 2px 12px rgba(0,0,0,0.3)'
        }}>
            <Link to="/inicio" style={{ textDecoration: 'none', marginRight: '16px' }}>
                <span style={{ fontWeight: '800', fontSize: '1.1rem', color: '#e94560' }}>
                     POC Suscripciones
                </span>
            </Link>

            <Link to="/inicio" style={{
                color: '#cdd6f4', textDecoration: 'none', padding: '8px 14px',
                borderRadius: '8px', fontSize: '0.9rem',
                background: location.pathname === '/inicio' ? 'rgba(233,69,96,0.2)' : 'transparent'
            }}>
                 Inicio
            </Link>

            {dropdown(' Usuarios', '', 'usuarios', [
                ['/usuarios/listar',     ' Listar'],
                ['/usuarios/agregar',    ' Agregar'],
                ['/usuarios/buscar',     ' Buscar'],
                ['/usuarios/actualizar', ' Actualizar'],
                ['/usuarios/eliminar',   ' Eliminar'],
            ])}

            {dropdown(' Suscripciones', '', 'streaming', [
                ['/streaming/listar',     ' Listar'],
                ['/streaming/agregar',    'Agregar'],
                ['/streaming/buscar',     'Buscar'],
                ['/streaming/actualizar', 'Actualizar'],
                ['/streaming/eliminar',   'Eliminar'],
            ])}

            <Link to="/acerca" style={{
                color: '#cdd6f4', textDecoration: 'none', padding: '8px 14px',
                borderRadius: '8px', fontSize: '0.9rem', marginLeft: 'auto',
                background: location.pathname === '/acerca' ? 'rgba(233,69,96,0.2)' : 'transparent'
            }}>
                Acerca de
            </Link>
        </nav>
    );
}

export default Menu;