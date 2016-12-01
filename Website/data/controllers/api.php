<?php

class API extends Controller {
    
    public $API;

    function __construct() {
        parent::__construct();
        
        $this->loadModel("API");
        $this->API = new APIModel();
    }
    
    public function index($params = null) {
        echo "<html><body>";
        echo "Please use one of the following functions:<br><br>";
        echo "getuserbyid (http://careforsometeasaboteur.com/api/getuserbyid/1)<br>";
        echo "getuserbyemail (http://careforsometeasaboteur.com/api/getuserbyemail/admin@careforsometeasaboteur.com)<br>";
        echo "getuserbyusername (http://careforsometeasaboteur.com/api/getuserbyusername/admin)<br>";
        echo "</html></body>";
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