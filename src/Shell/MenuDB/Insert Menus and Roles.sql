-- =========================================================
-- Drop tables in the reverse order of creation.
-- =========================================================

DROP TABLE IF EXISTS "MenuItemsAndRoles";
DROP TABLE IF EXISTS "MenuItems";
DROP TABLE IF EXISTS "ManagementArea";
DROP TABLE IF EXISTS "Microservices";

-- =========================================================
-- Create table: Microservices
-- =========================================================
CREATE TABLE "Microservices" (
	"ID"	INTEGER NOT NULL,
	"Name"	TEXT NOT NULL UNIQUE,
	"BaseURL"	TEXT NOT NULL UNIQUE,
	PRIMARY KEY("ID" AUTOINCREMENT)
);

-- =========================================================
-- Create table: ManagementArea
-- =========================================================
CREATE TABLE "ManagementArea" (
	"ID"	INTEGER NOT NULL,
	"Name"	TEXT NOT NULL,
	"Description"	TEXT NOT NULL,
	"MicroserviceID"	INTEGER NOT NULL,
	PRIMARY KEY("ID" AUTOINCREMENT),
	CONSTRAINT "MicroserviceID_FK" FOREIGN KEY("MicroserviceID") REFERENCES "Microservices"("ID")
);

-- =========================================================
-- Create table: MenuItems
-- =========================================================
CREATE TABLE "MenuItems" (
	"ID"	INTEGER NOT NULL,
	"TaskName"	TEXT NOT NULL,
	"UrlRelativePath"	TEXT NOT NULL,
	"IconName"	TEXT NOT NULL,
	"ManagementAreaID"	INTEGER NOT NULL,
	PRIMARY KEY("ID" AUTOINCREMENT),
	CONSTRAINT "ManagementAreaID_FK" FOREIGN KEY("ManagementAreaID") REFERENCES "ManagementArea"("ID")
);

-- =========================================================
-- Create table: "MenuItemsAndRoles"
-- =========================================================
CREATE TABLE "MenuItemsAndRoles" (
	"MenuItemID"	INTEGER NOT NULL,
	"RoleShortName"	TEXT NOT NULL,
	CONSTRAINT "MenuItemID_FK" FOREIGN KEY("MenuItemID") REFERENCES "MenuItems"("ID")
);

-- =========================================================
-- Insert data into table: Microservices
-- =========================================================
INSERT INTO Microservices (Name, BaseURL) VALUES ('Products', 'https://localhost:44311');
INSERT INTO Microservices (Name, BaseURL) VALUES ('Orders', 'https://localhost:33800');

-- =========================================================
-- Insert data into table: ManagementArea
-- =========================================================
INSERT INTO ManagementArea (Name, Description, MicroserviceID) VALUES
('Product Catalog', 'Manage product listings, pricing, and lifecycle.', (SELECT ID FROM Microservices WHERE Name = 'Products')),
('Category Management', 'Define and organize product categories and hierarchies.', (SELECT ID FROM Microservices WHERE Name = 'Products')),
('Inventory & Logistics', 'Track stock levels and manage warehouse movements.', (SELECT ID FROM Microservices WHERE Name = 'Products')),
('Settings', 'Configure core product microservice parameters.', (SELECT ID FROM Microservices WHERE Name = 'Products'));

INSERT INTO ManagementArea (Name, Description, MicroserviceID) VALUES
('Orders', 'Manage orders', (SELECT ID FROM Microservices WHERE Name = 'Orders')),
('Delivery', 'Manage delivery of orders.', (SELECT ID FROM Microservices WHERE Name = 'Orders'));

-- =========================================================
-- Insert data into table: MenuItems, MenuItemsAndRoles
-- =========================================================
--
-- Products
--
INSERT INTO MenuItems (TaskName, UrlRelativePath, IconName, ManagementAreaID) VALUES ('View All Products', '/v1/catalog/view-all', 'fa-list', (SELECT ID FROM ManagementArea WHERE Name = 'Product Catalog'));
INSERT INTO MenuItemsAndRoles (MenuItemID, RoleShortName) VALUES ((SELECT ID FROM MenuItems WHERE TaskName = 'View All Products'), 'products_viewer');
INSERT INTO MenuItemsAndRoles (MenuItemID, RoleShortName) VALUES ((SELECT ID FROM MenuItems WHERE TaskName = 'View All Products'), 'products_contributor');
INSERT INTO MenuItemsAndRoles (MenuItemID, RoleShortName) VALUES ((SELECT ID FROM MenuItems WHERE TaskName = 'View All Products'), 'products_admin');

INSERT INTO MenuItems (TaskName, UrlRelativePath, IconName, ManagementAreaID) VALUES ('Add New Product', '/v1/catalog/create', 'fa-plus', (SELECT ID FROM ManagementArea WHERE Name = 'Product Catalog'));
INSERT INTO MenuItemsAndRoles (MenuItemID, RoleShortName) VALUES ((SELECT ID FROM MenuItems WHERE TaskName = 'Add New Product'), 'products_contributor');
INSERT INTO MenuItemsAndRoles (MenuItemID, RoleShortName) VALUES ((SELECT ID FROM MenuItems WHERE TaskName = 'Add New Product'), 'products_admin');

INSERT INTO MenuItems (TaskName, UrlRelativePath, IconName, ManagementAreaID) VALUES ('Edit Details / Pricing', '/v1/catalog/edit', 'fa-edit', (SELECT ID FROM ManagementArea WHERE Name = 'Product Catalog'));
INSERT INTO MenuItemsAndRoles (MenuItemID, RoleShortName) VALUES ((SELECT ID FROM MenuItems WHERE TaskName = 'Edit Details / Pricing'), 'products_admin');

INSERT INTO MenuItems (TaskName, UrlRelativePath, IconName, ManagementAreaID) VALUES ('Delete Product', '/v1/catalog/delete', 'fa-trash', (SELECT ID FROM ManagementArea WHERE Name = 'Product Catalog'));
INSERT INTO MenuItemsAndRoles (MenuItemID, RoleShortName) VALUES ((SELECT ID FROM MenuItems WHERE TaskName = 'Delete Product'), 'products_admin');

INSERT INTO MenuItems (TaskName, UrlRelativePath, IconName, ManagementAreaID) VALUES ('View All Categories', '/v1/categories/view-all', 'fa-tags', (SELECT ID FROM ManagementArea WHERE Name = 'Category Management'));
INSERT INTO MenuItemsAndRoles (MenuItemID, RoleShortName) VALUES ((SELECT ID FROM MenuItems WHERE TaskName = 'View All Categories'), 'products_viewer');
INSERT INTO MenuItemsAndRoles (MenuItemID, RoleShortName) VALUES ((SELECT ID FROM MenuItems WHERE TaskName = 'View All Categories'), 'products_contributor');
INSERT INTO MenuItemsAndRoles (MenuItemID, RoleShortName) VALUES ((SELECT ID FROM MenuItems WHERE TaskName = 'View All Categories'), 'products_admin');

INSERT INTO MenuItems (TaskName, UrlRelativePath, IconName, ManagementAreaID) VALUES ('Create / Modify Categories', '/v1/categories/manage', 'fa-wrench', (SELECT ID FROM ManagementArea WHERE Name = 'Category Management'));
INSERT INTO MenuItemsAndRoles (MenuItemID, RoleShortName) VALUES ((SELECT ID FROM MenuItems WHERE TaskName = 'Create / Modify Categories'), 'products_admin');

INSERT INTO MenuItems (TaskName, UrlRelativePath, IconName, ManagementAreaID) VALUES ('View Inventory Report', '/v1/inventory/report', 'fa-warehouse', (SELECT ID FROM ManagementArea WHERE Name = 'Inventory & Logistics'));
INSERT INTO MenuItemsAndRoles (MenuItemID, RoleShortName) VALUES ((SELECT ID FROM MenuItems WHERE TaskName = 'View Inventory Report'), 'products_viewer');
INSERT INTO MenuItemsAndRoles (MenuItemID, RoleShortName) VALUES ((SELECT ID FROM MenuItems WHERE TaskName = 'View Inventory Report'), 'products_contributor');
INSERT INTO MenuItemsAndRoles (MenuItemID, RoleShortName) VALUES ((SELECT ID FROM MenuItems WHERE TaskName = 'View Inventory Report'), 'products_admin');

INSERT INTO MenuItems (TaskName, UrlRelativePath, IconName, ManagementAreaID) VALUES ('Adjust Stock Levels', '/v1/inventory/adjust', 'fa-truck', (SELECT ID FROM ManagementArea WHERE Name = 'Inventory & Logistics'));
INSERT INTO MenuItemsAndRoles (MenuItemID, RoleShortName) VALUES ((SELECT ID FROM MenuItems WHERE TaskName = 'Adjust Stock Levels'), 'products_contributor');
INSERT INTO MenuItemsAndRoles (MenuItemID, RoleShortName) VALUES ((SELECT ID FROM MenuItems WHERE TaskName = 'Adjust Stock Levels'), 'products_admin');

INSERT INTO MenuItems (TaskName, UrlRelativePath, IconName, ManagementAreaID) VALUES ('Manage Product Settings', '/v1/settings', 'fa-cog', (SELECT ID FROM ManagementArea WHERE Name = 'Settings'));
INSERT INTO MenuItemsAndRoles (MenuItemID, RoleShortName) VALUES ((SELECT ID FROM MenuItems WHERE TaskName = 'Manage Product Settings'), 'products_admin');

--
-- Orders
--
INSERT INTO MenuItems (TaskName, UrlRelativePath, IconName, ManagementAreaID) VALUES ('View All Orders', '/v1/orders/view-all', 'fa-list', (SELECT ID FROM ManagementArea WHERE Name = 'Orders'));
INSERT INTO MenuItemsAndRoles (MenuItemID, RoleShortName) VALUES ((SELECT ID FROM MenuItems WHERE TaskName = 'View All Orders'), 'orders_viewer');
INSERT INTO MenuItemsAndRoles (MenuItemID, RoleShortName) VALUES ((SELECT ID FROM MenuItems WHERE TaskName = 'View All Orders'), 'orders_contributor');
INSERT INTO MenuItemsAndRoles (MenuItemID, RoleShortName) VALUES ((SELECT ID FROM MenuItems WHERE TaskName = 'View All Orders'), 'orders_admin');


-- =========================================================
-- Checking data on all tables
-- =========================================================
SELECT
    ms.Name AS Microservice,
	ms.ID AS MicroserviceID,
    ms.BaseURL,
    ma.Name AS ManagementAreaName,
	ma.ID AS ManagementAreaID,
    mi.TaskName,
	mi.ID AS TaskID,
    mi.UrlRelativePath,
    mra.RoleShortName
FROM
    Microservices AS ms
INNER JOIN
    ManagementArea AS ma ON ms.ID = ma.MicroserviceID
INNER JOIN
    MenuItems AS mi ON ma.ID = mi.ManagementAreaID
INNER JOIN
    MenuItemsAndRoles AS mra ON mi.ID = mra.MenuItemID
ORDER BY
    ms.Name,
    ma.Name,
    mi.TaskName,
    mra.RoleShortName;