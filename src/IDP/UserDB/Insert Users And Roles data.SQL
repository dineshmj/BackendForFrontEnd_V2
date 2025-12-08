-- =========================================================
-- Drop tables in the reverse order of creation.
-- =========================================================

DROP TABLE IF EXISTS "UsersAndRoles";
DROP TABLE IF EXISTS "Users";
DROP TABLE IF EXISTS "Roles";

-- =========================================================
-- Create table: Roles
-- =========================================================
CREATE TABLE "Roles" (
	"ID"	INTEGER NOT NULL,
	"Name"	TEXT NOT NULL UNIQUE,
	"ShortName"	TEXT,
	PRIMARY KEY("ID" AUTOINCREMENT)
);

-- =========================================================
-- Create table: Users
-- =========================================================
CREATE TABLE "Users" (
	"ID"	INTEGER NOT NULL,
	"FirstName"	TEXT NOT NULL,
	"LastName"	TEXT NOT NULL,
	"Email"	TEXT NOT NULL UNIQUE,
	"UserName"	TEXT NOT NULL UNIQUE,
	"HashedPassword"	TEXT NOT NULL,
	PRIMARY KEY("ID" AUTOINCREMENT)
);

-- =========================================================
-- Create table: UsersAndRoles
-- =========================================================
CREATE TABLE "UsersAndRoles" (
	"UserID"	INTEGER NOT NULL,
	"RoleID"	INTEGER NOT NULL,
	CONSTRAINT "RoleID_FK" FOREIGN KEY("RoleID") REFERENCES "Roles"("ID"),
	CONSTRAINT "UserID_FK" FOREIGN KEY("UserID") REFERENCES "Users"("ID")
);

-- =========================================================
-- Insert data into table: Roles
-- =========================================================
INSERT INTO Roles (Name, ShortName) VALUES
('Products Viewer', 'products_viewer'),
('Products Contributor','products_contributor'),
('Products Administrator', 'products_admin');

INSERT INTO Roles (Name, ShortName) VALUES
('Orders Viewer', 'orders_viewer'),
('Orders Contributor','orders_contributor'),
('Orders Administrator', 'orders_admin');

-- =========================================================
-- Insert data into table: Users
-- =========================================================
INSERT INTO Users (FirstName, LastName, Email, UserName, HashedPassword) VALUES
('Christopher', 'Nolan', 'Christopher.Nolan@hollywood.com', 'ChrisNol', 'AQAAAAIAAYagAAAAEMVToNmmuzd16VejftGrpL+QYfHN0S8du8/xNHrxoluJQtLv6KnDp37a4hmmxf8YLA==');

INSERT INTO Users (FirstName, LastName, Email, UserName, HashedPassword) VALUES
('Tom', 'Hanks', 'Tom.Hanks@hollywood.com', 'TomHanks', 'AQAAAAIAAYagAAAAEI2qnAi/sa4N0B6/uGAoZ2ZOp7ChNtSwIoWEINX7pIviStTovCk4f6vr64pe/QpDjg==');

INSERT INTO Users (FirstName, LastName, Email, UserName, HashedPassword) VALUES
('James', 'Cameron', 'James.Cameron@hollywood.com', 'JamesCam', 'AQAAAAIAAYagAAAAEGl+McAddZ0jrLxVOA/8w6Hfs1XOvYLbqI0laq+EJwI4ee/xautLD5kgHDCAZZWkGg==');

INSERT INTO Users (FirstName, LastName, Email, UserName, HashedPassword) VALUES
('Julia', 'Roberts', 'Julia.Roberts@hollywood.com', 'JuliaRob', 'AQAAAAIAAYagAAAAEB7pYjocIOuxbtiiraM9YBV2DMwtOi2UXUYJycwSShPwzKFg6et4ULVdTYrpkoEhng==');

-- =========================================================
-- Insert data into table: UsersAndRoles
-- =========================================================

INSERT INTO UsersAndRoles (UserID, RoleID) VALUES ((SELECT ID FROM Users WHERE UserName = 'ChrisNol'), (SELECT ID FROM Roles WHERE ShortName = 'products_viewer'));
INSERT INTO UsersAndRoles (UserID, RoleID) VALUES ((SELECT ID FROM Users WHERE UserName = 'ChrisNol'), (SELECT ID FROM Roles WHERE ShortName = 'orders_viewer'));

INSERT INTO UsersAndRoles (UserID, RoleID) VALUES ((SELECT ID FROM Users WHERE UserName = 'TomHanks'), (SELECT ID FROM Roles WHERE ShortName = 'products_viewer'));
INSERT INTO UsersAndRoles (UserID, RoleID) VALUES ((SELECT ID FROM Users WHERE UserName = 'TomHanks'), (SELECT ID FROM Roles WHERE ShortName = 'products_contributor'));
INSERT INTO UsersAndRoles (UserID, RoleID) VALUES ((SELECT ID FROM Users WHERE UserName = 'TomHanks'), (SELECT ID FROM Roles WHERE ShortName = 'orders_viewer'));
INSERT INTO UsersAndRoles (UserID, RoleID) VALUES ((SELECT ID FROM Users WHERE UserName = 'TomHanks'), (SELECT ID FROM Roles WHERE ShortName = 'orders_contributor'));

INSERT INTO UsersAndRoles (UserID, RoleID) VALUES ((SELECT ID FROM Users WHERE UserName = 'JamesCam'), (SELECT ID FROM Roles WHERE ShortName = 'products_viewer'));
INSERT INTO UsersAndRoles (UserID, RoleID) VALUES ((SELECT ID FROM Users WHERE UserName = 'JamesCam'), (SELECT ID FROM Roles WHERE ShortName = 'products_contributor'));
INSERT INTO UsersAndRoles (UserID, RoleID) VALUES ((SELECT ID FROM Users WHERE UserName = 'JamesCam'), (SELECT ID FROM Roles WHERE ShortName = 'orders_viewer'));
INSERT INTO UsersAndRoles (UserID, RoleID) VALUES ((SELECT ID FROM Users WHERE UserName = 'JamesCam'), (SELECT ID FROM Roles WHERE ShortName = 'orders_contributor'));

INSERT INTO UsersAndRoles (UserID, RoleID) VALUES ((SELECT ID FROM Users WHERE UserName = 'JuliaRob'), (SELECT ID FROM Roles WHERE ShortName = 'products_viewer'));
INSERT INTO UsersAndRoles (UserID, RoleID) VALUES ((SELECT ID FROM Users WHERE UserName = 'JuliaRob'), (SELECT ID FROM Roles WHERE ShortName = 'products_contributor'));
INSERT INTO UsersAndRoles (UserID, RoleID) VALUES ((SELECT ID FROM Users WHERE UserName = 'JuliaRob'), (SELECT ID FROM Roles WHERE ShortName = 'products_admin'));
INSERT INTO UsersAndRoles (UserID, RoleID) VALUES ((SELECT ID FROM Users WHERE UserName = 'JuliaRob'), (SELECT ID FROM Roles WHERE ShortName = 'orders_viewer'));
INSERT INTO UsersAndRoles (UserID, RoleID) VALUES ((SELECT ID FROM Users WHERE UserName = 'JuliaRob'), (SELECT ID FROM Roles WHERE ShortName = 'orders_contributor'));
INSERT INTO UsersAndRoles (UserID, RoleID) VALUES ((SELECT ID FROM Users WHERE UserName = 'JuliaRob'), (SELECT ID FROM Roles WHERE ShortName = 'orders_admin'));

-- =========================================================
-- Checking data on all tables
-- =========================================================
SELECT
	u.ID AS UserID,
	CONCAT (u.FirstName, ' ', u.LastName) AS UserFullName,
	r.ID AS RoleID,
	r.ShortName AS RoleShortName,
	r.name AS RoleDescription
FROM
	Users AS u
		INNER JOIN UsersAndRoles AS ur ON u.ID = ur.UserID
		INNER JOIN Roles AS r ON ur.RoleID = r.ID
ORDER BY
	u.ID,
	r.ID