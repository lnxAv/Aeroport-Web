CREATE TABLE [Vols]
(
	[Numero] VARCHAR(15) NOT NULL , 
    [Heure_Depart] TIME NOT NULL, 
    [Heure_Arrivee] TIME NOT NULL, 
    [Id_Date_Depart] INT NOT NULL, 
    [Id_Date_Arrivee] INT NOT NULL, 
    [Code_CompAerien] VARCHAR(20) NULL, 
    CONSTRAINT [PK_Vols] PRIMARY KEY ([Numero]), 
    CONSTRAINT [FK_Vols_Dep_Calend] FOREIGN KEY ([Id_Date_Depart]) REFERENCES [Calendrier]([Id]),
	CONSTRAINT [FK_Vols_Arriv_Calend] FOREIGN KEY ([Id_Date_Arrivee]) REFERENCES [Calendrier]([Id]), 
    CONSTRAINT [FK_Vols_CompAerien] FOREIGN KEY ([Code_CompAerien]) REFERENCES [CompagniesAeriennes]([Code_IATA]),
)
