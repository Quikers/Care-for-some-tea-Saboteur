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
        
        header("Location:" . URL . "dashboard/profile");
    }
    
    public function adminpanel() {
        if (!isset($_SESSION["loggedIn"]) || $_SESSION["loggedIn"] != true || $_SESSION["user"]["account_type"] != 1) { header("Location:" . URL . "home"); return; }
        
        $this->view->title = "Admin Panel";
        $this->view->render("dashboard/adminpanel");
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
    
    public function uploadcard() {
        $this->loadModel("API");
        $API = new APIModel();
        $card = $API->GetCardByCardID($_POST["id"]);
        
        if ($card != false) {
            $API->UpdateCard(
                array(
                    "id" => $_POST["id"],
                    "name" => $_POST["name"],
                    "cost" => $_POST["cost"],
                    "attack" => $_POST["attack"],
                    "health" => $_POST["health"],
                    "effect" => $_POST["effect"],
                    "activated" => isset($_POST["activated"]) ? $_POST["activated"] : NULL,
                )
            );
        } else {
            $card = $API->CreateCard(
                array(
                    "name" => $_POST["name"],
                    "cost" => $_POST["cost"],
                    "attack" => $_POST["attack"],
                    "health" => $_POST["health"],
                    "effect" => $_POST["effect"]
                )
            );
        }
        
        header("Location:" . URL . "dashboard/mycards/");
    }
    
    public function uploaddeck() {
        $this->loadModel("API");
        $API = new APIModel();
        
        $deck = $API->GetDeckByDeckID($_POST["id"]);
        if ($deck != false) {
            $API->UpdateDeck(
                array(
                    "id" => $_POST["id"],
                    "name" => $_POST["name"],
                    "addedcards" => $_POST["addedcards"],
                    "deletedcards" => $_POST["deletedcards"],
                    "activated" => isset($_POST["activated"]) ? $_POST["activated"] : NULL,
                    "deleted" => isset($_POST["deleted"]) ? $_POST["deleted"] : NULL
                )
            );
        
            header("Location:" . URL . "dashboard/editor/deck/" . $deck["id"]);
        } else {
            $deck = $API->CreateDeck(
                array(
                    "name" => $_POST["name"],
                    "addedcards" => $_POST["addedcards"]
                )
            );
        
            header("Location:" . URL . "dashboard/mydecks/");
        }
    }
    
    public function editor($params = array()) {
        if (count($params) > 0) {
            $this->loadModel("API");
            $APIModel = new APIModel();
            if (count($params) > 1) {
                if ($params[0] == "card") {
                    $card = $APIModel->GetCardByCardID($params[1]);
                    $this->view->card = $card == false ? NULL : $card;
                } else if ($params[0] == "deck") {
                    $deck = $APIModel->GetDeckByDeckID($params[1]);
                    if ($_SESSION["user"]["account_type"] != 1 && $deck["userid"] != $_SESSION["user"]["id"]) {
                        header("Location:" . URL . "dashboard/mydecks");
                    }
                    $this->view->deck = $deck == false ? NULL : $deck;
                }
            }
            
            $this->view->effects = $APIModel->GetAllEffects();
            $this->view->type = $params[0];
            $this->view->title = "Editor";
            $this->view->render("dashboard/editor");
        } else {
            header("Location:" . URL . "home");
        }
    }
    
    public function delete($params = array()) {
        if (count($params) > 0) {
            $this->loadModel("Collection");
            $collectionModel = new CollectionModel();
            
            if (is_numeric($params[1])) {
                $collectionModel->Delete($params[0], $params[1]);
            } else if (is_string($params[1])) {
                foreach (explode(",", $params[1]) as $id) {
                    $collectionModel->Delete($params[0], $id);
                }
            }
        }
        
        header("Location:" . URL . "dashboard/my" . $params[0]);
    }

}