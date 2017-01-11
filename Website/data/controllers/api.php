<?php

// API documentation starts

class User {

    function __construct() { }

    public $getuserbyuserid = "<p>Gets a <strong>user</strong> object from the database by searching for the <strong>user's</strong> <strong>userID</strong>.<br>You can request multiple <strong>users</strong> simultaneously by separating the <strong>userIDs</strong> with a comma. (1,2,3 etc.)<br><br>Usage:</p><pre>" . URL . "api/getuserbyuserid/<strong>:userid</strong>(,<strong>:userid</strong>)</pre><br><p>Examples:</p><pre>" . URL . "api/getuserbyuserid/1<br>" . URL . "api/getuserbyuserid/1,2</pre>";
    public $getuserbyemail = "<p>Gets a <strong>user</strong> object from the database by searching for the <strong>user's</strong> <strong>e-mail</strong>.<br>You can request multiple <strong>users</strong> simultaneously by separating the <strong>e-mails</strong> with a comma.<br><br>Usage:</p><pre>" . URL . "api/getuserbyemail/<strong>:e-mail</strong>(,<strong>:e-mail</strong>)</pre><br><p>Examples:</p><pre>" . URL . "api/getuserbyemail/admin@careforsometeasaboteur.com<br>" . URL . "api/getuserbyemail/admin@careforsometeasaboteur.com,test@test.test</pre>";
    public $getuserbyusername = "<p>Gets a <strong>user</strong> object from the database by searching for the <strong>user's</strong> <strong>username</strong>.<br>You can request multiple <strong>users</strong> simultaneously by separating the <strong>usernames</strong> with a comma.<br><br>Usage:</p><pre>" . URL . "api/getuserbyusername/<strong>:username</strong>(,<strong>:username</strong>)</pre><br><p>Examples:</p><pre>" . URL . "api/getuserbyusername/admin<br>" . URL . "api/getuserbyusername/admin,test</pre>";
    public $checklogin = "<p>Attempts to log in a <strong>user</strong> and if successful will send back the <strong>user's</strong> object.<br><br>Usage:</p><pre>" . URL . "api/checklogin/<strong>:e-mail</strong>/<strong>:password</strong></pre><br><p>Examples:</p><pre>" . URL . "api/checklogin/testemail@test.com/testpassword</pre>";
}

class Card {

    function __construct() { }

    public $getcardbycardid = "<p>Gets a <strong>card</strong> object from the database by searching for the <strong>card's</strong> <strong>cardID</strong>.<br>You can request multiple <strong>cards</strong> simultaneously by separating the <strong>cardIDs</strong> with a comma.<br><br>Usage:</p><pre>" . URL . "api/getcardbycardid/<strong>:cardid</strong>(,<strong>:cardid</strong>)</pre><br><p>Examples:</p><pre>" . URL . "api/getcardbycardid/1<br>" . URL . "api/getcardbycardid/1,2</pre>";
    public $getcardbycardname = "<p>Gets a <strong>card</strong> object from the database by searching for the <strong>card's</strong> <strong>cardname</strong>.<br>You can request multiple <strong>cards</strong> simultaneously by separating the <strong>cardnames</strong> with a comma.<br><br>Usage:</p><pre>" . URL . "api/getcardbycardname/<strong>:cardname</strong>(,<strong>:cardname</strong>)</pre><br><p>Examples:</p><pre>" . URL . "api/getcardbycardname/testcard1<br>" . URL . "api/getcardbycardname/testcard1,testcard2</pre>";
}

class Deck {

    function __construct() { }

    public $getdeckbydeckid = "<p>Gets a <strong>deck</strong> object from the database by searching for the <strong>deck's</strong> <strong>deckID</strong>.<br>You can request multiple <strong>decks</strong> simultaneously by separating the <strong>deckIDs</strong> with a comma.<br><br>Usage:</p><pre>" . URL . "api/getdeckbydeckid/<strong>:deckid</strong>(,<strong>:deckid</strong>)</pre><br><p>Examples:</p><pre>" . URL . "api/getdeckbydeckid/1<br>" . URL . "api/getdeckbydeckid/1,2</pre>";
    public $getdeckbyuserid = "<p>Gets a <strong>deck</strong> object from the database by searching for the <strong>deck's</strong> <strong>userID</strong>.<br>You can request multiple user's <strong>decks</strong> simultaneously by separating the <strong>userIDs</strong> with a comma.<br><br>Usage:</p><pre>" . URL . "api/getdeckbyuserid/<strong>:userid</strong>(,<strong>:userid</strong>)</pre><br><p>Examples:</p><pre>" . URL . "api/getdeckbyuserid/1<br>" . URL . "api/getdeckbyuserid/1,2</pre>";
    public $getdeckbyusername = "<p>Gets a <strong>deck</strong> object from the database by searching for the <strong>deck's</strong> <strong>username</strong>.<br>You can request multiple user's <strong>decks</strong> simultaneously by separating the <strong>usernames</strong> with a comma.<br><br>Usage:</p><pre>" . URL . "api/getdeckbyusername/<strong>:username</strong>(,<strong>:username</strong>)</pre><br><p>Examples:</p><pre>" . URL . "api/getdeckbyusername/admin<br>" . URL . "api/getdeckbyusername/admin,test</pre>";
}

class Match {

    function __construct() { }

    public $getmatchbyid = "<p>Gets a <strong>match</strong> object from the database by searching for the <strong>match's</strong> <strong>matchID</strong>.<br>You can request multiple <strong>matches</strong> simultaneously by separating the <strong>matchIDs</strong> with a comma.<br><br>Usage:</p><pre>" . URL . "api/getmatchbymatchid/<strong>:matchid</strong>(,<strong>:matchid</strong>)</pre><br><p>Examples:</p><pre>" . URL . "api/getmatchbymatchid/1<br>" . URL . "api/getmatchbymatchid/1,2</pre>";
    public $getmatchbyuserid = "<p>Gets a <strong>match</strong> object from the database by searching for one of the <strong>match's</strong> player's <strong>userID</strong>.<br>You can request multiple user's <strong>matches</strong> simultaneously by separating the <strong>userIDs</strong> with a comma.<br><br>Usage:</p><pre>" . URL . "api/getmatchbyuserid/<strong>:userid</strong>(,<strong>:userid</strong>)</pre><br><p>Examples:</p><pre>" . URL . "api/getmatchbyuserid/1<br>" . URL . "api/getmatchbyuserid/1,2</pre>";
    public $getmatchbyusername = "<p>Gets a <strong>match</strong> object from the database by searching for one of the <strong>match's</strong> player's <strong>username</strong>.<br>You can request multiple user's <strong>matches</strong> simultaneously by separating the <strong>usernames</strong> with a comma.<br><br>Usage:</p><pre>" . URL . "api/getmatchbyusername/<strong>:username</strong>(,<strong>:username</strong>)</pre><br><p>Examples:</p><pre>" . URL . "api/getmatchbyusername/admin<br>" . URL . "api/getmatchbyusername/admin,test</pre>";
}

// API documentation ends

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
        $this->view->methodGroups = array("User", "Card", "Deck", "Match");
        
        $this->view->title = "API Documentation";
        $this->view->render('api/index');
    }
    
    public function getuserbyuserid($params = null) {
        if (count($params) > 0) {
            $includeFriends = false;
            
            if (isset($params[1]) && $params[1] == "true") { $includeFriends = true; }
            echo json_encode($this->API->GetUserByUserID(explode(",", $params[0]), $includeFriends));
        } else { echo 0; }
    }
    
    public function getuserbyemail($params = null) {
        if (count($params) > 0) {
            $includeFriends = false;
            
            if (isset($params[1]) && $params[1] == "true") { $includeFriends = true; }
            echo json_encode($this->API->GetUserByEmail(explode(",", $params[0]), $includeFriends));
        } else { echo 0; }
    }
    
    public function getuserbyusername($params = null) {
        if (count($params) > 0) {
            $includeFriends = false;
            
            if (isset($params[1]) && $params[1] == "true") { $includeFriends = true; }
            echo json_encode($this->API->GetUserByUsername(explode(",", $params[0]), $includeFriends));
        } else { echo 0; }
    }
    
    public function getuserfriends($params = null) {
        if (count($params) > 0) {
            if (!is_numeric($params[0])) { echo 0; return; }
            
            echo json_encode($this->API->GetUserFriendsByUserID($params[0]));
        } else { echo 0; }
    }
    
    public function checklogin($params = null) {
        if (count($params) == 2) {
            echo json_encode($this->API->CheckLogin($params[0], $params[1]));
        } else { echo 0; }
    }
    
    public function getcardbycardid($params = null) {
        if (count($params) > 0) {
            echo json_encode($this->API->GetCardByCardID(explode(",", $params[0])));
        } else { echo 0; }
    }
    
    public function getcardbycardname($params = null) {
        if (count($params) > 0) {
            echo json_encode($this->API->GetCardByCardName(explode(",", $params[0])));
        } else { echo 0; }
    }
    
    public function getdeckbydeckid($params = null) {
        if (count($params) > 0) {
            echo json_encode($this->API->GetDeckByDeckID(explode(",", $params[0])));
        } else { echo 0; }
    }
    
    public function getdeckbyuserid($params = null) {
        if (count($params) > 0) {
            echo json_encode($this->API->GetDeckByUserID(explode(",", $params[0])));
        } else { echo 0; }
    }
    
    public function getdeckbyusername($params = null) {
        if (count($params) > 0) {
            $user = $this->API->GetUserByUsername(array($params[0]), false);
            if (isset($user["data"])) { $user = $user["data"][0]["id"]; }
            echo json_encode($this->API->GetDeckByUserID(array($user)));
        } else { echo 0; }
    }

}