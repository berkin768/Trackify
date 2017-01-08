if (!jQuery) { throw new Error("This page requires jQuery") }

(function ($) {

    $('#adminButton')
        .click(function() {
            window.location.href = "Admin/AdminPage";
        });

    $('#table').find('tr').click(function () {         
        var userName = $(this).find('td').text();
        $.ajax({
            url: "/Profile/printUser",
            type: 'POST',
            data: { "DisplayName": userName },
            success: function (data) {            
                window.location.href = 'Profile/ProfileScreen/?name=' + data.displayName;
            }
        });        
    });

})(jQuery);