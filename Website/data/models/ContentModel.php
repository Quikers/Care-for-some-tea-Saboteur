<?php

class ContentModel extends Model {
    
    function __construct() {
        parent::__construct();
    }
    
    public function GetAllCards() {
        $result = $this->db->Query(
            'SELECT * FROM cards',
            NULL
        );
        
        if ($result != array() && $result != null) {
            return $result;
        } else {
            return false;
        }
    }
    
    public function GetCardDeckRelations($where, $value) {
        $result = $this->db->Query('SELECT * FROM cards_decks_rel WHERE `' . $where . '` = :value',
            array(
                "value" => $value
            ),
            true
        );
        
        if ($result != array() && $result != null) {
            return $result;
        } else {
            return false;
        }
    }
    
    public function GetAllDecks($includeCards = false) {
        $result = $this->db->Query(
            'SELECT * FROM decks',
            NULL
        );
        
        if ($includeCards && count($result) > 0) {
            foreach($result as $key => $deck) {
                $relArr = $this->GetCardDeckRelations(array("deckid", $deck["id"]));
                
                $cardArr = array();
                foreach ($relArr as $relation) {
                    array_push($cardArr, $this->GetCardByCardId($relation["cardid"]));
                }
                
                array_push($deck["cards"], $cardArr);
                $result[$key] = $deck;
            }
        }
        
        if ($result != array() && $result != null) {
            return $result;
        } else {
            return false;
        }
    }
    
    public function Delete($contentType, $value, $hardDelete = false) {
        $result = NULL;
        
        if (!$hardDelete) {
            $result = $this->db->Query(
                'UPDATE `' . $contentType . '` SET `deleted` = 1 WHERE `id` = :id',
                array(
                    "id" => $value
                )
            );
        } else {
            $result = $this->db->Query(
                'DELETE * FROM `' . $contentType . '` WHERE id = :id',
                array(
                    "id" => $value
                )
            );
        }
        
        
        if ($result != array() && $result != null) {
            return $result;
        } else {
            return false;
        }
    }

}