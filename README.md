# Курсовая работа 3 курс "Алгоритмы для клеточной игры"
## Описание приложение

На главном окне вы можете вы 3 режима: 
- сыграть в игру
- запустить турнир
- просмотреть матч

### 1. режим "Cыграть в игру"
Здесь вы можете выбрать, кто будет играть за первого и второго игрока. Это либо человек либо какой-то из перечисленных алгоритмов. Дальше вы увидете клеточное поле и контроллер с кнопочками или кнопку "Show" в зависимости играют ли люди или нет соотвественно. После окончания игра выпадет сообщение, какой игрок победил. Также игра автоматически сохраниться в папке с игрой.

### 2. режим "Запустить турнир"
Здесь вы можете выбрать, только количество кругов турнира и его название. Участвовать в нем будут все ИИ. После запуска вы через какое-то время увидете сообщение об его окончании. Также автоматически создаться папка с его названием и все игры турнира в ней и еще будет html-таблица турнира, которую вы сможете открыть с помощью браузера. 

### 3. режим "Просмотреть матч"
Тут вы выбираете матчи, которые сохранились, чтобы их просмотреть. Во время просмотра будет доступны: клеточное поле и ползунок для выбора хода.


## Как добавить ИИ для игры
В проекте вы должны создать класс, который наследуется от интерфейса **IArtificialIntelligence**. После того, как вы создадите свой класс с алгоритмом, надо еще добавить пару строчек в код, чтобы алгоритм участвовал в турнире и в игре была возможность выбрать в качестве первого или второго игрока. Для этого вам поможет Task List.

## Правила игры
Игровое поле разбито на 81 клетку (по 9 в каждом ряду). Клетки поля состоят из нескольких слоев. Количество слоев клетки поля симметричный относительно центра. Первый игрок занимает левую верхнюю клетку с оступом 1 клетку от стенок, второй, аналогично, – правую нижнюю. Игроки по очереди совершают ходы; начинает первый игрок. Каждый ход представляет собой перемещение игрока в одну из смежных ячеек. При этом два игрока не могут одновременно находиться в одной ячейке. После хода игрока, на клетке, на которой был игрок, исчезает один слой. Если слой был последний, то игроки узнаю, что клетки больше не существует. Игра заканчивается, когда какой-то игрок не может сделать ход или сделает неправильных ход.

## Описание HTML-таблицы турнира
**Avg. W1** - это среднее количество побед ИИ против одного соперника, играя как первый игрок.
**w1** - это количество ячеек, играя как первый игрок, где количество побед больше количества поражений на 10% от количества матчей.
**d1** - это количество ячеек, играя как первый игрок, где разница между количеством побед и поражений меньше 10% от количества матчей.
**Avg. W2**, **w1**, **d2** аналогично только играя как второй игрок.




[//]: # (These are reference links used in the body of this note and get stripped out when the markdown processor does its job. There is no need to format nicely because it shouldn't be seen. Thanks SO - http://stackoverflow.com/questions/4823468/store-comments-in-markdown-syntax)

   [dill]: <https://github.com/joemccann/dillinger>
   [git-repo-url]: <https://github.com/joemccann/dillinger.git>
   [john gruber]: <http://daringfireball.net>
   [df1]: <http://daringfireball.net/projects/markdown/>
   [markdown-it]: <https://github.com/markdown-it/markdown-it>
   [Ace Editor]: <http://ace.ajax.org>
   [node.js]: <http://nodejs.org>
   [Twitter Bootstrap]: <http://twitter.github.com/bootstrap/>
   [jQuery]: <http://jquery.com>
   [@tjholowaychuk]: <http://twitter.com/tjholowaychuk>
   [express]: <http://expressjs.com>
   [AngularJS]: <http://angularjs.org>
   [Gulp]: <http://gulpjs.com>

   [PlDb]: <https://github.com/joemccann/dillinger/tree/master/plugins/dropbox/README.md>
   [PlGh]: <https://github.com/joemccann/dillinger/tree/master/plugins/github/README.md>
   [PlGd]: <https://github.com/joemccann/dillinger/tree/master/plugins/googledrive/README.md>
   [PlOd]: <https://github.com/joemccann/dillinger/tree/master/plugins/onedrive/README.md>
   [PlMe]: <https://github.com/joemccann/dillinger/tree/master/plugins/medium/README.md>
   [PlGa]: <https://github.com/RahulHP/dillinger/blob/master/plugins/googleanalytics/README.md>
