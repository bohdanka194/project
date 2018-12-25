CREATE procedure log_payment_details @Client uniqueidentifier
as
if EXISTS (select ProductId from cart where Client=@Client)
	declare @now datetime; 
	set @now = GETDATE();
	insert into payment_history (Client, Item, Quantity, _When)
	   (select Client, ProductId, Quantity, @now from cart where cart.Client=@Client)
	delete from cart where Client=@Client