<div id="view-body">
    
    <h1>My Cards</h1>
    <p style="font-size: 16px;">To edit a card, press on the table row. This will redirect you to the editor page for that specific Card.</p><br>
    
    <a class="btn btn-control btn-success" href="<?= URL ?>dashboard/editor/card/"><i class="fa fa-plus" aria-hidden="true"></i> Create</a>
    <a class="btn btn-control btn-danger" href="#"><i class="fa fa-trash" aria-hidden="true"></i> Delete (0 selected)</a>
    
    <table id="cardsTable" class="display cell-border" cellspacing="0" width="100%">
        <thead>
            <tr>
                <th class="controlcol"><input type="checkbox" id="selectAll"></th>
                <th class="largecol">Name</th>
                <th class="tinycol">Attack</th>
                <th class="tinycol">Health</th>
                <th class="largecol">Effect</th>
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
    
    var selected = [];
    
    $(".btn-danger").click(function (e) {
        e.preventDefault();
        
        if (selected.length > 0) window.location = "<?= URL ?>dashboard/delete/cards/" + selected.join(",");
    });
    
    var table = $('#cardsTable').DataTable({
        "dom": "lftr",
        "paging": false,
        "scrollY": "45vh",
        "scrollCollapse": true,
        "aaSorting": [[7, "desc"]],
        "ajax": "<?= URL ?>api/getcardsbyuserid/<?= $_SESSION["user"]["id"] ?>",
        "columns": [
            { "data": "id" },
            { "data": "name" },
            { "data": "attack" },
            { "data": "health" },
            { "data": "effect.effect" },
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
            
            $(children[0]).html("<input type=\"checkbox\" class=\"select\" id=\"" + id + "\">");
        },
        "fnDrawCallback": function (oSettings) {
            $("input").iCheck({
                checkboxClass: 'icheckbox_square-red',
                radioClass: 'iradio_square-red'
            });
            
            $("#selectAll").on("ifChanged", function (event) {
                if ($(this).iCheck('update')[0].checked)
                    $("input:not(#selectAll)").iCheck("check");
                else
                    $("input:not(#selectAll)").iCheck("uncheck");
            });
            
            $("input:not(#selectAll)").on('ifChanged', function (event) {
                if ($(this).iCheck('update')[0].checked)
                    selected.push($(this).attr("id"));
                else {
                    selected.splice(selected.indexOf($(this).attr("id")), 1);
                }
                
                var icon = $(".fa-trash");
                $(".btn-danger").html(" Delete (" + selected.length + " selected)").prepend(icon);
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
    }
    
    return message;
}

</script>