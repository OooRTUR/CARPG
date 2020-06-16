# Модуль VehicleBuilder

Для демонстрации возможностей модуля Vehicle Builder использованы ассеты с ассетстора, вот вид их оригиналов.

[![](https://i.imgur.com/r7zFxp0.png) ]()


**Взаимодействие с модулем**

[![](https://i.imgur.com/yairTei.gif) ]()

[![](https://i.imgur.com/4fpIp6a.gif) ]()


**Хранение данных**

Данные относительного положения объектов хранятся в asset файлах

[![](https://i.imgur.com/R4siGdk.gif) ]()


**Результаты работы**

А так выглядят вариации объектов после компоновки различных частей оригинальных префабов.

Мех с танковой башней!

[![](https://i.imgur.com/E0AeY5H.png) ]()


Мех с ракетной установкой (как и запланировано в исходном префабе)

[![](https://i.imgur.com/SYRu5h1.png) ]()


Масклкар с пулеметом

[![](https://i.imgur.com/uxxLOJP.png) ]()


Маскл кар с ракетной установкой от меха

[![](https://i.imgur.com/GR8oLvN.png) ]()


Или даже так - маскл кар с башней от танка.

[![](https://i.imgur.com/6w3AEiF.png) ]()


Танк с ракетной установкой от меха вместо пушки

[![](https://i.imgur.com/8Hku4xA.png) ]()


** Добавление новых редактируемых частей **

Для реализации новых частей техники достаточно отнаследоваться от базовых классов BaseBuilderEditor, BaseBuilder и BaseBuildData, указать имя папки для новой части техники в BuilderConfiguration, создать вызывающие методы в VehicleBuilderEdiror и VehicleBuilder и добавить VehiclePartContext в contextStorage класса VehicleBuilder.

[![](https://i.imgur.com/IWGmNAD.png) ]()



**Ссылки на оригинальные ассеты в AssetStore**

https://assetstore.unity.com/packages/3d/props/guns/machine-guns-20611

https://assetstore.unity.com/packages/3d/vehicles/russian-military-vehicles-lite-t90-104569

https://assetstore.unity.com/packages/3d/muscle-car-mobile-editable-substance-texture-51072

https://assetstore.unity.com/packages/3d/characters/robots/medium-mech-striker-124342

