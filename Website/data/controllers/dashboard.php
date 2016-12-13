<?php

class Dashboard extends Controller {

    function __construct() {
        parent::__construct();
    }
    
    public function index() {
        if (isset($_SESSION["loggedIn"]) && $_SESSION["loggedIn"] == "1") {
            $this->view->title = "Dashboard";
            $this->view->render("dashboard/index");
        } else { header("Location:" . URL . "home"); }
    }

}