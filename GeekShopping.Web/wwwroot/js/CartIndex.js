$(".RemoveProduct").on('click', function () {
    var id = $(this).attr('itemid');
    $.ajax({
        url: '/Cart/Remove/' + id,
        type: 'GET',
        success: function () {
            location.reload()
        },
        error: function () {
            alert("Ocorreu um erro ao remover o produto")
        }
    })
});

function SendQuantityUpdate(productId, quantity) {
    if (quantity == $("#productCurrentQuantity").val()) {
        $(".Fade").hide();
        return;
    }
    var url = '/Cart/UpdateCount?productId=' + productId + '&count=' + quantity;

    $.ajax({
        url: url,
        type: 'POST',
        success: function () {
            location.reload();
        },
        error: function () {
            alert("Ocorreu um erro ao alterar o produto.");
        }
    });
}

$(".Modal .option").on('click', function () {
    var value = $(this).val();
    var productId = $("#productId").val();
    SendQuantityUpdate(productId, value);

})

$(".sendUpdate").on('click', function () {
    var value = parseInt($("#quantityUpdate").val());
    var productId = $("#productId").val();
    if (isValidNumber(value) == false) {
        alert("Desculpe, quantidade inválida, por favor verifique e tente novamente.");
        return;
    }
    SendQuantityUpdate(productId, value);

});

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

$(".unit").on('click', function () {
    $("#productId").val($(this).attr('itemid'));
    $("#productCurrentQuantity").val($(this).find("#currentQuantity").val())
    $(".Fade").css('display', 'flex');
})
$("#closeModal").on('click', function () {
    $(".Fade").hide();
})