-- Crear la base de datos
CREATE DATABASE ComercioTP_DB;
GO
USE ComercioTP_DB;
GO

------------------------------------------------------------
-- Tablas base
------------------------------------------------------------

-- Usuarios
CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    NombreUsuario VARCHAR(50) NOT NULL UNIQUE,
    Contrasena    VARCHAR(100) NOT NULL,
    Rol           VARCHAR(20) NOT NULL CHECK (Rol IN ('Administrador','Vendedor','Cliente')),
    Nombre        VARCHAR(100) NOT NULL,
    Email         VARCHAR(100),
    Telefono      VARCHAR(20)
);

-- Categorías
CREATE TABLE Categorias (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL
);

-- Productos
CREATE TABLE Productos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre       VARCHAR(100) NOT NULL,
    Descripcion  VARCHAR(255) NOT NULL,
    IdCategoria  INT NOT NULL FOREIGN KEY REFERENCES Categorias(Id),
    Talle        VARCHAR(10) NOT NULL CHECK (Talle IN ('S','M','L','XL')),
    PrecioBase   MONEY NOT NULL CHECK (PrecioBase >= 0),
    StockActual  INT NOT NULL CHECK (StockActual >= 0),
    StockMinimo  INT NOT NULL CHECK (StockMinimo >= 0)
);

-- Imagenes
CREATE TABLE Imagenes (
    IdImagen INT IDENTITY(1,1) PRIMARY KEY,
    IdProducto INT NOT NULL FOREIGN KEY REFERENCES Productos(Id),
    UrlImagen VARCHAR(255) NULL
);

-- Ventas
CREATE TABLE Ventas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdCliente INT NOT NULL FOREIGN KEY REFERENCES Usuarios(Id),
    Fecha DATETIME NOT NULL,
    NumeroFactura VARCHAR(20) UNIQUE NOT NULL
);

-- Detalle de Ventas
CREATE TABLE VentaDetalle (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdVenta INT NOT NULL FOREIGN KEY REFERENCES Ventas(Id),
    IdProducto INT NOT NULL FOREIGN KEY REFERENCES Productos(Id),
    Cantidad INT NOT NULL CHECK (Cantidad > 0),
    PrecioUnitario MONEY NOT NULL CHECK (PrecioUnitario >= 0)
);

------------------------------------------------------------
-- Nuevas tablas para e-commerce
------------------------------------------------------------

-- Carrito
CREATE TABLE Carrito (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT NOT NULL FOREIGN KEY REFERENCES Usuarios(Id),
    IdProducto INT NOT NULL FOREIGN KEY REFERENCES Productos(Id),
    Cantidad INT NOT NULL CHECK (Cantidad > 0),
    Talle VARCHAR(10) NOT NULL DEFAULT 'Único'
);

-- Direcciones
CREATE TABLE Direcciones (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT NOT NULL FOREIGN KEY REFERENCES Usuarios(Id),
    Calle VARCHAR(100),
    Numero VARCHAR(50),
    Ciudad VARCHAR(50),
    CodigoPostal VARCHAR(10),
    Provincia VARCHAR(50)
);

-- Métodos de pago
CREATE TABLE MetodosPago (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL
);

-- Pedidos (ESTRUCTURA FINAL CON TUS CAMBIOS APLICADOS)
CREATE TABLE Pedidos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT NOT NULL FOREIGN KEY REFERENCES Usuarios(Id),
    Fecha DATETIME NOT NULL,
    Estado VARCHAR(20) NOT NULL CHECK (Estado IN ('Pendiente','Pagado','Enviado','Cancelado')),
    
    -- IdDireccion ahora es NULLABLE y tiene ON DELETE SET NULL
    IdDireccion INT NULL,
    CONSTRAINT FK_Pedidos_IdDireccion FOREIGN KEY (IdDireccion) REFERENCES Direcciones(Id) ON DELETE SET NULL,
    
    IdMetodoPago INT FOREIGN KEY REFERENCES MetodosPago(Id),
    Total MONEY NOT NULL CHECK (Total >= 0),

    -- Columnas Históricas de Dirección (Tus nuevos campos)
    DireccionCalle VARCHAR(100),
    DireccionNumero VARCHAR(20),
    DireccionCiudad VARCHAR(50),
    DireccionCodigoPostal VARCHAR(20),
    DireccionProvincia VARCHAR(50)
);

-- Detalle del pedido
CREATE TABLE DetallePedido (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdPedido INT NOT NULL FOREIGN KEY REFERENCES Pedidos(Id),
    IdProducto INT NOT NULL FOREIGN KEY REFERENCES Productos(Id),
    Cantidad INT NOT NULL CHECK (Cantidad > 0),
    PrecioUnitario MONEY NOT NULL CHECK (PrecioUnitario >= 0),
    Talle VARCHAR(10) NOT NULL DEFAULT 'Único'
);

------------------------------------------------------------
-- Inserts de ejemplo
------------------------------------------------------------

-- Categorías
INSERT INTO Categorias (Nombre) VALUES
('Blusas'), ('Vestidos'), ('Bolsos'), ('Joyeria'), ('Accesorios'), ('Abrigos');

-- Productos
INSERT INTO Productos (Nombre, Descripcion, IdCategoria, Talle, PrecioBase, StockActual, StockMinimo) VALUES
('Blusa "Solsticio"', 'Una blusa elegante y versátil con detalles bordados.', 1, 'M', 25000, 50, 10),
('Vestido "Arena"',   'Perfecto para un día de verano, fresco y cómodo.',     2, 'L', 48000, 30, 5),
('Bolso "Nómada"',    'Accesorio artesanal que complementa cualquier look.',  3, 'M', 32000, 20, 5),
('Pendientes "Liana"','Detalle final para un estilo bohemio y chic.',         4, 'M', 8000, 100, 20),
('Vestido "Brisa"',   'Ligero y etéreo, ideal para ocasiones especiales.',    2, 'S', 62000, 15, 3),
('Saco "Primaveral"', 'Ligeramente abrigado perfecto para ir dejando atras el invierno..',    6, 'S', 42500, 25, 5),
('Sueter "Ligero"', 'Buen sueter..',    6, 'M', 52500, 25, 5);

-- Usuarios
INSERT INTO Usuarios (NombreUsuario, Contrasena, Rol, Nombre, Email, Telefono) VALUES
('admin', 'admin', 'Administrador', 'Admin Admin', 'admin@mail.com', '1122334455'),
('juanperez', 'clave123', 'Cliente', 'Juan Pérez', 'juanperez@mail.com', '1122334455'),
('anagomez', 'clave123', 'Cliente', 'Ana Gómez', 'ana.gomez@mail.com', '1133445566'),
('carlosruiz', 'clave123', 'Cliente', 'Carlos Ruiz', 'carlos.ruiz@mail.com', '1144556677');

-- Métodos de pago
INSERT INTO MetodosPago (Nombre) VALUES
('Transferencia Bancaria'),
('Tarjeta de Crédito'),
('Tarjeta de Débito'),
('Mercado Pago'),
('Efectivo en punto de entrega');

-- Direcciones
INSERT INTO Direcciones (IdUsuario, Calle, Numero, Ciudad, CodigoPostal, Provincia) VALUES
(2, 'Av. Siempre Viva', '123', 'Springfield', '1900', 'Buenos Aires'),
(3, 'Calle Falsa', '456', 'La Plata', '7000', 'Buenos Aires'),
(4, 'Ruta 90 km', '34', 'Rosario', '2000', 'Santa Fe'),
(2, 'Av. Libertador', '789', 'Zárate', '2800', 'Buenos Aires'),
(3, 'Calle Mayo', '654', 'Cordoba', '5000', 'Cordoba');

-- Ventas
INSERT INTO Ventas (IdCliente, Fecha, NumeroFactura) VALUES
(2, '2025-10-10', 'F0001'),
(3, '2025-10-12', 'F0002');

-- VentaDetalle
INSERT INTO VentaDetalle (IdVenta, IdProducto, Cantidad, PrecioUnitario) VALUES
(1, 1, 2,  28000),
(1, 4, 5,   9000),
(2, 2, 1,  52000),
(2, 5, 1,  75000);

-- Carrito
INSERT INTO Carrito (IdUsuario, IdProducto, Cantidad) VALUES
(2, 1, 3),
(3, 2, 1),
(4, 3, 2),
(2, 4, 4),
(3, 5, 1);

-- Pedidos (Actualizado con los campos de dirección histórica)
INSERT INTO Pedidos (IdUsuario, Fecha, Estado, IdDireccion, IdMetodoPago, Total, DireccionCalle, DireccionNumero, DireccionCiudad, DireccionCodigoPostal, DireccionProvincia) VALUES
(2, '2025-11-01', 'Pagado', 1, 2, 120000, 'Av. Siempre Viva', '123', 'Springfield', '1900', 'Buenos Aires'),
(3, '2025-11-05', 'Pendiente', 2, 1, 52000, 'Calle Falsa', '456', 'La Plata', '7000', 'Buenos Aires'),
(4, '2025-11-07', 'Enviado', 3, 4, 64000, 'Ruta 90 km', '34', 'Rosario', '2000', 'Santa Fe'),
(2, '2025-11-10', 'Cancelado', 4, 5, 32000, 'Av. Libertador', '789', 'Zárate', '2800', 'Buenos Aires'),
(3, '2025-11-12', 'Pagado', 5, 3, 48000, 'Calle Mayo', '654', 'Cordoba', '5000', 'Cordoba');

-- DetallePedido
INSERT INTO DetallePedido (IdPedido, IdProducto, Cantidad, PrecioUnitario) VALUES
(1, 1, 4, 25000),
(1, 3, 2, 32000),
(2, 2, 1, 48000),
(3, 4, 3, 8000),
(5, 5, 1, 62000);

-- Imagenes (Consolidado y corregido)
INSERT INTO Imagenes (IdProducto, UrlImagen) VALUES
(1, 'https://img.ltwebstatic.com/v4/j/pi/2025/04/15/ec/1744695942740c0ebd017afb104141f95abb9c156d_thumbnail_405x.webp'),
(2, 'https://http2.mlstatic.com/D_NQ_NP_913956-MLA84211306237_042025-O.webp'),
(3, 'https://img.ltwebstatic.com/images3_spmp/2025/03/12/03/1741764251df14f0c32747d76c9efe4505dd856929_thumbnail_405x.webp'),
(4, 'https://newswarovskiargentina.vtexassets.com/unsafe/1440x0/center/middle/https%3A%2F%2Fnewswarovskiargentina.vtexassets.com%2Farquivos%2Fids%2F653364%2Fpendientes-5705831-1.jpg%3Fv%3D638739523608530000'),
(5, 'https://img.ltwebstatic.com/v4/j/pi/2025/04/23/84/17453729703da87c9bc65401c1f4fc6168d7b0539a_thumbnail_560x.webp'),
(6, 'https://i.pinimg.com/1200x/16/7e/c5/167ec54de1eb97376ccda3bf34e6a3ce.jpg'),
(7, 'https://i.pinimg.com/736x/1f/56/6b/1f566b32c4fa0d73331b5148a21d6d7d.jpg'),
-- Segundas imágenes (Variaciones)
(1, 'https://i.pinimg.com/736x/4b/46/5c/4b465c16ae630ef26dc96e6f9460826e.jpg'),
(2, 'https://i.pinimg.com/1200x/ab/ab/0e/abab0e96ac0ce40f91da54d5629e2d09.jpg'),
(3, 'https://i.pinimg.com/1200x/2f/d7/69/2fd769462b00d7f9b410baaf9711c542.jpg'),
(4, 'https://i.pinimg.com/736x/30/be/42/30be42beb6248717f72d30e38ae75e19.jpg'),
(5, 'https://i.pinimg.com/736x/35/ce/04/35ce047e1224f17d95f92be262d165bf.jpg'),
(6, 'https://i.pinimg.com/1200x/4c/8c/4a/4c8c4ae97542465607123a4ad68839ce.jpg'),
(7, 'https://i.pinimg.com/736x/c9/98/c6/c998c685fc3b9dd27f9a56c68a82cb38.jpg');