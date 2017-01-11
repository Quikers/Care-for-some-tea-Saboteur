<?php

class APIModel extends Model {
    
    public $AccountModel;
    public $ContentModel;
    
    function __construct() {
        parent::__construct();
        
        $this->IncludeModel("Account");
        $this->IncludeModel("Content");
        
        $this->AccountModel = new AccountModel();
        $this->ContentModel = new ContentModel();
    }
    
    private function IncludeModel($model) {
        if ($model == "" || !file_exists("data/models/" . $model . "Model.php")) { return false; }
        
        require_once "data/models/" . $model . "Model.php";
        return true;
    }
    
    public function GetLastInsertedUser() {
        return $this->db->Query(
            "SELECT (`id`) FROM `users` ORDER BY `id` DESC LIMIT 1"
        );
    }
    
    public function GetUserByUserID($idArr, $includeFriends) {
        $result = array("data" => array());
        
        foreach ($idArr as $id) {
            $row = $this->db->Query(
                'SELECT `id`, `email`, `username`, `account_type`, `created`, `editted` FROM `users` WHERE `id` = :id',
                array(
                    "id" => $id
                ),
                true
            );
            
            if ($includeFriends) {
                $row["friends"] = array();
                $friendResult = $this->GetUserFriendsByUserID($row["id"]);
                foreach ($friendResult["data"] as $key => $friendRow) {
                    if ($friendRow["status"] == "1") { array_push($row["friends"], $friendRow["userid1"] != $id ? $friendRow["userid1"] : $friendRow["userid2"]); }
                }
            }

            if ($row != array()) { array_push($result["data"], $row); }
        }
        
        if ($result != array("data" => array()) && $result != array(array())) {
            if (count($result["data"]) == 1) { return $result["data"][0]; }
            else { return $result; }
        } else {
            return false;
        }
    }
    
    public function GetUserFriendsByUserID($userid) {
        $result = array("data" => array());
        
        $row = $this->db->Query(
            'SELECT * FROM `users_friends` WHERE `userid1` = :userid OR `userid2` = :userid',
            array(
                "userid" => $userid
            ),
            true
        );

        if ($row != array()) { $result["data"] = $row; }
        
        if ($result != array("data" => array()) && $result != array(array())) {
            if (count($result["data"]) == 1) { return $result["data"][0]; }
            else { return $result; }
        } else {
            return false;
        }
    }
    
    public function GetUserByUsername($usernameArr = array(), $includeFriends) {
        $result = array("data" => array());
        
        foreach ($usernameArr as $username) {
            $row = $this->db->Query(
                'SELECT `id`, `email`, `username`, `account_type`, `created`, `editted` FROM `users` WHERE `username` = :username',
                array(
                    "username" => $username
                ),
                true
            );
            
            if ($includeFriends) {
                $row["friends"] = array();
                $friendResult = $this->GetUserFriendsByUserID($row["id"]);
                foreach ($friendResult["data"] as $key => $friendRow) {
                    if ($friendRow["status"] == "1") { array_push($row["friends"], $friendRow["userid1"] != $id ? $friendRow["userid1"] : $friendRow["userid2"]); }
                }
            }

            if ($row != array()) { array_push($result["data"], $row); }
        }
        
        if ($result != array("data" => array()) && $result != array(array())) {
            if (count($result["data"]) == 1) { return $result["data"][0]; }
            else { return $result; }
        } else {
            return false;
        }
    }
    
    public function GetUserByEmail($emailArr, $includeFriends) {
        $result = array("data" => array());
        
        foreach ($emailArr as $email) {
            $row = $this->db->Query(
                'SELECT `id`, `email`, `username`, `account_type`, `created`, `editted` FROM `users` WHERE `email` = :email',
                array(
                    "email" => $email
                ),
                true
            );
            
            if ($includeFriends) {
                $row["friends"] = array();
                $friendResult = $this->GetUserFriendsByUserID($row["id"]);
                foreach ($friendResult["data"] as $key => $friendRow) {
                    if ($friendRow["status"] == "1") { array_push($row["friends"], $friendRow["userid1"] != $id ? $friendRow["userid1"] : $friendRow["userid2"]); }
                }
            }

            if ($row != array()) { array_push($result["data"], $row); }
        }
        
        if ($result != array("data" => array()) && $result != array(array())) {
            if (count($result["data"]) == 1) { return $result["data"][0]; }
            else { return $result; }
        } else {
            return false;
        }
    }
    
    public function CheckLogin($email, $password) {
        $result = $this->AccountModel->Login($email, $password);
        
        if ($result != false) {
            if (count($result) == 1) { return $result[0]; }
            else { return $result; }
        } else {
            return false;
        }
    }
    
    public function GetCardDeckRelByCardID($cardidArr) {
        return $this->ContentModel->GetCardDeckRelations("cardid", $cardidArr);
    }
    
    public function GetCardDeckRelByDeckID($deckidArr) {
        $result = array("data" => array());
        
        foreach ($deckidArr as $deckid) {
            $row = $this->db->Query(
                'SELECT * FROM `cards_decks_rel` WHERE `deckid` = :deckid',
                array(
                    "deckid" => $deckid
                ),
                true
            );  

            if ($row != array()) { array_push($result["data"], $row); }
        }
        
        if ($result != array("data" => array()) && $result != array(array())) {
            if (count($result["data"]) == 1) { return $result["data"][0]; }
            else { return $result; }
        } else {
            return false;
        }
    }
    
    public function GetCardByCardID($cardidArr) {
        $result = array("data" => array());
        
        foreach ($cardidArr as $cardid) {
            $row = $this->db->Query(
                'SELECT * FROM `cards` WHERE `id` = :cardid',
                array(
                    "cardid" => $cardid
                ),
                true
            );
            
            if (isset($row["id"])) {
                $row["effect"] = $this->db->Query(
                    'SELECT * FROM `effect_types` WHERE `id` = :effectid',
                    array(
                        "effectid" => $row["effect"]
                    ),
                    true
                );
            }

            if ($row != array()) { array_push($result["data"], $row); }
        }
        
        if ($result != array("data" => array()) && $result != array(array())) {
            if (count($result["data"]) == 1) { return $result["data"][0]; }
            else { return $result; }
        } else {
            return false;
        }
    }
    
    public function GetCardByCardName($cardnameArr) {
        $result = array("data" => array());
        
        foreach ($cardnameArr as $cardname) {
            $row = $this->db->Query(
                'SELECT * FROM `cards` WHERE `cardname` = :cardname',
                array(
                    "cardname" => $cardname
                ),
                true
            );
            
            if (isset($row["id"])) {
                $row["effect"] = $this->db->Query(
                    'SELECT * FROM `effect_types` WHERE `id` = :effectid',
                    array(
                        "effectid" => $row["effect"]
                    ),
                    true
                );
            }

            if ($row != array()) { array_push($result["data"], $row); }
        }
        
        if ($result != array("data" => array()) && $result != array(array())) {
            if (count($result["data"]) == 1) { return $result["data"][0]; }
            else { return $result; }
        } else {
            return false;
        }
    }
    
    public function GetDeckByUserID($userIDArr) {
        $result = array("data" => array());
        
        foreach ($userIDArr as $userID) {
            $row = $this->db->Query(
                'SELECT * FROM `decks` WHERE `userid` = :userid',
                array(
                    "userid" => $userID
                ),
                true
            );
            
            if (isset($row["id"])) {
                $card_deck_relArr = $this->GetCardDeckRelByDeckID(array($row["id"]));

                $row["cards"] = array();
                if ($card_deck_relArr != false) {
                    foreach ($card_deck_relArr as $card_deck_rel) {
                        array_push($row["cards"], $this->GetCardByCardID(array($card_deck_rel["cardid"])));
                    }
                }
            
                if ($row != array()) { array_push($result["data"], $row); }
            }
        }
        
        if ($result != array("data" => array()) && $result != array(array())) {
            if (count($result["data"]) == 1) { return $result["data"][0]; }
            else { return $result; }
        } else {
            return false;
        }
    }
    
    public function GetDeckByDeckID($deckIDArr) {
        $result = array("data" => array());
        
        foreach ($deckIDArr as $deckID) {
            $row = $this->db->Query(
                'SELECT * FROM `decks` WHERE `id` = :deckid',
                array(
                    "deckid" => $deckID
                ),
                true
            );
            
            if (isset($row["id"])) {
                $card_deck_relArr = $this->GetCardDeckRelByDeckID(array($row["id"]));

                $row["cards"] = array();
                if ($card_deck_relArr != false) {
                    foreach ($card_deck_relArr as $card_deck_rel) {
                        array_push($row["cards"], $this->GetCardByCardID(array($card_deck_rel["cardid"])));
                    }
                }
            
                if ($row != array()) { array_push($result["data"], $row); }
            }
        }
        
        if ($result != array("data" => array()) && $result != array(array())) {
            if (count($result["data"][0]) == 1) { return $result["data"][0][0]; }
            else { return $result; }
        } else {
            return false;
        }
    }
}