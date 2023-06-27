CREATE TABLE [dbo].[DeviceValueHistory]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Timestamp] DATETIME NOT NULL, 
    [Value] NVARCHAR(MAX) NULL, 
    [Device] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [FK_DeviceValueHistory_Device] FOREIGN KEY ([Id]) REFERENCES [Device]([Id])
)