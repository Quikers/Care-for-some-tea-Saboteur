<?php

class Account extends Controller {

    function __construct() {
        parent::__construct();
    }

    public function index($params = NULL) {
        if ($_SESSION["loggedIn"] != "1") {
            if (isset($_SESSION["login"])) { $this->view->login = $_SESSION["login"]; }
            $this->view->title = "Account";
            $this->view->render('account/index');
            
            unset($_SESSION["login"]);
            unset($_SESSION["message"]);
        } else { header("Location:" . URL . "dashboard"); }
    }

    public function login($params = NULL) {
        $this->loadModel("Account");
        $accountModel = new AccountModel();
        
        $result = $accountModel->login($_POST["email"], $_POST["password"]);
        
        if ($result != false) {
            
            $_SESSION["user"] = $result;
            $_SESSION["loggedIn"] = true;
            
            header("Location:" . URL . "dashboard");
        } else {
            $_SESSION["loggedIn"] = false;
            $_SESSION["message"] = "<h3 style=\"color: red; text-align: center; font-weight: 100;\">Invalid e-mail or password.</h3>";
            
            print_r($_SESSION);
            header("Location:" . URL . "account");
        }
    }
    
    public function logout() {
        Session::destroy();
        
        header("Location:" . URL . "home");
    }

    public function register() {
        $this->loadModel("Account");
        $this->loadModel("API");
        $accountModel = new AccountModel();
        $API = new APIModel();
        
        $result = 0;
        $afterMessage = "</h2>";
        $preMessage = "<h2 style=\"color: " . ($result > 0 ? "lightgreen" : "crimson") .  "; text-align: center; font-weight: 100;\">";
        
        if (isset($API->GetUserByEmail(array($_POST["email"]))["id"])) { $_SESSION["message"] = $preMessage . "Email already exists." . $afterMessage; }
        else if (isset($API->GetUserByUsername(array($_POST["username"]))["id"])) { $_SESSION["message"] = $preMessage . "Username already exists." . $afterMessage; }
        else {
            $result = $accountModel->Register($_POST["email"], $_POST["username"], $_POST["password"]);
            $preMessage = "<h1 style=\"color: " . ($result > 0 ? "#86cc86" : "crimson") .  "; text-align: center; font-weight: 100;\">";
            
            if ((int)$result > 0) {
                $_SESSION["message"] = $preMessage . "Successfully registered!". $afterMessage;
                $_SESSION["login"] = array("email" => $_POST["email"], "password" => $_POST["password"]);
            } else {
                $_SESSION["message"] = $preMessage . "Registration failed!<br>Please contact the system administrator." . $afterMessage;
            }
        }

        header("Location:" . URL . "account");
    }
}