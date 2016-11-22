    </div>

    <div id="footer">
        &copy; Care for some tea, Saboteur?, <?php echo Date("Y") == "2016" ? Date("Y") : "2016 - " . Date("Y") ; ?>
    </div>
    
    

    <script>


    $(document).ready(function () {
        $("body").fadeIn(250);
    });
    
    $(window).on("beforeunload", function () {
        $("body").fadeOut(250);
    });


    </script>
        
</body>
</html>