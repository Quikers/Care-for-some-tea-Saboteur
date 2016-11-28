<?php

class API extends Controller {

    function __construct() {
        parent::__construct();
    }
    
    public function getuser($params = null) {
        echo $params != null ? json_encode($params) : "";
    }

}