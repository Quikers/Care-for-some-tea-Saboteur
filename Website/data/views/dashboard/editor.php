<div id="content-body" <?= ( $this->type == "card" ? "style=\"min-width: 500px; width: 500px;\"" : "" ) ?>>
    <h1><?= ucfirst($this->type) ?> Editor</h1>
    <form style="width: auto; margin-top: 30px;" id="editorform" class="forms" method="POST" action="<?php

    if ($this->type == "card") {
        
        $effectval = "1";
        foreach($this->effects as $val => $effectArr) {
            if ($effectArr["id"] == $this->card["effect"]["id"]) { $effectval = $val + 1; }
        }
        
        ?><?= URL ?>dashboard/uploadcard">
        
        <fieldset>
            <legend>Main</legend>
            <label for="cardname">Card name</label><input type="text" id="cardname" name="name" value="<?= isset($this->card) ? $this->card["name"] : "" ?>" required><br>
            <label for="cost">Energy Cost</label><input type="number" min="0" max="10" value="<?= isset($this->card) ? $this->card["cost"] : "0" ?>" id="cost" name="cost" required><br>
        </fieldset>
        <fieldset>
            <legend>Combat</legend>
            <label for="attack">Attack</label><input type="number" min="0" max="12" value="<?= isset($this->card) ? $this->card["attack"] : "0" ?>" id="attack" name="attack" required>
            <input style="margin-left: 32px" type="number" min="0" max="12" value="<?= isset($this->card) ? $this->card["health"] : "0" ?>" id="health" name="health" required><label style="text-align: right;" for="health">Health</label><br>
            <label class="effect" for="effect">Effect</label><select id="effect" name="effect" required><?php 
                foreach ($this->effects as $effectArr) { echo "<option id=\"" . $effectArr["id"] . "\"" . ( $effectArr["id"] == $this->card["effect"]["id"] ? " selected=\"selected\"" : "" ) . ">" . $effectArr["effect"] . "</option>\n"; }
            ?></select><br>
        </fieldset>
        
        <?php
        
    } else if ($this->type == "deck") {
        
        ?><?= URL ?>dashboard/uploaddeck">
        <fieldset>
            <legend>Main</legend>
            <label for="cardname">Deck name</label><input type="text" id="deckname" name="name" value="<?= isset($this->deck) ? $this->deck["name"] : "" ?>" required><br>
            <input type="text" name="cards" value="" hidden>
            <div class="tablecontainer">
                <label>Current Cards</label><p style="display: inline-block; color: crimson; font-size: 16px;">All cards checked in the table below will be removed on upload!</p>
                <table id="currentCardsTable" class="display cell-border compact nowrap" cellspacing="0" width="49%">
                <thead>
                    <tr>
                        <th class="controlcol"><input type="checkbox" id="selectAll" class="delete"></th>
                        <th class="largecol">Name</th>
                        <th class="largecol">Effect</th>
                        <th class="tinycol">Cost</th>
                        <th class="tinycol">Attack</th>
                        <th class="tinycol">Health</th>
                    </tr>
                </thead>
            </table></div>
            <div class="tablecontainer">
                <label>All Cards</label><p style="display: inline-block; color: green; font-size: 16px;">All cards checked in the table below will be added on upload!</p>
                <table id="addCardsTable" class="display cell-border compact nowrap" cellspacing="0" width="100%">
                <thead>
                    <tr>
                        <th class="controlcol"><input type="checkbox" id="selectAll" class="add"></th>
                        <th class="largecol">Name</th>
                        <th class="largecol">Effect</th>
                        <th class="tinycol">Cost</th>
                        <th class="tinycol">Attack</th>
                        <th class="tinycol">Health</th>
                    </tr>
                </thead>
            </table></div><br>
        </fieldset>
        
        <?php
        
    }

    ?>
        <input class="btn btn-default form-control" type="submit" value="Upload">
    </form>
    
</div>

<script>
    
    $(document).ready(function () {
        var card = <?= isset($this->card) ? "JSON.parse('" . json_encode($this->card) . "')" : "''" ?>;
        var deck = <?= isset($this->deck) ? "JSON.parse('" . json_encode($this->deck) . "')" : "''" ?>;
    
        var deleteSelected = [];
        var addSelected = [];
                
        console.log(card);
        console.log(deck);
        
        <?php if ($this->type == "deck") { ?>
        
        var currentCardsTable = $("#currentCardsTable").DataTable({
            "dom": "tr",
            "paging": false,
            "responsive": false,
            "scrollY": "40vh",
            "scrollCollapse": true,
            "aaData": deck.cards,
            "aoColumns": [
                { "data": "id" },
                { "data": "name" },
                { "data": "effect.effect" },
                { "data": "cost" },
                { "data": "attack" },
                { "data": "health" }
            ],
            "rowCallback": function( row, data, index ) {
                var children = $(row).children();
                var id = $(children[0]).text();

                var datetds = [children[children.length - 2], children[children.length - 1]];
                for (var i = 0; i < datetds.length; i++)
                    $(datetds[i]).text($(datetds[i]).text().split(" ")[0]);

                $(children[0]).html("<input type=\"checkbox\" class=\"select delete\" id=\"" + id + "\">");
            },
            "fnDrawCallback": function (oSettings) {
                $("input.delete").iCheck({
                    checkboxClass: 'icheckbox_square-red',
                    radioClass: 'iradio_square-red'
                });

                $("#selectAll.delete").on("ifChanged", function (event) {
                    if ($(this).iCheck('update')[0].checked)
                        $(".delete:not(#selectAll)").iCheck("check");
                    else
                        $(".delete:not(#selectAll)").iCheck("uncheck");
                });

                $(".delete:not(#selectAll.delete)").on('ifChanged', function (event) {
                    if ($(this).iCheck('update')[0].checked)
                        deleteSelected.push($(this).attr("id"));
                    else {
                        deleteSelected.splice(deleteSelected.indexOf($(this).attr("id")), 1);
                    }
                });
            }
        });
        
        var addCardsTable = $("#addCardsTable").DataTable({
            "dom": "tr",
            "paging": false,
            "scrollY": "40vh",
            "scrollCollapse": true,
            "ajax": "<?= URL ?>api/getallcards",
            "aoColumns": [
                { "data": "id" },
                { "data": "name" },
                { "data": "effect.effect" },
                { "data": "cost" },
                { "data": "attack" },
                { "data": "health" }
            ],
            "rowCallback": function( row, data, index ) {
                var children = $(row).children();
                var id = $(children[0]).text();

                var datetds = [children[children.length - 2], children[children.length - 1]];
                for (var i = 0; i < datetds.length; i++)
                    $(datetds[i]).text($(datetds[i]).text().split(" ")[0]);

                $(children[0]).html("<input type=\"checkbox\" class=\"select add\" id=\"" + id + "\">");
            },
            "fnDrawCallback": function (oSettings) {
                $("input.add").iCheck({
                    checkboxClass: 'icheckbox_square-green',
                    radioClass: 'iradio_square-green'
                });

                $("#selectAll.add").on("ifChanged", function (event) {
                    if ($(this).iCheck('update')[0].checked)
                        $(".add:not(#selectAll)").iCheck("check");
                    else
                        $(".add:not(#selectAll)").iCheck("uncheck");
                });

                $(".add:not(#selectAll.add)").on('ifChanged', function (event) {
                    if ($(this).iCheck('update')[0].checked)
                        addSelected.push($(this).attr("id"));
                    else {
                        addSelected.splice(addSelected.indexOf($(this).attr("id")), 1);
                    }
                });
            }
        });
        
        <?php } ?>
        
    });
    
</script>