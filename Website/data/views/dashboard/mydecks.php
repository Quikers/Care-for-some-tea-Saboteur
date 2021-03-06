<div id="view-body">
    
    <h1>My Decks</h1>
    <p style="font-size: 16px;">To edit a deck, press on the table row. This will redirect you to the editor page for that specific Deck.</p><br>
    
    <a class="btn btn-control btn-success" href="<?= URL ?>dashboard/editor/deck/"><i class="fa fa-plus" aria-hidden="true"></i> Create</a>
    <a class="btn btn-control btn-danger" href="#"><i class="fa fa-trash" aria-hidden="true"></i> Delete (0 selected)</a>
    
    <table id="decksTable" class="display cell-border -border" cellspacing="0" width="100%">
        <thead>
            <tr>
                <th class="controlcol"><input type="checkbox" id="selectAll"></th>
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
    
    var selected = [];
    
    $(".btn-danger").click(function (e) {
        e.preventDefault();
        
        if (selected.length > 0) window.location = "<?= URL ?>dashboard/delete/decks/" + selected.join(",");
    });
    
    var table = $('#decksTable').DataTable({
        "dom": "lftr",
        "paging": false,
        "scrollY": "45vh",
        "scrollCollapse": true,
        "aaSorting": [[5, "desc"]],
        "ajax": "<?= URL ?>api/getdecksbyuserid/<?= $_SESSION["user"]["id"] ?>/true",
        "columns": [
            { "data": "id" },
            { "data": "name" },
            { "data": "cards" },
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
            var id = $(children[0]).text();
            
            var datetds = [children[children.length - 2], children[children.length - 1]];
            for (var i = 0; i < datetds.length; i++)
                $(datetds[i]).text($(datetds[i]).text().split(" ")[0]);
            
            for (var i = 1; i < children.length; i++) {
                $(children[i]).addClass("gotodeck").click(function () {
                    window.location = "<?= URL ?>dashboard/editor/deck/" + id;
                });
            }
            
            $.get("<?= URL ?>api/getcardsbydeckid/" + id, function (data) {
                data = data != "false" ? JSON.parse(data).data : [];
                $(children[2]).html(data.length);
            });
            
            $(children[3]).html( GetActivation($(children[3]).text()) );
            
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
            message = "<p style=\"color: crimson;\">Rejected</p>";
            break;
        case "0":
            message = "<p style=\"color: orange;\">Requested</p>";
            break;
        case "1":
            message = "<p style=\"color: green;\">Accepted</p>";
            break;
    }
    
    return message;
}

</script>