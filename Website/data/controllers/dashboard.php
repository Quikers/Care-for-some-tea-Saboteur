<?php

class Dashboard extends Controller {

    function __construct() {
        parent::__construct();
    }
    
    public function index() {
        if (isset($_SESSION["loggedIn"]) && $_SESSION["loggedIn"] == "1") {
            header("Location:" . URL . "dashboard/profile");
        } else { header("Location:" . URL . "home"); }
    }

}