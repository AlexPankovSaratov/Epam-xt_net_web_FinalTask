$('#btn_signout').click(function() {
    location.href = '/Pages/SignOut';
});
var getbutton = document.querySelector("#GoSearch");
getbutton.addEventListener("click", () => {
    $.ajax('/Pages/SearchPhoto.cshtml', {
        success: function() {
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

