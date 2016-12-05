<div id="form-container" class="form-group">
    
    <?php 
    
    $username = "";
    $password = "";
    
    if (isset($this->message)) { echo $this->message; }
    if (isset($this->login)) {
        $username = $this->login["username"];
        $password = $this->login["password"];
    }
    
    ?>
    
    <div id="login-container">
        <h2 class="changeView">Login<i class="fa fa-chevron-down fa-smfnt"></i></h2>
        <form method="POST" action="<?= URL ?>login">
            <input required class="form-control" type="text" name="username" id="iUsername" value="<?= $username ?>" placeholder="Username" autofocus><br>
            <input required class="form-control" type="password" name="password" id="iPassword" value="<?= $password ?>" placeholder="Password"><br>
            <input class="btn btn-default form-control" type="submit" id="iSubmit" value="Login">
        </form>
    </div>
    
    <div id="register-container">
        <h2 class="changeView">Registration<i class="fa fa-chevron-up fa-smfnt"></i></h2><br>
        <form method="POST" action="<?= URL ?>login/register" style="display: none;">
            <input required class="form-control" type="email"     name="email"     id="iEmail"     placeholder="E-mail"><br>
            <input required class="form-control" type="text"      name="username"  id="iUsername"  placeholder="Username"><br>
            <input required class="form-control" type="password"  name="password"  id="iPassword"  placeholder="Password"><br>
            <input required type="password"  name="password"  id="iPassword"  placeholder="Password" hidden>
            <input class="btn btn-default form-control" type="submit" id="iSubmit" value="Register">
        </form>
    </div>
</div>

<script>


$(document).ready(function () {
    $("#login-container #iUsername").focus(function () {
        console.log("Test");
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