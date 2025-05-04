from nba_api.stats.static import teams
from sqlalchemy import create_engine, Table, MetaData
from dotenv import load_dotenv
import os

# --- LOAD ENV ---
load_dotenv()
DB_USER = os.getenv("DB_USER")
DB_PASSWORD = os.getenv("DB_PASSWORD")
DB_HOST = os.getenv("DB_HOST")
DB_PORT = os.getenv("DB_PORT")
DB_NAME = os.getenv("DB_NAME")
DATABASE_URL = f'postgresql+psycopg2://{DB_USER}:{DB_PASSWORD}@{DB_HOST}:{DB_PORT}/{DB_NAME}'

# --- DB SETUP ---
engine = create_engine(DATABASE_URL)
conn = engine.connect()
metadata = MetaData()
metadata.reflect(bind=engine)
team_table = metadata.tables['team']

# --- CONFERENCE & DIVISION MAP BASED ON FULL TEAM NAME ---
team_info_map = {
    "Atlanta Hawks": ("East", "Southeast"),
    "Boston Celtics": ("East", "Atlantic"),
    "Brooklyn Nets": ("East", "Atlantic"),
    "Charlotte Hornets": ("East", "Southeast"),
    "Chicago Bulls": ("East", "Central"),
    "Cleveland Cavaliers": ("East", "Central"),
    "Detroit Pistons": ("East", "Central"),
    "Indiana Pacers": ("East", "Central"),
    "Miami Heat": ("East", "Southeast"),
    "Milwaukee Bucks": ("East", "Central"),
    "New York Knicks": ("East", "Atlantic"),
    "Orlando Magic": ("East", "Southeast"),
    "Philadelphia 76ers": ("East", "Atlantic"),
    "Toronto Raptors": ("East", "Atlantic"),
    "Washington Wizards": ("East", "Southeast"),

    "Dallas Mavericks": ("West", "Southwest"),
    "Denver Nuggets": ("West", "Northwest"),
    "Golden State Warriors": ("West", "Pacific"),
    "Houston Rockets": ("West", "Southwest"),
    "LA Clippers": ("West", "Pacific"),
    "Los Angeles Lakers": ("West", "Pacific"),
    "Memphis Grizzlies": ("West", "Southwest"),
    "Minnesota Timberwolves": ("West", "Northwest"),
    "New Orleans Pelicans": ("West", "Southwest"),
    "Oklahoma City Thunder": ("West", "Northwest"),
    "Phoenix Suns": ("West", "Pacific"),
    "Portland Trail Blazers": ("West", "Northwest"),
    "Sacramento Kings": ("West", "Pacific"),
    "San Antonio Spurs": ("West", "Southwest"),
    "Utah Jazz": ("West", "Northwest")
}

# --- LOAD TEAMS ---
nba_teams = teams.get_teams()
insert_data = []

for team in nba_teams:
    team_id = team['id']
    team_name = team['full_name']
    city = team['city']

    if team_name not in team_info_map:
        print(f"❌ Unknown team: {team_name}")
        continue

    conference, division = team_info_map[team_name]

    insert_data.append({
        "teamid": team_id,
        "name": team_name,
        "city": city,
        "conference": conference,
        "division": division
    })

    print(f"✅ Loaded: {team_name} | {conference} - {division}")

# --- INSERT ---
# --- INSERT ---
if insert_data:
    conn.execute(team_table.delete())  # Очистка перед вставкою
    conn.execute(team_table.insert(), insert_data)
    conn.commit()  # Додайте цю лінію
    print(f"🎉 {len(insert_data)} teams inserted.")
else:
    print("❌ No valid data to insert.")

conn.close()
