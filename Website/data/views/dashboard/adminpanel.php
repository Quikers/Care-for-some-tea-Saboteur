<div id="deckcard-container" style="position: static; margin: 10px;">

    <a class="btn btn-control btn-success" href="#"><i class="fa fa-check" aria-hidden="true"></i> Approve</a>
    <a class="btn btn-control btn-warning" href="#"><i class="fa fa-ban" aria-hidden="true"></i> Unapprove</a>
    <a class="btn btn-control btn-danger" href="#"><i class="fa fa-times" aria-hidden="true"></i> Reject</a>
    <p style="display: inline-block; vertical-align: super;"> Selected: </p>
    <p style="display: inline-block; vertical-align: super;" id="selectedView" class="decks">0 decks</p>
    <p style="display: inline-block; vertical-align: super;" id="selectedView" class="cards">0 cards </p><br>

    <div class="tablecontainer" id="deckstablecontainer">
        <label><h3>Decks</h3></label>
        <table id="decksTable" class="display cell-border compact nowrap" cellspacing="0" width="100%">
        <thead>
            <tr>
                <th class="controlcol"><input type="checkbox" id="selectAll" class="decks"></th>
                <th class="largecol">Name</th>
                <th class="tinycol">Cards</th>
                <th class="tinycol">Status</th>
                <th class="shortcol">Created on</th>
                <th class="shortcol">Editted on</th>
                <th class="shortcol">Editted hidden</th>
            </tr>
        </thead>
    </table></div>
    <div class="tablecontainer" id="cardstablecontainer">
        <label><h3>Cards</h3></label>
        <table id="cardsTable" class="display cell-border compact nowrap" cellspacing="0" width="49%">
        <thead>
            <tr>
                <th class="controlcol"><input type="checkbox" id="selectAll" class="cards"></th>
                <th class="largecol">Name</th>
                <th class="tinycol">Cost</th>
                <th class="tinycol">Attack</th>
                <th class="tinycol">Health</th>
                <th class="shortcol">Effect</th>
                <th class="tinycol">Status</th>
                <th class="shortcol">Created on</th>
                <th class="shortcol">Editted on</th>
                <th class="shortcol">Editted hidden</th>
            </tr>
        </thead>
    </table></div>

</div>

<script>


$(document).ready(function () {
    var selectedCards = [];
    var selectedDecks = [];
    
    $(".btn-success").click(function (e) {
        e.preventDefault();
        
        if (selectedCards.length > 0 || selectedDecks.length > 0) window.location = "<?= URL ?>dashboard/approve/" + selectedDecks.join(",") + "/" + selectedCards.join(",");
    });
    
    $(".btn-warning").click(function (e) {
        e.preventDefault();
        
        if (selectedCards.length > 0 || selectedDecks.length > 0) window.location = "<?= URL ?>dashboard/unapprove/" + selectedDecks.join(",") + "/" + selectedCards.join(",");
    });
    
    $(".btn-danger").click(function (e) {
        e.preventDefault();
        
        if (selectedCards.length > 0 || selectedDecks.length > 0) window.location = "<?= URL ?>dashboard/reject/" + selectedDecks.join(",") + "/" + selectedCards.join(",");
    });

    var decksTable = $("#decksTable").DataTable({
        "dom": "lftr",
        "paging": false,
        "scrollY": "40vh",
        "scrollCollapse": true,
        "ajax": "<?= URL ?>api/getalldecks",
        "aoColumns": [
            { "data": "id" },
            { "data": "name" },
            { "data": "cards.length" },
            { "data": "activated" },
            { "data": "created" },
            { "data": "editted", "iDataSort": 6 },
            { "data": "editted" }
        ],
        "columnDefs": [
            {
                "targets": [ 6 ],
                "visible": false,
                "searchable": false
            }
        ],
        "rowCallback": function( row, data, index ) {
            var children = $(row).children();
            var id = $.isNumeric($(children[0]).text()) ? $(children[0]).text() : $(children[0]).find("input[type=\"checkbox\"]").attr("id");

            var datetds = [children[children.length - 2], children[children.length - 1]];
            for (var i = 0; i < datetds.length; i++)
                $(datetds[i]).text($(datetds[i]).text().split(" ")[0]);

            $(children[3]).html( GetActivation($(children[3]).text()) );
            
            $(children[0]).html("<input type=\"checkbox\" class=\"select decks\" id=\"" + id + "\">");
        },
        "fnDrawCallback": function (oSettings) {
            $(".decks").iCheck({
                checkboxClass: 'icheckbox_square',
                radioClass: 'iradio_square'
            });

            $("#selectAll.decks").on("ifChanged", function (event) {
                if ($(this).iCheck('update')[0].checked)
                    $(".decks:not(#selectAll)").iCheck("check");
                else
                    $(".decks:not(#selectAll)").iCheck("uncheck");
            });

            $(".decks:not(#selectAll)").on('ifChanged', function (event) {
                if ($(this).iCheck('update')[0].checked)
                    selectedDecks.push($(this).attr("id"));
                else {
                    selectedDecks.splice(selectedDecks.indexOf($(this).attr("id")), 1);
                }
                
                $("#selectedView.decks").html(selectedDecks.length + " decks ");
            });
        }
    });

    var cardsTable = $("#cardsTable").DataTable({
        "dom": "lftr",
        "paging": false,
        "scrollY": "45vh",
        "scrollCollapse": true,
        "aaSorting": [[8, "desc"]],
        "ajax": "<?= URL ?>api/getallcards",
        "columns": [
            { "data": "id" },
            { "data": "name" },
            { "data": "cost" },
            { "data": "attack" },
            { "data": "health" },
            { "data": "effect.type" },
            { "data": "activated" },
            { "data": "created" },
            { "data": "editted", "iDataSort": 9 },
            { "data": "editted" }
        ],
        "columnDefs": [
            {
                "targets": [ 9 ],
                "visible": false,
                "searchable": false
            }
        ],
        "rowCallback": function( row, data, index ) {
            var children = $(row).children();
            var id = $(children[0]).text();
            
            var datetds = [children[children.length - 2], children[children.length - 1]];
            for (var i = 0; i < datetds.length; i++)
                $(datetds[i]).text($(datetds[i]).text().split(" ")[0]);
            
            for (var i = 1; i < children.length; i++) {
                $(children[i]).addClass("gotocard").click(function () {
                    window.location = "<?= URL ?>dashboard/editor/card/" + id;
                });
            }
            
            $(children[6]).html( GetActivation($(children[6]).text()) );
            
            $(children[0]).html("<input type=\"checkbox\" class=\"select cards\" id=\"" + id + "\">");
        },
        "fnDrawCallback": function (oSettings) {
            $(".cards").iCheck({
                checkboxClass: 'icheckbox_square',
                radioClass: 'iradio_square'
            });
            
            $("#selectAll.cards").on("ifChanged", function (event) {
                if ($(this).iCheck('update')[0].checked)
                    $(".cards:not(#selectAll)").iCheck("check");
                else
                    $(".cards:not(#selectAll)").iCheck("uncheck");
            });
            
            $(".cards:not(#selectAll)").on('ifChanged', function (event) {
                if ($(this).iCheck('update')[0].checked)
                    selectedCards.push($(this).attr("id"));
                else {
                    selectedCards.splice(selectedCards.indexOf($(this).attr("id")), 1);
                }
                
                $("#selectedView.cards").html(selectedCards.length + " cards");
            });
        }
    });
});

function GetActivation( a ) {
    var message = "";
    
    switch(a) {
        default:
            console.log("Activation key \"" + a + "\" not recognized.");
            break;
        case "-1":
            message = "<p style=\"color: crimson\">Rejected</p>";
            break;
        case "0":
            message = "<p style=\"color: orange\">Requested</p>";
            break;
        case "1":
            message = "<p style=\"color: green\">Accepted</p>";
            break;
        case "Accepted": case "Requested": case "Rejected": 
            message = "<p style=\"color: green\">" + a + "</p>";
            break;
    }
    
    return message;
}


</script>