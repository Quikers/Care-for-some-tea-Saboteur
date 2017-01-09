<div id="content-body">
    
    
    This is the Profile page.
    
    <form class="forms" method="POST" action="<?= URL ?>/dashboard/updateprofile">
        <label for="email">E-mail</label><input id="email" type="text" name="email" autofocus disabled value="<?= $_SESSION["user"]["email"] ?>"> <a href="#" class="edit">edit</a><br>
        <label for="username">Username</label><input id="username" type="text" name="username" disabled value="<?= $_SESSION["user"]["username"] ?>"> <a href="#" class="edit">edit</a><br>
        <label for="password">Password</label><input id="password" type="text" name="password"><br>
        <label for="cPassword">Confirm Password</label><input id="cPassword" type="text" name="cPassword"><br>
        <input type="submit" value="Update">
    </form>
    
    
</div>


<script>


$(document).ready(function () {
    $("a.edit").click(function (e) {
        e.preventDefault();
        var element = $(this).prev();
        
        if (element.attr("disabled")) element.removeAttr("disabled");
        else element.attr("disabled", true);
    });
});


</script>