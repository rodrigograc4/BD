"""
This Script is made to generate a 1000 objects.
"""

from enum import Enum
from random import sample, randint as ri, randrange as rr


class Eras:
    text = "Era (era_id, nome)"
    start = 1
    end = 6

    class own(Enum):
        Discovery_Age = 1
        Colonial_Age = 2
        Fortress_Age = 3
        Industrial_Age = 4
        Imperial_Age = 5


class Civ:
    text = "Civilizacao (civ_id, nome)"
    start = 1
    end = 21

    class own(Enum):
        Gaia = 0
        British = 1
        Dutch = 2
        Fremch = 3
        Germans = 4
        Ottomans = 5
        Portuguese = 6
        Russians = 7
        Spanish = 8
        Aztecs = 9
        Carib = 10
        Cherokee = 11
        Comanche = 12
        Cree = 13
        Quechua = 14
        Iroquois = 15
        Lakota = 16
        Maya = 17
        Nootka = 18
        Seminole = 19
        Tupi = 20


class Troops:
    text = "Civilizacao_Tropas (tropa, FK_civ_id)"
    start = 1
    end = 31

    units = {
        1: "Musketeer",
        2: "Crossbowman",
        3: "Hussar",
        4: "Cuirassier",
        5: "Falconet",
        6: "Skirmisher",
        7: "Halberdier",
        8: "Cavalry Archer",
        9: "Dragoon",
        10: "Frigate",
        11: "Monitor",
        12: "War Dog",
        13: "Mortar",
        14: "Rocket",
        15: "Factory Wagon",
        16: "Villager",
        17: "Gatling Gun",
        18: "Archer",
        19: "Longbowman",
        20: "Ulhans",
        21: "Doppelsoldner",
        22: "War Wagon",
        23: "Cassador",
        24: "Pikeman",
        25: "Rodelero",
        26: "Lancer",
        27: "Culverin",
        28: "Pikeman",
        29: "Abus Gun",
        30: "Grenadier",
    }

    Infantry = {
        1: "Musketeer",
        2: "Crossbowman",
        3: "Skirmisher",
        4: "Cassador",
        5: "Archer",
        6: "Longbowman",
        7: "Doppelsoldner",
        8: "Halberdier",
        9: "Pikeman",
        10: "Rodelero",
    }

    Cavalry = {
        1: "Hussar",
        2: "Cuirassier",
        3: "Cavalry Archer",
        4: "Dragoon",
        5: "Ulhans",
        6: "War Wagon",
        7: "Lancer",
    }

    Artillery = {
        1: "Falconet",
        2: "Mortar",
        3: "Rocket",
        4: "Gatling Gun",
        5: "Culverin",
        6: "Abus Gun",
        7: "Grenadier",
    }

    Naval = {
        1: "Frigate",
        2: "Monitor",
        3: "Fire Ship",
        4: "Caravel",
        5: "Galleon",
    }

    Animals = {
        1: "War Dog",
        2: "Cougar",
        3: "Jaguar",
        4: "Puma",
        5: "Llama",
        6: "Cow",
        7: "Sheep",
    }


class Big_Civ:
    text = "Civilizacao (lider, capital, FK_civ_id)"
    start = 1
    end = 9

    lider = {
        0: "Mother Earth",
        1: "Queen Elizabeth I",
        2: "Stadtholder William III",
        3: "King Louis XIV",
        4: "Frederick the Great",
        5: "Sultan Suleiman I",
        6: "Henry the Navigator",
        7: "Ivan the Terrible",
        8: "Queen Isabella I",
    }

    capital = {
        0: "Earth",
        1: "London",
        2: "Amsterdam",
        3: "Paris",
        4: "Berlin",
        5: "Istanbul",
        6: "Lisbon",
        7: "Moscow",
        8: "Madrid",
    }


class Smol_Civ:
    text = "Civilizacao (limite_tropas, FK_civ_id)"
    start = 9
    end = 21


class Cartas:
    textCiv = "Cartas_Civilizacao (carta, FK_civ_id)"
    textPlayer = "Cartas_Era (carta, FK_jogador_id)"
    start = 1
    end = 31

    cards = {
        1: "2 Explorers",
        2: "6 Musketeers",
        3: "8 Crossbowmen",
        4: "3 Hussars",
        5: "3 Cuirassiers",
        6: "2 Falconets",
        7: "2 Cannons",
        8: "1 Artillery Foundry",
        9: "10 Skirmishers",
        10: "12 Halberdiers",
        11: "8 Cavalry Archers",
        12: "10 Dragoons",
        13: "5 Warships",
        14: "2 Frigates",
        15: "2 Monitors",
        16: "1 Factory",
        17: "1 Blockhouse",
        18: "1 Gatling Gun",
        19: "1 Fortress",
        20: "2 War Dogs",
        21: "2 Mortars",
        22: "3 Rockets",
        23: "1 Factory",
        24: "3 Villagers",
        25: "8 Villagers",
        26: "Schooner",
        27: "2 Barracks",
        28: "1 Stable",
        29: "1 Advanced Arsenal",
        30: "1 Church",
    }


class Equipa:
    text = "Equipa (equipa_id)"
    ## 1 = Team 1
    ## 2 = Team 2

    def getEquipas():
        ins.write("\t(1),\n\t(2),\n\t(3);\n")


class Jogador:
    text = (
        "Jogador (jogador_id, nome, clan, cor, FK_equipa_id, FK_grande_id, FK_era_id)"
    )
    startColor = 1
    endColor = 10

    startName = 1
    endName = 51

    startClan = 1
    endClan = 6

    colors = {
        1: "Brown",
        2: "Blue",
        3: "Red",
        4: "Yellow",
        5: "Purple",
        6: "Green",
        7: "Orange",
        8: "Light Blue",
        9: "Pink",
    }

    players = {
        1: "TheViper",
        2: "Hera",
        3: "Daut",
        4: "Yo",
        5: "Liereyy",
        6: "Mr_Yo",
        7: "Mbl",
        8: "Tatoh",
        9: "Nicov",
        10: "Vivi",
        11: "Dogao",
        12: "Jordan_23",
        13: "Spring",
        14: "F1Re",
        15: "TheMax",
        16: "St4rk",
        17: "MbL40C",
        18: "Vinchester",
        19: "ACCM",
        20: "Goku",
        21: "BacT",
        22: "H2O",
        23: "Edie",
        24: "JorDan_AoE",
        25: "Ming",
        26: "Aizamk",
        27: "Kasva",
        28: "Nicov_AoE",
        29: "KaiserKlein",
        30: "Chris",
        31: "Nili",
        32: "Yoggi",
        33: "Zuppi",
        34: "Islands",
        35: "Iamgrunt",
        36: "Kimo",
        37: "DracKeN",
        38: "Kynesie",
        39: "EAGLEMUT",
        40: "Kamigawa",
        41: "K1NGS",
        42: "HansKarlo",
        43: "Myth",
        44: "HaRRy",
        45: "SwagginGator",
        46: "HeHe",
        47: "PraT",
        48: "Stonewall",
        49: "ASavage",
        50: "MusketJr",
    }

    clans = {1: "ESOC", 2: "AOEC", 3: "TWL", 4: "TLS", 5: "NNN"}


class Alia:
    text = "Alia (FK_civ_id, FK_jogador_id)"

    def getAlia():
        ins.write("\t(10,1)\n" "\t(14,2)\n" "\t(13,3)\n" "\t(19,1)\n")


class Tech:
    text = "Technology (nome, x, y, FK_era_ID, FK_Obj_ID, FK_jogador_ID, Entidade, Quantidade, Parametro)"

    names = {
        1: "Bastion",
        2: "Steam Power",
        3: "Placer Mines",
        4: "Gang Saw",
        5: "Hunting Dogs",
        6: "Infantry Breastplate",
        7: "Flint Lock",
        8: "Socket Bayonet",
        9: "Cavalry Cuirass",
        10: "Broad Shot",
    }

    entity = {
        1: "Building",
        2: "Factory",
        3: "Villager",
        4: "Villager",
        5: "Villager",
        6: "Infantry",
        7: "Infantry",
        8: "Infantry",
        9: "Cavalry",
        10: "Warships",
    }

    effect = {
        1: "+1000 health",
        2: "x2 gather rate",
        3: "+10 gold gather rate",
        4: "+10 wood gather rate",
        5: "+10 food gather rate",
        6: "+10 health",
        7: "+10 ranged attack",
        8: "+10 melee attack",
        9: "+10 health",
        10: "+20 ranged attack",
    }


class Prop:
    text = "Prop (nome, x, y, FK_era_ID, FK_jogador_ID, resource, quantity)"

    food = {1: 300, 2: 400, 3: 500, 4: 600}

    wood = {
        1: 300,
        2: 400,
        3: 500,
    }

    gold = {
        1: 1000,
        2: 2000,
        3: 5000,
    }


# Other usefull Enums


# Other usefull functions


class Utils:
    def gameType():
        ## Needs some rework
        game = ri(1, 7)
        if game == 6:
            return null
        if game == 7:
            return ri(1, 5)
        return game * 2

        class own(Enum):
            One_v_One = 1
            Two_v_Two = 2
            Three_v_Three = 3
            Four_v_Four = 4
            Free_For_All = 5
            Inbalanced = 6

    def jogadors(numJogadors):
        return sample(range(1, 51), numJogadors)

    def cors(numJogadors):
        return sample(range(1, 9), numJogadors)

    def locations(grain):
        return sample(range(1, 1001), grain)


def main():
    def fInsert(thing, func, start, end):
        ins.write(f"{insert}{thing.text} {values}\n")
        for i in range(start, end):
            if i == end - 1:
                ins.write(f"{func(i)};\n")
            else:
                ins.write(f"{func(i)},\n")

    def eraNciv():
        stuff = [Eras, Civ]

        # Eras, Civilizações
        for thing in stuff:
            text = lambda i: f"\t({i}, '{thing.own(i).name.replace('_', ' ')}')"
            fInsert(thing, text, thing.start, thing.end)

    def bigCiv():
        ins.write(f"{insert}{Big_Civ.text} {values}\n")

        ins.write(
            f"\t('{Big_Civ.lider.Lider_0.value}', '{Big_Civ.capital.Capital_0.value}', 0)\n\t('{Big_Civ.lider.Lider_1.value}', '{Big_Civ.capital.Capital_1.value}', 1)\n\t('{Big_Civ.lider.Lider_2.value}', '{Big_Civ.capital.Capital_2.value}', 2)\n\t('{Big_Civ.lider.Lider_3.value}', '{Big_Civ.capital.Capital_3.value}', 3)\n\t('{Big_Civ.lider.Lider_4.value}', '{Big_Civ.capital.Capital_4.value}', 4)\n\t('{Big_Civ.lider.Lider_5.value}', '{Big_Civ.capital.Capital_5.value}', 5)\n\t('{Big_Civ.lider.Lider_6.value}', '{Big_Civ.capital.Capital_6.value}', 6)\n\t('{Big_Civ.lider.Lider_7.value}', '{Big_Civ.capital.Capital_7.value}', 7)\n\t('{Big_Civ.lider.Lider_8.value}', '{Big_Civ.capital.Capital_8.value}', 8)\n"
        )

    # text = lambda i: f"\t({ri(7, 14)}, {i})"
    # fInsert(Smol_Civ, text, Smol_Civ.start, Smol_Civ.end)

    outputFile = "game1.sql"
    insert = "\nINSERT INTO Empires."
    values = "\nVALUES"
    null = "null"

    ins.write("USE p4g6\nGO\n")

    # gameType = gameType()

    """eraNciv()"""

    # "Civilizacao (lider, capital, FK_civ_id)"
    # text = (
    #     lambda i: f"\t('{Big_Civ.lider(f'Lider_{i}}', '{Big_Civ.capital(f'Capital_{i}')}', {i})"
    # )
    # fInsert(Big_Civ, text, 1, 31)

    ## By hand Grande Civilização

    """Big_Civ()"""

    """
    # Civilizações Tropas,
    troopie = rr(1, 31)
    text = (
        lambda i: f"\t('{Troops.own(jogadors[i-1]).name.replace('_', ' }', '{Jogador.Names(jogadors[i-1]).name.replace('_', ' }', '{Jogador.Clans(ri(1, 5)).name.replace('_', ' }', '{Jogador.Cor(cors[i-1]).name.replace('_', ' }"
    )
    fInsert(Jogador, text, 1, numJogadors + 1)

    
    jogadors = Utils.jogadors(numJogadors)
    cors = Utils.cors(numJogadors)

    """
    # Jogador
    # text = (
    #     lambda i: f"\t({i}, '{Jogador.Names(jogadors[i-1]).name.replace('_', ' }', '{Jogador.Clans(ri(1, 5)).name.replace('_', ' }', '{Jogador.Cor(cors[i-1]).name.replace('_', ' }"
    # )
    # fInsert(Jogador, text, 1, numJogadors + 1)

    # 1000 objects
    #    :
    # 400 props
    # 100 techs (doesnt need position)
    # 50 buildings
    # 4 heroes
    # 100 civil
    # 200 infantry
    # 46 cavalry
    # 8 artillery
    # 100 ships
    # 300 animals

    locations = Utils.locations(1000)
    numJogadors = 6

    # Create Techs (no need for locations)
    # ins.write(f"{insert} {Tech.text} {values}")

    # Tropas_Civ & Cartas_Civ
    """
    ins.write(f"{insert}{Troops.text} {values}\n")
    for i in range(1, 7):
        for j in range(1, ri(7, 12)):
            ins.write(f"\t({Troops.units.get(ri(Troops.start, Troops.end))}, {i}),\n")
    """

    """
    ins.write(f"{insert}{Cartas.textCiv} {values}\n")
    for i in range(1, 7):
        for j in range(1, ri(7, 12)):
            ins.write(f"\t('{Cartas.cards.get(ri(Cartas.start, Cartas.end))}', {i}),\n")

    ins.write(f"{insert}{Cartas.textPlayer} {values}\n")
    for i in range(1, 7):
        for j in range(1, ri(7, 12)):
            ins.write(f"\t('{Cartas.cards.get(ri(Cartas.start, Cartas.end))}', {i}),\n")
    """

    """
    color = Utils.cors(numJogadors)
    print(color)

    ins.write(f"{insert}{Jogador.text} {values}\n")
    for i in range(1, 7):
        ins.write(
            f"\t({i},'{Jogador.players.get(ri(Jogador.startName, Jogador.endName))}','{Jogador.clans.get(ri(Jogador.startClan, Jogador.endClan))}','{Jogador.colors.get(color[i-1])}',{i%3+1},{ri(Big_Civ.start, Big_Civ.end)},{ri(Eras.start+1, Eras.end)}),\n"
        )

    ins.write(f"{insert}{Alia.text} {values}\n")
    for i in range(1, 7):
        if ri(1, 10) > 4:
            for j in range(1, ri(1, 4)):
                ins.write(f"\t({ri(Smol_Civ.start, Smol_Civ.end)}, {i}),\n")

    """

    def makeObjects(make):
        for i in range(10):
            make(i)

    """
    Empires.Objeto
    Empires.Prop
    Empires.Tecnologia
    Empires.Edificios
    Empires.Unidades
    Empires.Heroi
    Empires.Unidades_Treinaveis
    Empires.Civil
    Empires.Infantaria
    Empires.Cavalaria
    Empires.Artilharia
    Empires.Naval
    Empires.Animal
    """

    amount = 10
    indx = 1

    def start(text):
        ins.write(f"{insert}{text} {values}\n")

    # Empires.Objeto
    text = "Objeto (Nome, Localizacao_X, Localizacao_Y, FK_Era_Id, FK_jogador_id_tem, FK_jogador_id_elimina, Obj_Id)"
    obj.write(f"{insert}{text} {values}\n")

    def makeObject(ObjectName, objId, x=1, y=1):
        if x != "null" or y != "null":
            x = ri(0, 100)
            y = ri(0, 100)
        isDead = ri(1, 6) if ri(1, 10) < 3 else "null"
        max = ri(1, 3) * 4 if ri(1, 11) < 2 else "null"
        obj.write(
            f"\t('{ObjectName}', {x}, {y}, {ri(Eras.start, Eras.end-1)}, {ri(1, 6)}, {isDead}, {objId}),\n"
        )

    # Empires.Prop

    type = {
        1: "Food",
        2: "Wood",
        3: "Gold",
        4: "Stone",
    }

    text = "Prop (Recurso, Quantidade, FK_Obj_Id)"
    start(text)
    for i in range(200):
        Type = type[ri(1, 4)]
        makeObject("Prop" + str(i), indx)
        ins.write(f"\t('{Type}', {ri(1, 5)*100}, {indx}),\n")
        indx += 1

    # Empires.Tecnologia
    text = "Tecnologia (Entidade, Efeito, FK_Obj_Id)"
    start(text)
    for i in range(1, 11):
        makeObject("Tech" + str(i), indx, "null", "null")
        ins.write(f"\t('{Tech.entity[i]}', '{Tech.effect[i]}', {indx}),\n")
        indx += 1

    # Empires.Edificios
    type = {
        1: "Military",
        2: "Economic",
        3: "Religious",
        4: "Defensive",
    }

    text = "Edificios (Pontos_de_Vida, Tempo_de_Construcao, Tipo, Line_of_Sight, Xp_de_Construcao, N_Max_de_Construtores, FK_Obj_Id)"
    start(text)
    for i in range(50):
        makeObject("Building" + str(i), indx)
        hp = ri(2, 6) * 500
        max = ri(1, 3) * 4 if ri(1, 11) < 2 else "null"
        ins.write(
            f"\t({hp}, {ri(1, 5)*30}, '{type[ri(1, 4)]}', {ri(0, 13)}, {hp/100}, {max}, {indx}),\n"
        )
        indx += 1

    # Empires.Unidades

    def makeUnit(hp, speed, resist="null", sight="null"):
        uni.write(f"\t({hp}, {speed}, {resist}, {sight}, {indx}),\n")

    text = (
        "Unidades (Pontos_de_Vida, Velocidade, Resistencia, Line_of_Sight, FK_Obj_Id)"
    )

    uni.write(f"{insert}{text} {values}\n")

    # Empires.Heroi
    habs = {
        1: "Sharpshooter",
        2: "Healer",
        3: "Scout",
        4: "Builder",
    }

    text = "Heroi (Pontos_Recuperados, Habilidades, Ataque_Corpo_a_Corpo, Ataque_Cerco, Ataque_a_Distancia, FK_Uni_Id)"
    start(text)
    for i in range(numJogadors):
        makeObject("Hero" + str(i), indx)
        max = ri(1, 3) * 4 if ri(1, 11) < 2 else "null"
        makeUnit(
            ri(6, 13) * 10,
            ri(3, 5),
            ri(1, 4),
            ri(7, 12),
        )
        ins.write(
            f"\t({ri(1, 5)*10}, '{habs[ri(1, 4)]}', {ri(10, 21)}, {ri(10, 21)}, {ri(10, 21)}, {indx}),\n"
        )
        indx += 1

    # Empires.Unidades_Treinaveis       #50

    def makeTrainable(pop, time):
        train.write(f"\t({pop}, {time}, {indx}),\n")

    text = "Unidades_Treinaveis (Populacao, Tempo_de_Recruta, FK_Uni_Id)"
    train.write(f"{insert}{text} {values}\n")

    # Empires.Civil         #10
    text = "Civil (Velocidade_Colheita, Limite_de_Unidades, FK_UT_Id)"
    start(text)
    for i in range(numJogadors * 10):
        makeObject("Civil" + str(i), indx)
        makeUnit(
            ri(1, 21) * 5,
            ri(3, 5),
            ri(0, 2),
            ri(7, 12),
        )
        makeTrainable(1, 40)
        ins.write(f"\t({ri(4, 5)*10}, {ri(3, 8)}, {indx}),\n")
        indx += 1

    # Empires.Infantaria    #10
    text = "Infantaria (Ataque_Corto_a_Corpo, Ataque_a_Distancia, FK_UT_id)"
    start(text)
    for i in range(numJogadors * 10):
        unit = ri(1, 10)
        makeObject(str(Troops.Infantry[unit]) + str(i), indx)
        makeUnit(
            ri(12, 24) * 5,
            ri(3, 5),
            ri(1, 3),
            ri(7, 12),
        )
        makeTrainable(1, 40)
        ranged = ri(12, 20) if unit > 6 else "null"
        melee = ri(14, 20) if unit > 6 else ri(6, 12)
        ins.write(f"\t({melee}, {ranged}, {indx}),\n")
        indx += 1

    # Empires.Cavalaria    #10
    text = "Cavalaria (Dano_em_Area, Ataque_Corto_a_Corpo, FK_UT_id)"
    start(text)
    for i in range(numJogadors * 10):
        makeObject(str(Troops.Cavalry[ri(1, 7)]) + str(i), indx)
        makeUnit(
            ri(1, 21) * 5,
            ri(3, 5),
            ri(0, 2),
            ri(7, 12),
        )
        makeTrainable(2, 50)
        ins.write(f"\t({ri(0, 2)}, {ri(10, 21)}, {indx}),\n")
        indx += 1

    # Empires.Artilharia    #10
    text = "Artilharia (Velocidade_Montagem, Ataque_Cerco, FK_UT_id)"
    start(text)
    for i in range(numJogadors * 10):
        makeObject(str(Troops.Artillery[ri(1, 7)]) + str(i), indx)
        makeUnit(
            ri(12, 21) * 10,
            ri(1, 4),
            ri(0, 2),
            ri(10, 15),
        )
        makeTrainable(ri(4, 5), 60)
        ins.write(f"\t({ri(4, 5)}, {ri(25, 35)}, {indx}),\n")
        indx += 1

    # Empires.Naval  #10
    text = "Naval (Limite_de_Unidades, FK_UT_Id)"
    start(text)
    for i in range(numJogadors * 10):
        makeObject(str(Troops.Naval[ri(1, 5)]) + str(i), indx)
        makeUnit(
            ri(30, 45) * 10,
            ri(5, 7),
            ri(5, 9),
            ri(10, 15),
        )
        makeTrainable(0, 80)
        ins.write(f"\t({ri(2, 5)*10}, {indx}),\n")
        indx += 1

    # Empires.Animal  #10
    text = "Animal (Food, Min_Food, Max_Food, Ataque_Corto_a_Corpo, FK_UT_id)"
    start(text)
    for i in range(numJogadors * 10):
        lif = ri(1, 7)
        min_food = "null"
        max_food = "null"
        food = "null"
        melee = ri(7, 12)
        if lif > 4:
            min_food = ri(5, 8) * 10
            max_food = ri(3, 8) * 100
            food = ri(min_food, max_food)
            melee = "null"

        makeObject(str(Troops.Animals[lif]) + str(i), indx)
        makeUnit(
            ri(4, 12) * 10,
            ri(4, 6),
            ri(2, 4),
            ri(3, 10),
        )
        makeTrainable(0, 30)
        ins.write(f"\t({food}, {min_food}, {max_food}, {melee}, {indx}),\n")
        indx += 1


if __name__ == "__main__":
    ins = open("insert.sql", "w")
    obj = open("objects.sql", "w")
    uni = open("units.sql", "w")
    train = open("trainable.sql", "w")
    global outputFile
    global insert
    global values
    global null
    main()
