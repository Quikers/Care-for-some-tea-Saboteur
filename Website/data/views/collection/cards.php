<div id="deckcard-container" style="position: static; margin: 10px;">
    
    <h1>Collection: Cards</h1>
    <p>Here you will find all available Cards with all their information on them.</p>
    
    <table id="cardsTable" class="display cell-border compact nowrap" cellspacing="0" width="100%">
        <thead>
            <tr>
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
    </table>
    
</div>

<script>

$(document).ready(function () {
    var cardsTable = $("#cardsTable").DataTable({
        "dom": "lftr",
        "paging": false,
        "scrollY": "45vh",
        "scrollCollapse": true,
        "aaSorting": [[7, "desc"]],
        "ajax": "<?= URL ?>api/getallcards",
        "columns": [
            { "data": "name" },
            { "data": "cost" },
            { "data": "attack" },
            { "data": "health" },
            { "data": "effect.type" },
            { "data": "activated" },
            { "data": "created" },
            { "data": "editted", "iDataSort": 8 },
            { "data": "editted" }
        ],
        "columnDefs": [
            {
                "targets": [ 8 ],
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
            
            $(children[5]).html( GetActivation($(children[5]).text()) );
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