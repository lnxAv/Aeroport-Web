CREATE TABLE [dbo].[CompagniesAeriennes]
(
	[Code_IATA] VARCHAR(20) NOT NULL, 
    [Code_OACI] VARCHAR(20) NOT NULL, 
    [Nom] VARCHAR(70) NOT NULL, 
    [Logo] IMAGE NULL, 
    [Num_Phone] VARCHAR(20) NULL, 
    CONSTRAINT [PK_CompagniesAeriennes] PRIMARY KEY ([Code_IATA]) 

)
