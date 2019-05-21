create database artsheva_books
go 
use artsheva_books
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

create table payment_history (
    [Client]  UNIQUEIDENTIFIER NOT NULL,
	[Item] UNIQUEIDENTIFIER NOT NULL,
	[_When] DATETIME NOT NULL,
	[Quantity] INT NULL, 

	constraint [FK_payment_history.Item] 
	foreign key ([Item]) 
	references [dbo].[books] ([Id]) 
	ON DELETE CASCADE,
);

