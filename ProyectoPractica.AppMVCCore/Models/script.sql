-- Creaci�n de la base de datos (Creaci�n de la base de datos)
CREATE DATABASE TiendaLibrosOnlineDB;
GO

USE TiendaLibrosOnlineDB;
GO

-- Tabla: Autores
CREATE TABLE Autores (
    Id INT PRIMARY KEY IDENTITY(1,1), -- ID del autor
    Nombre VARCHAR(255) NOT NULL,      -- Nombre del autor
    Biografia TEXT                     -- Biograf�a del autor
);
GO

-- Tabla: Categorias
CREATE TABLE Categorias (
    Id INT PRIMARY KEY IDENTITY(1,1), -- ID de la categor�a
    Nombre VARCHAR(100) NOT NULL UNIQUE -- Nombre de la categor�a (debe ser �nico)
);
GO

-- Tabla: Libros
CREATE TABLE Libros (
    Id INT PRIMARY KEY IDENTITY(1,1),     -- ID del libro
    Titulo VARCHAR(255) NOT NULL,         -- T�tulo del libro
    ISBN VARCHAR(20) UNIQUE NOT NULL,    -- ISBN del libro (debe ser �nico)
    Descripcion TEXT,                    -- Descripci�n del libro
    Precio DECIMAL(10, 2) NOT NULL,       -- Precio del libro
    AutorId INT FOREIGN KEY REFERENCES Autores(Id),   -- ID del autor
    CategoriaId INT FOREIGN KEY REFERENCES Categorias(Id), -- ID de la categor�a
    FechaPublicacion DATE,               -- Fecha de publicaci�n
    PortadaBytes VARBINARY(MAX)           -- Portada del libro (imagen)
);
GO

-- Tabla: Clientes
CREATE TABLE Clientes (
    Id INT PRIMARY KEY IDENTITY(1,1),     -- ID del cliente
    Nombre VARCHAR(255) NOT NULL,         -- Nombre del cliente
    Email VARCHAR(255) UNIQUE NOT NULL,    -- Correo electr�nico del cliente (debe ser �nico)
    Direccion VARCHAR(255),               -- Direcci�n del cliente
    Telefono VARCHAR(20)                  -- N�mero de tel�fono del cliente
);
GO

-- Tabla: Ordenes
CREATE TABLE Ordenes (
    Id INT PRIMARY KEY IDENTITY(1,1),     -- ID de la orden
    ClienteId INT FOREIGN KEY REFERENCES Clientes(Id), -- ID del cliente que realiz� la orden
    FechaOrden DATETIME DEFAULT GETDATE(), -- Fecha y hora de la orden (por defecto la actual)
    Total DECIMAL(10, 2) NOT NULL         -- Total de la orden
);
GO

-- Tabla: DetallesOrden
CREATE TABLE DetallesOrden (
    Id INT PRIMARY KEY IDENTITY(1,1),             -- ID del detalle de la orden
    OrdenId INT FOREIGN KEY REFERENCES Ordenes(Id), -- ID de la orden a la que pertenece este detalle
    LibroId INT FOREIGN KEY REFERENCES Libros(Id),   -- ID del libro en este detalle
    Cantidad INT NOT NULL DEFAULT 1,              -- Cantidad del libro en este detalle (por defecto 1)
    PrecioUnitario DECIMAL(10, 2) NOT NULL       -- Precio unitario del libro al momento de la orden
);
GO

-- Tabla: Usuarios (para la administraci�n del sitio)
CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY(1,1),     -- ID del usuario administrador
    Nombre VARCHAR(255) NOT NULL,         -- Nombre del usuario administrador
    Email VARCHAR(255) UNIQUE NOT NULL,    -- Correo electr�nico del usuario administrador (debe ser �nico)
    Password CHAR(32) NOT NULL,           -- Hash de la contrase�a del usuario administrador
    Rol VARCHAR(50) NOT NULL DEFAULT 'ADMIN' -- Rol del usuario (por defecto ADMIN)
);
GO

-- PASSWORD default 12345
INSERT INTO Usuarios (Nombre, Email, Password, Rol)
VALUES ('SuperAdmin', 'superadmin@libros.com', '827ccb0eea8a706c4c34a16891f84e7b', 'SUPERADMIN');
GO