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

    <?php Session::init(); ?>
    
    <div id="header">
        <div id="logo"></div>
        <div id="nav">
            <div id="nav-bg"></div>
            <div id="li-container">
                <?php if (isset($_SESSION["loggedIn"]) && $_SESSION["loggedIn"] == "1") { ?><p>You are logged in as <strong><?= $_SESSION["user"]["username"] ?></strong> <a class="button" href="<?= URL ?>account/logout">Logout</a></p><?php } ?>
                <center>
                    <div class="li <?= strtolower($this->title) == "home" ? " active" : "" ?>"><a href="<?= URL ?>home">Home</a></div>
                    <div class="li <?= strtolower($this->title) == "content" ? " active" : "" ?>"><a href="<?= URL ?>content">Content</a></div>
                    <div class="li <?= strtolower($this->title) == "contact" ? " active" : "" ?>"><a href="<?= URL ?>contact">Contact</a></div>
                </center>
                <div class="li <?= strtolower($this->title) == "account" ? " active" : "" ?>"><a href="<?= URL ?><?= $_SESSION["loggedIn"] == "1" ? "dashboard" : "account" ?>">Account</a></div>
                <?php if ($_SESSION["loggedIn"] != true) { ?>
                <?php } else { ?>
                <?php } ?>
            </div>
        </div>
    </div>

    <div id="content">
    
    