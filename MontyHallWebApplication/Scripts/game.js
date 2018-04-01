$(function () {
    var doors;
    var gamesCount = 0;
    var winsCount = 0;
    var car;
    var choosedDoor;
    var isGameEnded;

    // рестарт игры
    function Restart() {
        doors = [false, false, false];
        car = getRandomInt(0, 3);
        choosedDoor = -1;
        isGameEnded = false;

        $('#message').text("");

        doors[car] = true;
        contents = $('.reveal-content h2');

        // закрываем двери
        closeAllDoors();

        // ждем окончания закрытия дверей
        setTimeout(function(){
            for (var i = 0; i < 3; i++) {
                contents[i].innerText = doors[i] ? "Автомобиль" : "Коза";
            }
        }, 500);
    }
    
    Restart();

    $('#restart').click(Restart);

    $('.door').click(function(){
        var index = $(this).attr('id');

        // если игра закончена
        if(isGameEnded){
            openDoor(index);
            setTimeout(function(){
                Restart();
            }, 1000);
            return;
        }

        // если дверь еще не выбрана
        if(choosedDoor == -1){
            choosedDoor = index;
            doorsCanOpen = [];

            // находим все двери, которые можно открыть
            for (var i = 0; i < 3; i++) {
                if(doors[i])
                    continue;

                if(i == choosedDoor)
                    continue;

                doorsCanOpen.push(i);
            }

            // выбираем случайную дверь, которую откроем
            var doorToOpen = getRandomInt(0, doorsCanOpen.length);
            openDoor(doorsCanOpen[doorToOpen]);
        }
        else { // если мы уже выбирали дверь
            openAllDoors();

            gamesCount++;
            var result = doors[index];
            var message = result ? "Вы победили!" : "Вы проиграли :(";
            $('#message').text(message);
            $('#message').css('color', result ? "green" : "red");


            if (result) {
                winsCount++;
            }

            $('#gamesCount')[0].innerText = gamesCount;
            $('#percentageOfWin')[0].innerText = (winsCount / gamesCount) * 100;

            isGameEnded = true;

            setTimeout(Restart, 2000);
        }
    });
});

// функция получения рандомного целого числа
function getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min)) + min;
}

// открыть дверь
function openDoor(i){
    var door = $('.door')[i];
    door.style.left = '-100%';
}

function openAllDoors() {
    for (var i = 0; i < 3; i++) {
        openDoor(i);
    }
}

// закрыть дверь
function closeDoor(i){
    var door = $('.door')[i];
    door.style.left = '0';
}

function closeAllDoors() {
    for (var i = 0; i < 3; i++) {
        closeDoor(i);
    }
}