import React from 'react';
import { Link } from 'react-router-dom';

const Inicio = () => {
    return (
        <div style={s.page}>
            <div style={s.hero}>
                <h1 style={s.title}> Sistema de Suscripciones</h1>
                <p style={s.subtitle}>
                    Gestión de usuarios y suscripciones de servicios de streaming
                </p>
                <div style={s.badge}>Universidad de Ibagué · Desarrollo de Aplicaciones Empresariales</div>
            </div>

            <div style={s.grid}>
                {/* CARD USUARIOS */}
                <div style={s.card}>
                    <div style={s.cardIcon}></div>
                    <h2 style={s.cardTitle}>Usuarios</h2>
                    <p style={s.cardDesc}>
                        Administra los usuarios del sistema. Crea, consulta,
                        actualiza y elimina registros de usuarios.
                    </p>
                    <div style={s.cardNote}>
                        <strong>⚠️ Paso 1:</strong> Primero debes crear un usuario
                        antes de asociarle una suscripción.
                    </div>
                    <div style={s.btnGroup}>
                        <Link to="/usuarios/listar"  style={s.btnPrimary}>Listar</Link>
                        <Link to="/usuarios/agregar" style={s.btnSuccess}> Agregar</Link>
                        <Link to="/usuarios/buscar"  style={s.btnSecondary}>Buscar</Link>
                    </div>
                </div>

                {/* CARD SUSCRIPCIONES */}
                <div style={s.card}>
                    <div style={s.cardIcon}></div>
                    <h2 style={s.cardTitle}>Suscripciones Streaming</h2>
                    <p style={s.cardDesc}>
                        Gestiona las suscripciones de streaming. Cada suscripción
                        debe estar vinculada a un usuario existente.
                    </p>
                    <div style={s.cardNote}>
                        <strong>⚠️ Paso 2:</strong> Para crear una suscripción
                        necesitas el código del usuario registrado.
                    </div>
                    <div style={s.btnGroup}>
                        <Link to="/streaming/listar"  style={s.btnPrimary}>Listar</Link>
                        <Link to="/streaming/agregar" style={s.btnSuccess}> Agregar</Link>
                        <Link to="/streaming/buscar"  style={s.btnSecondary}>Buscar</Link>
                    </div>
                </div>
            </div>

            {/* GUÍA RÁPIDA */}
            <div style={s.guide}>
                <h3 style={s.guideTitle}> Guía rápida de uso</h3>
                <div style={s.steps}>
                    <div style={s.step}>
                        <span style={s.stepNum}>1</span>
                        <div>
                            <strong>Crea un Usuario</strong>
                            <p style={s.stepDesc}>Ve a Usuarios → Agregar. Completa código, documento, nombre y email.</p>
                        </div>
                    </div>
                    <div style={s.stepArrow}>→</div>
                    <div style={s.step}>
                        <span style={s.stepNum}>2</span>
                        <div>
                            <strong>Anota el Código</strong>
                            <p style={s.stepDesc}>El código del usuario es la clave para vincular la suscripción.</p>
                        </div>
                    </div>
                    <div style={s.stepArrow}>→</div>
                    <div style={s.step}>
                        <span style={s.stepNum}>3</span>
                        <div>
                            <strong>Crea la Suscripción</strong>
                            <p style={s.stepDesc}>Ve a Suscripciones → Agregar. Ingresa el código del usuario y completa los datos.</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

const s = {
    page: { fontFamily: 'Segoe UI, sans-serif' },
    hero: {
        background: 'linear-gradient(135deg, #1a1a2e 0%, #16213e 50%, #0f3460 100%)',
        color: '#fff', borderRadius: '16px', padding: '50px 40px',
        textAlign: 'center', marginBottom: '30px'
    },
    title: { fontSize: '2.2rem', margin: '0 0 12px', fontWeight: '700' },
    subtitle: { fontSize: '1.1rem', color: '#a8b2d8', margin: '0 0 16px' },
    badge: {
        display: 'inline-block', background: 'rgba(233,69,96,0.2)',
        border: '1px solid #e94560', color: '#e94560',
        padding: '6px 18px', borderRadius: '20px', fontSize: '0.85rem'
    },
    grid: { display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '24px', marginBottom: '30px' },
    card: {
        background: '#fff', borderRadius: '12px', padding: '30px',
        boxShadow: '0 4px 20px rgba(0,0,0,0.08)',
        border: '1px solid #f0f0f0'
    },
    cardIcon: { fontSize: '2.5rem', marginBottom: '12px' },
    cardTitle: { fontSize: '1.4rem', color: '#1a1a2e', margin: '0 0 10px', fontWeight: '700' },
    cardDesc: { color: '#666', lineHeight: '1.6', margin: '0 0 16px', fontSize: '0.95rem' },
    cardNote: {
        background: '#fff8e1', border: '1px solid #ffe082',
        borderRadius: '8px', padding: '10px 14px',
        fontSize: '0.85rem', color: '#795548', marginBottom: '20px'
    },
    btnGroup: { display: 'flex', gap: '10px', flexWrap: 'wrap' },
    btnPrimary: {
        background: '#0f3460', color: '#fff', padding: '8px 18px',
        borderRadius: '8px', textDecoration: 'none', fontSize: '0.9rem', fontWeight: '600'
    },
    btnSuccess: {
        background: '#e94560', color: '#fff', padding: '8px 18px',
        borderRadius: '8px', textDecoration: 'none', fontSize: '0.9rem', fontWeight: '600'
    },
    btnSecondary: {
        background: '#f5f5f5', color: '#333', padding: '8px 18px',
        borderRadius: '8px', textDecoration: 'none', fontSize: '0.9rem',
        border: '1px solid #ddd', fontWeight: '600'
    },
    guide: {
        background: '#f8f9ff', borderRadius: '12px', padding: '30px',
        border: '1px solid #e8eaf6'
    },
    guideTitle: { color: '#1a1a2e', margin: '0 0 24px', fontSize: '1.1rem' },
    steps: { display: 'flex', alignItems: 'center', gap: '16px', flexWrap: 'wrap' },
    step: {
        display: 'flex', alignItems: 'flex-start', gap: '14px',
        flex: '1', minWidth: '180px',
        background: '#fff', borderRadius: '10px', padding: '16px',
        boxShadow: '0 2px 8px rgba(0,0,0,0.06)'
    },
    stepNum: {
        background: '#e94560', color: '#fff', borderRadius: '50%',
        width: '32px', height: '32px', display: 'flex',
        alignItems: 'center', justifyContent: 'center',
        fontWeight: '700', fontSize: '1rem', flexShrink: 0
    },
    stepArrow: { fontSize: '1.5rem', color: '#ccc', flexShrink: 0 },
    stepDesc: { margin: '4px 0 0', color: '#888', fontSize: '0.85rem', lineHeight: '1.4' }
};

export default Inicio;