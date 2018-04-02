$(function () {
    $("#execute").click(function () {

        var count = $("#count").val();

        // проверяем на валидность
        if (!isNumeric(count)) {
            // ошибка - не отправляем на сервер
            $("#count").addClass('invalid');
            var feedback = $('.feedback');
            feedback.removeClass('d-none');
            feedback.text('Введите целое число.')
            return;
        }

        // проверяем диапазон
        if (count < 0 || 2147000000 < count) {
            $("#count").addClass('invalid');
            var feedback = $('.feedback');
            feedback.removeClass('d-none');
            feedback.text('Введите целое число в диапазоне от 0 до 2 147 000 000.')
            return;
        }

        // удаляем классы невалидности
        $("#count").removeClass('invalid');
        $('.feedback').addClass('d-none');


        var strategy = $("#change").prop("checked");

        // отключаем кнопку
        $(this).prop('disabled', true);
        $(this).text('Играем...');

        // сбрасываем предыдущий результат
        $('#loading').html('');

        // отправляем запрос
        $.ajax({
            url: "/StartPlaying",
            type: "POST",
            data: {
                count: count,
                strategy: strategy
            }
        }).done(function (data) {
            var count = data.count;
            var strategy = data.strategy;
            var percentage = data.result;

            var result = "<p>Количество игр: " + count + "<br/>" +
                "Стратегия: " + (strategy ? "" : "не ") + "менять дверь<br/>" +
                "Процент побед: " + Round(percentage) + " %</p>";

            $("#loading").html(result);
        }).fail(function () {
            alert("Ошибка");
        }).always(function () {
            $("#execute").prop('disabled', false);
            $("#execute").text('Сыграть');
        });
    });
});

function isNumeric(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
}

function Round(num) {
    return Math.round(num * 100) / 100;
}