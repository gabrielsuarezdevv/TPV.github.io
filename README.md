# TPV ( Terminal Punto de Venta para Hostelería ) - GABRIEL SUÁREZ ROMERO

Aplicación web de gestión de ventas desarrollada con **ASP.NET Core MVC**, orientada al sector de la hostelería y restauración. Permite gestionar productos por categorías, añadirlos a un ticket, aplicar descuentos y procesar el cobro de forma rápida e intuitiva.

---

## 🖥️ Demo

<img width="1867" height="906" alt="image" src="https://github.com/user-attachments/assets/233e0025-20c7-46e7-a8d2-8b9ea05a53b9" />


---

## ✨ Funcionalidades

- **Catálogo de productos** organizado por categorías: Embotellados, Entrantes, Carnes, Guisos, Pescado
- **Búsqueda de productos** en tiempo real
- **Ticket de venta** con unidades, descripción, precio unitario y total
- **Teclado numérico** integrado para introducir cantidades y precios manualmente
- **Descuentos** configurables por porcentaje (0%, 5%, 10%, 15%, 20%, 25%, 30%, 35%, 40%, 50%)
- **Función Aparcar** para pausar un ticket y atender otro
- **Apertura de cajón** portamonedas
- **Total a pagar** y botón de cobro
- **Gestión de productos y categorías** a través de base de datos

---

## 🛠️ Tecnologías utilizadas

| Capa | Tecnología |
|------|-----------|
| Backend | ASP.NET Core MVC (.NET 8) |
| Frontend | Razor Views, HTML5, CSS3, JavaScript |
| Base de datos | SQL Server / SQLite |
| ORM | Entity Framework Core |
| IDE | Visual Studio 2022 |

---

## 📁 Estructura del proyecto

```
tpv/
├── Controllers/        # Lógica de negocio y rutas MVC
├── Data/               # Contexto de base de datos (DbContext)
├── Migrations/         # Migraciones de Entity Framework
├── Models/             # Entidades: Producto, Categoría, LineaTicket...
├── Views/              # Vistas Razor (.cshtml)
├── wwwroot/            # Archivos estáticos (CSS, JS, imágenes)
├── appsettings.json    # Configuración de la aplicación
└── Program.cs          # Punto de entrada de la aplicación
```

---

## 🚀 Instalación y ejecución local

### Requisitos previos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- Visual Studio 2022 o VS Code con extensión C# Dev Kit
- SQL Server o SQLite (según configuración)

### Pasos

1. **Clona el repositorio**

```bash
git clone https://github.com/gabrielsuarezdevv/tpvmio.git
cd tpvmio
```

2. **Configura la cadena de conexión** en `appsettings.json`

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=tpv_db;Trusted_Connection=True;"
}
```

3. **Aplica las migraciones** para crear la base de datos

```bash
dotnet ef database update
```

4. **Ejecuta el proyecto**

```bash
dotnet run
```

5. Abre el navegador en `https://localhost:5001`

---


## 📌 Estado del proyecto

> ✅ Proyecto iniciado como parte de las prácticas del ciclo DAW (Desarrollo de Aplicaciones Web) con falta de algunas updates avanzadas como sistema de login de empleados, selector de turnos, etc.

---

## 👨‍💻 Autor

**Gabriel Suárez**
- Portfolio: [gabrielsuarezdevv.github.io](https://gabrielsuarezdevv.github.io)
- LinkedIn: [linkedin.com/in/gabrielsuarezdev](https://linkedin.com/in/gabrielsuarezdev)
- GitHub: [@gabrielsuarezdevv](https://github.com/gabrielsuarezdevv)
