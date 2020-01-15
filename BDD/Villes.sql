CREATE TABLE [dbo].[Villes]
(
	[Id_Ville] INT NOT NULL IDENTITY , 
    [Nom] VARCHAR(70) NOT NULL, 
    CONSTRAINT [PK_Villes] PRIMARY KEY ([Id_Ville])
)
