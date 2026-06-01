# 🏥 Sistema de Gestión de Citas Médicas

> Sistema de escritorio para la gestión integral de citas médicas, desarrollado en **C# .NET 8** con **Windows Forms**, siguiendo principios de arquitectura limpia (SOLID, DRY, KISS, YAGNI) y separación por capas.

---

## 📋 Tabla de Contenidos

- [Requisitos del Sistema](#-requisitos-del-sistema)
- [Instalación y Ejecución](#-instalación-y-ejecución)
- [Estructura del Proyecto](#-estructura-del-proyecto)
- [Guía de Uso](#-guía-de-uso)
  - [Paso 1 – Panel Principal](#paso-1--panel-principal)
  - [Paso 2 – Registrar Especialidades](#paso-2--registrar-especialidades)
  - [Paso 3 – Registrar Médicos](#paso-3--registrar-médicos)
  - [Paso 4 – Registrar Pacientes](#paso-4--registrar-pacientes)
  - [Paso 5 – Agendar una Cita](#paso-5--agendar-una-cita)
  - [Paso 6 – Consultar, Cancelar y Reprogramar Citas](#paso-6--consultar-cancelar-y-reprogramar-citas)
- [Reglas de Negocio](#-reglas-de-negocio)
- [Arquitectura y Principios](#-arquitectura-y-principios)

---

## 💻 Requisitos del Sistema

| Requisito | Versión mínima |
| :--- | :--- |
| Sistema Operativo | Windows 10 / Windows 11 |
| .NET SDK | 8.0 o superior |
| Visual Studio | 2022 (recomendado) |

---

## 🚀 Instalación y Ejecución

### Opción A — Desde Visual Studio

1. Clona el repositorio:
   ```bash
   git clone https://github.com/Enyer-06/SistemaCitasMedicas.git
   ```
2. Abre el archivo de solución `SistemaCitasMedicas.sln` en Visual Studio 2022.
3. Establece `SistemaCitasMedicas.UI` como proyecto de inicio.
4. Presiona **F5** para compilar y ejecutar.

### Opción B — Desde la terminal

```powershell
git clone https://github.com/Enyer-06/SistemaCitasMedicas.git
cd SistemaCitasMedicas\SistemaCitasMedicas
dotnet run --project SistemaCitasMedicas.UI\SistemaCitasMedicas.UI.csproj
```

---

## 📁 Estructura del Proyecto

```
SistemaCitasMedicas.UI/
│
├── Domain/                     ← Entidades y Enumeraciones
│   ├── Entities/               (Paciente, Medico, Especialidad, Cita)
│   └── Enums/                  (EstadoCita)
│
├── Application/                ← Lógica de Negocio
│   ├── Interfaces/             (IRepository<T>, ICitaRepository, INotificacionService, IValidador<T>...)
│   ├── DTOs/                   (CitaDTO, ResultadoValidacion, ResultadoOperacion)
│   ├── Validators/             (ValidadorBase, PacienteValidador, MedicoValidador, CitaValidador)
│   └── Services/               (PacienteService, MedicoService, EspecialidadService, CitaService)
│
├── Infrastructure/             ← Implementaciones Concretas
│   ├── Repositories/           (En memoria — preparado para migración a SQL)
│   └── Notifications/          (EmailNotificacionService)
│
├── Presentation/               ← Interfaz de Usuario (Windows Forms)
│   ├── Forms/                  (MainForm, EspecialidadForm, PacienteForm, MedicoForm, CitaForm, ConsultaCitaForm, ReprogramarCitaForm)
│   └── Helpers/                (MensajesUI)
│
└── Program.cs                  ← Composición de Dependencias (DI manual)
```

---

## 📖 Guía de Uso

### Paso 1 — Panel Principal

Al iniciar la aplicación, se muestra el **Panel Clínico Principal** con un menú lateral que da acceso a todos los módulos del sistema.

> 📸 _[Captura de pantalla: Vista del panel principal con el menú lateral y el mensaje de bienvenida]_

**Módulos disponibles en el menú lateral:**
- 🩺 **Especialidades** — Gestión del catálogo de especialidades médicas.
- 👤 **Pacientes** — Registro y consulta de pacientes.
- 👨‍⚕️ **Médicos** — Registro de médicos con asignación de especialidad.
- 📅 **Agendar Cita** — Programación de nuevas citas.
- 🔍 **Consultar Citas** — Búsqueda, cancelación y reprogramación de citas.

> [!IMPORTANT]
> Se recomienda seguir el orden del flujo de la guía: **Especialidades → Médicos → Pacientes → Citas**.

---

### Paso 2 — Registrar Especialidades

Antes de registrar médicos, es necesario configurar las especialidades disponibles en la clínica.

1. Haz clic en el botón **🩺 Especialidades** en el menú lateral.
2. En el formulario, ingresa el **nombre** de la especialidad (ej. `Cardiología`, `Pediatría`).
3. Opcionalmente, agrega una **descripción**.
4. Haz clic en **Registrar Especialidad**.

> 📸 _[Captura de pantalla: Formulario de especialidades con la lista de especialidades registradas]_

> [!NOTE]
> Los nombres de especialidades deben ser únicos. El sistema rechazará duplicados automáticamente.

---

### Paso 3 — Registrar Médicos

Con las especialidades registradas, puedes agregar los médicos de la clínica.

1. Haz clic en **👨‍⚕️ Médicos** en el menú lateral.
2. Completa los campos obligatorios:
   - **Nombre** y **Apellido** del médico.
   - **Especialidad** (selecciona de la lista desplegable).
3. Los campos de **Teléfono** y **Email** son opcionales.
4. Haz clic en **Registrar Médico**.

> 📸 _[Captura de pantalla: Formulario de médicos con el ComboBox de especialidades desplegado y la grilla de médicos registrados]_

> [!NOTE]
> Si aún no hay especialidades registradas, el sistema mostrará una advertencia al abrir este formulario.

---

### Paso 4 — Registrar Pacientes

Registra los pacientes de la clínica para poder asignarles citas.

1. Haz clic en **👤 Pacientes** en el menú lateral.
2. Completa todos los campos **obligatorios**:
   - **Nombre** y **Apellido**
   - **Teléfono** (solo dígitos, entre 7 y 15 caracteres)
   - **Email** (formato válido, ej. `juan@correo.com`)
   - **Fecha de Nacimiento** (debe ser una fecha en el pasado)
3. Haz clic en **Registrar Paciente**.

> 📸 _[Captura de pantalla: Formulario de pacientes con los campos llenados y la grilla con pacientes registrados]_

> [!NOTE]
> Todos los campos son obligatorios para los pacientes. El sistema validará el formato del email y el teléfono antes de guardar.

---

### Paso 5 — Agendar una Cita

Con pacientes y médicos registrados, puedes programar citas médicas.

1. Haz clic en **📅 Agendar Cita** en el menú lateral.
2. Selecciona el **Paciente** de la lista desplegable.
3. Selecciona el **Médico** de la lista desplegable.
4. Escoge la **Fecha** de la cita (debe ser una fecha futura).
5. Selecciona la **Hora** en el rango permitido (de `08:00` a `18:00`, en intervalos de 30 minutos).
6. Opcionalmente, escribe el **Motivo** de la consulta (máximo 500 caracteres).
7. Haz clic en **Confirmar Agendamiento**.

> 📸 _[Captura de pantalla: Formulario de agendamiento con paciente, médico, fecha y hora seleccionados]_

> [!IMPORTANT]
> El sistema **bloqueará automáticamente** citas si:
> - El **médico** ya tiene otra cita activa en la misma fecha y hora.
> - El **paciente** ya tiene otra cita activa en la misma fecha y hora.

---

### Paso 6 — Consultar, Cancelar y Reprogramar Citas

Gestiona las citas existentes desde el módulo de consulta.

1. Haz clic en **🔍 Consultar Citas** en el menú lateral.

#### Ver todas las citas

> 📸 _[Captura de pantalla: Listado de todas las citas activas con estados, médicos y pacientes]_

#### Filtrar por Paciente o Médico

- Selecciona un **Paciente** del combo y haz clic en **Filtrar** para ver solo sus citas.
- Selecciona un **Médico** del combo y haz clic en **Filtrar** para ver la agenda del médico.
- Haz clic en **Ver Todas** para limpiar los filtros.

> 📸 _[Captura de pantalla: Vista con las citas filtradas por un paciente específico]_

#### Cancelar una Cita

1. Selecciona la fila de la cita en la tabla.
2. Haz clic en **❌ Cancelar Cita**.
3. Confirma la cancelación en el diálogo que aparece.

> 📸 _[Captura de pantalla: Diálogo de confirmación de cancelación de cita]_

#### Reprogramar una Cita

1. Selecciona la fila de la cita en la tabla.
2. Haz clic en **🔄 Reprogramar Cita**.
3. En el formulario emergente, selecciona la **nueva fecha** y la **nueva hora**.
4. Haz clic en **Guardar**.

> 📸 _[Captura de pantalla: Formulario de reprogramación con la nueva fecha y hora seleccionadas]_

> [!NOTE]
> No es posible reprogramar citas que ya han sido canceladas. El sistema mostrará un aviso.

---

## 📏 Reglas de Negocio

| Regla | Descripción |
| :--- | :--- |
| Sin citas duplicadas (médico) | Un médico no puede tener dos citas activas en la misma fecha y hora |
| Sin citas duplicadas (paciente) | Un paciente no puede tener dos citas activas en la misma fecha y hora |
| Fechas futuras | Solo se pueden agendar citas para fechas posteriores al día de hoy |
| Horario laboral | Las citas deben estar dentro del rango `08:00 AM – 06:00 PM` |
| Unicidad de especialidades | No se permiten dos especialidades con el mismo nombre |
| No reprogramar citas canceladas | Una cita cancelada no puede ser reprogramada |

---

## 🏛️ Arquitectura y Principios

El sistema aplica los principios de diseño de software definidos en la especificación técnica:

| Principio | Cómo se aplica |
| :--- | :--- |
| **SoC** | Tres capas estrictamente separadas: Presentación, Negocio y Datos. Las notificaciones son un módulo transversal independiente. |
| **DRY** | `ValidadorBase` centraliza validaciones comunes (campos vacíos, email, teléfono, fechas futuras). `IRepository<T>` elimina el CRUD repetido. |
| **KISS** | Repositorios en memoria para V1.0. Sin frameworks innecesarios. Formularios simples y directos. |
| **YAGNI** | Sin facturación, recetas, historial clínico, seguros, telemedicina, chat ni IA en V1.0. |
| **SRP** | Cada clase tiene una sola responsabilidad: `CitaService` orquesta, `CitaValidador` valida, `EmailNotificacionService` notifica. |
| **OCP** | `INotificacionService` permite añadir SMS/WhatsApp sin modificar `CitaService`. |
| **LSP** | `SqlCitaRepository` podría reemplazar a `CitaRepository` sin romper ningún servicio. |
| **ISP** | Interfaces pequeñas: `INotificacionService` (1 método), `IValidador<T>` (1 método). |
| **DIP** | Los servicios reciben sus dependencias por constructor. Toda la composición se hace en `Program.cs`. |

---

## 📦 Tecnologías Utilizadas

- **Lenguaje:** C# 12
- **Framework:** .NET 8 — Windows Forms
- **Persistencia V1.0:** En memoria (`List<T>`) — preparado para migrar a SQL Server sin cambiar la capa de negocio
- **IDE:** Visual Studio 2022

---

## 🔮 Extensiones Futuras (V2.0)

| Extensión | Cambios necesarios |
| :--- | :--- |
| Migrar a SQL Server | Crear `SqlCitaRepository : ICitaRepository`. Cambiar una línea en `Program.cs`. |
| Agregar SMS | Crear `SmsNotificacionService : INotificacionService`. Cero cambios en `CitaService`. |
| Agregar WhatsApp | Crear `WhatsAppNotificacionService`. Cero cambios en servicios existentes. |
| Exportar citas a Excel | Crear `CitaExportService` con dependencia en `ICitaRepository`. |

---

<div align="center">
  <p><strong>Sistema de Gestión de Citas Médicas — V1.0</strong></p>
  <p>Desarrollado con principios de Arquitectura Limpia · C# .NET 8 · Windows Forms</p>
</div>
