// On Page Load
$(document).ready(function () {

    // Popup login button click
    $('#btn-login').on('click', function () {

        //input json
        var input = {
            userName: $("#UserName").val(),
            password: $("#Password").val()
        };

        // ajax call using POST
        $.ajax({
            type: "POST",
            url: "/Index?handler=Login",
            headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
            data: input,
            success: function (data) {
                location.reload();
            },
            error: function (error) {
                alert("An error has occured!");
            }
        });
    });

    // on add to cart button click
    $('.add-to-cart').on('click', function () {

        //input json
        var input = {
            productId: $(this).attr("data-id")
        };

        // ajax call using POST
        $.ajax({
            type: "POST",
            url: "/Index?handler=AddToCart",
            headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
            data: input,
            success: function (data) {
                if (data) {
                    toastr.success("Product added to cart.", "Success!");
                } else {
                    toastr.error("Unable to add product to cart.", "Error!");
                }
            },
            error: function (error) {
                alert("An error has occured!");
            }
        });
    });
})