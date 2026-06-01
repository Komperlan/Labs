from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from bs4 import BeautifulSoup
import requests
import time
from pathlib import Path

options = webdriver.ChromeOptions()
options.add_argument('--headless')
options.add_argument('--no-sandbox')
options.add_argument('--disable-dev-shm-usage')
options.add_argument('user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36')

def download_hero_responses(hero_name):
    url = f'https://dota2.fandom.com/wiki/{hero_name}/Responses'
    
    print(f"Парсинг: {url}")
    
    try:
        driver = webdriver.Chrome(options=options)
        driver.get(url)
        
        WebDriverWait(driver, 10).until(
            EC.presence_of_element_located((By.TAG_NAME, "audio"))
        )
        
        time.sleep(2)
        
        html = driver.page_source
        driver.quit()
        
        soup = BeautifulSoup(html, 'html.parser')
        
        folder = Path(hero_name)
        folder.mkdir(exist_ok=True)
        
        audio_tags = soup.find_all('audio')
        
        if not audio_tags:
            print(f"Аудио не найдено")
            return
        
        print(f"Найдено аудио: {len(audio_tags)}")
        

        downloaded = 0
        for idx, audio in enumerate(audio_tags, 1):
            source = audio.find('source')
            if source and source.get('src'):
                audio_url = source['src']
                
                filename = audio_url.split('/')[-2]
                if not filename.endswith('.mp3'):
                    filename = f"audio_{idx}.mp3"
                
                filepath = folder / filename
                
                if filepath.exists():
                    print(f"    [{idx}/{len(audio_tags)}] ⊙ {filename}")
                    downloaded += 1
                    continue
                
                try:
                    response = requests.get(audio_url, timeout=30)
                    with open(filepath, 'wb') as f:
                        f.write(response.content)
                    print(f"    [{idx}/{len(audio_tags)}] ✓ {filename}")
                    downloaded += 1
                except Exception as e:
                    print(f"    [{idx}/{len(audio_tags)}] ✗ {filename}: {e}")
                
                time.sleep(0.1)
        
        print(f"Скачано: {downloaded}/{len(audio_tags)}")
        
    except Exception as e:
        print(f"Ошибка: {e}")


heroes = [
    'Crystal_Maiden',
    'Winter_Wyvern',
    'Invoker',
    'Silencer',
    'Templar_Assassin'
]

for hero in heroes:
    download_hero_responses(hero)
    print()
    time.sleep(0.5)
