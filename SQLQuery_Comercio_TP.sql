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
    Color        VARCHAR(30) NULL,
    Material     VARCHAR(50) NULL,
    PrecioBase   MONEY NOT NULL CHECK (PrecioBase >= 0),
    StockActual  INT NOT NULL CHECK (StockActual >= 0),
    StockMinimo  INT NOT NULL CHECK (StockMinimo >= 0)
);

-- Compras (sin Proveedores; se guarda el proveedor como texto)
CREATE TABLE Compras (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ProveedorNombre   VARCHAR(100) NOT NULL,
    ProveedorContacto VARCHAR(100) NULL,
    Fecha DATETIME NOT NULL
);

-- Detalle de Compras
CREATE TABLE CompraDetalle (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdCompra INT NOT NULL FOREIGN KEY REFERENCES Compras(Id),
    IdProducto INT NOT NULL FOREIGN KEY REFERENCES Productos(Id),
    Cantidad INT NOT NULL CHECK (Cantidad > 0),
    PrecioUnitario MONEY NOT NULL CHECK (PrecioUnitario >= 0)
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
INSERT INTO Productos (Nombre, Descripcion, IdCategoria, Talle, Color, Material, PrecioBase, StockActual, StockMinimo) VALUES
('Blusa "Solsticio"', 'Una blusa elegante y versátil con detalles bordados.', 1, 'M', 'Crema',   'Algodón',   25000, 50, 10),
('Vestido "Arena"',   'Perfecto para un día de verano, fresco y cómodo.',     2, 'L', 'Beige',   'Lino',      48000, 30, 5),
('Bolso "Nómada"',    'Accesorio artesanal que complementa cualquier look.',  3, 'M', 'Natural', 'Macramé',   32000, 20, 5),
('Pendientes "Liana"','Detalle final para un estilo bohemio y chic.',         4, 'M', 'Dorado',  'Metal',      8000, 100, 20),
('Vestido "Brisa"',   'Ligero y etéreo, ideal para ocasiones especiales.',    2, 'S', 'Blanco',  'Gasa',      62000, 15, 3);

-- Usuarios
INSERT INTO Usuarios (NombreUsuario, Contrasena, Rol) VALUES
('admin1', 'admin123', 'Administrador'),
('vendedor1', 'venta123', 'Vendedor'),
('cliente1', 'compra123', 'Cliente');

-- Compras (sin tabla Proveedores)
INSERT INTO Compras (ProveedorNombre, ProveedorContacto, Fecha) VALUES
('Textiles del Sur',   'Laura Martínez',  '2025-10-01'),
('Artesanos Andinos',  'Roberto Díaz',    '2025-10-05');

-- CompraDetalle
INSERT INTO CompraDetalle (IdCompra, IdProducto, Cantidad, PrecioUnitario) VALUES
(1, 1, 10, 15000),
(1, 4, 20,  4000),
(2, 2,  5, 35000),
(2, 5,  3, 42000);

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

-- Imagenes (URL vacía por ahora, una por producto)
INSERT INTO Imagenes (IdProducto, UrlImagen) VALUES
(1, 'https://lh3.googleusercontent.com/aida-public/AB6AXuCLddfPR4hF7HXWUDNQpdAEYypRgjRZh0M9VLcIqV1Zpz6lDdTzEBlbXKmLgfn98NnlK1tnO25L-gygHslKuZViJEpsnExMU1CvhdM7OCiDea3BkR3OEjMFCfJZFxgK6BKNmCM1pB9KIAo27wUde6CILvHn8O98XkhnQ06z2DsTsTk05g310beJdKM5GDI1puhZLTWCFGAZNm8NG8fph_vKJ6CK7kIX3xMbr0GHNTdNqMNAjPv2WMGi1juApeL70Bl8s3472D9QS_rk'),
(2, 'https://lh3.googleusercontent.com/aida-public/AB6AXuDow8Hsjsk-7pLj07IAMqGa2HWFasXSB2_lM8_5tywHJPAnqQAbJ2T2Ls6T83ZC-OIPO-3cOe7AmtVxlGbRBtvmKKxU2Cx8-9DcSH48STSWxIAAysFYACWC6bYvUHLuFZ6iJn22FaJ1ROODRgJhTsU2VWdbgsgbkuyUSPDZxKDlYr4vNQdMqMkPqvARe9lIi1v8xKrYC0XPK5nf49L6baQ55rcPvJs8b12NHii6O40rlpzxd9q0kUCX8oHbCqm9WBS7UdWvP-Gv3JAU'),
(3, 'https://lh3.googleusercontent.com/aida-public/AB6AXuCocRlxGHe-YwR6vQ_fZUgW7RpJP5CKHRttTI0g_72lIgkHmuDG7vQx9fLKsdlT61DG-3WWXPRDKt-HlQFeJfq4XG5q672_RWl79dmmcKcQZG2ue2mjl8T13YJPN2PfPvjRfzHyRPfEjof0k-CQNMkyMfo1l8ZtBLNzJJHN5rrlbyoiLs1AqLmDcCgUjxlq8cLiSRrm-5lLMfrLi1_-tWW_is1T_65Md1D5yGv66ie8wnbqFb3KF3NGkZu5OIx3Llnj61jrhQD5Q7Nd'),
(4, 'https://lh3.googleusercontent.com/aida-public/AB6AXuDnYC_ZysDXxgz2WfiSCmiP7J666NKTRC4Qacf1Q-Dfxw4Dgy6-uVU6i9L6ROnbDY7LyOkTmqRryLla-m8jQxpnd67lgdDZrS4_34M1AVjHsbTnbHBi2OTH5mMD360_3V8O3Psh6EJwvR0y8vzWtBcbQ9cQFgpLd_ASM-2bQfw0pPeKQn7TlqseUemom57JPskdJ870iaUAVLS2jHTlKvmVYDjlbPzQj_1xt01clogaYNLA86q00r49uUJSHNUm0rDqAO5HBYcIHbvA'),
(5, 'https://lh3.googleusercontent.com/aida-public/AB6AXuDZwckRThPhFbJArBgSWS3ZCdI9_oqEQGGgzjhpFJ3feWySuBw4fEy6fU6mJvBRH3jp73fYIcpIyckydBOGNIVk98b2lQZi0QCiP-o_87SEdVJGcL9ybANAI1Rp4hHHqGjVQYhmKs9cLCvWXuKjHy7dJf80guPLSonTV-p4I1ukvP0p0a-2812MHpZgiNaqOQKUo-W00HWxhBoOWprM0uDW1kGmsuk_XhrZFn3yCwCke-CDOaS0dZgiPhjikZKUZK2BmT7vfSz66e1r');