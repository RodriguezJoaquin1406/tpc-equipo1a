-- Crear la base de datos
CREATE DATABASE ComercioTP_DB;
GO
USE ComercioTP_DB;
GO

-- Tabla de Clientes
CREATE TABLE Clientes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Email VARCHAR(100),
    Telefono VARCHAR(20)
);

-- Tabla de Proveedores
CREATE TABLE Proveedores (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Contacto VARCHAR(100)
);

-- Tabla de Marcas
CREATE TABLE Marcas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL
);

-- Tabla de Categorías
CREATE TABLE Categorias (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL
);

-- Tabla de Productos
CREATE TABLE Productos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    IdMarca INT FOREIGN KEY REFERENCES Marcas(Id),
    IdCategoria INT FOREIGN KEY REFERENCES Categorias(Id),
    StockActual INT NOT NULL,
    StockMinimo INT NOT NULL,
    PorcentajeGanancia DECIMAL(5,2) NOT NULL
);

-- Tabla intermedia Producto-Proveedor (relación muchos a muchos)
CREATE TABLE ProductoProveedor (
    IdProducto INT FOREIGN KEY REFERENCES Productos(Id),
    IdProveedor INT FOREIGN KEY REFERENCES Proveedores(Id),
    PRIMARY KEY (IdProducto, IdProveedor)
);

-- Tabla de Compras
CREATE TABLE Compras (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdProveedor INT FOREIGN KEY REFERENCES Proveedores(Id),
    Fecha DATETIME NOT NULL
);

-- Detalle de Compras
CREATE TABLE CompraDetalle (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdCompra INT FOREIGN KEY REFERENCES Compras(Id),
    IdProducto INT FOREIGN KEY REFERENCES Productos(Id),
    Cantidad INT NOT NULL,
    PrecioUnitario MONEY NOT NULL
);

-- Tabla de Ventas
CREATE TABLE Ventas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdCliente INT FOREIGN KEY REFERENCES Clientes(Id),
    Fecha DATETIME NOT NULL,
    NumeroFactura VARCHAR(20) UNIQUE NOT NULL
);

-- Detalle de Ventas
CREATE TABLE VentaDetalle (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdVenta INT FOREIGN KEY REFERENCES Ventas(Id),
    IdProducto INT FOREIGN KEY REFERENCES Productos(Id),
    Cantidad INT NOT NULL,
    PrecioUnitario MONEY NOT NULL
);

-- Tabla de Usuarios
CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    NombreUsuario VARCHAR(50) NOT NULL UNIQUE,
    Contraseña VARCHAR(100) NOT NULL,
    Rol VARCHAR(20) NOT NULL CHECK (Rol IN ('Administrador', 'Vendedor', 'Cliente'))
);

---------------------------------------------Inserts 
-- Clientes
INSERT INTO Clientes (Nombre, Email, Telefono) VALUES
('Juan Pérez', 'juanperez@mail.com', '1122334455'),
('Ana Gómez', 'ana.gomez@mail.com', '1133445566'),
('Carlos Ruiz', 'carlos.ruiz@mail.com', '1144556677');

-- Proveedores
INSERT INTO Proveedores (Nombre, Contacto) VALUES
('Distribuidora Sur', 'Laura Martínez'),
('TechParts S.A.', 'Roberto Díaz'),
('ElectroMax', 'Sofía López');

-- Marcas
INSERT INTO Marcas (Nombre) VALUES
('Samsung'), ('Apple'), ('Sony'), ('Motorola'), ('LG');

-- Categorías
INSERT INTO Categorias (Nombre) VALUES
('Celulares'), ('Televisores'), ('Audio'), ('Accesorios'), ('Computación');

-- Productos
INSERT INTO Productos (Nombre, IdMarca, IdCategoria, StockActual, StockMinimo, PorcentajeGanancia) VALUES
('Galaxy S22', 1, 1, 50, 10, 25.00),
('iPhone 13', 2, 1, 30, 5, 30.00),
('Smart TV 55"', 3, 2, 20, 5, 20.00),
('Auriculares Bluetooth', 4, 3, 100, 20, 40.00),
('Notebook LG Gram', 5, 5, 15, 3, 35.00);

-- Producto-Proveedor (relaciones muchos a muchos)
INSERT INTO ProductoProveedor (IdProducto, IdProveedor) VALUES
(1, 1), (1, 2),
(2, 2),
(3, 3),
(4, 1), (4, 3),
(5, 2);

-- Usuarios
INSERT INTO Usuarios (NombreUsuario, Contraseña, Rol) VALUES
('admin1', 'admin123', 'Administrador'),
('vendedor1', 'venta123', 'Vendedor'),
('cliente1', 'compra123', 'Cliente');

-- Compras
INSERT INTO Compras (IdProveedor, Fecha) VALUES
(1, '2025-10-01'),
(2, '2025-10-05');

-- CompraDetalle
INSERT INTO CompraDetalle (IdCompra, IdProducto, Cantidad, PrecioUnitario) VALUES
(1, 1, 10, 50000),
(1, 4, 20, 3500),
(2, 2, 5, 65000),
(2, 5, 3, 120000);

-- Ventas
INSERT INTO Ventas (IdCliente, Fecha, NumeroFactura) VALUES
(1, '2025-10-10', 'F0001'),
(2, '2025-10-12', 'F0002');

-- VentaDetalle
INSERT INTO VentaDetalle (IdVenta, IdProducto, Cantidad, PrecioUnitario) VALUES
(1, 1, 2, 62500),
(1, 4, 5, 4900),
(2, 2, 1, 84500),
(2, 5, 1, 162000);
