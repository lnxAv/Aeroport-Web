CREATE TABLE [dbo].[Aeroport]
(
	[Code_IATA] VARCHAR(20) NOT NULL , 
    [Code_OACI] VARCHAR(20) NOT NULL, 
    [Nom] VARCHAR(50) NOT NULL, 
	[Id_Ville] INT NOT NULL,
    CONSTRAINT [PK_Aeroport] PRIMARY KEY ([Code_IATA]),
	CONSTRAINT [FK_Aerop_Ville] FOREIGN KEY ([Id_Ville]) REFERENCES [Villes]([Id_Ville]) 
)
