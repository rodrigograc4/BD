/* View for the Civ Forms */

-- Drop the View
DROP VIEW  Empires.Todas_Civilizacao

--  Create the View
GO
CREATE VIEW Empires.Todas_Civilizacao AS
	SELECT civ_id AS ID, nome AS Nome, lider AS Lider, capital AS Capital, limite_tropas AS Limite_Tropas
	FROM (Empires.Civilizacao LEFT OUTER JOIN Empires.Grande_Civilizacao
			ON Civilizacao.civ_id = Grande_Civilizacao.FK_civ_id)
		LEFT OUTER JOIN Empires.Pequena_Civilizacao 
			ON Civilizacao.civ_id = Pequena_Civilizacao.FK_civ_id;
GO

-- Select from the View
Select * From Empires.Todas_Civilizacao


/*##########################################/
/###########################################/
/##########################################*/


/* View for the Player Forms */

-- Drop the View
DROP VIEW  Empires.Jogadores_Full_Info

--  Create the View
GO
CREATE VIEW Empires.Jogadores_Full_Info AS
	SELECT jogador_id AS ID, Jogador.nome Nome, clan AS Clan, cor AS Cor, Era.nome AS Era, Civilizacao.nome AS Civilizacao
	FROM (Empires.Jogador LEFT OUTER JOIN Empires.Era
			ON Jogador.FK_era_id = Era.era_id)
		LEFT OUTER JOIN Empires.Civilizacao 
			ON Jogador.FK_grande_id = Civilizacao.civ_id
GO

-- Select from the View
SELECT * FROM Empires.Jogadores_Full_Info


/*##########################################/
/###########################################/
/##########################################*/


/* View for the Object Forms */

-- Drop the View
DROP VIEW  Empires.Jogadores_Objects

--  Create the View
GO
CREATE VIEW Empires.Jogadores_Objects AS
	SELECT jogador_id AS ID, Objeto.Nome AS OBJETO, Objeto.Obj_Id AS OBJ_ID
	FROM Empires.Jogador LEFT OUTER JOIN Empires.Objeto
			ON Jogador.jogador_id = Objeto.FK_jogador_id_tem
	GROUP BY jogador_id, Objeto.Nome, Objeto.Obj_Id
GO


-- Select from the View
SELECT * FROM Empires.Jogadores_Objects