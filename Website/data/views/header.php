<!doctype html>
<html>
<head>
    <title><?=(isset($this->title)) ? "COMET | " . ucfirst($this->title) : 'COMET'; ?></title>
    <link rel="icon" href="<?= URL; ?>favicon.png" />
    
    <link rel="stylesheet" href="<?= URL; ?>public/css/bootstrap/bootstrap.min.css" />
    <link rel="stylesheet" href="<?= URL; ?>public/css/bootstrap/bootstrap-theme.min.css" />
    <link rel="stylesheet" href="<?= URL; ?>public/css/datatables/datatables.css" />
    <link rel="stylesheet" href="<?= URL; ?>public/css/datatables/datatablesCustomized.css" />
    
    <link rel="stylesheet" href="<?= URL; ?>public/css/default.css" />
</head>
<body style="display: none;">

    <?php Session::init(); ?>
    
    <script src="<?= URL; ?>public/js/jquery.js"></script>
    <script src="<?= URL; ?>public/js/bootstrap/bootstrap.min.js"></script>
    <script src="<?= URL; ?>public/js/datatables/datatables.js"></script>
    
    <div id="header">
        <div id="logo"></div>
        <div id="nav">
            <div id="nav-bg"></div>
            <div id="li-container">
                <center>
                    <div class="li<?= strtolower($this->title) == "home" ? " active" : "" ?>"><a href="<?= URL ?>home">Home</a></div>
                    <div class="li<?= strtolower($this->title) == "content" ? " active" : "" ?>"><a href="<?= URL ?>content">Content</a></div>
                    <div class="li<?= strtolower($this->title) == "contact" ? " active" : "" ?>"><a href="<?= URL ?>contact">Contact</a></div>
                </center>
                <div class="li<?= strtolower($this->title) == "login" ? " active" : "" ?>"><a href="<?= URL ?>login">Login</a></div>
                <?php if ($_SESSION["loggedIn"] != true) { ?>
                <?php } else { ?>
                <?php } ?>
            </div>
        </div>
    </div>

    <div id="content">
    
    