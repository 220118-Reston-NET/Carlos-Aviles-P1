CREATE TABLE [Customer] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [name] nvarchar(255),
  [age] int,
  [address] nvarchar(255),
  [phone] nvarchar(255)
)
GO

CREATE TABLE [Product] (
  [id] [pk],
  [name] nvarchar(255),
  [price] float,
  [description] nvarchar(255),
  [category] nvarchar(255),
  [minimumAge] int
)
GO

CREATE TABLE [Store] (
  [id] int PRIMARY KEY,
  [name] nvarchar(255),
  [address] nvarchar(255)
)
GO

CREATE TABLE [StoreInventory] (
  [storeId] int,
  [productId] int,
  [quantity] int
)
GO

CREATE TABLE [Order] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [storeId] int,
  [totalPrice] float
)
GO

CREATE TABLE [PurchasedItem] (
  [orderId] int,
  [productId] int,
  [quantity] int
)
GO

CREATE TABLE [customers_orders] (
  [customerId] int,
  [orderId] int
)
GO

CREATE TABLE [stores_orders] (
  [storeId] int,
  [orderId] int
)
GO

ALTER TABLE [StoreInventory] ADD FOREIGN KEY ([storeId]) REFERENCES [Store] ([id])
GO

ALTER TABLE [StoreInventory] ADD FOREIGN KEY ([productId]) REFERENCES [Product] ([id])
GO

ALTER TABLE [Order] ADD FOREIGN KEY ([storeId]) REFERENCES [Store] ([id])
GO

ALTER TABLE [PurchasedItem] ADD FOREIGN KEY ([orderId]) REFERENCES [Order] ([id])
GO

ALTER TABLE [PurchasedItem] ADD FOREIGN KEY ([productId]) REFERENCES [Product] ([id])
GO

ALTER TABLE [customers_orders] ADD FOREIGN KEY ([customerId]) REFERENCES [Customer] ([id])
GO

ALTER TABLE [customers_orders] ADD FOREIGN KEY ([orderId]) REFERENCES [Order] ([id])
GO

ALTER TABLE [stores_orders] ADD FOREIGN KEY ([storeId]) REFERENCES [Store] ([id])
GO

ALTER TABLE [stores_orders] ADD FOREIGN KEY ([orderId]) REFERENCES [Order] ([id])
GO
