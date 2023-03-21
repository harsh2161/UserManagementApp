function BtnClick(e) {
    var username = $("#username").val();
    var password = $("#password").val();
    var obj = {
        UserEmail: username,
        UserPassword: password
    }
    var url = $("#RedirectTo").val();
    SubmitForm(obj,url)
}


function SubmitForm(obj,url) {
    $.ajax({
        url: "/Account/CheckAuthentication",
        method: "POST",
        data: obj,
        success: function (data) {
            console.log(data);
            if (data == "NotAuthenticated") {
                alert("Wrong Credentials");
            }
            else {
                window.location = "Home/Dashboard";
            }
        },
        error: function (error) {
            alert("Unknow Error : " + error);
        }
    })
}