select * from [stores_orders] as so
join [Order] as o on o.id = so.orderId
join [Store] as s on s.id = 1