go

create table [dbo].[books] (
    [Id] uniqueidentifier  NOT NULL,
	[Title] NVARCHAR (MAX) NOT NULL,
	[Author] NVARCHAR (MAX) NOT NULL,
	[Description] NVARCHAR (MAX) NOT NULL, 
	[ISBN10] NVARCHAR (MAX) NOT NULL,
	[Price] FLOAT NOT NULL,
	[Pages] INT NOT NULL,
	[Rating] SMALLINT NOT NULL,
	[Votes] INT NOT NULL,
	[Image] NVARCHAR (MAX) NOT NULL,
 	CONSTRAINT [PK_dbo.books] PRIMARY KEY CLUSTERED ([Id] ASC)
); 

CREATE TABLE [dbo].[cart] (
    [Client]    UNIQUEIDENTIFIER NOT NULL,
    [ProductId] UNIQUEIDENTIFIER NOT NULL,
	[Quantity] INT NULL 
);

create table [dbo].[payment_history] (
    [Client]  UNIQUEIDENTIFIER NOT NULL,
	[Item] UNIQUEIDENTIFIER NOT NULL,
	[_When] DATETIME NOT NULL,
	[Quantity] INT NULL, 

	constraint [FK_payment_history.Item] 
	foreign key ([Item]) 
	references [dbo].[books] ([Id]) 
	ON DELETE CASCADE,
);

GO

CREATE procedure dbo.log_payment_details @Client uniqueidentifier
as
if EXISTS (select ProductId from cart where Client=@Client)
	declare @now datetime; 
	set @now = GETDATE();
	insert into payment_history (Client, Item, Quantity, _When)
	   (select Client, ProductId, Quantity, @now from cart where cart.Client=@Client)
	delete from cart where Client=@Client

GO

create procedure dbo.update_cart @user uniqueidentifier,
							 @item uniqueidentifier,
							 @quantity INT
as 
if EXISTS (select * from cart where Client = @user and ProductId = @item)
	update cart set Quantity = Quantity + @quantity
	where Client = @user and ProductId = @item;
else 
	insert into cart (Client, ProductId, Quantity) 
	values(@user, @item, @quantity);