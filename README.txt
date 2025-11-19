# TP Cuatrimestral - Comercio Multipropósito

Aplicación web para administrar compras y ventas de un negocio multipropósito.

---

## _________________________________________ Etapas del Proyecto _______________________________________________

### Etapa 1 - Modelo de Dominio y Pantallas (SE REVISARA EL 31/10)

Para la primera etapa del TPC se solicita:
- la arquitectura de clases (modelo de dominio),
- armado de pantallas de la aplicación (SIN funcionalidad, sólo ventanas, algunos controles y navegación) y
- lectura desde base de datos de al menos UNA entidad.

#### VAMOS REALIZANDO:

- Capa dominio ------------------------------------------------------------------- ✅ CREADO  
  tenía mal escrito el nombre (domio, ya lo corregí)

- Clases base: Cliente, Proveedor, Producto, Marca, Categoría, Usuario ----------- ✅ CREADO (REVISAR)  
- Clases funcionales: Compra, CompraDetalle, Venta, VentaDetalle ----------------- ✅ CREADO (REVISAR)

- Capa Negocio ------------------------------------------------------------------- ✅ CREADO  
  + AccesoDatos  
  + CategoriaNegocio  
  + ClienteNegocio  
  + CompraNegocio  
  + MarcaNegocio  
  + ProductoNegocio  
  + UsuarioNegocio  
  + VentaNegocio  

- FRONT / Páginas de Presentación  
  + Página Principal ------------------------------------------------------------- ✅ CREADO (re base)  
  + Página master page (`Main.Master`) con header y footer ----------------------- ✅ CREADO  
  + Página catálogo (`PaginasPublic/Catalogo.aspx`) ------------------------------ 🔄 FALTA BACKEND, VISUAL CASI LISTO  
    - Carrusel de imágenes ------------------------------------------------------- ✅ CREADO (con imágenes repetidas para probar)

---

### CAMBIOS POST DEVOLUCIÓN PARA CONVERTIR EN ECOMMERCE

- Base de datos nueva ------------------------------------------------------------ ✅ CREADO  
- Ventas pasa a ser Pedido (dominio y negocio) ----------------------------------- ✅ CREADO  
- Cliente eliminado, Usuario como clase principal -------------------------------- ✅ CREADO  
- Compras y CompraDetalle eliminadas --------------------------------------------- ✅ ELIMINADO  
- Dirección y Método de Pago ----------------------------------------------------- ✅ CREADO  

- Página Login ------------------------------------------------------------------- ✅ CREADO  
  - Valida correo y nombre de usuario -------------------------------------------- ✅ HECHO  
  - Inicia sesión y redirige a Inicio con sesión activa -------------------------- ✅ HECHO  
  - Menú dinámico en `Main.Master` según sesión ---------------------------------- ✅ HECHO  

- Página Registro ---------------------------------------------------------------- ✅ CREADO  
  - Alta de usuario -------------------------------------------------------------- ✅ HECHO  
  - Redirige a `Home.aspx` con sesión iniciada ----------------------------------- ✅ HECHO  

- Página Ubicaciones ------------------------------------------------------------- ✅ REALIZADO  
  - Mapa con ubicación ----------------------------------------------------------- ✅ REALIZADO  

- Página Inicio (`Inicio.aspx`) -------------------------------------------------- ✅ LIMPIADA  
  - Contenido visual puro, sin lógica de sesión ---------------------------------- ✅ HECHO  

- Página Perfil (`PaginasPrivadas/Home.aspx`) ------------------------------------ ✅ CREADO  
  - Muestra datos del usuario ---------------------------------------------------- ✅ HECHO  
  - Permite editar nombre, email y teléfono -------------------------------------- ✅ NUEVO  
  - Permite eliminar cuenta ------------------------------------------------------ ✅ NUEVO  
  - Mensaje de bienvenida si viene del registro ---------------------------------- ✅ NUEVO  

- UsuarioNegocio y UsuarioDatos -------------------------------------------------- ✅ AMPLIADO  
  - Método `Actualizar(Usuario)` ------------------------------------------------- ✅ NUEVO  
  - Método `Eliminar(int id)` ---------------------------------------------------- ✅ NUEVO  

---

### *FALTA* (para más adelante)

- Carrito de compras (lo dejamos para el final)
- Clase que finaliza la compra (pedido confirmado)

-- CORRECCIONES PENDIENTES SEGUNDA REVISION

- Hacer listado para administrador de productos------------------------------------ HECHO
(Se podria cambiar y hacer que se puedan modificar algunos campos sin tener que ir a otra pagina)
- Falta verificar Borrado de productos .------------------------------------------- HECHO Y VERIFICADO
- Paginas ABML de marca, categoria, y usuarios.
- Hacer una constante para tipo de usuario administrador. ------------------------- HECHO

*Pd no preocuparnos tanto por tema permisos, primero hacer que todo en la aplicacion funcione.
que todo se pueda cargar, modificar, listar y eliminar desde la pagina y luego ver tema permisos.

- ABML MARCA - Listado hecho, Eliminado No, Modificado NO, CargarNuevo NO.
- ABML USUARIOS - Listado HECHO, Eliminado NO, Modificado NO, ¿Cargar nuevo User admin? Hay que hacerlo
- Pagina Carrito, CODE BEHIND SI, Falta visual (aspx)






## __________________________________________ Etapa 2 - ABMs y Listados ________________________________________

Para la segunda etapa del TPC se solicita:
- completar y corregir las cuestiones pendientes de la primera etapa,
- desarrollar todos los ABMs y listados de las entidades administrables de la aplicación,  
  pero NO de las que correspondan a funcionalidad core (Turnos, Incidencia, Venta o Mesas)

#### VAMOS REALIZANDO:

- ABM de Usuario (modificación y eliminación desde perfil) ------------------------ ✅ CREADO
- Validaciones en registro y login ------------------------------------------------ ✅ HECHO
- Redirecciones y control de sesión en páginas privadas --------------------------- ✅ HECHO

- ABM de Usuario (agregar, modificar, eliminar) ----------------------------------- ✅ CREADO
- ABM de Producto (listar, buscar, agregar, modificar, eliminar) ------------------ ✅ CREADO
- ABM de Categoría (listar, agregar, modificar, eliminar) ------------------------- ✅ CREADO
- ABM de Método de Pago (listar, agregar, modificar, eliminar) -------------------- ✅ CREADO
- ABM de Dirección (listar, agregar, modificar, eliminar) ------------------------- ✅ CREADO

- Campo Número agregado a la tabla Direcciones (SQL no estaba el varchar)---------- ✅ HECHO


🔜 Próximo paso: funcionalidad core (carrito, pedido, confirmación de compra)

---

## ___________________________________________ Etapa 3 - Funcionalidad Core ____________________________________

Para la tercera etapa del TPC deberán construir:
- La funcionalidad del core de la aplicación (turnos: nuevo, re programar; mesa: abrir, generar pedido, cerrar; ventas: comprar, vender, etc.).
- Las funcionalidades que aportan valor de agregado al core (búsquedas dinámicas, registrarse, olvidé mi pass, etc.).
- Validaciones a lo largo de la aplicación: tipos de datos, requeridos, formatos.

#### VAMOS REALIZANDO:

🔜 Próximo objetivo: carrito de compras y finalización de pedido

---

## ___________________________________________ Etapa Final - Seguridad y Optimización __________________________

Para la cuarta y última etapa del TPC deberán:
- Cerrar toda la funcionalidad pendiente (abms y filtrados en todas las entidades).
- Validaciones a lo largo de toda la aplicación.
- Seguridad y perfiles de usuario, con la funcionalidad correspondiente para cada perfil.
- Optimización del diseño visual de cara al usuario (colorcitos, botoncitos lindos, etc.).

#### VAMOS REALIZANDO:

🔜 A definir luego de completar el carrito y pedidos

---

## Tecnologías utilizadas

- ASP.NET Web Forms  
- WinForms  
- C#  
- SQL Server  
- Git + GitHub  
- [Google Stitch](https://stitch.withgoogle.com)

---
