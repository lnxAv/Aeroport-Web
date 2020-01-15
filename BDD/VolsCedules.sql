CREATE TABLE [dbo].[VolsCedules]
(
	[Numero_Vol] VARCHAR(15) NOT NULL, 
    [Date_Depart_Revisee] DATETIME NOT NULL DEFAULT Null, 
    [Date_Arrivee_Revisee] DATETIME NOT NULL DEFAULT Null, 
    [Statut] VARCHAR(15) NOT NULL CHECK([Statut] IN ('PARTI','ARRIVE','ANNULE','EMBARQUEMENT')),
    [Etat] VARCHAR(15) NOT NULL CHECK([Statut] IN ('EN AVANCE','EN RETARD','A TEMPS')),
    [Date_MAJ] DATETIME NOT NULL DEFAULT Null, 
    CONSTRAINT [PK_VolsCedules] PRIMARY KEY ([Numero_Vol]),
	CONSTRAINT [FK_VolCed_Vols] FOREIGN KEY ([Numero_Vol]) REFERENCES [Vols]([Numero]),
)
