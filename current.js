<script src ="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script type ="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery.SPServices/2014.02/jquery.SPServices.js"></script>
    <script type ="text/javascript">
    $(document).ready(function() {

        $().SPServices({
            operation: "GetGroupCollectionFromUser",
            userLoginName: $().SPServices.SPGetCurrentUser(),
            async: false,
            completefunc: function(xData, Status) {

                //If the current User does belong to the group "SharePoint Group Name"
                if ($(xData.responseXML).find("Group[Name = 'Qassas Group']").length == 1) {
                    // Hide Section Based on SharePoint Group
                    document.getElementById('ID1').style.display = "none";
                } else {
                    // Show Section Based on SharePoint Group
                    document.getElementById('ID1').style.display = "block";
                }
            }
        });
    }); 
</script>
