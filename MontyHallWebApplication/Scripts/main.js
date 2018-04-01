$(function () {
    $("#execute").click(function () {

        var count = $("#count").val();

        // проверяем на валидность
        if (!isNumeric(count)) {
            // ошибка - не отправляем на сервер
            $("#count").addClass('invalid');
            $('.feedback').removeClass('d-none');
            return;
        }

        // удаляем классы невалидности
        $("#count").removeClass('invalid');
        $('.feedback').addClass('d-none');


        var strategy = $("#change").prop("checked");

        // отключаем кнопку
        $(this).prop('disabled', true);
        $(this).text('Играем...');

        // отправляем запрос
        $.ajax({
            url: "/GetPercentageOfWin",
            type: "POST",
            data: {
                count: count,
                strategy: strategy
            }
        }).done(function (data) {
                $("#loading").text("Процент побед: " + data + " %");
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