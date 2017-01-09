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
    
    public function profile() {
        $this->view->title = "Profile";
        $this->view->render("dashboard/profile");
    }
    
    public function updateprofile() {
        $this->loadModel("Account");
        $AccountModel = new AccountModel();
        
        if (isset($_POST["email"])) { $AccountModel->UpdateUserField("email", $_POST["email"]); }
        if (isset($_POST["username"])) { $AccountModel->UpdateUserField("username", $_POST["username"]); }
        if (isset($_POST["password"])) { $AccountModel->UpdateUserField("password", $_POST["password"]); }
    }
    
    public function mycards() {
        $this->view->title = "My Cards";
        $this->view->render("dashboard/mycards");
    }
    
    public function mydecks() {
        $this->view->title = "My Decks";
        $this->view->render("dashboard/mydecks");
    }

}