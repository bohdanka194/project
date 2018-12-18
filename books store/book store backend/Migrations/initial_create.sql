create database cqrs_read
go 
use cqrs_read
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
