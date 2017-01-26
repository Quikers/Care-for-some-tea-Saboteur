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
        $file_url = URL . "public/gameclient/comet.zip";
        header('Collection-Type: application/octet-stream');
        header("Collection-Transfer-Encoding: Binary"); 
        header("Collection-disposition: attachment; filename=\"" . basename($file_url) . "\""); 
        readfile($file_url);
    }
    
}