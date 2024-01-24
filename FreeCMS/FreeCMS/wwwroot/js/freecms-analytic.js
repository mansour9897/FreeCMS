$(document).ready(function () {
    var url = window.location.href; // Returns full URL    
    var referrer = document.referrer;
    var ajaxDate = { page: url, referrer: referrer }
    $.ajax({
        url: "/Analytic",
        type: "POST",
        data: ajaxDate,
        success: function (data, textStatus, jqXHR) {
        }
    });
});