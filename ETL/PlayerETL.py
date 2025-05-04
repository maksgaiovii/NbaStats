import pandas as pd
from sqlalchemy import create_engine, MetaData, Table, update
from datetime import datetime

# Створюємо підключення до PostgreSQL
engine = create_engine("postgresql+psycopg2://postgres:1234@localhost:5432/nbaStats")
conn = engine.connect()

# Завантажуємо метадані
metadata = MetaData()
metadata.reflect(bind=engine)
player_table = metadata.tables.get("player")
if player_table is None:
    raise Exception("❌ Таблиця 'player' не знайдена в базі даних")

# Завантаження CSV-файлу Kaggle
df = pd.read_csv("C:\\Users\\Legion\\Downloads\\all_seasons.csv\\all_seasons.csv")  # вказати актуальний шлях до CSV

# Мапінг: скорочення → teamID
team_abbreviation_to_id = {
    "ATL": 1610612737,
    "BOS": 1610612738,
    "CLE": 1610612739,
    "NOP": 1610612740,
    "CHI": 1610612741,
    "DAL": 1610612742,
    "DEN": 1610612743,
    "GSW": 1610612744,
    "HOU": 1610612745,
    "LAC": 1610612746,
    "LAL": 1610612747,
    "MIA": 1610612748,
    "MIL": 1610612749,
    "MIN": 1610612750,
    "BKN": 1610612751,
    "NYK": 1610612752,
    "ORL": 1610612753,
    "IND": 1610612754,
    "PHI": 1610612755,
    "PHX": 1610612756,
    "POR": 1610612757,
    "SAC": 1610612758,
    "SAS": 1610612759,
    "OKC": 1610612760,
    "TOR": 1610612761,
    "UTA": 1610612762,
    "MEM": 1610612763,
    "WAS": 1610612764,
    "DET": 1610612765,
    "CHA": 1610612766
}

# Підрахунок оновлень
updated = 0

for _, row in df.iterrows():
    player_id = row.get("player_id")
    name = row.get("full_name")
    surname = name.split()[-1] if name and " " in name else name

    # Замість повної назви беремо скорочення (з поля 'team')
    abbreviation = row.get("team_abbreviation")
    teamid = team_abbreviation_to_id.get(abbreviation.upper())

    # Парсимо дату народження
    birth_date = None
    try:
        birth_date = datetime.strptime(row.get("birth_date", ""), "%Y-%m-%d").date()
    except:
        pass

    try:
        stmt = (
            update(player_table)
            .where(player_table.c.playerid == player_id)
            .values({
                "name": name,
                "surname": surname,
                "position": row.get("position"),
                "teamid": teamid,
                "height": row.get("height_cm"),
                "weight": row.get("weight_kg"),
                "BirthDate": birth_date
            })
        )
        result = conn.execute(stmt)
        if result.rowcount == 0:
            # Якщо не оновлено жодного рядка — вставляємо нового гравця
            conn.execute(player_table.insert().values({
                "playerid": player_id,
                "name": name,
                "surname": surname,
                "position": row.get("position"),
                "teamid": teamid,
                "height": row.get("height_cm"),
                "weight": row.get("weight_kg"),
                "BirthDate": birth_date
            }))
        updated += 1
    except Exception as e:
        print(f"❌ Failed for {player_id} – {e}")

print(f"✅ Гравців оновлено або вставлено: {updated}")
