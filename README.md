# Tercer Prototipo - Gestión de Usuarios y Suscripciones Streaming

## Integrantes
- Juan David Fandiño Hernández
- Ronald Andres Barrios Hernández

## Descripción
Aplicación distribuida para gestionar usuarios y suscripciones de streaming mediante servicios web REST.

El sistema implementa dos objetos relacionados bajo el modelo maestro-detalle:

- Usuario
- Suscripción Streaming

Relación:
- Un usuario puede tener varias suscripciones.
- Una suscripción pertenece a un usuario.

## Tecnologías utilizadas

### Backend
- Java
- Spring Boot
- Spring Data JPA / Hibernate
- Oracle Database XE
- Servicios Web REST

### Cliente 1
- C#
- Windows Forms
- RestSharp

### Cliente 2
- React
- JavaScript
- Fetch API

## Estructura del repositorio

```txt
Backend/
Cliente1VS/
Cliente2React/
README.md
