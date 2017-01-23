<?php

// API documentation starts

class User {

    function __construct() {
        
    }

    public $getuserbyuserid = "<p>Gets a <strong>user</strong> object from the database by searching for the <strong>user's</strong> <strong>userID</strong>.<br>You can request multiple <strong>users</strong> simultaneously by separating the <strong>userIDs</strong> with a comma. (1,2,3 etc.)<br><br>Usage:</p><pre>" . URL . "api/getuserbyuserid/<strong>:userid</strong>(,<strong>:userid</strong>)</pre><br><p>Examples:</p><pre>" . URL . "api/getuserbyuserid/1<br>" . URL . "api/getuserbyuserid/1,2</pre>";
    public $getuserbyemail = "<p>Gets a <strong>user</strong> object from the database by searching for the <strong>user's</strong> <strong>e-mail</strong>.<br>You can request multiple <strong>users</strong> simultaneously by separating the <strong>e-mails</strong> with a comma.<br><br>Usage:</p><pre>" . URL . "api/getuserbyemail/<strong>:e-mail</strong>(,<strong>:e-mail</strong>)</pre><br><p>Examples:</p><pre>" . URL . "api/getuserbyemail/admin@careforsometeasaboteur.com<br>" . URL . "api/getuserbyemail/admin@careforsometeasaboteur.com,test@test.test</pre>";
    public $getuserbyusername = "<p>Gets a <strong>user</strong> object from the database by searching for the <strong>user's</strong> <strong>username</strong>.<br>You can request multiple <strong>users</strong> simultaneously by separating the <strong>usernames</strong> with a comma.<br><br>Usage:</p><pre>" . URL . "api/getuserbyusername/<strong>:username</strong>(,<strong>:username</strong>)</pre><br><p>Examples:</p><pre>" . URL . "api/getuserbyusername/admin<br>" . URL . "api/getuserbyusername/admin,test</pre>";
    public $checklogin = "<p>Attempts to log in a <strong>user</strong> and if successful will send back the <strong>user's</strong> object.<br><br>Usage:</p><pre>" . URL . "api/checklogin/<strong>:e-mail</strong>/<strong>:password</strong></pre><br><p>Examples:</p><pre>" . URL . "api/checklogin/testemail@test.com/testpassword</pre>";

}

class Card {

    function __construct() {
        
    }

    public $getcardbycardid = "<p>Gets a <strong>card</strong> object from the database by searching for the <strong>card's</strong> <strong>cardID</strong>.<br>You can request multiple <strong>cards</strong> simultaneously by separating the <strong>cardIDs</strong> with a comma.<br><br>Usage:</p><pre>" . URL . "api/getcardbycardid/<strong>:cardid</strong>(,<strong>:cardid</strong>)</pre><br><p>Examples:</p><pre>" . URL . "api/getcardbycardid/1<br>" . URL . "api/getcardbycardid/1,2</pre>";
    public $geteffectbyeffectid = "<p>Gets an <strong>effect</strong> object from the database by searching for the <strong>effect's</strong> <strong>effectID</strong>.<br>You can request multiple <strong>effecs</strong> simultaneously by separating the <strong>effectIDs</strong> with a comma.<br><br>Usage:</p><pre>" . URL . "api/geteffectbyeffectid/<strong>:effectid</strong>(,<strong>:effectid</strong>)</pre><br><p>Examples:</p><pre>" . URL . "api/geteffectbyeffectid/1<br>" . URL . "api/geteffectbyeffectid/1,2</pre>";

}

class Deck {

    function __construct() {
        
    }

    public $getdeckbydeckid = "<p>Gets a <strong>deck</strong> object from the database by searching for the <strong>deck's</strong> <strong>deckID</strong>.<br>You can request multiple <strong>decks</strong> simultaneously by separating the <strong>deckIDs</strong> with a comma.<br><br>Usage:</p><pre>" . URL . "api/getdeckbydeckid/<strong>:deckid</strong>(,<strong>:deckid</strong>)</pre><br><p>Examples:</p><pre>" . URL . "api/getdeckbydeckid/1<br>" . URL . "api/getdeckbydeckid/1,2</pre>";
    public $getdeckbyuserid = "<p>Gets a <strong>deck</strong> object from the database by searching for the <strong>deck's</strong> <strong>userID</strong>.<br>You can request multiple user's <strong>decks</strong> simultaneously by separating the <strong>userIDs</strong> with a comma.<br><br>Usage:</p><pre>" . URL . "api/getdeckbyuserid/<strong>:userid</strong>(,<strong>:userid</strong>)</pre><br><p>Examples:</p><pre>" . URL . "api/getdeckbyuserid/1<br>" . URL . "api/getdeckbyuserid/1,2</pre>";
    public $getdeckbyusername = "<p>Gets a <strong>deck</strong> object from the database by searching for the <strong>deck's</strong> <strong>username</strong>.<br>You can request multiple user's <strong>decks</strong> simultaneously by separating the <strong>usernames</strong> with a comma.<br><br>Usage:</p><pre>" . URL . "api/getdeckbyusername/<strong>:username</strong>(,<strong>:username</strong>)</pre><br><p>Examples:</p><pre>" . URL . "api/getdeckbyusername/admin<br>" . URL . "api/getdeckbyusername/admin,test</pre>";

}

class Match {

    function __construct() {
        
    }

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
            if (isset($params[1]) && strtolower($params[1]) == "true") {
                $includeFriends = true;
            }

            $result = NULL;
            if (strpos($params[0], ",") != false) {
                $result["data"] = array();
                foreach (explode(",", $params[0]) as $userid) {
                    array_push($result["data"], $this->API->GetUserByUserID($userid, $includeFriends));
                }
            } else if (is_numeric($params[0])) {
                $result = $this->API->GetUserByUserID($params[0], $includeFriends);
            } else {
                echo "false";
                return;
            }

            echo json_encode($result);
        } else {
            echo "false";
            return;
        }
    }

    public function getuserbyemail($params = null) {
        if (count($params) > 0) {
            $includeFriends = false;
            if (isset($params[1]) && strtolower($params[1]) == "true") {
                $includeFriends = true;
            }

            $result = NULL;
            if (strpos($params[0], ",") != false) {
                $result["data"] = array();
                foreach (explode(",", $params[0]) as $userid) {
                    array_push($result["data"], $this->API->GetUserByEmail($userid, $includeFriends));
                }
            } else if (strpos($params[0], "@") != false) {
                $result = $this->API->GetUserByEmail($params[0], $includeFriends);
            } else {
                echo "false";
                return;
            }

            echo json_encode($result);
        } else {
            echo "false";
            return;
        }
    }

    public function getuserbyusername($params = null) {
        if (count($params) > 0) {
            $includeFriends = false;
            if (isset($params[1]) && strtolower($params[1]) == "true") {
                $includeFriends = true;
            }

            $result = NULL;
            if (strpos($params[0], ",") != false) {
                $result["data"] = array();
                foreach (explode(",", $params[0]) as $userid) {
                    array_push($result["data"], $this->API->GetUserByUsername($userid, $includeFriends));
                }
            } else if (strpos($params[0], "@") != false) {
                $result = $this->API->GetUserByUsername($params[0], $includeFriends);
            } else {
                echo "false";
                return;
            }

            echo json_encode($result);
        } else {
            echo "false";
            return;
        }
    }

    public function getuserfriends($params = null) {
        if (count($params) > 0) {
            $result = NULL;
            if (strpos($params[0], ",") != false) {
                foreach (explode($params[0]) as $userid) {
                    if (is_numeric($userid)) {
                        array_push($result["data"], $this->API->GetUserFriendsByUserID($userid));
                    } else {
                        echo 0;
                        return;
                    }
                }
            } else if (is_numeric($params[0])) {
                $result = $this->API->GetUserFriendsByUserID($params[0]);
            } else {
                echo "false";
                return;
            }

            echo json_encode($result);
        } else {
            echo "false";
            return;
        }
    }

    public function checklogin($params = null) {
        if (count($params) == 2) {
            echo json_encode($this->API->CheckLogin($params[0], $params[1]));
        } else {
            echo 0;
        }
    }

    public function getcardbycardid($params = null) {
        if (count($params) > 0) {
            $result = NULL;
            if (strpos($params[0], ",") != false) {
                $result["data"] = array();
                foreach (explode(",", $params[0]) as $cardid) {
                    array_push($result["data"], $this->API->GetCardByCardID($cardid));
                }
            } else if (is_numeric($params[0])) {
                $result = $this->API->GetCardByCardID($params[0]);
            } else {
                echo "false";
                return;
            }

            echo json_encode($result);
        } else {
            echo "false";
            return;
        }
    }

    public function getcardsbyuserid($params = null) {
        if (count($params) > 0) {
            $result = NULL;
            $result["data"] = array();
            if (strpos($params[0], ",") != false) {
                foreach (explode(",", $params[0]) as $userid) {
                    array_push($result["data"], $this->API->GetCardsByUserID($userid));
                }
            } else if (is_numeric($params[0])) {
                $result["data"] = $this->API->GetCardsByUserID($params[0]);
            } else {
                echo "false";
                return;
            }

            echo json_encode($result);
        } else {
            echo "false";
            return;
        }
    }

    public function getcardsbydeckid($params = null) {
        if (count($params) > 0) {
            $result["data"] = array();
            if (strpos($params[0], ",") != false) {
                foreach (explode(",", $params[0]) as $cardid) {
                    $relArr = $this->API->GetCardDeckRelByDeckID($cardid);
                    if ($relArr != false && count($relArr) > 0) {
                        foreach($relArr as $relObj) {
                            array_push($result["data"], $this->API->GetCardByCardID($relObj["cardid"]));
                        }
                    } else {
                        echo "false";
                        return;
                    }
                }
            } else if (is_numeric($params[0])) {
                $relArr = $this->API->GetCardDeckRelByDeckID($params[0]);
                if ($relArr != false && count($relArr) > 0) {
                    foreach($relArr as $relObj) {
                        array_push($result["data"], $this->API->GetCardByCardID($relObj["cardid"]));
                    }
                } else {
                    echo "false";
                    return;
                }
            } else {
                echo "false";
                return;
            }

            echo json_encode($result);
        } else {
            echo "false";
            return;
        }
    }

    public function getdeckbydeckid($params = null) {
        if (count($params) > 0) {
            $result = NULL;
            if (is_numeric($params[0])) {
                $result = $this->API->GetDeckByDeckID($params[0]);
            } else {
                echo "false";
                return;
            }

            echo json_encode($result);
        } else {
            echo "false";
            return;
        }
    }

    public function getdecksbyuserid($params = null) {
        if (count($params) > 0) {
            $result = NULL;
            if (is_numeric($params[0])) {
                $temp = $this->API->GetDeckByUserID($params[0]);
                if ($temp != false) {
                    $result = array("data" => is_numeric(array_keys($temp)[0]) ? $temp : array($temp));
                } else { $result = $temp; }
            } else {
                echo "false";
                return;
            }

            echo json_encode($result);
        } else {
            echo "false";
            return;
        }
    }

    public function getdecksbyusername($params = null) {
        if (count($params) > 0) {
            $userid = $this->API->GetUserByUsername(array($params[0]), false)["id"];

            $result = NULL;
            if (is_numeric($userid)) {
                $temp = $this->API->GetDeckByUserID($userid);
                $result = array("data" => is_numeric(array_keys($temp)[0]) ? $temp : array($temp));
            } else {
                echo "false";
                return;
            }

            echo json_encode($result);
        } else {
            echo "false";
            return;
        }
    }

}
