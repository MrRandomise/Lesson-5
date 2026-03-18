# Save & Load System — Unity

> Учебный проект курса **[OTUS: Разработчик игр на Unity](https://otus.ru/)**

Реализация системы сохранения и загрузки игрового состояния в Unity с использованием асинхронного хранилища файлов, шифрования данных (AES-256) и паттернов чистой архитектуры.

---

## 🎯 Описание проекта

В рамках Урока 5 курса OTUS реализована полноценная система сохранения игровых данных, вдохновлённая возможностями ассета [EasySave](https://assetstore.unity.com/packages/tools/input-management/easy-save-the-complete-save-data-serializer-system-768). Система позволяет:

- **Сохранять** положение, поворот и параметры всех юнитов на сцене.
- **Сохранять** количество ресурсов.
- **Создавать несколько слотов** сохранений с произвольными именами.
- **Перезаписывать** существующий файл сохранения с подтверждением.
- **Загружать** сохранённое состояние и восстанавливать сцену.
- **Шифровать** файлы сохранений алгоритмом AES-256 (CBC, PKCS7).
- **Хранить скриншот** и дату создания каждого сохранения для удобного предпросмотра.

---

## 🛠️ Технологии

| Инструмент / Библиотека | Версия | Назначение |
|---|---|---|
| **Unity** | 2021 LTS | Игровой движок |
| **C#** | 9 | Язык разработки |
| **Zenject** | — | Dependency Injection |
| **Odin Inspector** | — | Расширение инспектора Unity |
| **TextMeshPro** | 3.0.7 | UI-текст |
| **Newtonsoft.Json** | — | JSON-сериализация данных сохранения |
| **AES Encryption** | 256-bit CBC | Шифрование файлов сохранений |

---

## 🏗️ Архитектура

```
Assets/Scripts/
├── SaveSystem/
│   ├── Core/
│   │   ├── SavingSystem.cs          # Оркестратор: сбор и восстановление состояния
│   │   ├── SceneSaveManager.cs      # Публичный API: SaveScene / LoadScene
│   │   ├── ISaveState.cs            # Интерфейс для любого сохраняемого объекта
│   │   └── ISaveLoade.cs            # Интерфейс хранилища (Save / Load)
│   ├── FileSaverSystem/
│   │   ├── FileSystemSaverLoader.cs # Реализация ISaveLoade: файловая система
│   │   ├── AesEncryptionProvider.cs # AES-256 шифрование / дешифрование
│   │   ├── Saver.cs                 # Запись зашифрованной строки в файл
│   │   └── Reader.cs                # Чтение файла сохранения
│   ├── Data/
│   │   ├── UnitSavingManager.cs     # ISaveState для юнитов
│   │   ├── ResourceSavingManager.cs # ISaveState для ресурсов
│   │   ├── SaveLoadInfoManager.cs   # Захват метаданных (имя, дата, скриншот)
│   │   └── SceneObjectsManager.cs   # Менеджер объектов сцены
│   ├── Tools/
│   │   └── FileNameManager.cs       # Пути к файлам сохранений
│   └── Struct/
│       └── SaveDataStruct.cs        # Структура данных файла сохранения
├── GameEngine/
│   ├── Objects/
│   │   ├── Unit.cs                  # Компонент юнита (тип, HP, позиция)
│   │   └── Resource.cs              # Компонент ресурса (ID, количество)
│   └── Systems/
│       ├── UnitManager.cs           # Управление юнитами
│       └── ResourceService.cs       # Управление ресурсами
├── UIManagers/                      # UI-контроллеры (меню, формы, предпросмотр)
├── Components/                      # MonoBehaviour-компоненты для UI
├── Factory/                         # Фабрика для создания объектов сохранения
└── Installer/                       # Zenject-инсталляторы
```

### Поток сохранения

```
SceneSaveManager.SaveScene(name)
  └─► SavingSystem.CaptureState(saveStates, name)
        ├─ UnitSavingManager.CaptureState()    → List<Dictionary>
        ├─ ResourceSavingManager.CaptureState() → List<Dictionary>
        └─► FileSystemSaverLoader.Save(data, name)
              ├─ SaveLoadInfoManager.CaptureState() → SaveDataStruct
              ├─ AesEncryptionProvider.AesEncryption()
              └─ Saver.Save() → *.sav
```

### Поток загрузки

```
SceneSaveManager.LoadScene(name)
  └─► SavingSystem.RestoreState(saveStates, name)
        └─► FileSystemSaverLoader.Load(name)
              ├─ Reader.Load() → encrypted string
              ├─ AesEncryptionProvider.AesDecryption()
              └─ JsonConvert.Deserialize → SaveDataStruct
        ├─ UnitSavingManager.RestoreState(data)
        └─ ResourceSavingManager.RestoreState(data)
```

---

## 📁 Формат файла сохранения

Каждый файл `*.sav` представляет собой однострочную AES-256-зашифрованную, Base64-закодированную JSON-строку следующей структуры:

```json
{
  "SaveName": "MySave",
  "SaveDate": "2024-01-01T12:00:00",
  "SaveScreen": "<base64-png>",
  "SaveObjects": [
    { "StateType": "Unit", "Scene": "0", "Type": "Warrior",
      "PositionX": "1.5", "PositionY": "0", "PositionZ": "3.2",
      "RotationX": "0", "RotationY": "90", "RotationZ": "0", "HP": "10" },
    { "StateType": "Resource", "Scene": "0", "ID": "Gold", "Amount": "150" }
  ]
}
```

---

## 🚀 Быстрый старт

1. Клонируйте репозиторий:
   ```bash
   git clone https://github.com/MrRandomise/Lesson-5.git
   ```
2. Откройте проект в **Unity 2021 LTS**.
3. Откройте сцену из папки `Assets/Scenes/`.
4. Нажмите **Play**. Для управления сохранениями используйте кнопку меню в UI.

> **Примечание.** Плагины Zenject и Odin Inspector включены в репозиторий в папке `Assets/Plugins/`.

---

## 📖 Чему посвящён урок

| Тема | Детали |
|---|---|
| Паттерн **State Capture** | Каждый игровой объект реализует `ISaveState` для независимого захвата и восстановления состояния |
| **Dependency Injection** | Все зависимости разрешаются через Zenject без использования синглтонов |
| **Шифрование данных** | AES-256 CBC обеспечивает защиту файлов сохранений от чтения и редактирования |
| **Предпросмотр сохранений** | Скриншот и дата сохранения отображаются в UI при выборе слота |
| **Мультисценность** | Данные других сцен сохраняются и восстанавливаются без потерь |

---

## 👤 Автор

Проект выполнен в рамках курса **OTUS: Разработчик игр на Unity**.
