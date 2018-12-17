create database cqrs_read
go 
use cqrs_read
go
create table [dbo].[books] (
    [Id] uniqueidentifier  NOT NULL,
	[Title] NVARCHAR (128) NOT NULL unique,
	[Author] NVARCHAR (128) NOT NULL unique,
	[Price] INT NOT NULL,
	CONSTRAINT [PK_dbo.books] PRIMARY KEY CLUSTERED ([Id] ASC)
);
