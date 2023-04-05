var tempoLimite = 3000;

window.addEventListener('load', function () {
    
    clearTimeout(temporizador);
    $(".load-wrapper").css('display', 'none');
});

var temporizador = setTimeout(function () {
    
    $(".load-wrapper").css('display', 'block');
}, tempoLimite);


$(".RemoveProduct").on('click', function () {
    var id = $(this).attr('itemid');
    $.ajax({
        url: '/Cart/Remove/' + id,
        type: 'DELETE',
        success: function () {
            location.reload()
        },
        error: function (error) {
            alert(error.responseJSON.message);
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
        error: function (error) {
            alert(error.responseJSON.message);
        }
    });
}

$("#applyCoupon").on('click', function () {
    var formData = $('form').serialize();
    $.ajax({
        type: 'POST',
        url: '/Cart/ApplyCoupon',
        data: formData,
        beforeSend: function () {
            $(".fa-mail-forward").hide();
            $(".Coupon-loader").show();
        },
        success: function () {
            location.reload();
        },
        error: function (error) {
            $(".fa-mail-forward").show();
            $(".feedbackCoupon").html(error.responseJSON.message);
        },
        complete: function () {
            $(".Coupon-loader").hide();
        }
    });
});
$("#removeCoupon").on('click', function () {
    $.ajax({
        type: 'DELETE',
        url: '/Cart/RemoveCoupon',
        success: function () {
            location.reload();
        },
        error: function (error) {
            alert(error.responseJSON.message);
        }
    });
});

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
    $(".Fade").fadeIn().css('display', 'flex');
    $(".Modal").animate({ top: "50%" }, 500);
})
$("#closeModal").on('click', function () {
    CloseCartModal();
})
$(".Fade").click(function (e) {
    if (e.target == this) {
        CloseCartModal();
    }
});
function CloseCartModal() {
    $(".Fade").fadeOut()
    $(".Modal").animate({ top: "-100%" }, 500);
}