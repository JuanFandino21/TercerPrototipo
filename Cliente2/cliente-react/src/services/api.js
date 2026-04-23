const URL = "http://localhost:31230/streaming";

const handleResponse = async (res) => {
  const text = await res.text();

  try {
    return JSON.parse(text);
  } catch {
    return text;
  }
};

export const crear = async (data) => {
  const res = await fetch(URL, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(data),
  });

  return handleResponse(res);
};

export const getAll = async () => {
  const res = await fetch(URL);
  return handleResponse(res);
};

export const buscar = async (id) => {
  const res = await fetch(`${URL}/${id}`);
  return handleResponse(res);
};

export const actualizar = async (id, data) => {
  const res = await fetch(`${URL}/${id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(data),
  });

  return handleResponse(res);
};

export const eliminar = async (id) => {
  const res = await fetch(`${URL}/${id}`, {
    method: "DELETE",
  });

  return handleResponse(res);
};