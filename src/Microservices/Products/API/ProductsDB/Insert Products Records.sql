-- =========================================================
-- Drop tables in the reverse order of creation.
-- =========================================================

DROP TABLE IF EXISTS Inventory;
DROP TABLE IF EXISTS Products;
DROP TABLE IF EXISTS Categories;

-- =========================================================
-- Create table: Categories
-- =========================================================
CREATE TABLE Categories (
	ID INTEGER PRIMARY KEY AUTOINCREMENT,
	Name TEXT NOT NULL UNIQUE
);

-- =========================================================
-- Create table: Products
-- =========================================================

CREATE TABLE Products (
	ID INTEGER PRIMARY KEY AUTOINCREMENT,
	Name TEXT NOT NULL,
	Price REAL NOT NULL, -- Using REAL for monetary values
	CategoryID INTEGER NOT NULL,
	UNIQUE (Name),
	FOREIGN KEY (CategoryID) REFERENCES Categories(ID) ON DELETE RESTRICT
);

-- =========================================================
-- Create table: Inventory
-- =========================================================
CREATE TABLE Inventory (
	ID INTEGER PRIMARY KEY AUTOINCREMENT,
	ProductID INTEGER,
	StockQuantity INTEGER NOT NULL,
	LastUpdated DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	FOREIGN KEY (ProductID) REFERENCES Products(ID) ON DELETE CASCADE
);

-- =========================================================
-- Insert data into table: Categories.
-- =========================================================
INSERT INTO Categories (Name) VALUES
('Electronics'),
('Accessories');

-- =========================================================
-- Insert data into table: Products
-- =========================================================
INSERT INTO Products (Name, Price, CategoryID) VALUES ('Laptop Pro', 1299.99, (SELECT ID FROM Categories WHERE Name = 'Electronics'));
INSERT INTO Products (Name, Price, CategoryID) VALUES ('Wireless Mouse', 29.99, (SELECT ID FROM Categories WHERE Name = 'Accessories'));
INSERT INTO Products (Name, Price, CategoryID) VALUES ('Mechanical Keyboard', 89.99, (SELECT ID FROM Categories WHERE Name = 'Accessories'));
INSERT INTO Products (Name, Price, CategoryID) VALUES ('USB-C Hub', 49.99, (SELECT ID FROM Categories WHERE Name = 'Accessories'));
INSERT INTO Products (Name, Price, CategoryID) VALUES ('Monitor 27"', 399.99, (SELECT ID FROM Categories WHERE Name = 'Electronics'));

-- =========================================================
-- Insert data into table: Inventory
-- =========================================================
INSERT INTO Inventory (ProductID, StockQuantity) VALUES ((SELECT ID FROM Products WHERE Name = 'Laptop Pro'), 45);
INSERT INTO Inventory (ProductID, StockQuantity) VALUES ((SELECT ID FROM Products WHERE Name = 'Wireless Mouse'), 150);
INSERT INTO Inventory (ProductID, StockQuantity) VALUES ((SELECT ID FROM Products WHERE Name = 'Mechanical Keyboard'), 78);
INSERT INTO Inventory (ProductID, StockQuantity) VALUES ((SELECT ID FROM Products WHERE Name = 'USB-C Hub'), 92);
INSERT INTO Inventory (ProductID, StockQuantity) VALUES ((SELECT ID FROM Products WHERE Name = 'Monitor 27"'), 23);

-- =========================================================
-- Checking data on all tables
-- =========================================================
SELECT
    p.ID AS ProductID,
    p.Name AS ProductName,
    c.Name AS CategoryName,
    p.Price,
    i.StockQuantity
FROM
    Products AS p
INNER JOIN
    Categories AS c ON p.CategoryID = c.ID
INNER JOIN
    Inventory AS i ON p.ID = i.ProductID
ORDER BY
    c.Name,
    p.Name;