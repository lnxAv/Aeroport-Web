CREATE TABLE [dbo].[Notifications]
(
	[Id] INT NOT NULL  IDENTITY, 
    [Num_Phone] VARCHAR(20) NOT NULL, 
	[Num_Vol] VARCHAR(15) NOT NULL,
    [Date_Notification] DATETIME NOT NULL DEFAULT Null, 
    [Statut] BIT NOT NULL DEFAULT 1, 
    CONSTRAINT [PK_Notifications] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Notif_VolCed] FOREIGN KEY ([Num_Vol]) REFERENCES [VolsCedules]([Numero_Vol]), 
    
)
