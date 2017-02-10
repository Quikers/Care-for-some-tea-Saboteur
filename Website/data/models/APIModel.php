<?php

class APIModel extends Model {
    
    public $AccountModel;
    public $CollectionModel;
    
    function __construct() {
        parent::__construct();
        
        $this->IncludeModel("Account");
        $this->IncludeModel("Collection");
        
        $this->AccountModel = new AccountModel();
        $this->CollectionModel = new CollectionModel();
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
            if (count($result) == 1) { return $result[array_keys($result)[0]]; }
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
            if (count($result) == 1) { return $result[array_keys($result)[0]]; }
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
            if (count($result) == 1) { return $result[array_keys($result)[0]]; }
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
            if (count($result) == 1) { return $result[array_keys($result)[0]]; }
            else { return $result; }
        } else {
            return false;
        }
    }
    
    public function CheckLogin($email, $password) {
        $result = $this->AccountModel->Login($email, $password);
        
        if ($result != false) {
            if (count($result) == 1) { return $result[array_keys($result)[0]]; }
            else { return $result; }
        } else {
            return false;
        }
    }

    public function CheckUsernameLogin($username, $password) {
        $result = $this->AccountModel->UsernameLogin($username, $password);

        if ($result != false) {
            if (count($result) == 1) { return $result[array_keys($result)[0]]; }
            else { return $result; }
        } else {
            return false;
        }
    }
    
    public function UpdateStatus($type, $status, $id) {
        if ($type == "deck") {
            $result = $this->db->Query(
                'UPDATE `decks` SET `activated`=:activated WHERE `id` = :deckid',
                array(
                    "deckid" => $id,
                    "activated" => $status
                )
            );
        } else if ($type == "card") {
            $result = $this->db->Query(
                'UPDATE `cards` SET `activated`=:activated WHERE `id` = :cardid',
                array(
                    "cardid" => $id,
                    "activated" => $status
                )
            );
        }
    }
    
    public function GetCardDeckRelByCardID($cardid) {
        return $this->CollectionModel->GetCardDeckRelations("cardid", $cardid);
    }
    
    public function GetCardDeckRelByDeckID($deckid) {
        return $this->CollectionModel->GetCardDeckRelations("deckid", $deckid);
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
            if (count($result) == 1) { return $result[array_keys($result)[0]]; }
            else { return $result; }
        } else {
            return false;
        }
    }
    
    public function GetCardsByUserID($userid) {
        $result = $this->db->Query(
            'SELECT * FROM `cards` WHERE `userid` = :userid AND `deleted` = 0',
            array(
                "userid" => $userid
            ),
            true
        );
        
        
        if (!isset($result["id"])) {
            if (count($result) > 0) {
                foreach ($result as $key => $card) {
                    $result[$key]["effect"] = $this->GetCardEffectByEffectID($card["effect"]);
                }
            }
        } else if ($result != false) {
            $result["effect"] = $this->GetCardEffectByEffectID($result["effect"]);
        }
        
        if ($result != array() && $result != array(array())) {
            if (count($result) == 1) { return $result[array_keys($result)[0]]; }
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
            if (count($result) == 1) { return $result[array_keys($result)[0]]; }
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
    
    public function CreateCard($card) {
        echo 'INSERT INTO `cards`(`userid`, `name`, `cost`, `attack`, `health`, `effect`, `activated`, `deleted`) VALUES (' . $_SESSION["user"]["id"] . ', ' . $card["name"] . ', ' . $card["cost"] . ', ' . $card["attack"] . ', ' . $card["health"] . ', ' . $card["effect"] . ', 0, 0)';
        
        $this->db->Query(
            'INSERT INTO `cards`(`userid`, `name`, `cost`, `attack`, `health`, `effect`, `activated`, `deleted`) VALUES (:userid, :name, :cost, :attack, :health, :effect, :activated, :deleted)',
            array(
                "userid" => $_SESSION["user"]["id"],
                "name" => $card["name"],
                "cost" => $card["cost"],
                "attack" => $card["attack"],
                "health" => $card["health"],
                "effect" => $card["effect"],
                "activated" => 0,
                "deleted" => 0
            )
        );
        
        $result = $this->db->Query(
            'SELECT * FROM `cards` ORDER BY `id` DESC LIMIT 1',
            NULL
        );
        
        if ($result != array() && $result != array(array())) {
            if (count($result) == 1) { return $result[array_keys($result)[0]]; }
            else { return $result; }
        } else {
            return false;
        }
    }
    
    public function UpdateCard($card) {
        $this->db->Query(
            'UPDATE `cards` SET `name`=:name, `cost`=:cost, `attack`=:attack, `health`=:health, `effect`=:effect, `activated`=:activated WHERE `id` = :cardid',
            array( 
                "name" => $card["name"],
                "cost" => $card["cost"],
                "attack" => $card["attack"],
                "health" => $card["health"],
                "effect" => $card["effect"],
                "activated" => $_SESSION["user"]["account_type"] == 1 ? 1 : 0,
                "cardid" => $card["id"]
            )
        );
    }
    
    public function CreateDeck($deck) {
        $this->db->Query(
            'INSERT INTO `decks`(`userid`, `name`, `activated`, `deleted`) VALUES (:userid, :name, :activated, :deleted)',
            array( 
                "userid" => $_SESSION["user"]["id"],
                "name" => $deck["name"],
                "activated" => $_SESSION["user"]["account_type"] == 1 ? 1 : 0,
                "deleted" => 0
            )
        );
        
        $result = $this->db->Query(
            'SELECT * FROM `decks` ORDER BY `id` DESC LIMIT 1',
            NULL
        );
        
        $addedCardsArr = count($deck["addedcards"]) > 0 ? explode(",", $deck["addedcards"]) : array();
        if (count($addedCardsArr) > 0) {
            foreach ($addedCardsArr as $cardid) {
                $this->db->Query(
                    'INSERT INTO `cards_decks_rel` (`deckid`, `cardid`) VALUES (:deckid, :cardid)',
                    array( 
                        "deckid" => $result["id"],
                        "cardid" => $cardid
                    )
                );
            }
        }
        
        if ($result != array() && $result != array(array())) {
            if (count($result) == 1) { return $result[array_keys($result)[0]]; }
            else { return $result; }
        } else {
            return false;
        }
    }
    
    public function UpdateDeck($deck) {
        $this->db->Query(
            'UPDATE `decks` SET `name`=:name, `activated`=:activated WHERE `id` = :deckid',
            array( 
                "name" => $deck["name"],
                "activated" => 0,
                "deckid" => $deck["id"]
            )
        );
        
        $deletedCardsArr = count($deck["deletedcards"]) > 0 ? explode(",", $deck["deletedcards"]) : array();
        if (count($deletedCardsArr) > 0) {
            foreach ($deletedCardsArr as $cardid) {
                $this->db->Query(
                    'DELETE FROM `cards_decks_rel` WHERE `deckid`=:deckid AND `cardid`=:cardid',
                    array( 
                        "deckid" => $deck["id"],
                        "cardid" => $cardid
                    )
                );
            }
        }
        
        $addedCardsArr = count($deck["addedcards"]) > 0 ? explode(",", $deck["addedcards"]) : array();
        if (count($addedCardsArr) > 0) {
            foreach ($addedCardsArr as $cardid) {
                $this->db->Query(
                    'INSERT INTO `cards_decks_rel` (`deckid`, `cardid`) VALUES (:deckid, :cardid)',
                    array( 
                        "deckid" => $deck["id"],
                        "cardid" => $cardid
                    )
                );
            }
        }
    }
    
    public function GetAllDecks() {
        $result = $this->db->Query(
            'SELECT * FROM `decks`',
            NULL
        );
        
        foreach($result as $key => $deck) {
            if (isset($deck["id"])) {
                $card_deck_relArr = $this->GetCardDeckRelByDeckID($deck["id"]);

                $deck["cards"] = array();
                if ($card_deck_relArr != false) {
                    foreach ($card_deck_relArr as $card_deck_rel) {
                        array_push($deck["cards"], $this->GetCardByCardID($card_deck_rel["cardid"]));
                    }
                }
                
                $result[$key] = $deck;
            }
        }
        
        if ($result != array() && $result != array(array())) {
            if (count($result) == 1) { return $result[array_keys($result)[0]]; }
            else { return $result; }
        } else {
            return false;
        }
    }
    
    public function GetDecksByUserID($userid) {
        $result = $this->db->Query(
            'SELECT * FROM `decks` WHERE `userid` = :userid AND `deleted` = 0',
            array( "userid" => $userid ),
            true
        );
        
        if (count($result) > 0) {
            if (is_numeric(array_keys($result)[0])) {
                foreach($result as $key => $deck) {
                    $deck["cards"] = array();
                    $result[$key] = $deck;
                }
            } else if ($result != false) {
                $result["cards"] = array();
            }
        }
        
        if ($result != array() && $result != array(array())) {
            if (count($result) == 1) { return $result[array_keys($result)[0]]; }
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
            if (count($result) == 1) { return $result[array_keys($result)[0]]; }
            else { return $result; }
        } else {
            return false;
        }
    }
}
