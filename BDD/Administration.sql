CREATE TABLE [dbo].[Administration]
(
	[Code_IATA] VARCHAR(20) NOT NULL , 
    [Couleur] VARCHAR(15) NOT NULL, 
    [Logo] IMAGE NOT NULL DEFAULT NULL, 
    CONSTRAINT [PK_Administration] PRIMARY KEY ([Code_IATA]), 
    CONSTRAINT [FK_Administ_Aerop] FOREIGN KEY ([Code_IATA]) REFERENCES [Aeroport]([Code_IATA]) 
)
