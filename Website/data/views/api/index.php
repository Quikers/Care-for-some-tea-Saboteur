<link rel="stylesheet" href="<?= URL ?>public/css/api.css">

<div id="cluster">
    <aside>
        <div id="api-nav">
            <a class="header" href="<?= URL ?>api/docs">Documentation Home</a>
            
            <?php
            
            foreach($this->methodGroups as $groupName) {
                echo "<a id=\"" . $groupName . "\" class=\"header1-link\" href=\"" . URL . "api/docs#" . $groupName . "\"" . $groupName . "</a>";
                foreach(get_class_vars($groupName) as $varName => $description) {
                    echo "";
                }
            }
            
            ?>
            
        </div>
    </aside>

    <div id="api-content">
        <h1>API Documentation</h1>
        <p>Please use one of the following functions:<br><br>
        getuserbyid (http://careforsometeasaboteur.com/api/getuserbyid/1)<br>
        getuserbyemail (http://careforsometeasaboteur.com/api/getuserbyemail/admin@careforsometeasaboteur.com)<br>
        getuserbyusername (http://careforsometeasaboteur.com/api/getuserbyusername/admin)</p>
    </div>
</div>