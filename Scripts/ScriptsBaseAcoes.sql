USE BaseAcoes
GO

CREATE TABLE dbo.Acoes(
	Id INT IDENTITY(1,1) NOT NULL,
	Codigo VARCHAR(15) NOT NULL,
	DataReferencia VARCHAR(20) NOT NULL,
	Valor NUMERIC (10,4) NOT NULL,
	CONSTRAINT PK_Acoes PRIMARY KEY (Id)
)
GO
