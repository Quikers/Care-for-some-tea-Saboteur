<div id="container">
    <h1>Profile</h1><br>
    <p>On this page you can edit your account information<br><br>If you want something to stay unchanged then leave it blank or don't change it's value.</p>

    <form class="forms" method="POST" action="<?= URL ?>dashboard/updateprofile">
        <label for="email">E-mail</label><input id="email" type="text" name="email" autofocus disabled value="<?= $_SESSION["user"]["email"] ?>"> <a href="#" class="edit">edit</a><br>
        <label for="username">Username</label><input id="username" type="text" name="username" disabled value="<?= $_SESSION["user"]["username"] ?>"> <a href="#" class="edit">edit</a><br>
        <label for="password">Password</label><input pattern=".{4,}" title="The password needs to be a minimum of 4 characters long." id="password" type="password" name="password"><br>
        <label for="cPassword">Confirm Password</label><input pattern=".{4,}" title="The password needs to be a minimum of 4 characters long." id="cPassword" type="password" name="cPassword"><br>
        <p id="password-status">The passwords don't match.</p><br>
        <input type="submit" value="Update">
    </form>

</div>


<script>


$(document).ready(function () {
    var sessionEmail = "<?= $_SESSION["user"]["email"] ?>";
    var sessionUsername = "<?= $_SESSION["user"]["username"] ?>";
    
    function CheckPasswordValidity(input) {
        $("#cPassword").attr("required", true);
        
        if ($(input).val() !== $("#password").val()) {
            $("input[type=\"password\"]").css("border", "2px solid crimson");
            $("input[type=\"password\"]").css("background", "salmon");
            $("#password-status").css("display", "inline-block");
        } else {
            $("input[type=\"password\"]").css("border", "2px solid green");
            $("input[type=\"password\"]").css("background", "lightgreen");
            $("#password-status").css("display", "none");
        }
    }
    
    function ClearPasswordBoxes() {
        $("input[type=\"password\"]").css("border", "1px solid #bfbfbd");
        $("input[type=\"password\"]").css("background", "white");
        $("#cPassword").removeAttr("required");
        $("#password-status").css("display", "none");
    }
    
    $("#cPassword").keyup(function () {
        if ($(this).val() !== "")
            CheckPasswordValidity(this);
        
        if ($(this).val() === "" && $("#password").val() === "")
            ClearPasswordBoxes();
    });
    
    $("#password").keyup(function () {
        if ($(this).val() !== "")
            CheckPasswordValidity(this);
        
        if ($(this).val() === "" && $("#cPassword").val() === "")
            ClearPasswordBoxes();
    });
    
    $("a.edit").click(function (e) {
        e.preventDefault();
        var element = $(this).prev();
        
        if (element.attr("disabled")) {
            element.removeAttr("disabled");
            element.focus();
            element.select();
        }
        else if (element.val() == sessionEmail || element.val() == sessionUsername) element.attr("disabled", true);
    });
    
    $("input[type\"email\"], input[type\"text\"]").blur(function () {
        var element = $(this);
        if (element.val() == sessionEmail || element.val() == sessionUsername) element.attr("disabled", true);
    });
});


</script>