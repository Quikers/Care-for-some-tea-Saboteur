<div id="view-body">
    
    <a id="editorbutton" href="<?= URL ?>dashboard/cardeditor/">Create a new Card</a>
    
    This is the My Cards page.<br><br>
    
    <table id="cardsTable" class="display" cellspacing="0" width="100%">
        <thead>
            <tr>
                <th class="controlcol">Controls</th>
                <th class="largecol">Name</th>
                <th class="tinycol">Attack</th>
                <th class="tinycol">Health</th>
                <th class="largecol">Effect</th>
                <th class="shortcol">Created on</th>
                <th class="shortcol">Editted on</th>
            </tr>
        </thead>
    </table>
    
</div>

<script>


$(document).ready(function () {
    
    $('#cardsTable').DataTable({
        "dom": "lftipr",
        "ajax": "<?= URL ?>api/getcardsbyuserid/<?= $_SESSION["user"]["id"] ?>",
        "columns": [
            { "data": "id" },
            { "data": "name" },
            { "data": "attack" },
            { "data": "health" },
            { "data": "effect.effect" },
            { "data": "created" },
            { "data": "editted" }
        ],
        "rowCallback": function( row, data, index ) {
            var children = $(row).children();
            var id = $(children[0]).text();
            
            var datetds = [children[children.length - 2], children[children.length - 1]];
            for (var i = 0; i < datetds.length; i++)
                $(datetds[i]).text($(datetds[i]).text().split(" ")[0]);
            
            $(row).attr("id", id);
            $(children[0]).html("<a class=\"controlbutton\" href=\"<?= URL ?>dashboard/cardeditor/" + id + "\"><i class=\"fa fa-pencil\" aria-hidden=\"true\"></i> Edit</a><br><a class=\"controlbutton\" href=\"#\"><i class=\"fa fa-trash\" aria-hidden=\"true\"></i> Delete</a>");
        }
    });
    
});


</script>