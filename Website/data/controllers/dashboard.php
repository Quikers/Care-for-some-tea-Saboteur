<?php

class Dashboard extends Controller {

    function __construct() {
        parent::__construct();
    }
    
    public function index() {
        header("Location:" . URL . "home");
    }
    
    public function profile() {
        if (!isset($_SESSION["loggedIn"]) || $_SESSION["loggedIn"] != true) { header("Location:" . URL . "account"); return; }
        
        $this->view->title = "Profile";
        $this->view->render("dashboard/profile");
    }
    
    public function updateprofile() {
        if (!isset($_SESSION["loggedIn"]) || $_SESSION["loggedIn"] != true) { header("Location:" . URL . "account"); return; }
        
        $this->loadModel("Account");
        $AccountModel = new AccountModel();
        
        echo "<html><body><pre>" . print_r($_POST) . "</pre></body></html>";
        
        if (isset($_POST["email"])) { $AccountModel->UpdateUserField("email", $_POST["email"]); }
        if (isset($_POST["username"])) { $AccountModel->UpdateUserField("username", $_POST["username"]); }
        if (isset($_POST["password"])) { $AccountModel->UpdateUserField("password", $_POST["password"]); }
        
        //header("Location:" . URL . "dashboard/profile");
    }
    
    public function mycards() {
        if (!isset($_SESSION["loggedIn"]) || $_SESSION["loggedIn"] != true) { header("Location:" . URL . "account"); return; }
        
        $this->view->title = "My Cards";
        $this->view->render("dashboard/mycards");
    }
    
    public function mydecks() {
        if (!isset($_SESSION["loggedIn"]) || $_SESSION["loggedIn"] != true) { header("Location:" . URL . "account"); return; }
        
        $this->view->title = "My Decks";
        $this->view->render("dashboard/mydecks");
    }
    
    public function delete($params = array()) {
        if (count($params) > 0) {
            $this->loadModel("Content");
            $contentModel = new ContentModel();
            
            if (is_numeric($params[1])) {
                $contentModel->Delete($params[0], $params[1]);
            } else if (is_string($params[1])) {
                foreach (explode(",", $params[1]) as $id) {
                    $contentModel->Delete($params[0], $id);
                }
            }
        }
        
        header("Location:" . URL . "dashboard/my" . $params[0]);
    }

}