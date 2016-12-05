<?php

class User {

    function __construct() { }

    public $getuserbyid = "";
    public $getuserbyemail = "";
    public $getuserbyusername = "";
}

class Match {

    function __construct() { }

    public $getmatchbyid = "";
    public $getmatchbyplayer = "";
}

class API extends Controller {
    
    public $API;

    function __construct() {
        parent::__construct();
        
        $this->loadModel("API");
        $this->API = new APIModel();
    }
    
    public function index() {
        header("Location:" . URL . "api/docs");
    }
    
    public function docs($params = null) {
        $this->view->methodGroups = array("User", "Match");
        
        $this->view->title = "API Documentation";
        $this->view->render('api/index');
    }
    
    public function getuserbyid($params = null) {
        if (count($params) > 0) {
            echo json_encode($this->API->GetUserByID(explode(",", $params[0])));
        } else { echo 0; }
    }
    
    public function getuserbyemail($params = null) {
        if (count($params) > 0) {
            echo json_encode($this->API->GetUserByEmail(explode(",", $params[0])));
        } else { echo 0; }
    }
    
    public function getuserbyusername($params = null) {
        if (count($params) > 0) {
            echo json_encode($this->API->GetUserByUsername(explode(",", $params[0])));
        } else { echo 0; }
    }

}