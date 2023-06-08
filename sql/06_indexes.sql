CREATE CLUSTERED INDEX idxBigCiv          ON Empires.Grande_Civilizacao(FK_civ_id);
CREATE CLUSTERED INDEX idxPlayer          ON Empires.Jogador(jogador_id);
CREATE CLUSTERED INDEX idxPlayersEra      ON Empires.Jogador(FK_era_id);
CREATE CLUSTERED INDEX idxObject          ON Empires.Objeto(Obj_Id);
CREATE CLUSTERED INDEX idxObjectsEra      ON Empires.Objeto(FK_Era_Id);
CREATE CLUSTERED INDEX idxObjectsOwner    ON Empires.Objeto(FK_jogador_id_tem);
CREATE CLUSTERED INDEX idxObjectsKiller   ON Empires.Objeto(FK_jogador_id_elimina);
CREATE CLUSTERED INDEX idxTech            ON Empires.Tecnologia(FK_Obj_Id);
           