<?php

class Content extends Controller {

    function __construct() {
        parent::__construct();
    }

    public function index() {
        header("Location:" . URL . "content/cards");
    }
    
    public function cards() {
        $this->view->title = "Cards";
        $this->view->render("content/cards");
    }
    
    public function decks() {
        $this->view->title = "Decks";
        $this->view->render("content/decks");
    }
    
    public function downloadclient() {
        $file_url = URL . "public/gameclient/comet.zip";
        header('Content-Type: application/octet-stream');
        header("Content-Transfer-Encoding: Binary"); 
        header("Content-disposition: attachment; filename=\"" . basename($file_url) . "\""); 
        readfile($file_url);
    }
    
}