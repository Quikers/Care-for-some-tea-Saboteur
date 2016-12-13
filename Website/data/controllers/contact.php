<?php

class Contact extends Controller {

    function __construct() {
        parent::__construct();
    }

    public function index() {
        $this->view->title = "Contact";
        $this->view->render("contact/index");
    }
    
}