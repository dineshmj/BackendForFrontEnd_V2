-- =========================================================
-- Drop tables in the reverse order of creation.
-- =========================================================
DROP TABLE IF EXISTS OrderItems;
DROP TABLE IF EXISTS OrderDetails;
DROP TABLE IF EXISTS Orders;
DROP TABLE IF EXISTS Customers;

-- =========================================================
-- Create table: Customers
-- =========================================================
CREATE TABLE Customers (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    FirstName TEXT NOT NULL, -- New Column
    LastName TEXT NOT NULL   -- New Column
);

-- =========================================================
-- Create table: Orders
-- =========================================================
CREATE TABLE Orders (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    CustomerId INTEGER NOT NULL,
    DateOfOrder TEXT NOT NULL,
    TotalAmount REAL NOT NULL,
    FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
);

-- =========================================================
-- Create table: OrderDetails
-- =========================================================
CREATE TABLE OrderDetails (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    OrderId INTEGER NOT NULL UNIQUE,
    InvoiceNumber TEXT NOT NULL UNIQUE,
    PaymentMethod TEXT NOT NULL,
    DispatchStatus TEXT NOT NULL,
    FOREIGN KEY (OrderId) REFERENCES Orders(Id)
);

-- =========================================================
-- Create table: OrderItems
-- =========================================================
CREATE TABLE OrderItems (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    OrderId INTEGER NOT NULL,
    ProductId TEXT NOT NULL,
    Quantity INTEGER NOT NULL,
    FOREIGN KEY (OrderId) REFERENCES Orders(Id)
);

-- =========================================================
-- Insert data into table: Customers
-- =========================================================
INSERT INTO Customers (FirstName, LastName) VALUES
('Alice', 'Johnson'),
('Bob', 'Williams'),
('Charlie', 'Brown');

-- =========================================================
-- Insert data into table: Orders
-- =========================================================
INSERT INTO Orders (CustomerId, DateOfOrder, TotalAmount) VALUES
(1, '2025-11-20 10:30:00', 150.75),
(2, '2025-11-21 14:45:00', 59.99),
(1, '2025-11-21 16:00:00', 89.50);

-- =========================================================
-- Insert data into table: OrderDetails
-- =========================================================
INSERT INTO OrderDetails (OrderId, InvoiceNumber, PaymentMethod, DispatchStatus) VALUES
(1, 'INV-2025-1001', 'Credit Card', 'Shipped'),
(2, 'INV-2025-1002', 'PayPal', 'Delivered'),
(3, 'INV-2025-1003', 'Debit Card', 'Processing');

-- =========================================================
-- Insert data into table: OrderItems
-- =========================================================
INSERT INTO OrderItems (OrderId, ProductId, Quantity) VALUES
(1, 'P-452', 1),
(1, 'P-880', 2),
(2, 'P-101', 1),
(3, 'P-223', 5);

-- =========================================================
-- Checking data on all tables
-- =========================================================
SELECT
    O.Id AS OrderId,
    O.DateOfOrder AS DateOfOrder,
    O.TotalAmount AS TotalAmount,
    OD.PaymentMethod AS PaymentMethod,
    OD.InvoiceNumber AS InvoiceNumber,
    SUM(OI.Quantity) AS NumberOfItems,
    C.FirstName || ' ' || C.LastName AS CustomerName, 
    OD.DispatchStatus AS DispatchStatus
FROM
    Orders O
INNER JOIN
    Customers C ON O.CustomerId = C.Id
INNER JOIN
    OrderDetails OD ON O.Id = OD.OrderId
INNER JOIN
    OrderItems OI ON O.Id = OI.OrderId
GROUP BY
    O.Id, O.DateOfOrder, O.TotalAmount, OD.PaymentMethod, OD.InvoiceNumber, C.FirstName, C.LastName, OD.DispatchStatus
ORDER BY
    O.Id;