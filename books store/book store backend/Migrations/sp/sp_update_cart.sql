create procedure update_cart @user uniqueidentifier,
							 @item uniqueidentifier,
							 @quantity INT
as 
if EXISTS (select * from cart where Client = @user and ProductId = @item)
	update cart set Quantity = Quantity + @quantity
	where Client = @user and ProductId = @item;
else 
	insert into cart (Client, ProductId, Quantity) 
	values(@user, @item, @quantity);