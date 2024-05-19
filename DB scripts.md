CREATE TABLE Customer (
    UserId UNIQUEIDENTIFIER PRIMARY KEY,
    Username VARCHAR(30),
    Email VARCHAR(20),
    FirstName VARCHAR(20),
    LastName VARCHAR(20),
    CreatedOn DATETIME,
    IsActive BIT
);

CREATE TABLE Supplier (
    SupplierId UNIQUEIDENTIFIER PRIMARY KEY,
    SupplierName VARCHAR(50),
    CreatedOn DATETIME,
    IsActive BIT
);

CREATE TABLE Product (
    ProductId UNIQUEIDENTIFIER PRIMARY KEY,
    ProductName VARCHAR(50),
    UnitPrice DECIMAL(18, 2),
    SupplierId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Supplier(SupplierId),
    CreatedOn DATETIME,
    IsActive BIT
);

CREATE TABLE [Order] (
    OrderId UNIQUEIDENTIFIER PRIMARY KEY,
    ProductId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Product(ProductId),
    OrderStatus INT,
    OrderType INT,
    OrderBy UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Customer(UserId),
    OrderedOn DATETIME,
    ShippedOn DATETIME,
    IsActive BIT
);
