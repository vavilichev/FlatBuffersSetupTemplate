# FlatBuffers + Google Sheets Template

Шаблон для быстрого разворачивания связки **FlatBuffers + Google Sheets** на проекте в Unity.

### Для чего
Связка подходит для средних и крупных проектов, в которых ожидается использование больших объемов статических данных (конфигов),
которые в свою очередь, обычно хранятся в Google таблицах, используют функционал этих таблиц на полную (графики, формулы, кросс-ссылки и т.д.).
Данный шаблон позволяет импортировать данные из Google таблиц и запаковывать конфиги упакованные FlatBuffers банарники. 
Работать с отдельными таблицами, с отдельными листами, или же наоборот со списком листов, изменять внутри уже созданного конфига только часть данных.

### Почему FlatBuffers
**FlatBuffers** - разработанный компанией Google фреймворк, позволяющий максимально исключить десериализацию объектов конфигов (что фактически означает экономию памяти и буст скорости чтения). 
Фреймворк можно использовать и на мелких проектах, но вероятнее всего это окажется нецелесообразным, учитывая громоздкость сетапа.<br>

Сам фреймворк подходит только для статических данных, т.к. читая данные из бинарника, он не создает новые объекты в оперативной памяти, а лиш хранит ссылки и смещения прямо внутри массива байтов, загруженных в память.
Таким образом, путешествуя по дереву ссылок, вытаскиваются конечные примитивные типы данных: числа, строки, булки и т.д. 
Такое можно провернуть лишь при чтении (ведь для записи новых данных в класс, нужен экземпляр этого класса), поэтому фреймворк и подходит лишь для статики. 
Несомненно, весь этот прием для увеличения скорости чтения, экономии оперативной памяти и уменьшения фрагментации памяти при работе с конфигами.<br>

Еще один весомы плюс - гибкость. Компилятор Flatс, который компилирует специальные .fbs схемы в код, с которым может работать фреймворк, может компилировать эти файлы в код на разных языках.
Таким образом, FlatBuffers подходит не только для Unity, но и для других движков, а также для работы сервера.<br>

![image](https://github.com/user-attachments/assets/ceec8272-e7dc-4cb1-b725-f72f2e00b766)

Ну и если очень надо, можно опционально подключить генерацию кода с поддержкой сериализации кода в JSON (Newtonsoft). Но этой частью здесь мы пользоваться не будем.

Подробнее про фреймворк FlatBuffers можно почитать на оффициальном сайте [flatbuffers.dev](https://flatbuffers.dev/).

___

## Подготовка проекта

### 1. Ставим Nuget для Unity
Тут все просто, ставим NuGet для Unity, используя ссылку ниже, или, если угодно, с [офф репозитория скопировать можно](https://github.com/GlitchEnzo/NuGetForUnity):
```
https://github.com/GlitchEnzo/NuGetForUnity.git?path=/src/NuGetForUnity
```

### 2. Ставим FlatBuffers
Через появившееся меню NuGet в Unity заходим в менеджер пакетов NuGet. Путь **NuGet/Manage NuGet Packages**. В поиске вбиваем **Google.Flatbuffers** и устанавливаем пакет.
<br>
![image](https://github.com/user-attachments/assets/3e1fe5aa-2ee3-4fd0-9715-1abcc791b93f)

> [!WARNING]
> В следующем пункте описано добавление компилятора flatc.exe, однако, в данном шаблоне он уже добавлен: **Assets/FlatBuffers/flatc.exe**

В случае, если вы настраиваете проект самостоятельно, то с [официального репозитория FlatBuffers](https://github.com/google/flatbuffers/releases) нужно скачать компилятор flatc для своей операционной системы. Для себя я рассматриваю пример для Windows, если что.
Исполняемый файл можно поместить куда-то на ПК (в этом случае нужно также добавить путь к flatc.exe параметры окружающей среды, в Path), или же в папку с проектом. В данном шаблоне компилятор располагается внутри папки с проектом: **Assets/FlatBuffers/flatc.exe**.
FlatBuffers готов к использованию в Unity.



