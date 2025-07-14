# Documentación de Arquitectura, Patrones de Diseño y Seguridad

## Arquitectura

Implementa una arquitectura basada en microservicios, compuesta por servicios independientes: Products.Api, Sales.Api, Movement.Api, Purchase.Api`y Auth.Api. 
Cada microservicio está estructurado por capas con su propio dominio, infraestructura, repositorios y modelos.

- **Capas principales en cada microservicio:**
  - **Api:** Puntos de entrada HTTP (Web API).
  - **Domain services:** Lógica de negocio y modelos del dominio.
  - **Infrastructura:** Configuración de bases de datos, migraciones y acceso a datos.
  - **Repositorios:** Implementación de acceso a datos usando patrones de repositorio y Unit of Work.
  - **Services:** Validación de Dtos de entrada y coordina las operaciones con el repositorio y lógica de negocio 

- **Persistencia:** Se utiliza Entity Framework Core como ORM para la persistencia en bases de datos, con migraciones y contextos separados por dominio.
- **Comunicación:** Algunos servicios (por ejemplo, Products y Sales) se comunican vía HTTP usando `AddHttpClient` para acceder a otros microservicios (como Movement).

## Patrones de Diseño

1. **Repository Pattern:**
   - Clases como `MovementRepositories`, `ProductRepositories` y `SalesRepositories` heredan de `EntityFrameworkGenericRepository`, lo que permite la reutilización y abstracción de la lógica de acceso a datos.
   - Interfaces como `IMovementRepositories` y `IProductRepositories` definen contratos claros para los repositorios.
   
2. **Unit of Work:**
   - Interfaces como `IDomainApplicationUnitOfWork` (referenciada en Sales) permiten agrupar múltiples operaciones de repositorio bajo una misma transacción.

3. **Dependency Injection(Patron singleton):**
   - Todos los servicios, repositorios y validadores se registran en el contenedor de DI de .NET mediante `AddScoped`, `AddSingleton`, etc.

4. **DTOs y Separación de Modelos:**
   - Uso de DTOs para exponer datos a través de las APIs, desacoplando los modelos de dominio de los modelos expuestos externamente.

5. **Validación de Argumentos:**
   - Uso de extensiones como `ValidateArgumentOrThrow` para validar argumentos y lanzar excepciones si los datos no son válidos.

## Seguridad

1. **Autenticación y Autorización JWT:**
   - Todos los microservicios principales (`Products.Api`, `Sales.Api`, `Auth.Api`, `Purchase.Api`) configuran autenticación JWT.

2. **CORS (Cross-Origin Resource Sharing):**
   - Políticas CORS que habilitan únicamente orígenes confiables como el frontend (`http://localhost:3000`), restringiendo métodos y encabezados permitidos.

3. **Validación y protección de datos:**
   - Uso de FluentValidation y validadores personalizados para asegurar la integridad de los datos recibidos en las APIs.
   - Validación explícita de argumentos en métodos de repositorio, previniendo ataques comunes por datos maliciosos o nulos.

4. **Separación de Contextos y Acceso a Datos:**
   - Cada microservicio tiene su propio `ApplicationDbContext`, lo que limita el alcance y visibilidad de los datos e impide fugas entre dominios.
