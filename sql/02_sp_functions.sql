-- Stored Procedures:
-- Procedure para inserir uma nova civilizacao na base de dados

GO	
DROP PROC Empires.InsertCivilizacao;
DROP PROC Empires.UpdateJogador;
GO

GO
CREATE PROC Empires.InsertCivilizacao
		@Nome			VARCHAR(50), 
		@FK_grande_id	INT
    AS
    BEGIN
        SET NOCOUNT ON;
        INSERT INTO Empires.Civilizacao (Nome, civ_id)
        VALUES (@Nome, @FK_grande_id);
    END
GO

-- Procedure para atualizar um jogador na base de dados
CREATE PROC Empires.UpdateJogador 
		@Nome			VARCHAR(50),
		@FK_civ_id		INT,
        @FK_equipa_id	INT,
        @jogador_id		INT
    AS
    BEGIN
        SET NOCOUNT ON;
        UPDATE Empires.Jogador
        SET Nome = @Nome,
            FK_grande_id = @FK_civ_id,
            FK_equipa_id = @FK_equipa_id
        WHERE jogador_id = @jogador_id;
    END


-- User Defined Functions:

GO
DROP FUNCTION Empires.GetCivilizacaoCount;
DROP FUNCTION Empires.GetTotalObjectsPerPlayer;
DROP FUNCTION Empires.GetTotalCardsPerCivilizacao;
DROP FUNCTION Empires.GetTotalCardsPerPlayer;
GO

GO
CREATE FUNCTION Empires.GetCivilizacaoCount()
RETURNS INT
AS
BEGIN
  DECLARE @Count INT;
  SELECT @Count = COUNT(*) FROM Empires.Civilizacao;
  RETURN @Count;
END
GO

/* Não está implementado
GO
CREATE FUNCTION Empires.GetPlayerScore(@jogador_id INT)
RETURNS INT
AS
BEGIN
  DECLARE @Score INT;
  -- Calcular o score do jogador
  -- Soma dos objetos??
  RETURN @Score;
END
GO

GO
CREATE FUNCTION Empires.GetTeamScore(@equipa_id INT)
RETURNS INT
AS
BEGIN
  DECLARE @TotalScore INT;
  SELECT @TotalScore = SUM(dbo.Empires.GetPlayerScore(jogador_id))
  FROM Empires.Jogador
  WHERE FK_equipa_id = @equipa_id;
  RETURN @TotalScore;
END
GO
*/

GO
CREATE FUNCTION Empires.GetTotalObjectsPerPlayer(@jogador_id INT)
RETURNS INT
AS
BEGIN
  DECLARE @TotalObjects INT;
  SELECT @TotalObjects = COUNT(*) 
  FROM Empires.Objeto
  WHERE FK_jogador_id_tem = @jogador_id OR FK_jogador_id_elimina = @jogador_id;
  RETURN @TotalObjects;
END
GO

GO
CREATE FUNCTION Empires.GetTotalCardsPerCivilizacao(@civ_id INT)
RETURNS INT
AS
BEGIN
  DECLARE @TotalCards INT;
  SELECT @TotalCards = COUNT(*)
  FROM Empires.Cartas_Civilizacao
  WHERE FK_civ_id = @civ_id;
  RETURN @TotalCards;
END
GO

GO
CREATE FUNCTION Empires.GetTotalCardsPerPlayer(@jogador_id INT)
RETURNS INT
AS
BEGIN
  DECLARE @TotalCards INT;
  SELECT @TotalCards = COUNT(*)
  FROM Empires.Cartas_Jogador
  WHERE FK_jogador_id = @jogador_id;
  RETURN @TotalCards;
END
GO