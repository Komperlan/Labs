import requests
import config
import re
from bs4 import BeautifulSoup
import pandas as pd

def extract_engine_info(engine_str):
    volume_match = re.search(r'(\d+(\.\d+)?)\s*л', engine_str)
    power_match = re.search(r'(\d+)\s*л\.с\.', engine_str)

    volume = float(volume_match.group(1)) if volume_match else None
    power = int(power_match.group(1)) if power_match else None

    return pd.Series([volume, power])

def extract_number(text):
    numbers = re.findall(r'\d+', text.replace('\xa0', ''))  # \xa0 — неразрывный пробел
    return int(''.join(numbers)) if numbers else None

def save_to_csv(result):
    df = pd.DataFrame(result)
    specs_df = pd.DataFrame(df['specs'].tolist(), columns=['engine', 'body', 'wd', 'transmission'])
    df = pd.concat([df.drop(columns=['specs']), specs_df], axis=1)
    df['mileage'] = df['mileage'].apply(extract_number)
    df['price'] = df['price'].apply(extract_number)
    df[['engine_volume_l', 'engine_power_hp']] = df['engine'].apply(extract_engine_info)
    del df['engine']
    print(df)
    df.to_csv("cars_data.csv", index=False, encoding="utf-8")


def parse_page(soup, result):
    items = soup.find_all("div", class_="ListingItemUniversal__info-Jm1WZ")

    for item in items:
        title_tag = item.find("a", class_="ListingItemTitle__link")
        title = title_tag.get_text(strip=True)

        color = item.find("div", class_="ListingItemUniversalSpecs__subtitle-fOpZ7").text.strip()
        specs = item.find_all("span", class_="ListingItemUniversalSpecs__spec-IcgjK")
        specs_text = [s.text.strip() for s in specs]

        year = item.find("div", class_="Typography2__h5-mkmlZ").text.strip()
        mileage = item.find("div", class_="ListingItemUniversalCondition__status-FCDjU").text.strip()
        
        parent = item.parent.parent
        price = parent.find("div", class_="ListingItemUniversalPrice__title-Mi4tV").text.strip()

        result.append({
            "title": title,
            "color": color,
            "specs": specs_text,
            "year": year,
            "mileage": mileage,
            "price": price,
        })

result = []

for page in range(1, 100):
    response = requests.get(config.MAIN_PAGE_URL.format(page))
    soup = BeautifulSoup(response.text, "html.parser")
    print(f"Парсим страницу {page}")

    parse_page(soup, result)

save_to_csv(result)

