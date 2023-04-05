var tempoLimite = 3000;
window.addEventListener('load', function () {

    clearTimeout(temporizador);
    $(".load-wrapper").css('display', 'none');
});
var temporizador = setTimeout(function () {

    $(".load-wrapper").css('display', 'flex');
}, tempoLimite);


$(document).ready(function () {
    if ($('div.modal').length) {
        $("#successModal").fadeIn().css("display","flex");
        $("#successModal").animate({ top: "0" }, 600);
        var bar = $("#bar");
        setTimeout(function () {
            bar.css("width", "100%");
        }, 100);

        setTimeout(function () {
            $("#successModal").animate({ top: "-100%" }, 1500);
            $("#successModal").fadeOut();
            
        }, 3000);
    };

    $(".card").on('click', function () {
        var id = $(this).attr('itemid');
        window.location.replace('/Home/Details/' + id);
    });

    $("#search-input").on('keyup', function () {
        var input = $(this).val().toUpperCase();
        if (input.length == 0) {
            $(".card").show();
            return $("#searchResultCount").hide()
        }
        $(".card").each(function () {
            var productName = $(this).find("h4").text().toUpperCase();
            if (productName.indexOf(input) != -1) {
                $(this).show();
            }
            else {
                $(this).hide();
            }
        });
        var totalResult = $(".card").length - $(".card:hidden").length
        $("#searchResultCount").show()
        $("#totalProductsSearched").text("Produto(s) encontrados: " + totalResult);
    });
});