DROP TABLE Empires.Animal
GO
DROP TABLE Empires.Naval 
GO
DROP TABLE Empires.Artilharia 		
GO
DROP TABLE Empires.Cavalaria 
GO
DROP TABLE Empires.Infantaria 
GO
DROP TABLE Empires.Civil 
GO
DROP TABLE Empires.Unidades_Treinaveis 
GO
DROP TABLE Empires.Heroi 
GO
DROP TABLE Empires.Unidades 
GO
DROP TABLE Empires.Edificios 
GO
DROP TABLE Empires.Tecnologia 
GO
DROP TABLE Empires.Prop 
GO
DROP TABLE Empires.Objeto 
GO
DROP TABLE Empires.Cartas_Jogador
GO
DROP TABLE Empires.Jogador 
GO
DROP TABLE Empires.Equipa
GO
DROP TABLE Empires.Cartas_Civilizacao 
GO
DROP TABLE Empires.Grande_Civilizacao 
GO
DROP TABLE Empires.Pequena_Civilizacao 
GO
DROP TABLE Empires.Civilizacao_Tropas 
GO
DROP TABLE Empires.Civilizacao 
GO
DROP TABLE Empires.Era
GO
DROP TABLE Empires.Alia
GO

DROP VIEW Todas_Civilizacao;
GO

DROP SCHEMA Empires;
GO

-- Drop the foreign key constraint
ALTER TABLE Empires.Grande_Civilizacao
    DROP CONSTRAINT FK_GC_civ_id;

