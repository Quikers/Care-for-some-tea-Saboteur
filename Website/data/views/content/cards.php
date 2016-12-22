<div id="cluster">
    
    <aside id="deckcard-filter">
        <input id="searchbox" type="text" name="search" placeholder="Search"/>
        <input id="damagemin" type="number" min="0" max="10" name="damagemin"/>
        <input id="damagemax" type="number" min="0" max="10" name="damagemax"/>
    </aside>
    <div id="deckcard-container">
        
    </div>
    
</div>

<script>


$(document).ready(function () {
    $("#searchbox").keyup(function () {
        $("#deckcard-container").text($(this).val());
        console.log("Key Up");
    });
    
    $("#damagemin").change(function () {
        if (parseInt($(this).val()) > parseInt($("#damagemax").val())) {
            $(this).val() = $("#damagemax").val();
        }
    });
    
    $("#damagemax").change(function () {
        if (parseInt($(this).val()) < parseInt($("#damagemin").val())) {
            $(this).val() = $("#damagemin").val();
        }
    });
});


</script>