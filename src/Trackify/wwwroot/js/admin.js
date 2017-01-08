if (!jQuery) { throw new Error("This page requires jQuery") }

(function ($) {
    $("#addButton")
        .click(function () {
            window.location.href = "AddUser";
        });

    $("#save")
        .click(function () {
            var userName = $("#input").val();    
            $.ajax({
                url: "/Profile/getUsers",
                type: 'POST',
                data: { "DisplayName": userName },
                success: function (data) {
                    if (data.sit === "success") {
                        alert("Add Operation Completed");
                    } else {
                        alert("Something Failed");
                    }
                    window.location.href = "/";
                }
            });
        });

        $(".user-action").click(function (e) {
            e.preventDefault();           
            var d = {
                DisplayName: $(this).data("username")              
            };
            var URL = $(this).data("url");
            // now make the ajax call         
            $.ajax({
                url: URL,
                type: 'POST',
                data: d,
                success: function (data) {
                    if (data.sit === "success") {
                        alert("Delete Operation Completed");
                        location.reload();
                    }
                    else if (data.sit === "fail") {
                        alert("Something Failed");
                        location.reload();
                    }
                    else if (data.sit === "update") {                        
                        window.location.href = "UpdatePage?name=" + data.displayName;
                    }                                   
                }
            });
        });

})(jQuery);