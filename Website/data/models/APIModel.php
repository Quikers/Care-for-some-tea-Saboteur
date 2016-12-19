<?php

class APIModel extends Model {
    
    function __construct() {
        parent::__construct();
    }
    
    public function GetLastInsertedUser() {
        return $this->db->Query(
            "SELECT (`id`) FROM `users` ORDER BY `id` DESC LIMIT 1"
        );
    }
    
    public function GetUserByUserID($idArr) {
        $result = array();
        
        foreach ($idArr as $id) {
            $row = $this->db->Query(
                'SELECT `id`, `email`, `username`, `account_type`, `created`, `editted` FROM `users` WHERE `id` = :id',
                array(
                    "id" => $id
                )
            );
            
            if ($row != array()) { array_push($result, $row); }
        }
        
        if ($result != array() && $result != array(array())) {
            if (count($result) == 1) { return $result[0]; }
            else { return $result; }
        } else {
            return false;
        }
    }
    
    public function GetUserByUsername($usernameArr) {
        $result = array();
        
        foreach ($usernameArr as $username) {
            $row = $this->db->Query(
                'SELECT `id`, `email`, `username`, `account_type`, `created`, `editted` FROM `users` WHERE `username` = :username',
                array(
                    "username" => $username
                )
            );
            
            if ($row != array()) { array_push($result, $row); }
        }
        
        if ($result != array() && $result != array(array())) {
            if (count($result) == 1) { return $result[0]; }
            else { return $result; }
        } else {
            return false;
        }
    }
    
    public function GetUserByEmail($emailArr) {
        $result = array();
        
        foreach ($emailArr as $email) {
            $row = $this->db->Query(
                'SELECT `id`, `email`, `username`, `account_type`, `created`, `editted` FROM `users` WHERE `email` = :email',
                array(
                    "email" => $email
                )
            );
            
            if ($row != array()) { array_push($result, $row); }
        }
        
        if ($result != array() && $result != array(array())) {
            if (count($result) == 1) { return $result[0]; }
            else { return $result; }
        } else {
            return false;
        }
    }
    
    public function GetCardDeckRelByCardID($cardidArr) {
        $result = array();
        
        foreach ($cardidArr as $cardid) {
            $row = $this->db->Query(
                'SELECT * FROM `cards_decks_rel` WHERE `cardid` = :cardid',
                array(
                    "cardid" => $cardid
                ),
                false
            );
            
            if ($row != array()) { array_push($result, $row); }
        }
        
        if ($result != array() && $result != array(array())) {
            if (count($result) == 1) { return $result[0]; }
            else { return $result; }
        } else {
            return false;
        }
    }
    
    public function GetCardDeckRelByDeckID($deckidArr) {
        $result = array();
        
        foreach ($deckidArr as $deckid) {
            $row = $this->db->Query(
                'SELECT * FROM `cards_decks_rel` WHERE `deckid` = :deckid',
                array(
                    "deckid" => $deckid
                ),
                false
            );  
            
            if ($row != array()) { array_push($result, $row); }
        }
            print_r($result);
        
        if ($result != array() && $result != array(array())) {
            if (count($result) == 1) { return $result[0]; }
            else { return $result; }
        } else {
            return false;
        }
    }
    
    public function GetCardByCardID($cardidArr) {
        $result = array();
        
        foreach ($cardidArr as $cardid) {
            $row = $this->db->Query(
                'SELECT * FROM `card` WHERE `id` = :cardid',
                array(
                    "cardid" => $cardid
                ),
                false
            );
            
            if ($row != array()) { array_push($result, $row); }
        }
        
        if ($result != array() && $result != array(array())) {
            if (count($result) == 1) { return $result[0]; }
            else { return $result; }
        } else {
            return false;
        }
    }
    
    public function GetDeckByUserID($userIDArr) {
        $result = array();
        
        foreach ($userIDArr as $userID) {
            $row = $this->db->Query(
                'SELECT * FROM `decks` WHERE `userid` = :userid',
                array(
                    "userid" => $userID
                ),
                false
            );
            
            foreach ($row as $key => $deck) {
                $card_deck_relArr = $this->GetCardDeckRelByDeckID(array($deck["id"]));
            
                $cardArr = array();
                foreach ($card_deck_relArr as $card_deck_rel) {
                    array_push($cardArr, $this->GetCardByCardID(array($card_deck_rel["cardid"])));
                }
                
                $row[$key]["cards"] = $cardArr;
            }
            
            if ($row != array()) { array_push($result, $row); }
        }
        
        if ($result != array() && $result != array(array())) {
            if (count($result) == 1) { return $result[0]; }
            else { return $result; }
        } else {
            return false;
        }
    }
    
    public function GetDeckByUsername($usernameArr) {
        $result = array();
        
        $userIDArr = array();
        foreach ($usernameArr as $username) {
            array_push($userIDArr, $this->GetUserByUsername($username)["id"]);
        }
        
        foreach ($userIDArr as $userID) {
            $row = $this->db->Query(
                'SELECT * FROM `decks` WHERE `userid` = :userid',
                array(
                    "userid" => $userID
                ),
                false
            );
            
            foreach ($row as $key => $deck) {
                $card_deck_relArr = $this->db->Query(
                    'SELECT * FROM `cards_decks_rel` WHERE `deckid` = :deckid',
                    array(
                        "deckid" => $deck["id"]
                    ),
                    false
                );
            
                $cardArr = array();
                foreach ($card_deck_relArr as $card_deck_rel) {
                    array_push($cardArr,
                        $this->db->Query(
                            'SELECT * FROM `cards` WHERE `id` = :cardid',
                            array(
                                "cardid" => $card_deck_rel["cardid"]
                            )
                        )
                    );
                }
                
                $row[$key]["cards"] = $cardArr;
            }
            
            if ($row != array()) { array_push($result, $row); }
        }
        
        if ($result != array() && $result != array(array())) {
            if (count($result) == 1) { return $result[0]; }
            else { return $result; }
        } else {
            return false;
        }
    }
}