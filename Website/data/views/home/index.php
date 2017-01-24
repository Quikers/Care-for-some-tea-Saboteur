<div id="content-body">
    <?php if (!isset($_SESSION["loggedIn"]) || $_SESSION["loggedIn"] == false) { ?>
    
    <h1>Welcome, young Saboteur.</h1><br>
    <p>Welcome to Care for some tea, Saboteur?'s website!</p>
    <p style="color: #DDDD00; font-weight: 600;">This website is currently under construction so bear with us for a few more weeks!</p>
    
    <?php } else { ?>
    
    <div id="dashboarditem-container">
        <div class="dashboarditem" href="<?= URL ?>dashboard/profile">
            <h2 style="margin-top: 30px;">Profile</h2>
            <p>Edit your profile information, such as your E-mail, Username and Password.</p>
        </div>
        <div class="dashboarditem" href="<?= URL ?>dashboard/mycards">
            <h2>My Cards</h2>
            <p>On this page you will find a table with all of your created Cards.<br>You also have the option to create a new Card, edit a Card or delete a Card.</p>
        </div>
        <div class="dashboarditem" href="<?= URL ?>dashboard/mydecks">
            <h2>My Decks</h2>
            <p>On this page you will find a table with all of your created Decks.<br>You also have the option to create a new Deck, edit a Deck or delete a Deck.</p>
        </div>
    </div>
    
    <?php } ?>
</div>

<script>


$(document).ready(function () {
    
    $(".dashboarditem").click(function () {
        window.location = $(this).attr("href");
    });
    
});


</script>