<?php

class AccountModel extends Model {
    
    function __construct() {
        parent::__construct();
    }
    
    public function Login($email, $password) {
        $result = $this->db->Query(
            'SELECT `id`, `email`, `username`, `account_type`, `created`, `editted` FROM users WHERE `email` = :email AND `password` = PASSWORD(:password)',
            array(
                "email" => $email,
                "password" => $password
            )
        );
        
        if ($result != array()) {
            return $result;
        } else {
            return false;
        }
    }
    
    public function GetLastInsertedUser() {
        return $this->db->Query("SELECT (`id`) FROM `users` ORDER BY `id` DESC LIMIT 1");
    }
    
    public function Register($email, $username, $password) {
        try {
            $lastID = $this->GetLastInsertedUser()["id"];
            $this->db->Query(
                'INSERT INTO `users`(`email`, `username`, `password`, `account_type`, `activated`) VALUES (:email, :username, PASSWORD(:password), 3, 1)',
                array(
                    "email" => $email,
                    "username" => $username,
                    "password" => $password
                ), true, false, true
            );
            $newID = $this->GetLastInsertedUser()["id"];
        } catch(Exception $ex) { return -1; }
        
        return $lastID != $newID ? $newID : 0;
    }

}