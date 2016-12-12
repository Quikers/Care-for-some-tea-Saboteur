<?php

class Home extends Controller {

    function __construct() {
        parent::__construct();
    }
    
    public function index() {
        $this->view->session = json_encode($_SESSION);
        $this->view->title = "Home";
        $this->view->render('home/index');
    }

}