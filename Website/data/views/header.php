<!doctype html>
<html>
<head>
    <title><?=(isset($this->title)) ? "COMET | " . ucfirst($this->title) : 'COMET'; ?></title>
    <link rel="icon" href="<?= URL; ?>favicon.png" />
    
    <link rel="stylesheet" href="<?= URL; ?>public/css/bootstrap/bootstrap.min.css" />
    <link rel="stylesheet" href="<?= URL; ?>public/css/bootstrap/bootstrap-theme.min.css" />
    <link rel="stylesheet" href="<?= URL; ?>public/css/datatables/datatables.css" />
    <link rel="stylesheet" href="<?= URL; ?>public/css/datatables/datatablesCustomized.css" />
    
    <link rel="stylesheet" href="<?= URL; ?>public/css/header.css" />
    <link rel="stylesheet" href="<?= URL; ?>public/css/default.css" />
    
    <script src="<?= URL; ?>public/js/jquery.js"></script>
    <script src="<?= URL; ?>public/js/bootstrap/bootstrap.min.js"></script>
    <script src="<?= URL; ?>public/js/datatables/datatables.js"></script>
    <script src="https://use.fontawesome.com/bcfab1323f.js"></script>
</head>
<body style="display: none;">

    <?php Session::init();
    
    $active["home"] = "";
    $active["content"] = "";
    $active["cards"] = "";
    $active["decks"] = "";
    $active["contact"] = "";
    $active["account"] = "";
    $active["dashboard"] = "";
    $active["profile"] = "";
    $active["mycards"] = "";
    $active["mydecks"] = "";
    
    switch(strtolower($this->title)) {
        case "home":
            $active["home"] = " active";
            break;
        case "content": case "cards": case "decks":
            $active["content"] = " active";
            $active[strtolower($this->title)] = " active";
            break;
        case "contact":
            $active["contact"] = " active";
            break;
        case "account": case "profile": case "dashboard": case "mycards": case "mydecks":
            $active["account"] = " active";
            $active[strtolower($this->title)] = " active";
            break;
    }
    
    ?>
    
    <div id="header">
        <div id="logo"></div>
        <div id="nav">
            <div id="nav-bg"></div>
            <div id="li-container">
                <?php if (isset($_SESSION["loggedIn"]) && $_SESSION["loggedIn"] == "1") { ?><p>Welcome, <strong><?= $_SESSION["user"]["username"] ?></strong> <a class="button" href="<?= URL ?>account/logout">Logout</a></p><?php } ?>
                <center>
                    <div class="li<?= $active["home"] ?>"><a href="<?= URL ?>home">Home</a></div>
                    <div class="li dropdown<?= $active["content"] ?>">
                        Content
                        <div class="dropdown-content">
                            <div class="li-dropdown<?= $active["cards"] ?>"><a href="<?= URL ?>content/cards">Cards</a></div>
                            <div class="li-dropdown<?= $active["decks"] ?>"><a href="<?= URL ?>content/decks">Decks</a></div>
                        </div>
                    </div>
                    <div class="li<?= $active["contact"] ?>"><a href="<?= URL ?>contact">Contact</a></div>
                </center>
                <?php if ($_SESSION["loggedIn"] != 1) { ?>
                <div class="li<?= $active["account"] ?>"><a href="<?= URL ?><?= $_SESSION["loggedIn"] == "1" ? "dashboard" : "account" ?>">Account</a></div>
                <?php } else { ?>
                <div class="li dropdown<?= $active["account"] ?>">
                    Account
                    <div class="dropdown-content">
                        <div class="li-dropdown<?= $active["profile"] ?>"><a href="<?= URL ?>dashboard/profile">Profile</a></div>
                        <div class="li-dropdown<?= $active["mycards"] ?>"><a href="<?= URL ?>dashboard/mycards">My Cards</a></div>
                        <div class="li-dropdown<?= $active["mydecks"] ?>"><a href="<?= URL ?>dashboard/mydecks">My Decks</a></div>
                    </div>
                </div>
                <?php } ?>
            </div>
        </div>
    </div>
    
<script>
    
$(document).ready(function () {
    $(".dropdown").mouseenter(function () {
        $(this).find(".dropdown-content").addClass("show");
    });
    
    $(".dropdown").mouseleave(function () {
        $(this).find(".dropdown-content").removeClass("show");
    });
});

</script>

    <div id="content">
    
    