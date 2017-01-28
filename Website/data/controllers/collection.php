<?php

class Collection extends Controller {

    function __construct() {
        parent::__construct();
    }

    public function index() {
        header("Location:" . URL . "collection/cards");
    }
    
    public function cards() {
        $this->view->title = "Cards";
        $this->view->render("collection/cards");
    }
    
    public function decks() {
        $this->view->title = "Decks";
        $this->view->render("collection/decks");
    }
    
    public function downloadclient() {
        $file_url = "public/downloads/comet.zip";
        
        header("Content-type: application/zip"); 
        header("Content-Disposition: attachment; filename=" . basename($file_url));
        header("Content-length: " . filesize($file_url));
        header("Pragma: no-cache"); 
        header("Expires: 0"); 
        readfile($file_url);
    }
    
}