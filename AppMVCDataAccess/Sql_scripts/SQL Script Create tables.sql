/* Drop all tables script 
DROP TABLE OrderLineItem;
DROP TABLE ProductStock;
DROP TABLE Sizes;
DROP TABLE Product;
DROP TABLE Category;
DROP TABLE [Order];
DROP TABLE OrderStatus;
DROP TABLE [User];
*/

CREATE TABLE [dbo].[User]
  (
     [email]    [varchar](50) NOT NULL PRIMARY KEY,
     [name]     [varchar](50) NOT NULL,
     [surname]  [varchar](50) NOT NULL,
     [phone]    [varchar](20) NULL,
     [address]  [varchar](50) NULL,
     [password] [nvarchar](MAX) NULL,
     [isAdmin]  bit NOT NULL DEFAULT 0
  )

CREATE TABLE [dbo].[Order]
  (
     [id]       int IDENTITY(1, 1) NOT NULL PRIMARY KEY,
     [date]     datetime NULL,
     [total]    decimal NULL,
	 [address]  text NULL,
	 [note]     text NULL,
     [status]   int NOT NULL,
     [customer] [varchar](50) NOT NULL FOREIGN KEY REFERENCES [dbo].[User](email)
  )

CREATE TABLE [dbo].Category
  (
     [name]        [varchar](50) NOT NULL PRIMARY KEY,
     [description] text NULL
  )

CREATE TABLE [dbo].Product
  (
     [id]          int IDENTITY(1, 1) NOT NULL PRIMARY KEY,
     [name]        [varchar](50) NOT NULL,
     [description] text NULL,
     [price]       decimal NOT NULL,
     [discount]    int NULL,
     [category]    [varchar](50) FOREIGN KEY REFERENCES Category(name),
  )

CREATE TABLE [dbo].Sizes
  (
     [id]   int IDENTITY(1, 1) NOT NULL PRIMARY KEY,
     [size] [varchar](50) NOT NULL
  )

CREATE TABLE [dbo].ProductStock
  (
     [product_id] int NOT NULL FOREIGN KEY REFERENCES Product(id),
     [size_id]    int NOT NULL FOREIGN KEY REFERENCES Sizes(id),
     [stock]      int NOT NULL,
     PRIMARY KEY (product_id, size_id)
  )

CREATE TABLE [dbo].OrderLineItem
  (
     [order_id]   int NOT NULL FOREIGN KEY REFERENCES [Order](id),
     [product_id] int NOT NULL FOREIGN KEY REFERENCES Product(id),
     [size_id]    int NOT NULL FOREIGN KEY REFERENCES Sizes(id),
     [amount]     int NOT NULL
	 PRIMARY KEY (order_id, product_id, size_id)
  ) 