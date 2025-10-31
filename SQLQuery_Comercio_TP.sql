-- Crear la base de datos
CREATE DATABASE ComercioTP_DB;
GO
USE ComercioTP_DB;
GO

------------------------------------------------------------
-- Tablas base
------------------------------------------------------------

-- Clientes
CREATE TABLE Clientes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre   VARCHAR(100) NOT NULL,
    Email    VARCHAR(100),
    Telefono VARCHAR(20)
);

-- Categorías
CREATE TABLE Categorias (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL
);

-- Productos (sin Marcas; se agregan campos útiles)
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


-- Ventas
CREATE TABLE Ventas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdCliente INT NOT NULL FOREIGN KEY REFERENCES Clientes(Id),
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

-- Usuarios
CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    NombreUsuario VARCHAR(50) NOT NULL UNIQUE,
    Contrasena    VARCHAR(100) NOT NULL,
    Rol VARCHAR(20) NOT NULL CHECK (Rol IN ('Administrador','Vendedor','Cliente'))
);

-- Imagenes
CREATE TABLE Imagenes (
    IdImagen INT IDENTITY(1,1) PRIMARY KEY,
    IdProducto INT NOT NULL FOREIGN KEY REFERENCES Productos(Id),
    UrlImagen VARCHAR(255) NULL
);


-- 29/10
-- borre marcas, proveedores y pocentaje de ganancia en productos

------------------------------------------------------------
-- Inserts de ejemplo
------------------------------------------------------------

-- Clientes
INSERT INTO Clientes (Nombre, Email, Telefono) VALUES
('Juan Pérez', 'juanperez@mail.com', '1122334455'),
('Ana Gómez', 'ana.gomez@mail.com', '1133445566'),
('Carlos Ruiz', 'carlos.ruiz@mail.com', '1144556677');

-- Categorías (ropa y accesorios)
INSERT INTO Categorias (Nombre) VALUES
('Blusas'), ('Vestidos'), ('Bolsos'), ('Joyeria'), ('Accesorios');

-- Productos (sin marcas; con color, material y precio base)
INSERT INTO Productos (Nombre, Descripcion, IdCategoria, Talle, PrecioBase, StockActual, StockMinimo) VALUES
('Blusa "Solsticio"', 'Una blusa elegante y versátil con detalles bordados.', 1, 'M', 25000, 50, 10),
('Vestido "Arena"',   'Perfecto para un día de verano, fresco y cómodo.',     2, 'L', 48000, 30, 5),
('Bolso "Nómada"',    'Accesorio artesanal que complementa cualquier look.',  3, 'M', 32000, 20, 5),
('Pendientes "Liana"','Detalle final para un estilo bohemio y chic.',         4, 'M', 8000, 100, 20),
('Vestido "Brisa"',   'Ligero y etéreo, ideal para ocasiones especiales.',    2, 'S', 62000, 15, 3);

-- Usuarios
INSERT INTO Usuarios (NombreUsuario, Contrasena, Rol) VALUES
('admin1', 'admin123', 'Administrador'),
('vendedor1', 'venta123', 'Vendedor'),
('cliente1', 'compra123', 'Cliente');

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

-- Imagenes 
INSERT INTO Imagenes (IdProducto, UrlImagen) VALUES
(1, 'https://img.ltwebstatic.com/v4/j/pi/2025/04/15/ec/1744695942740c0ebd017afb104141f95abb9c156d_thumbnail_405x.webp'),
(2, 'https://http2.mlstatic.com/D_NQ_NP_913956-MLA84211306237_042025-O.webp'),
(3, 'https://img.ltwebstatic.com/images3_spmp/2025/03/12/03/1741764251df14f0c32747d76c9efe4505dd856929_thumbnail_405x.webp'),
(4, 'https://newswarovskiargentina.vtexassets.com/unsafe/1440x0/center/middle/https%3A%2F%2Fnewswarovskiargentina.vtexassets.com%2Farquivos%2Fids%2F653364%2Fpendientes-5705831-1.jpg%3Fv%3D638739523608530000'),
(5, 'https://img.ltwebstatic.com/v4/j/pi/2025/04/23/84/17453729703da87c9bc65401c1f4fc6168d7b0539a_thumbnail_560x.webp');

--30/10
--mas imagenes para el carrusel
INSERT INTO Imagenes (IdProducto, UrlImagen) VALUES
(1, 'https://img.ltwebstatic.com/v4/j/pi/2025/04/15/ec/1744695942740c0ebd017afb104141f95abb9c156d_thumbnail_405x.webp'),
(2, 'https://http2.mlstatic.com/D_NQ_NP_913956-MLA84211306237_042025-O.webp'),
(3, 'https://img.ltwebstatic.com/images3_spmp/2025/03/12/03/1741764251df14f0c32747d76c9efe4505dd856929_thumbnail_405x.webp'),
(4, 'https://newswarovskiargentina.vtexassets.com/unsafe/1440x0/center/middle/https%3A%2F%2Fnewswarovskiargentina.vtexassets.com%2Farquivos%2Fids%2F653364%2Fpendientes-5705831-1.jpg%3Fv%3D638739523608530000'),
(5, 'https://img.ltwebstatic.com/v4/j/pi/2025/04/23/84/17453729703da87c9bc65401c1f4fc6168d7b0539a_thumbnail_560x.webp');

