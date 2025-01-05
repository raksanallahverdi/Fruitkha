$(function () {
    $('.typeButton').on('click', function (e) {
        e.preventDefault();
        const typeId = $(this).data('id'); 
        console.log("Selected type:", typeId);

        $.ajax({
            url: "/Shop/FilterByType",  
            method: "GET",  
            data: { typeId: typeId },
            success: function (response) {
                console.log("Response:", response);
                $('#productRow').html(response); 
            },
            error: function (xhr, status, error) {
                console.error("Error:", error);
                alert("An error occurred while fetching the products.");
            }
        });
    });
});
$(function () {
    $('.read-more-btn').on('click', function (e) {
        console.log("salam")
        e.preventDefault();
        const NewsId = $(this).data('id');
        console.log("Selected News:", NewsId);

        $.ajax({
            url: "/News/Single",
            method: "GET",
            data: { NewsId: NewsId },
            success: function (response) {
                console.log("Response:", response);
                window.location.href = "/News/Single?NewsId=" + NewsId;

              
            },
            error: function (xhr, status, error) {
                console.error("Error:", error);
                alert("An error occurred while fetching the products.");
            }
        });
    });
});

$(function () {
    $('.readMore').on('click', function (e) {
        console.log("salam")
        e.preventDefault();
        const ProductId = $(this).data('id');
        console.log("Selected News:", ProductId);

        $.ajax({
            method: "GET",
            url: "/Product/Single",
            data: { id: ProductId },
            success: function (response) {
                console.log("Response:", response);
                window.location.href = "/Product/Single/" + ProductId;
            },
            error: function (xhr, status, error) {
                console.error("Error:", error);
                alert("An error occurred while fetching the products.");
            }
        });
    });
});


$(document).ready(function () {
    $(document).on('click', '.addToBasket', function () {
        const productId = $(this).data('id');
        console.log('Button clicked, productId:', productId);

        $.ajax({
            method: "POST",
            url: "/cart/AddProduct",
            data: {
                productId: productId
            },
            success: function (response) {
                console.log('Success response:', response); 
                alert(response);
            },
            error: function (xhr, status, error) {
                console.log('Error:', error); 
                alert('Error: ' + error); 
            }
        });
    });
});








