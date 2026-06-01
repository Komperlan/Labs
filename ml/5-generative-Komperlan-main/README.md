# Лабораторная 5. Генеративные модели

Проект посвящен генерации изображений с помощью вариационного автоэнкодера. В репозитории есть две версии эксперимента: для иконок и для изображений в anime-стиле.

## Файлы

- `Iconlabgen.ipynb` - VAE для датасета иконок.
- `Animelabgen.ipynb` - VAE для датасета anime-изображений.
- `icon_morph.gif`, `icon_morph2.gif` - примеры интерполяций для иконок.
- `anime_morph2.gif`, `anime_morph3.gif` - примеры интерполяций для anime-изображений.
- `LICENSE` - лицензия проекта.

## Идея

Обе версии используют один общий подход:

- загрузка изображений из локальной папки;
- приведение изображений к размеру `64x64`;
- обучение сверточного `Variational AutoEncoder`;
- восстановление изображений;
- генерация изображения из случайного латентного вектора;
- интерполяция между двумя изображениями в латентном пространстве и сохранение результата в GIF.

## Архитектура

В ноутбуках реализованы:

- класс `AnimeDataset` для чтения `.png`, `.jpg`, `.jpeg`, `.avif`;
- сверточный encoder;
- латентные векторы `mu` и `logvar`;
- decoder на транспонированных свертках;
- функция интерполяции `generate_interpolation_gif`.

В качестве функции потерь используется `L1Loss` для реконструкции. Модель сохраняет лучшие веса в `best_vae.pth`.

## Эксперимент с иконками

Файл: `Iconlabgen.ipynb`.

Параметры:

- `IMG_SIZE`: `64`;
- `BATCH_SIZE`: `64`;
- `LATENT_DIM`: `512`;
- `LR`: `1e-4`;
- `EPOCHS`: `100`;
- устройство: `torch_directml.device()`.

В сохраненном запуске найдено `1554` изображения:

- train: `1398`;
- validation: `156`.

К концу обучения validation loss снизился до `0.0404`.

## Эксперимент с anime-изображениями

Файл: `Animelabgen.ipynb`.

Параметры:

- `IMG_SIZE`: `64`;
- `BATCH_SIZE`: `64`;
- `LATENT_DIM`: `256`;
- `LR`: `1e-4`;
- `EPOCHS`: `100`;
- устройство: `torch_directml.device()`.

В сохраненном запуске найдено `5813` изображений:

- train: `5231`;
- validation: `582`.

В ноутбуке указана ссылка на датасет anime-изображений: https://drive.google.com/drive/folders/1XrM_2Q8HGHsGU_vsmi6N3ZXUUF32Z4LR?usp=drive_link

## Особенности запуска

В ноутбуках используются абсолютные локальные пути:

```python
DATA_PATH = "C:/Users/Андрей/Desktop/Icons"
DATA_PATH = "C:/Users/dryu/Downloads/AbsolutePeakDataSet/AbsolutePeakDataSet"
```

Перед запуском их нужно заменить на путь к датасету на своей машине.

Ноутбуки рассчитаны на DirectML:

```python
DEVICE = torch_directml.device()
```

Если используется CUDA или CPU, строку выбора устройства нужно адаптировать.

## Зависимости

```bash
pip install torch torchvision torch-directml pillow imageio numpy matplotlib jupyter
```

Если `torch-directml` не нужен, его можно убрать и заменить выбор устройства на стандартный PyTorch-вариант.

## Запуск

```bash
jupyter notebook Iconlabgen.ipynb
jupyter notebook Animelabgen.ipynb
```
