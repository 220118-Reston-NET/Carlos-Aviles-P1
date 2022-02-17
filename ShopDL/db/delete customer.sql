ALTER TABLE [stores_orders] ADD FOREIGN KEY ([storeId]) REFERENCES [Store] ([id])
GO

ALTER TABLE [stores_orders] ADD FOREIGN KEY ([orderId]) REFERENCES [Order] ([id])
GO
