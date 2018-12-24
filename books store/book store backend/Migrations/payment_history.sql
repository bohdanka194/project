use cqrs_read;
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

