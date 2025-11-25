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
    Cantidad INT NOT NULL CHECK (Cantidad > 0)
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

-- Pedidos
CREATE TABLE Pedidos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT NOT NULL FOREIGN KEY REFERENCES Usuarios(Id),
    Fecha DATETIME NOT NULL,
    Estado VARCHAR(20) NOT NULL CHECK (Estado IN ('Pendiente','Pagado','Enviado','Cancelado')),
    IdDireccion INT FOREIGN KEY REFERENCES Direcciones(Id),
    IdMetodoPago INT FOREIGN KEY REFERENCES MetodosPago(Id),
    Total MONEY NOT NULL CHECK (Total >= 0)
);

-- Detalle del pedido
CREATE TABLE DetallePedido (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdPedido INT NOT NULL FOREIGN KEY REFERENCES Pedidos(Id),
    IdProducto INT NOT NULL FOREIGN KEY REFERENCES Productos(Id),
    Cantidad INT NOT NULL CHECK (Cantidad > 0),
    PrecioUnitario MONEY NOT NULL CHECK (PrecioUnitario >= 0)
);

------------------------------------------------------------
-- Inserts de ejemplo
------------------------------------------------------------


-- Categorías
INSERT INTO Categorias (Nombre) VALUES
('Blusas'), ('Vestidos'), ('Bolsos'), ('Joyeria'), ('Accesorios');

-- Productos
INSERT INTO Productos (Nombre, Descripcion, IdCategoria, Talle, PrecioBase, StockActual, StockMinimo) VALUES
('Blusa "Solsticio"', 'Una blusa elegante y versátil con detalles bordados.', 1, 'M', 25000, 50, 10),
('Vestido "Arena"',   'Perfecto para un día de verano, fresco y cómodo.',     2, 'L', 48000, 30, 5),
('Bolso "Nómada"',    'Accesorio artesanal que complementa cualquier look.',  3, 'M', 32000, 20, 5),
('Pendientes "Liana"','Detalle final para un estilo bohemio y chic.',         4, 'M', 8000, 100, 20),
('Vestido "Brisa"',   'Ligero y etéreo, ideal para ocasiones especiales.',    2, 'S', 62000, 15, 3);

-- Usuarios
INSERT INTO Usuarios (NombreUsuario, Contrasena, Rol, Nombre, Email, Telefono) VALUES
('admin', 'admin', 'Administrador', 'Admin Admin', 'admin@mail.com', '1122334455'),
('juanperez', 'clave123', 'Cliente', 'Juan Pérez', 'juanperez@mail.com', '1122334455'),
('anagomez', 'clave123', 'Cliente', 'Ana Gómez', 'ana.gomez@mail.com', '1133445566'),
('carlosruiz', 'clave123', 'Cliente', 'Carlos Ruiz', 'carlos.ruiz@mail.com', '1144556677');


-- Ventas
INSERT INTO Ventas (IdCliente, Fecha, NumeroFactura) VALUES
(1, '2025-10-10', 'F0001'),
(2, '2025-10-12', 'F0002');

-- VentaDetalle
INSERT INTO VentaDetalle (IdVenta, IdProducto, Cantidad, PrecioUnitario) VALUES
(1, 1, 2,  28000),
(1, 4, 5,   9000),
(2, 2, 1,  52000),
(2, 5, 1,  75000);

-- Imagenes (duplicadas para carrusel)
INSERT INTO Imagenes (IdProducto, UrlImagen) VALUES
(1, 'https://img.ltwebstatic.com/v4/j/pi/2025/04/15/ec/1744695942740c0ebd017afb104141f95abb9c156d_thumbnail_405x.webp'),
(2, 'https://http2.mlstatic.com/D_NQ_NP_913956-MLA84211306237_042025-O.webp'),
(3, 'https://img.ltwebstatic.com/images3_spmp/2025/03/12/03/1741764251df14f0c32747d76c9efe4505dd856929_thumbnail_405x.webp'),
(4, 'https://newswarovskiargentina.vtexassets.com/unsafe/1440x0/center/middle/https%3A%2F%2Fnewswarovskiargentina.vtexassets.com%2Farquivos%2Fids%2F653364%2Fpendientes-5705831-1.jpg%3Fv%3D638739523608530000'),
(5, 'https://img.ltwebstatic.com/v4/j/pi/2025/04/23/84/17453729703da87c9bc65401c1f4fc6168d7b0539a_thumbnail_560x.webp'),
(1, 'https://img.ltwebstatic.com/v4/j/pi/2025/04/15/ec/1744695942740c0ebd017afb104141f95abb9c156d_thumbnail_405x.webp'),
(2, 'https://http2.mlstatic.com/D_NQ_NP_913956-MLA84211306237_042025-O.webp'),
(3, 'https://img.ltwebstatic.com/images3_spmp/2025/03/12/03/1741764251df14f0c32747d76c9efe4505dd856929_thumbnail_405x.webp'),
(4, 'https://newswarovskiargentina.vtexassets.com/unsafe/1440x0/center/middle/https%3A%2F%2Fnewswarovskiargentina.vtexassets.com%2Farquivos%2Fids%2F653364%2Fpendientes-5705831-1.jpg%3Fv%3D638739523608530000'),
(5, 'https://img.ltwebstatic.com/v4/j/pi/2025/04/23/84/17453729703da87c9bc65401c1f4fc6168d7b0539a_thumbnail_560x.webp');

INSERT INTO Carrito (IdUsuario, IdProducto, Cantidad) VALUES
(1, 1, 3),    -- Juan Pérez tiene 3 Blusas en su carrito.
(2, 2, 1),    -- Ana Gómez agregó 1 Vestido al carrito.
(3, 3, 2),    -- Carlos Ruiz quiso 2 Bolsos.
(1, 4, 4),    -- Juan Pérez añadió 4 Pendientes.
(2, 5, 1);    -- Ana Gómez seleccionó 1 Vestido adicional.

-- Direcciones (se relaciona con Usuarios)
INSERT INTO Direcciones (IdUsuario, Calle, Ciudad, CodigoPostal, Provincia) VALUES
(1, 'Av. Siempre Viva 123', 'Springfield', '1900', 'Buenos Aires'),
(2, 'Calle Falsa 456', 'La Plata', '7000', 'Buenos Aires'),
(3, 'Ruta 90 km 34', 'Rosario', '2000', 'Santa Fe'),
(1, 'Av. Libertador 789', 'Zárate', '2800', 'Buenos Aires'),
(2, 'Calle Mayo 654', 'Cordoba', '5000', 'Cordoba');

-- Pedidos (se relaciona con Usuarios, Direcciones y Métodos de pago)
INSERT INTO Pedidos (IdUsuario, Fecha, Estado, IdDireccion, IdMetodoPago, Total) VALUES
(1, '2025-11-01', 'Pagado', 1, 2, 120000),  -- Pedido pagado por Juan Pérez
(2, '2025-11-05', 'Pendiente', 2, 1, 52000), -- Pedido en espera de Ana Gómez
(3, '2025-11-07', 'Enviado', 3, 4, 64000),  -- Carlos Ruiz hizo un pedido enviado con Mercado Pago.
(1, '2025-11-10', 'Cancelado', 4, 5, 32000), -- Un pedido cancelado de Juan Pérez.
(2, '2025-11-12', 'Pagado', 5, 3, 48000);  -- Pedido con T. Debito por Ana Gómez.

-- DetallePedido (se relaciona con Pedidos y Productos)
INSERT INTO DetallePedido (IdPedido, IdProducto, Cantidad, PrecioUnitario) VALUES
(1, 1, 4, 25000),  -- Juan Pérez compró 4 Blusas.
(1, 3, 2, 32000),  -- Añadió 2 Bolsos.
(2, 2, 1, 48000),  -- Ana Gómez compró un Vestido.
(3, 4, 3, 8000),  -- Carlos Ruiz obtuvo 3 Pendientes Liana.
(5, 5, 1, 62000);  -- Ana Gómez compró el Vestido Brisa.

-- Métodos de pago
INSERT INTO MetodosPago (Nombre) VALUES
('Transferencia Bancaria'),
('Tarjeta de Crédito'),
('Tarjeta de Débito'),
('Mercado Pago'),
('Efectivo en punto de entrega');


