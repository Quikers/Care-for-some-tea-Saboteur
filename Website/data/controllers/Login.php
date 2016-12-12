<?php

class Login extends Controller {

    function __construct() {
        parent::__construct();
    }

    public function index($params = NULL) {
        $this->view->title = "Login";
        $this->view->render('login/index');
        
        unset($_SESSION["message"]);
    }

    public function login($params = NULL) {
        $this->loadModel("Login");
        $loginModel = new LoginModel();
        
        $result = $loginModel->login($_POST["email"], $_POST["password"]);
        
        if ($result != false) {
            
            $_SESSION["user"] = $result;
            $_SESSION["loggedIn"] = true;
            
            header("Location:" . URL . "dashboard");
        } else {
            $_SESSION["loggedIn"] = false;
            $_SESSION["message"] = "<h3 style=\"color: red; text-align: center; font-weight: 100;\">Invalid username or password.</h3>";
            
            print_r($_SESSION);
            header("Location:" . URL . "login");
        }
    }

    public function register() {
        $this->loadModel("Login");
        $loginModel = new LoginModel();
        
        $result = $loginModel->Register($_POST["email"], $_POST["username"], $_POST["password"]);
        
        $preMessage = "<h1 style=\"color: " . ($result > 0 ? "lightgreen" : "crimson") .  "; text-align: center; font-weight: 100;\">";
        $afterMessage = "</h1>";
        if ((int)$result > 0) {
            $_SESSION["message"] = $preMessage . "Successfully registered!". $afterMessage;
            $_SESSION["login"] = array("username" => $_POST["username"], "password" => $_POST["password"]);
        } else if ($result == 0) {
            $_SESSION["message"] = $preMessage . "Username already exists." . $afterMessage;
        } else {
            $_SESSION["message"] = $preMessage . "Registration failed!<br>Please contact the system administrator." . $afterMessage;
        }
        
        header("Location:" . URL . "login");
    }
}