const BASE = "http://localhost:8090";

async function request(url, options = {}) {
  try {
    const res = await fetch(url, options);
    const text = await res.text();

    let data;

    try {
      data = text ? JSON.parse(text) : null;
    } catch {
      data = text;
    }

    if (!res.ok) {
      return {
        ok: false,
        status: res.status,
        mensaje:
          typeof data === "string"
            ? data
            : data?.mensaje || `Error HTTP ${res.status}`,
        data,
      };
    }

    return {
      ok: true,
      status: res.status,
      mensaje:
        typeof data === "string"
          ? data
          : data?.mensaje || "Operación realizada correctamente",
      data,
    };
  } catch (error) {
    return {
      ok: false,
      status: 0,
      mensaje:
        "No se pudo conectar con el servidor. Verifica que el backend esté encendido.",
      data: error.message,
    };
  }
}

// USUARIOS

export const getUsuarios = async () => {
  const res = await request(`${BASE}/usuarios/all`);

  if (!res.ok && res.status === 404) {
    return {
      ok: true,
      status: 200,
      data: [],
      mensaje: "Aún no hay usuarios registrados.",
    };
  }

  return res;
};

export const getUsuarioByCodigo = (codigo) => {
  return request(`${BASE}/usuarios/find/${codigo}`);
};

export const buscarUsuarioPorNombre = async (nombre) => {
  const res = await request(
    `${BASE}/usuarios/buscar?nombre=${encodeURIComponent(nombre)}`
  );

  if (!res.ok && res.status === 404) {
    return {
      ok: true,
      status: 200,
      data: [],
      mensaje: "No se encontraron usuarios.",
    };
  }

  return res;
};

export const buscarUsuarioPorEmail = (email) => {
  return request(`${BASE}/usuarios/buscar?email=${encodeURIComponent(email)}`);
};

export const crearUsuario = (data) => {
  return request(`${BASE}/usuarios/`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  });
};

export const actualizarUsuario = (codigo, data) => {
  return request(`${BASE}/usuarios/update/${codigo}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  });
};

export const eliminarUsuario = (codigo) => {
  return request(`${BASE}/usuarios/delete/${codigo}`, {
    method: "DELETE",
  });
};

// SUSCRIPCIONES

export const getAll = async (params = {}) => {
  const query = new URLSearchParams(params).toString();
  const res = await request(`${BASE}/streaming/all${query ? `?${query}` : ""}`);

  if (!res.ok && res.status === 404) {
    return {
      ok: true,
      status: 200,
      data: [],
      mensaje: "No hay suscripciones para mostrar.",
    };
  }

  return res;
};

export const buscar = (id) => {
  return request(`${BASE}/streaming/find/${id}`);
};

export const crear = (data) => {
  return request(`${BASE}/streaming/`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  });
};

export const actualizar = (id, data) => {
  return request(`${BASE}/streaming/update/${id}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  });
};

export const eliminar = (id) => {
  return request(`${BASE}/streaming/delete/${id}`, {
    method: "DELETE",
  });
};