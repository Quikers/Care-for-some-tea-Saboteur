<link rel="stylesheet" href="<?= URL ?>public/css/api.css">

<div id="cluster">
    <aside>
        <div id="api-nav">
            <a class="header" href="<?= URL ?>api/docs">Documentation Home</a>
            
            <?php
            
            $content = array();
            
            foreach($this->methodGroups as $groupName) {
                $content[$groupName] = array();
                
                echo '<a class="header1-link" href="#' . $groupName . '">' . $groupName . '</a>';
                foreach(get_class_vars($groupName) as $varName => $description) {
                    $content[$groupName][$varName] = $description;
                    echo '<a class="header2-link" href="#' . $varName . '">' . $varName . '</a>';
                }
            }
            
            ?>
            
        </div>
    </aside>

    <div id="api-content">
        <h1>API Documentation</h1>
        <p>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Please use one of the following functions:<br><br>
        <a id="getuserbyid" class="jumpTo"></a>getuserbyid (http://careforsometeasaboteur.com/api/getuserbyid/1)<br>
        <a id="getuserbyemail" class="jumpTo"></a>getuserbyemail (http://careforsometeasaboteur.com/api/getuserbyemail/admin@careforsometeasaboteur.com)<br>
        <a id="getuserbyusername" class="jumpTo"></a>getuserbyusername (http://careforsometeasaboteur.com/api/getuserbyusername/admin)</p>
        
        <p>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test<br>Test</p>
    </div>
</div>