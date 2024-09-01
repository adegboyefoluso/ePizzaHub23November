﻿function AddToCart(ItemId, Name, UnitPrice, Quantity) {
    $.ajax({
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        url: '/Cart/AddToCart/' + ItemId + "/" + UnitPrice + "/" + Quantity,
        success: function (response) {
            if (response.status == 'success') {
                $("#cartCounter").text(response.count);
            }
        }
    });
}
function DeleteItem(id) {
    if (id > 0) {
        $.ajax({
            type: 'GET',
            url: '/Cart/DeleteItem/' + id,
            success: function (data) {
                if (data > 0) {
                    location.reload();
                }
            }
        });
    }
}

function UpdateQuantity(id, currentQuantity, quantity) {
    if ((currentQuantity >= 1 && quantity == 1) || (currentQuantity > 1 && quantity == -1)) {
        $.ajax({
            url: '/Cart/UpdateQuantity/' + id + "/" + quantity,
            type: 'GET',
            success: function (response) {
                if (response > 0) {
                    location.reload();
                }
            }
        });
    }
}

$(document).ready(function () {
    $.ajax({
        type: 'GET',
        url: '/Cart/GetCartCount',
        success: function (response) {
            $("#cartCounter").text(response);
        }
    });
});