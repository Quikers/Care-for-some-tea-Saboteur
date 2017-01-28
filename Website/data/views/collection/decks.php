<div id="content-body">
    
    <h1>Collection: Decks</h1>
    <p>Here you will find all available Decks with all their information on them.</p>
    
    <table id="decksTable" class="display cell-border nowrap" cellspacing="0" width="100%">
        <thead>
            <tr>
                <th width="78%">Name</th>
                <th width="2%" >Cards</th>
                <th width="5%">Status</th>
                <th width="5%">Created on</th>
                <th width="5%">Editted on</th>
                <th>Editted hidden</th>
            </tr>
        </thead>
    </table>
    
</div>

<script>

$(document).ready(function () {
    var decksTable = $("#decksTable").DataTable({
        "dom": "lftr",
        "paging": false,
        "scrollY": "40vh",
        "scrollCollapse": true,
        "aaSorting": [[5, "desc"]],
        "ajax": "<?= URL ?>api/getalldecks",
        "aoColumns": [
            { "data": "name" },
            { "data": "cards.length" },
            { "data": "activated" },
            { "data": "created" },
            { "data": "editted", "iDataSort": 5 },
            { "data": "editted" }
        ],
        "columnDefs": [
            {
                "targets": [ 5 ],
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

            $(children[2]).html( GetActivation($(children[2]).text()) );
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