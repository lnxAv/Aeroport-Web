CREATE TABLE [dbo].[Admins]
(
	[Id_Admin] INT NOT NULL  IDENTITY, 
    [Nom] VARCHAR(50) NOT NULL, 
    [Nom_Utilisateur] VARCHAR(50) NOT NULL, 
    [Mot_Passe] VARCHAR(100) NOT NULL, 
    [Autorise] BIT NOT NULL DEFAULT 1, 
    [Code_Aeroport] VARCHAR(20) NOT NULL, 
    CONSTRAINT [PK_Admins] PRIMARY KEY ([Id_Admin]), 
    CONSTRAINT [FK_Admins_Adminstrat] FOREIGN KEY ([Code_Aeroport]) REFERENCES [Administration]([Code_IATA]),
)
