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

        // Get the card element containing the clicked button
        var card = $(this).closest('.card');

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
                if (data.isSuccess) {

                    // Find the badge element for available quantity
                    var badge = card.find(".list-group li:nth-child(4) span");

                    // Get the current quantity from the badge
                    var currentQuantity = parseInt(badge.text().match(/\d+/)); // Extract the number

                    if (currentQuantity > 1) {

                        // Decrease the quantity by 1
                        var updatedQuantity = currentQuantity - 1;
                        badge.html(`Available(${updatedQuantity})`);
                    } else {

                        // Update to "Out of Stock"
                        badge
                            .removeClass() // Remove all existing classes from the <span>
                            .addClass("badge rounded-pill bg-danger") // Add the new class for "Out Of Stock"
                            .text("Out Of Stock"); // Update the text content

                        // Hide the "Add to Cart" button
                        card.find('.add-to-cart').remove();
                    }

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