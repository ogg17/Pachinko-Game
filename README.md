
# Pachinko Game

> **Проект интересен с архитектурной точки зрения, а не графической, а также анализом плюсов и минусов, который написан ниже.**

Реализация простой версии игры Пачинко, падают шарики, врезаются в препятствия, если они попадают в красную лунку, то просто исчезают, если попадают в зеленую лунку, то дают +50 очков. Каждые 12 часов количество шариков устанавливается на 200, для того чтобы можно было поиграть еще и выиграть какое-то количество игровых очков.
Данная игра является частью более крупного проекта, которые содержит множество минигр, данная игра являясь минигрой позволяет игроку каждые 12 часов пополнять количество игровых очков, которые он может потратить в других миниграх.
### Особенности реализации:
* Основа архитектуры проекта это глобальная машина состояний, которая позволяет управлять состояниями игры, в данном проекте это 2 стейта, инициализация(`InitState`) и игровой процесс(`GameState`).

* Также в данном проекте реализована собственная DI система, которая благодаря рефлексии позволяет инжектить зависимости в поля классов с атрибутом `[inject]`. Благодаря этой системе можно строить гибкую архитектуру основанную на сервисах, которые затем можно использовать в качестве зависимостей в других классах.

* Для реализации различных всплывающих окон или меню, реализована небольшая стейт машина(`MenusStateMachine`) для смены текущего меню. 

* Также в качестве сервиса в проекте существует шина событий, которая может понадобится для реализации различных функций игры: управление, звук, эффекты и т.д.

* Для быстрого доступа к другим `MonoBehaviour` был реализован сервис `MonoBehaviourContainer`, который позволяет в бутстрап классе добавить в данный контейнер монобехи, а затем в использовать их в стейтах, т.к. класс стейта не наследуются от `MonoBehaviour`.

* Существуют сервисы `RuntiumeData`, которые благодаря классу `SaveReactiveData`, позволяют просто реализовать хранение и обработку данных игры, а также связь данных и UI, например `CoinsData` хранит количество монет, и благодаря `ISubject<>` внутри `SaveReactiveData`, позволяет оповещать, всех кто подписался на него, об изменении значения, что удобно для отображения количества монет в UI.

* Реализована система локализации, которая позволяет загружать из JSON файла текст для различных элементов UI, соответственно благодаря, хранению текущего языка в `SettingsData` сервисе, при изменении языка игры, меняется JSON файл откуда загружается текст, на файл с другой локализацией. Хотя в данном проекте эта система пока что не используется, так как нет текста, который требует локализации.

### Что не так? Что можно улучшить или переписать в будущем?
* Данный проект основан на определенном шаблоне, который часто использовался для различных проектов, что дает некоторые особенности, например некоторые части почти не используются, та же система локализации, на самом деле довольно много различных фич из шаблона было вырезано, так как вообще не использовались, а также чтобы было легче понять что происходит в проекте.

* На данный момент сама игра не отделена от UI, что в перспективе может привести к некоторым проблемам при обновлении визуала или при переиспользовании игры с другим визуалом, что не так критично ввиду маленького масштаба проекта.

* Переписать `SaveReactiveData`, т.к. он не совсем соответствует принципу единой ответственности, а также жестко привязан к PlayerPrefs, что делает его довольно простым, но не гибким, хотелось бы иметь возможность использовать различные способы сохранения данных, не привязывая класс к какой-то одной. Например это можно реализовать с помощью интерфейса, реализующего несколько методов: `Save` `Load`, а также несколько систем сохранения реализующих данный интерфейс, в зависимости от способа сохранения. И уже использовать выбранную систему через интерфейс, что позволит довольно просто менять систему сохранения. Так же можно было бы разделить `SaveReactiveData` на просто `ReactiveData` и функцию сохранения реализовать уже через другой класс, который просто бы работал с данными.

* В некоторых частях игры, например в системе локализации в качестве конфигурации используется словарь, который задается прямо в коде, что не очень удобно, если над проектом будет работать несколько людей, можно перенести все такие моменты в конфигурационные файлы, это могут быть как `ScriptableObject`-ы, так и просто JSON файл. Для этого было бы хорошо создать систему конфигурации, в которой можно было бы выбирать тип конфигурации и задавать место хранения файла.

Данный проект интересен тем, что представляет собой типичный проект в вакууме, который интересно проанализировать.

Gameplay
======

![Image Sequence_001_0000](https://github.com/ogg17/Pachinko-Game/assets/40641614/d6604c0c-42d9-47fa-8b8a-152ae6f2b8a8)


https://github.com/ogg17/Pachinko-Game/assets/40641614/f7d4d0f0-b2c1-4f1a-8cc3-849460de38a0


