[![Open in Visual Studio Code](https://classroom.github.com/assets/open-in-vscode-718a45dd9cf7e7f842a935f5ebbe5719a5e09af4491e668f4dbf3b35d5cca122.svg)](https://classroom.github.com/online_ide?assignment_repo_id=13748499&assignment_repo_type=AssignmentRepo)
# Лабораторная работа 7

Прогноз погоды. Внешние библиотеки.

## Задача

Реализовать консольное приложение, отображающие прогноз погоды для выбранного списка городов, используя сторонние библиотеки.

## Источник данных

- [Open-Meteo](https://open-meteo.com/en/docs#latitude=59.94&longitude=30.31&hourly=temperature_2m&forecast_days=16) для прогноза
- [Api-Ninjas](https://api-ninjas.com/api/city) для определения координат по названию города

## Функциональные требования

 - Отображать прогноз погоды на несколько дней вперед (значение по умолчанию задается конфигом)
 - Обновлять с некоторой частотой (задается конфигом)
 - Переключаться между городами с помощью клавиш "n", "p"
 - Заканчивать работу программы по Esc
 - Увеличивать\уменьшать количество дней прогноза по нажатие клавиш "+", "-"

Список городов, частота обновления, количество дней прогноза должны быть определены в конфиге(например в формате ini, json, xml)

## Отображение

В качестве образца для визуализации предлагается взять следующий:

![image](interface.png) Скриншот взят с  https://wttr.in

## Реализация

В данной лабораторной работе вам не запрещено использовать другие библиотеки.

В качестве библиотеки для [HTTP-запросов](https://en.wikipedia.org/wiki/HTTP) требуется воспользоваться [C++ Requests](https://github.com/libcpr/cpr)


В данной работе, при взаимодействии с внешними сервисами, может возникать достаточно большое количество коллизий и краевых случаев. Внимательно, подумайте об этом! Ваша программа должна корректно работать и "не падать"

## Deadline

1. 20.02.24 0.85
2. 27.02.24 0.65
3. 05.03.24 0.5
    
