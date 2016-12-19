<?php 
    
$username = "";
$password = "";

if (isset($_SESSION["message"])) {
    echo '<center><div id="message-content">';
    echo $_SESSION["message"];
    echo "</div></center>";
}
if (isset($this->login)) {
    $username = $this->login["email"];
    $password = $this->login["password"];
}
?>

<div id="form-container" class="form-group">
    <div id="login-container">
        <h2 class="changeView">Login<i class="fa fa-chevron-down fa-smfnt"></i></h2>
        <form method="POST" action="<?= URL ?>account/login">
            <input required class="form-control" type="text" name="email" id="iEmail" value="<?= $email ?? "" ?>" placeholder="E-mail" autofocus><br>
            <input required class="form-control" type="password" name="password" id="iPassword" value="<?= $password ?? "" ?>" placeholder="Password"><br>
            <input class="btn btn-default form-control" type="submit" id="iSubmit" value="Login">
        </form>
    </div>
    
    <div id="register-container">
        <h2 class="changeView">Registration<i class="fa fa-chevron-up fa-smfnt"></i></h2><br>
        <form method="POST" action="<?= URL ?>account/register" style="display: none;">
            <input required class="form-control" type="email"     name="email"     id="iEmail"     placeholder="E-mail"><br>
            <input required class="form-control" type="text"      name="username"  id="iUsername"  placeholder="Username"><br>
            <input required class="form-control" type="password"  name="password"  id="iPassword"  placeholder="Password"><br>
            <input required class="form-control" type="password"  name="passwordConfirm"  id="iPasswordConfirm"  placeholder="Confirm password"><br>
            <input class="btn btn-default form-control" type="submit" id="iSubmit" value="Register">
        </form>
    </div>
</div>

<script>


$(document).ready(function () {
    $("#register-container #iPasswordConfirm").keyup(function () {
        console.log("Key Up");
        if ($(this).val() !== $("#register-container #iPassword").val())
            this.setCustomValidity("Passwords Don't Match");
        else
            this.setCustomValidity("");
    });

    $(".changeView").click(function(event) {
        var focussedItem;

        if ($("#login-container form").css("display") === "none") {
            focussedItem = $("#login-container #iUsername");

            $("#register-container form").slideUp();
            $("#login-container form").slideDown();
        } else {
            focussedItem = $("#register-container #iEmail");

            $("#login-container form").slideUp();
            $("#register-container form").slideDown();
        }

        var down = $(".fa.fa-chevron-down");
        var up = $(".fa.fa-chevron-up");

        down.removeClass("fa-chevron-down");
        down.addClass("fa-chevron-up");
        up.removeClass("fa-chevron-up");
        up.addClass("fa-chevron-down");

        $(focussedItem).focus();
    });
});


</script>