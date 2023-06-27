CREATE TABLE [dbo].[Device]
(
	[Id] UNIQUEIDENTIFIER NOT NULL , 
    [Name] NVARCHAR(MAX) NOT NULL, 
    [Config] NVARCHAR(MAX) NOT NULL, 
    [DeviceType] UNIQUEIDENTIFIER NOT NULL, 
    PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Device_DeviceType] FOREIGN KEY ([Id]) REFERENCES [DeviceType]([Id]) 
)