DROP TRIGGER Empires.trg_DeleteJogador
GO

DROP TRIGGER Empires.trg_DeleteGrandeCiv
GO

DROP TRIGGER Empires.trg_DeleteCiv
GO

-- Criação do Trigger trg_DeleteJogador
CREATE TRIGGER trg_DeleteJogador
ON Empires.Jogador
INSTEAD OF DELETE
AS
BEGIN
  SET NOCOUNT ON;

  -- Deletar objetos relacionados ao jogador
  DELETE FROM Empires.Objeto
  WHERE FK_jogador_id_tem IN (SELECT jogador_id FROM deleted)
     OR FK_jogador_id_elimina IN (SELECT jogador_id FROM deleted);

  -- Deletar o jogador
  DELETE FROM Empires.Jogador
  WHERE jogador_id IN (SELECT jogador_id FROM deleted);
END;

GO

-- Criação do Trigger trg_DeleteGrandeCiv
CREATE TRIGGER trg_DeleteGrandeCiv
ON Empires.Grande_Civilizacao
INSTEAD OF DELETE
AS
BEGIN
    SET NOCOUNT ON;

    -- Deletar jogadores relacionados à Grande Civilizacao
    DELETE FROM Empires.Jogador
    WHERE FK_grande_id IN (SELECT FK_civ_id FROM deleted);

    -- Deletar o jogador
    DELETE FROM Empires.Grande_Civilizacao
    WHERE FK_civ_id IN (SELECT FK_civ_id FROM deleted);
END;

GO

-- Criação do Trigger trg_DeleteCiv
CREATE TRIGGER trg_DeleteCiv
ON Empires.Civilizacao
INSTEAD OF DELETE
AS
BEGIN
    SET NOCOUNT ON;

    -- Deletar a Civ relacionados à Civilizacao
    DELETE FROM Empires.Grande_Civilizacao
    WHERE FK_civ_id IN (SELECT civ_id FROM deleted);

	DELETE FROM Empires.Pequena_Civilizacao
    WHERE FK_civ_id IN (SELECT civ_id FROM deleted);

	DELETE FROM Empires.Alia
    WHERE FK_civ_id IN (SELECT civ_id FROM deleted);

    -- Deletar a Civilizacao
    DELETE FROM Empires.Civilizacao
    WHERE civ_id IN (SELECT civ_id FROM deleted);
END;

GO
