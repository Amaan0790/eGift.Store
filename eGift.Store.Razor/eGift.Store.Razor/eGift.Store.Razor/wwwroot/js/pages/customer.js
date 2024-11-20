// On Page Load
$(document).ready(function () {

    // Auto fill age based on date of birth
    $("#customer-date-of-birth").change(function () {
        var dob = new Date($(this).val());
        if (!isNaN(dob.getTime())) {
            var today = new Date();
            var age = today.getFullYear() - dob.getFullYear();
            var monthDiff = today.getMonth() - dob.getMonth();

            // Adjust if the birthday hasn't happened yet this year
            if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < dob.getDate())) {
                age--;
            }
            $("#customer-age").val(age);
        }
    });

    // When the file input changes, display the selected image
    $('#customer-profile-image').on('change', function () {
        var input = this;

        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#customer-profile-image-preview').attr('src', e.target.result); // Set the image preview
            }

            reader.readAsDataURL(input.files[0]); // Read the file as a data URL
            $('#IsClear').val(false); // Set IsClear flag
        }
    });

    // When the "Clear" button is clicked, reset the image preview and clear the file input
    $('#customer-clear-btn').on('click', function () {
        $('#customer-profile-image-preview').attr('src', '/images/default/user_default_image.png'); // Reset to default image
        $('#customer-profile-image').val(''); // Clear the file input
        $('#IsClear').val(true); // Set IsClear flag
    });
});