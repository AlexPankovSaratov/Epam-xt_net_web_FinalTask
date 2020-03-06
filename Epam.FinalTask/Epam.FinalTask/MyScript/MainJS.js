$('#btn_signout').click(function() {
    location.href = '/Pages/SignOut';
});

//$(".LikeButton").click(function () {
//    let val = $(this).attr("value");
//    var countLikes = $.post('/Pages/LikePhoto.cshtml', { Id: val }).responseText;
//    $(this).children(".CountLikes").html(countLikes);
//})

$(".LikeButton").click(function () {
    let val = $(this).attr("value");
    var button = $(this);
    $.post(
        '/Pages/LikePhoto.cshtml',
        { Id: val },
        function (data) {
            $(button).children(".CountLikes").html(parseInt(data));
        });
})

var getbutton = document.querySelector("#GoSearch");
if (getbutton != undefined) {
    getbutton.addEventListener("click", () => {
        $.ajax('/Pages/SearchPhoto.cshtml', {
            success: function () {
                $('.photo').hide();
                var SearchMode = document.getElementById("SearchMode").value;
                var SearchValue = document.getElementById("SearchValue").value.toUpperCase().trim();
                console.log(SearchMode);
                console.log(SearchValue);
                switch (SearchMode) {
                    case "Author":
                        $('.Author-' + SearchValue).show();
                        break;
                    case "Country":
                        $('.Country-' + SearchValue).show();
                        break;
                    case "All photo":
                        $('.photo').show();
                        break;
                    default:
                }
            }
        });
    });
}


