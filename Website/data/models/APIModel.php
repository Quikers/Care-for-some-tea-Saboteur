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
    
    public function GetUserByUserID($userid, $includeFriends) {
        $result = $this->db->Query(
            'SELECT `id`, `email`, `username`, `account_type`, `created`, `editted` FROM `users` WHERE `id` = :userid',
            array(
                "userid" => $userid
            ),
            true
        );

        if ($includeFriends) {
            $result["friends"] = array();
            $friendResult = $this->GetUserFriendsByUserID($userid);
            foreach ($friendResult as $friendRow) {
                if (isset($friendRow["status"]) && $friendRow["status"] == "1") { array_push($result["friends"], $friendRow["userid1"] != $result["id"] ? $friendRow["userid1"] : $friendRow["userid2"]); }
            }
        }
        
        if ($result != array() && $result != array(array())) {
            if (count($result) == 1) { return $result[0]; }
            else { return $result; }
        } else {
            return false;
        }
    }
    
    public function GetUserFriendsByUserID($userid) {
        $result = $this->db->Query(
            'SELECT * FROM `users_friends` WHERE `userid1` = :userid OR `userid2` = :userid',
            array(
                "userid" => $userid
            ),
            true
        );
        
        if ($result != array() && $result != array(array())) {
            if (count($result) == 1) { return $result[0]; }
            else { return $result; }
        } else {
            return false;
        }
    }
    
    public function GetUserByUsername($username, $includeFriends) {
        $result = $this->db->Query(
            'SELECT `id`, `email`, `username`, `account_type`, `created`, `editted` FROM `users` WHERE `username` = :username',
            array(
                "username" => $username
            ),
            true
        );

        if ($includeFriends) {
            $result["friends"] = array();
            $friendResult = $this->GetUserFriendsByUserID($result["id"]);
            foreach ($friendResult as $friendRow) {
                if (isset($friendRow["status"]) && $friendRow["status"] == "1") { array_push($result["friends"], $friendRow["userid1"] != $result["id"] ? $friendRow["userid1"] : $friendRow["userid2"]); }
            }
        }
        
        if ($result != array() && $result != array(array())) {
            if (count($result) == 1) { return $result[0]; }
            else { return $result; }
        } else {
            return false;
        }
    }
    
    public function GetUserByEmail($email, $includeFriends) {
        $result = $this->db->Query(
            'SELECT `id`, `email`, `username`, `account_type`, `created`, `editted` FROM `users` WHERE `email` = :email',
            array(
                "email" => $email
            ),
            true
        );

        if ($includeFriends) {
            $result["friends"] = array();
            $friendResult = $this->GetUserFriendsByUserID($result["id"]);
            foreach ($friendResult as $friendRow) {
                if (isset($friendRow["status"]) && $friendRow["status"] == "1") { array_push($result["friends"], $friendRow["userid1"] != $result["id"] ? $friendRow["userid1"] : $friendRow["userid2"]); }
            }
        }
        
        if ($result != array() && $result != array(array())) {
            if (count($result) == 1) { return $result[0]; }
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
    
    public function GetCardDeckRelByCardID($cardid) {
        return $this->ContentModel->GetCardDeckRelations("cardid", $cardid);
    }
    
    public function GetCardDeckRelByDeckID($deckid) {
        return $this->ContentModel->GetCardDeckRelations("deckid", $deckid);
    }
    
    public function GetAllCards() {
        $result = $this->db->Query(
            'SELECT * FROM `cards`',
            NULL
        );

        if (!isset($result["id"])) {
            if (count($result) > 0) {
                foreach ($result as $key => $card) {
                    if ($card["deleted"] == "1") { unset($result[$key]); }
                    else {
                        $result[$key]["effect"] = $this->GetCardEffectByEffectID($card["effect"]);
                    }
                }
            }
        } else {
            $result["effect"] = $this->GetCardEffectByEffectID($result["effect"]);
        }
        
        if ($result != array() && $result != array(array())) {
            return array("data" => $result);
        } else {
            return false;
        }
    }
    
    public function GetCardByCardID($cardid) {
        $result = $this->db->Query(
            'SELECT * FROM `cards` WHERE `id` = :cardid',
            array(
                "cardid" => $cardid
            ),
            true
        );

        if (isset($result["id"])) { $result["effect"] = $this->GetCardEffectByEffectID($result["effect"]); }
        
        if ($result != array() && $result != array(array())) {
            if (count($result) == 1) { return $result[0]; }
            else { return $result; }
        } else {
            return false;
        }
    }
    
    public function GetCardsByUserID($userid) {
        $result = $this->db->Query(
            'SELECT * FROM `cards` WHERE `userid` = :userid',
            array(
                "userid" => $userid
            ),
            true
        );
        
        
        if (!isset($result["id"])) {
            if (count($result) > 0) {
                foreach ($result as $key => $card) {
                    if ($card["deleted"] == "1") { unset($result[$key]); }
                    else {
                        $result[$key]["effect"] = $this->GetCardEffectByEffectID($card["effect"]);
                    }
                }
            }
        } else {
            $result["effect"] = $this->GetCardEffectByEffectID($result["effect"]);
        }
        
        if ($result != array() && $result != array(array())) {
            if (count($result) == 1) { return $result[0]; }
            else { return $result; }
        } else {
            return false;
        }
    }
    
    public function GetCardEffectByEffectID($effectid) {
        $result = $this->db->Query(
            'SELECT * FROM `effect_types` WHERE `id` = :effectid',
            array(
                "effectid" => $effectid
            ),
            true
        );
        
        if ($result != array() && $result != array(array())) {
            if (count($result) == 1) { return $result[0]; }
            else { return $result; }
        } else {
            return false;
        }
    }
    
    public function GetAllEffects() {
        $result = $this->db->Query(
            'SELECT * FROM `effect_types`',
            NULL,
            false
        );
        
        if ($result != array() && $result != array(array())) {
            return $result;
        } else {
            return false;
        }
    }
    
    public function GetDeckByUserID($userid) {
        $result = $this->db->Query(
            'SELECT * FROM `decks` WHERE `userid` = :userid',
            array( "userid" => $userid ),
            true
        );
        
        if (count($result) > 0) {
            if (is_numeric(array_keys($result)[0])) {
                foreach($result as $key => $deck) {
                    if ($deck["deleted"] == "1") { unset($result[$key]); }
                    else {
                        $deck["cards"] = array();
                        $result[$key] = $deck;
                    }
                }
            } else {
                $result["cards"] = array();
                if ($deck["deleted"] == "1") { $result = array(); }
            }
        }
        
        if ($result != array() && $result != array(array())) {
            if (count($result) == 1) { return $result[0]; }
            else { return $result; }
        } else {
            return false;
        }
    }
    
    public function GetDeckByDeckID($deckid) {
        $result = $this->db->Query(
            'SELECT * FROM `decks` WHERE `id` = :deckid',
            array( "deckid" => $deckid ),
            true
        );

        if (isset($result["id"])) {
            $card_deck_relArr = $this->GetCardDeckRelByDeckID($result["id"]);

            $result["cards"] = array();
            if ($card_deck_relArr != false) {
                foreach ($card_deck_relArr as $card_deck_rel) {
                    array_push($result["cards"], $this->GetCardByCardID($card_deck_rel["cardid"]));
                }
            }
        }
        
        if ($result != array() && $result != array(array())) {
            if (count($result) == 1) { return $result[0]; }
            else { return $result; }
        } else {
            return false;
        }
    }
}