<!doctype html>
<html>
<head>
    <title><?=(isset($this->title)) ? "COMET | " . ucfirst($this->title) : 'COMET'; ?></title>
    <link rel="icon" href="<?= URL ?>favicon.png" />
    
    <link rel="stylesheet" href="<?= URL ?>public/css/bootstrap/bootstrap.min.css" />
    <link rel="stylesheet" href="<?= URL ?>public/css/bootstrap/bootstrap-theme.min.css" />
    <link rel="stylesheet" href="<?= URL ?>public/css/datatables/datatables.css" />
    <link rel="stylesheet" href="<?= URL ?>public/css/datatables/datatablesCustomized.css" />
    <link rel="stylesheet" href="<?= URL ?>public/css/icheck/all.css">
    
    <link rel="stylesheet" href="<?= URL ?>public/css/header.css" />
    <link rel="stylesheet" href="<?= URL ?>public/css/default.css" />
    
    <script src="<?= URL ?>public/js/jquery.js"></script>
    <script src="<?= URL ?>public/js/bootstrap/bootstrap.min.js"></script>
    <script src="<?= URL ?>public/js/datatables/datatables.js"></script>
    <script src="<?= URL ?>public/js/icheck/icheck.min.js"></script>
    
    <script src="https://use.fontawesome.com/bcfab1323f.js"></script>
</head>
<body>

    <?php Session::init();
    
    $active["home"] = "";
    $active["collection"] = "";
    $active["cards"] = "";
    $active["decks"] = "";
    $active["contact"] = "";
    $active["account"] = "";
    $active["dashboard"] = "";
    $active["profile"] = "";
    $active["adminpanel"] = "";
    $active["mycards"] = "";
    $active["mydecks"] = "";
    
    $title = str_replace(" ", "", strtolower($this->title));
    
    switch($title) {
        default: echo $title; break;
        case "home": case "contact":
            $active[$title] = " active";
            break;
        case "collection": case "cards": case "decks":
            $active["collection"] = " active";
            $active[$title] = " active";
            break;
        case "account": case "profile": case "dashboard": case "adminpanel": case "mycards": case "mydecks":
            $active["account"] = " active";
            $active[$title] = " active";
            break;
    }
    
    ?>
    
    <div id="header">
        <div id="logo"></div>
        <div id="nav">
            <div id="nav-bg"></div>
            <div id="li-container">
                <?php if (isset($_SESSION["loggedIn"]) && $_SESSION["loggedIn"] == "1") { ?>
                <p>Welcome, <strong><?= $_SESSION["user"]["username"] ?></strong> <a class="button" href="<?= URL ?>account/logout">Logout</a></p>
                <?php } else { ?>
                <div id="downloadButton" class="li"><a href="<?= URL ?>collection/downloadclient"><i class="fa fa-download" aria-hidden="true"></i> Download</a></div>
                <?php } ?>
                <center>
                    <div class="li<?= $active["home"] ?>"><a href="<?= URL ?>home"><i class="fa fa-home" aria-hidden="true"></i> Home</a></div>
                    <div class="li dropdown<?= $active["collection"] ?>">
                        <i class="fa fa-bars" aria-hidden="true"></i> Collection
                        <div class="dropdown-content">
                            <div class="li-dropdown<?= $active["cards"] ?>"><a href="<?= URL ?>collection/cards"><i class="fa fa-list" aria-hidden="true"></i> Cards</a></div>
                            <div class="li-dropdown<?= $active["decks"] ?>"><a href="<?= URL ?>collection/decks"><i class="fa fa-list" aria-hidden="true"></i> Decks</a></div>
                        </div>
                    </div>
                    <div class="li<?= $active["contact"] ?>"><a href="<?= URL ?>contact"><i class="fa fa-phone" aria-hidden="true"></i> Contact</a></div>
                </center>
                <?php if ($_SESSION["loggedIn"] != 1) { ?>
                <div class="li<?= $active["account"] ?>"><a href="<?= URL ?><?= $_SESSION["loggedIn"] == "1" ? "dashboard" : "account" ?>"><i class="fa fa-user" aria-hidden="true"></i> Account</a></div>
                <?php } else { ?>
                <div class="li dropdown<?= $active["account"] ?>">
                    <i class="fa fa-bars" aria-hidden="true"></i> Account
                    <div class="dropdown-content">
                        <div class="li-dropdown<?= $active["profile"] ?>"><a href="<?= URL ?>dashboard/profile"><i class="fa fa-user" aria-hidden="true"></i> Profile</a></div>
                        <?php if ($_SESSION["user"]["account_type"] == 1) { ?>
                        <div class="li-dropdown<?= $active["adminpanel"] ?>"><a href="<?= URL ?>dashboard/adminpanel"><i class="fa fa-tachometer" aria-hidden="true"></i> Admin</a></div>
                        <?php } ?>
                        <div class="li-dropdown<?= $active["mycards"] ?>"><a href="<?= URL ?>dashboard/mycards"><i class="fa fa-list" aria-hidden="true"></i> My Cards</a></div>
                        <div class="li-dropdown<?= $active["mydecks"] ?>"><a href="<?= URL ?>dashboard/mydecks"><i class="fa fa-list" aria-hidden="true"></i> My Decks</a></div>
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
    