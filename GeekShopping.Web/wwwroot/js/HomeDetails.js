$(document).ready(function () {
    $("#open-modal").click(function () {
        $("#modal-background").fadeIn();
        $("#modal").animate({ bottom: "0" }, 500);
    });

    $("#close-modal").click(function () {
        closeModal();
    });
    $("#modal-background").click(function (e) {
        if (e.target == this) {
            closeModal();
        }
    });
});
function closeModal() {
    $("#modal-background").fadeOut();
    $("#modal").animate({ bottom: "-100%" }, 500);
}
$('#quantityUpdate').on('keydown', function (event) {
    if (event.key.length === 1 && !/\d/.test(event.key)) {
        event.preventDefault();
    }
});

function isValidNumber(value) {
    var regex = /^[1-9][0-9]*$/;
    if (regex.test(value)) {
        return true;
    }
    return false;
}
$(".modal-units div").on('click', function () {
    var value = $(this).attr('id');
    sendQuantity(value);
});
$("#update-quantity").on('click', function () {
    var value = $("#quantityUpdate").val();
    sendQuantity(value);
    
});
function sendQuantity(value) {
    if (!isValidNumber(value)) return alert("Desculpe, por favor adicione um número válido");
    $("#Count").val(value);
    $("#model-count").text(value);
    closeModal();
}