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
        <h2 class="changeView">Login<i class="fa fa-chevron-down"></i></h2>
        <form method="POST" action="<?= URL ?>login">
            <input required class="form-control" type="text" name="username" id="iUsername" value="<?= $username ?>" placeholder="Username" autofocus><br>
            <input required class="form-control" type="password" name="password" id="iPassword" value="<?= $password ?>" placeholder="Password"><br>
            <input class="btn btn-default form-control" type="submit" id="iSubmit" value="Login">
        </form>
    </div>
    
    <div id="register-container">
        <h2 class="changeView">Registration<i class="fa fa-chevron-down"></i></h2><br>
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


$(".changeView").click(function(event) {
    console.log("Clicked!");
    console.log($("#login-container form").css("display"));
    
    var value1 = $("#login-container form").css("display") === "none" ? 
    {
        display: "block"
    } : { 
        display: "none"
    };
    var value2 = $("#login-container form").css("display") === "none" ? 
    {
        display: "none"
    } : { 
        display: "block"
    };
    
    $("#login-container form").css(value1);
    $("#register-container form").css(value2);
});


</script>