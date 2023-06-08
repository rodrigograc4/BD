/* ##### SCHEMA ##### */
-- Create the Schema
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'Empires')
BEGIN
	EXEC('CREATE SCHEMA Empires')
END

GO

/* ##### TABLES ##### */

-- Era
CREATE TABLE Empires.Era (
	era_id		INT,
	nome		  VARCHAR(50)		NOT NULL,
	PRIMARY KEY (era_id),
); 

GO

-- Civilizacao
CREATE TABLE Empires.Civilizacao (
	civ_id			INT,		
	nome		    VARCHAR(50)		NOT NULL,

	PRIMARY KEY (civ_id),
);

GO

CREATE TABLE Empires.Civilizacao_Tropas (
	tropa			VARCHAR(50),
	FK_civ_id		INT,

	CONSTRAINT PK_Civilizacao_Tropas PRIMARY KEY (FK_civ_id, tropa),
	FOREIGN KEY (FK_civ_id)		REFERENCES Empires.Civilizacao(civ_id)
					ON DELETE CASCADE			ON UPDATE CASCADE,
); 

GO

-- Pequena Civilizacao
CREATE TABLE Empires.Pequena_Civilizacao (
	limite_tropas	INT		NOT NULL,
	FK_civ_id		INT,

	PRIMARY KEY (FK_civ_id),
	FOREIGN KEY (FK_civ_id) REFERENCES Empires.Civilizacao(civ_id),
); 
 
GO

-- Grande Civilizacao
CREATE TABLE Empires.Grande_Civilizacao (
    lider VARCHAR(50) NOT NULL,	
    capital VARCHAR(50) NOT NULL,
    FK_civ_id INT,
    PRIMARY KEY (FK_civ_id),
    FOREIGN KEY (FK_civ_id) REFERENCES Empires.Civilizacao(civ_id),
);

GO

CREATE TABLE Empires.Cartas_Civilizacao (
	carta			  VARCHAR(50)		NOT NULL,
	FK_civ_id		INT,

	CONSTRAINT PK_Cartas_Civilizacao PRIMARY KEY (FK_civ_id, carta),
	FOREIGN KEY (FK_civ_id)		REFERENCES Empires.Civilizacao(civ_id)
					ON DELETE CASCADE			ON UPDATE CASCADE,

); 

GO

-- Equipa
CREATE TABLE Empires.Equipa (
	equipa_id		int
	PRIMARY KEY (equipa_id)
); 

GO

-- Jogador
CREATE TABLE Empires.Jogador (
	jogador_id		INT,
	nome			VARCHAR(50)		NOT NULL,
	clan			VARCHAR(50),
	cor				VARCHAR(50),
	FK_era_id		INT				NOT NULL,
	FK_grande_id	INT 			NOT NULL,
	FK_equipa_id	INT				NOT NULL,

	PRIMARY KEY (jogador_id),
	FOREIGN KEY (FK_era_id)			REFERENCES Empires.Era(era_id),
	FOREIGN KEY (FK_grande_id)		REFERENCES Empires.Grande_Civilizacao(FK_civ_id),
	FOREIGN KEY (FK_equipa_id)		REFERENCES Empires.Equipa(equipa_id),
); 

GO

CREATE TABLE Empires.Cartas_Jogador (
	carta			VARCHAR(50)		NOT NULL,
	FK_jogador_id	INT,

	CONSTRAINT PK_Cartas_Jogador PRIMARY KEY (FK_jogador_id, carta),
	FOREIGN KEY (FK_jogador_id)		REFERENCES Empires.Jogador(jogador_id)
					ON DELETE CASCADE			ON UPDATE CASCADE,
); 

GO

CREATE TABLE Empires.Alia (
	FK_civ_id		INT,
	FK_jogador_id	INT,

	PRIMARY KEY (FK_civ_id, FK_jogador_id),
	FOREIGN KEY (FK_civ_id)		REFERENCES Empires.Civilizacao(civ_id),
	FOREIGN KEY (FK_jogador_id)	REFERENCES Empires.Jogador(jogador_id)
					ON DELETE CASCADE			ON UPDATE CASCADE,
); 

GO

CREATE TABLE Empires.Objeto (
  Nome					    VARCHAR(50)   NOT NULL,
  Localizacao_X			INT,
  Localizacao_Y			INT,
  Obj_Id				    INT,
  FK_Era_Id			    INT           NOT NULL,
  FK_jogador_id_tem		INT,
  FK_jogador_id_elimina		INT,

  PRIMARY KEY (Obj_Id),
  FOREIGN KEY (FK_Era_Id) REFERENCES Empires.Era (Era_Id),
  FOREIGN KEY (FK_jogador_id_tem) REFERENCES Empires.Jogador (jogador_id),
  FOREIGN KEY (FK_jogador_id_elimina) REFERENCES Empires.Jogador (jogador_id),
);

GO

CREATE TABLE Empires.Prop (
  Recurso				  VARCHAR(50),
  Quantidade			INT,
  FK_Obj_Id				INT,
  PRIMARY KEY (FK_Obj_Id),
  FOREIGN KEY (FK_Obj_Id) REFERENCES Empires.Objeto (Obj_Id)
  				ON DELETE CASCADE			ON UPDATE CASCADE,
); 

GO


CREATE TABLE Empires.Tecnologia (
  Entidade		    VARCHAR(50)   NOT NULL,
  Efeito 		  VARCHAR(50)   NOT NULL,
  FK_Obj_Id				INT,
  PRIMARY KEY (FK_Obj_Id),
  FOREIGN KEY (FK_Obj_Id) REFERENCES Empires.Objeto (Obj_Id)
  				ON DELETE CASCADE			ON UPDATE CASCADE,
); 

GO


CREATE TABLE Empires.Edificios (
  Pontos_de_Vida		    INT         NOT NULL,
  Tempo_de_Construcao	  INT         NOT NULL,
  Tipo					        VARCHAR(50) NOT NULL,
  Line_of_Sight			    INT,
  Xp_de_Construcao		  INT         NOT NULL,
  N_Max_de_Construtores	INT,
  FK_Obj_Id				      INT,
  PRIMARY KEY (FK_Obj_Id),
  FOREIGN KEY (FK_Obj_Id) REFERENCES Empires.Objeto (Obj_Id)
  				ON DELETE CASCADE			ON UPDATE CASCADE,
); 

GO


CREATE TABLE Empires.Unidades (
  Pontos_de_Vida		INT   NOT NULL,
  Velocidade			  INT   NOT NULL,
  Resistencia			  INT,
  Line_of_Sight			INT,
  FK_Obj_Id				  INT   NOT NULL,
  PRIMARY KEY (FK_Obj_Id),
  FOREIGN KEY (FK_Obj_Id) REFERENCES Empires.Objeto (Obj_Id)
  				ON DELETE CASCADE			ON UPDATE CASCADE,
); 

GO

CREATE TABLE Empires.Heroi (
  Pontos_Recuperados	  INT,
  Habilidades			      VARCHAR(50),
  Ataque_Corpo_a_Corpo	INT,
  Ataque_Cerco			    INT,
  Ataque_a_Distancia	  INT,
  FK_Uni_Id				      INT NOT NULL,
  PRIMARY KEY (FK_Uni_Id),
  FOREIGN KEY (FK_Uni_Id) REFERENCES Empires.Unidades (FK_Obj_Id)
  				ON DELETE CASCADE			ON UPDATE CASCADE,
);

GO

CREATE TABLE Empires.Unidades_Treinaveis (
  Populacao				      INT     NOT NULL,
  Tempo_de_Recruta		  INT     NOT NULL,
  FK_Uni_Id				      INT,
  PRIMARY KEY (FK_Uni_Id),
  FOREIGN KEY (FK_Uni_Id) REFERENCES Empires.Unidades (FK_Obj_Id)
  				ON DELETE CASCADE			ON UPDATE CASCADE,
); 

GO

CREATE TABLE Empires.Civil (
  Velocidade_Colheita	  INT     NOT NULL,	
  Limite_de_Unidades	  INT     NOT NULL,
  FK_UT_Id				      INT,
  PRIMARY KEY (FK_UT_Id),
  FOREIGN KEY (FK_UT_Id) REFERENCES Empires.Unidades_Treinaveis (FK_Uni_Id)
  				ON DELETE CASCADE			ON UPDATE CASCADE,
); 

GO

CREATE TABLE Empires.Infantaria (
  Ataque_Corpo_a_Corpo			INT     NOT NULL,	
  Ataque_a_Distancia			 INT,
  FK_UT_Id				      INT,
  PRIMARY KEY (FK_UT_Id),
  FOREIGN KEY (FK_UT_Id) REFERENCES Empires.Unidades_Treinaveis (FK_Uni_Id)
				ON DELETE CASCADE			ON UPDATE CASCADE,
); 

GO

CREATE TABLE Empires.Cavalaria (
  Dano_em_Area			      INT   NOT NULL,	
  Ataque_Corpo_a_Corpo	  INT   NOT NULL,
  FK_UT_Id				        INT,
  PRIMARY KEY (FK_UT_Id),
  FOREIGN KEY (FK_UT_Id) REFERENCES Empires.Unidades_Treinaveis (FK_Uni_Id)
  				ON DELETE CASCADE			ON UPDATE CASCADE,
);

GO

CREATE TABLE Empires.Artilharia (
  Velocidade_Montagem	  INT     NOT NULL,	
  Ataque_Cerco			    INT     NOT NULL,
  FK_UT_Id				      INT,
  PRIMARY KEY (FK_UT_Id),
  FOREIGN KEY (FK_UT_Id) REFERENCES Empires.Unidades_Treinaveis (FK_Uni_Id)
  				ON DELETE CASCADE			ON UPDATE CASCADE,
);

GO		

CREATE TABLE Empires.Naval (
  Limite_de_Unidades	  INT NOT NULL,	
  FK_UT_Id				      INT,
  PRIMARY KEY (FK_UT_Id),
  FOREIGN KEY (FK_UT_Id) REFERENCES Empires.Unidades_Treinaveis (FK_Uni_Id)
  				ON DELETE CASCADE			ON UPDATE CASCADE,
); 

GO

CREATE TABLE Empires.Animal (
  Food					        INT,	
  Min_Food				      INT,	
  Max_Food				      INT,
  Ataque_Corpo_a_Corpo	INT,
  FK_UT_Id				      INT,
  PRIMARY KEY (FK_UT_Id),
  FOREIGN KEY (FK_UT_Id) REFERENCES Empires.Unidades_Treinaveis (FK_Uni_Id)
  				ON DELETE CASCADE			ON UPDATE CASCADE,
); 

GO	
