# Tekus Providers

Este proyecto es un Backend RESTful desarrollado con **.NET 8**, un frontend con **Angular 19** y , enfocado en la escalabilidad, mantenibilidad y la aplicación de la **Arquitectura Limpia**.

---

## Stack Tecnológico

* **Back-End Framework:** .NET 8 (C#)
* **ORM:** Entity Framework Core (Code First)
* **Front-End Framework:** Angular 19
* **Base de Datos:** SQL Server
* **Mapeo de Objetos:** AutoMapper
* **Validación:** FluentValidation
* **Documentación:** Swagger / OpenAPI

---

### 1. Clean Architecture
El código está dividido en capas para separar las responsabilidades:
* **Domain:** Entidades.
* **Application:** Casos de uso, interfaces, DTOs y validaciones.
* **Infrastructure:** Implementación de acceso a datos (EF Core), repositorios y servicios externos.
* **API:** Controladores y puntos de entrada.

### 2. Repository Pattern & Unit of Work
Se implementó el patrón **Repositorio** para abstraer la lógica de acceso a datos y el patrón **Unit of Work**, asegurando que múltiples operaciones de base de datos se completen exitosamente o fallen en conjunto.

### 3. Respuestas Genéricas
Todos los endpoints de la API retornan una estructura de respuesta estandarizada para facilitar el consumo por parte del cliente (Frontend).

**Estructura JSON:**
```json
{
  "header": {
    "responseCode": 200,
    "message": "Operación exitosa",
    "success": true
  },
  "data": {
    "id": "guid-...",
    "name": "Ejemplo"
  }
}
```

### 4. Estado Actual y Notas

El proyecto actualmente cuenta con las funcionalidades principales de CRUD, aunque requiere su ajuste para incluir algunas popiedades adicionales. El código entregado es funcional y compila correctamente.

* **Cobertura de Pruebas Unitarias:** Implementar los test dentro del proyecto Tekus.Providers.Test, aplicando el patrón AAA(Arrange, Act, Assert) haciendo uso de las librerías xUnit y Moq.
* **Frontend/Backend:** Finalizar la administración de proveedores a través del CRUD, con el fin de incluir los campos personalizados y asignación de paises.
Los campos personalizados se manejaran como un Dictionary, con un valor de tipo generico y se almacenarán en base de datos como objetos JSON.
Los paises se manejaran a través de un array de strings, los cuales se almacenarán en base de datos como objetos JSON.
