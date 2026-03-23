# Prueba Técnica Akkodis

## 📌 Descripción

Este proyecto corresponde a la resolución de la prueba técnica, la cual se ha dividido en dos partes independientes:

* **Parte 1:** Aplicación de escritorio (WinForms)
* **Parte 2:** API REST (ASP.NET Core Web API)

Ambas partes se han desarrollado como ejercicios separados, siguiendo los requisitos indicados en el enunciado.

---

## 🧩 Parte 1 – Aplicación WinForms

Se ha desarrollado una aplicación de escritorio que permite la gestión de clientes.

### Funcionalidades principales:

* Visualización de clientes
* Alta de nuevos clientes
* Eliminación de clientes
* Importación de datos desde fichero

---

## 🌐 Parte 2 – Web API

Se ha desarrollado una API REST que permite gestionar clientes mediante endpoints HTTP.

### Endpoints implementados:

* `GET /database/clientes` → Obtiene todos los clientes
* `GET /database/clientes/{dni}` → Obtiene un cliente por DNI
* `POST /database/clientes` → Crea un nuevo cliente
* `DELETE /database/clientes/{dni}` → Elimina un cliente

---

## 📂 Gestión de datos

Siguiendo el enunciado, se ha optado por trabajar con ficheros en lugar de base de datos.

### Formatos utilizados:

* **CSV** → utilizado para importación de datos
* **JSON** → utilizado para almacenamiento persistente

---

## 📁 Carpeta Ejemplos

Se ha incluido una carpeta llamada `Ejemplos` que contiene:

* `clientes.csv` → fichero de ejemplo para importación
* `clientes.json` → fichero de ejemplo con datos

Estos archivos permiten probar fácilmente la funcionalidad de ambas aplicaciones
