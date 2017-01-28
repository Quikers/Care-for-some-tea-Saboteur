<div id="deckcard-container" style="position: static; margin: 10px;">
    
    <h1>Collection: Decks</h1>
    <p>Here you will find all available Decks with all their information on them.</p>
    
    <table id="decksTable" class="display cell-border compact nowrap" cellspacing="0" width="100%">
        <thead>
            <tr>
                <th class="largecol">Name</th>
                <th class="tinycol">Cards</th>
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
});


</script>