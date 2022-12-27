

// occurs when a user clicks the create button
function Create() {
    
    var listName = $('#listName').val();
    var url = _spPageContextInfo.siteAbsoluteUrl;
    var title = $('#Create_Title').val();
    var lname = $('#lname').val();
    var city = $('#city').val();
    var dob = $('#dob').val();
    createListItemWithDetails(listName, url, title, lname, city, dob, function (data) {
        alert("Item has been created. Updating available items");
        // Read();
    }, function () {
        alert("Ooops, an error occured. Please try again");
    });
}
// occurs when a user clicks the read button
function Read() {
    //clear both text boxes 
    $('#UpdateTitle').val('');
    $('#Create_Title').val('');

    var listName = $('#listName').val();
    var url = _spPageContextInfo.siteAbsoluteUrl;

    getListItems(listName, url, function (data) {
        var items = data.d.results;

        // remove all of the previous items
        $('#UpdateItems option').each(function (index, option) { $(option).remove(); });
        $('#DeleteItems option').each(function (index, option) { $(option).remove(); });

        // Add all the new items
        for (var i = 0; i < items.length; i++) {
            $('#UpdateItems').append(new Option(items[i].Title, items[i].Id, false, false));
            $('#DeleteItems').append(new Option(items[i].Title, items[i].Id, false, false));

        }
    }, function (data) {
        alert(JSON.stringify(data));
        alert("Ooops, an error occured. Please try again");
    });
}

// CREATE Operation
// listName: The name of the list you want to get items from
// siteurl: The url of the site that the list is in. // title: The value of the title field for the new item
// success: The function to execute if the call is sucesfull
// failure: The function to execute if the call fails
function createListItemWithDetails(listName, siteUrl, title, lname, city, dob, success, failure) {

    var itemType = GetItemTypeForListName(listName);
    var item = {
        "__metadata": { "type": itemType },
        "Title": title,
        "LastName": lname,
        "CityId": city,
        "DOB": dob

    };

    $.ajax({
        url: siteUrl + "/_api/web/lists/getbytitle('" + listName + "')/items",
        type: "POST",
        contentType: "application/json;odata=verbose",
        data: JSON.stringify(item),
        headers: {
            "Accept": "application/json;odata=verbose",
            "X-RequestDigest": $("#__REQUESTDIGEST").val()
        },
        success: function (data) {
            success(data);
        },
        error: function (data) {
            failure(data);
        }
    });
}
// Getting the item type for the list
function GetItemTypeForListName(name) {

    return "SP.Data.Table_x005f_dataListItem";

}

$(document).ready(function () {

    addDepartment();
});
function addDepartment() {
    debugger;
    
    var fullUrl = _spPageContextInfo.webAbsoluteUrl + "/_api/web/lists/getByTitle('City')/items";
    $.ajax({
        url: fullUrl,
        type: "GET",
        headers: {
            "accept": "application/json;odata=verbose",
            "content-type": "application/json;odata=verbose",
        },
        success: function onQueryEmpSucceeded(data) {
            $.each(data.d.results, function (key, value) {
                $('#city').append(new Option(value.City, value.Id));
            });

        },
        error: function onQueryEmpFailed(data) {
            error: onQueryEmpFailed
        }
    });
}

$(function(){
$("#btntable").click(function(){
    debugger;
     var listName = $('#listName').val();
var requestUri = _spPageContextInfo.webAbsoluteUrl + "/_api/web/lists/getbytitle('" + listName + "')/items?$select=Id,Title,LastName,DOB,City/City&$expand=City";
$.ajax({
url: requestUri,
type: "GET",
headers: {
"accept":"application/json; odata=verbose"
},
success: onSuccess,
error: onError
});
function onSuccess(data) {
var items = data.d.results;
var fullResults = '<table id="tableCars" style="width:100%" border="1 px"><thead><tr><td><b>ID</b></td>' +'<td><b>First Name</b></td>'+  '<td><b>Last Name</b></td>'+ '<td><b>City</b></td>' +'<td><b>DOB</b></td>' + '<td><b>Action</b></td>' + '</tr></thead><tbody>';
for (var i = 0; i < items.length; i++) {
fullResults += '<tr>';
fullResults += '<td>' + items[i].Id + '</td>';
fullResults += '<td>' + items[i].Title + '</td>';
fullResults += '<td>' + items[i].LastName + '</td>';
fullResults += '<td>' + items[i].City.City + '</td>';
fullResults += '<td>' + items[i].DOB + '</td>';
fullResults +=" '<td>'<a href='javascript: DeleteListItemUsingItemId(" +items[i].Id + ")'>Delete </a> || <a href='javascript: UpdateEmployeeListData(" +items[i].Id + ")'>Edit </a>'";
fullResults += '</tr>';
}
$('#resultsTable').append(fullResults);
}
function onError(error) {
alert('Error');
}
});
});
//Udate items

function UpdateEmployeeListData(Id) {  
    debugger;
    var listName = $('#listName').val();
    var url = _spPageContextInfo.siteAbsoluteUrl;
    var title = $('#Create_Title').val();
    var lname = $('#lname').val();    
    var city = $('#city').val();
    var dob = $('#dob').val();

  
  
    $.ajax  
        ({  
            url: _spPageContextInfo.webAbsoluteUrl + "/_api/web/lists/GetByTitle('" + listName + "')/items(" + Id + ")",  
            type: "POST",  
            data: JSON.stringify  
                ({  
                    __metadata:  
                    {  
                        type: "SP.Data.Table_x005f_dataListItem"  
                    },  
                       "Title": title,
                       "LastName": lname,
                       "CityId": city,
                       "DOB": dob
                      
  
                }),  
            headers:  
            {  
                "Accept": "application/json;odata=verbose",  
                "Content-Type": "application/json;odata=verbose",  
                "IF-MATCH": "*",  
                "X-HTTP-Method": "MERGE",  
                "X-RequestDigest": $("#__REQUESTDIGEST").val()  
            },  
            success: function (data, status, xhr) {  
                alert("Item has been Udated. Updating available items");
                getEmployeeListData();  
            },  
            error: function (xhr, status, error) {  
                $("#ResultDiv").empty().text(xhr.responseJSON.error);  
            }  
        });  
}  

// Delete item
function DeleteListItemUsingItemId(Id) {  
    debugger;
    var listName = $('#listName').val();
    var check = confirm("Are you sure you want to Delete ?");  
    if (check == true) {  
        $.ajax  
            ({  
                url: _spPageContextInfo.webAbsoluteUrl + "/_api/web/lists/getByTitle('" + listName + "')/items(" + Id + ")",  
                type: "POST",  
                headers:  
                
                {  
                    "Accept": "application/json;odata=verbose",  
                    "Content-Type": "application/json;odata=verbose",  
                    "IF-MATCH": "*",  
                    "X-HTTP-Method": "DELETE",  
                    "X-RequestDigest": $("#__REQUESTDIGEST").val()  
                },  
  
                success: function (data, status, xhr) {  
                    console.log("Success");  
                      alert("Item has been Deleted.");
                    getEmployeeListData();  
                },  
                error: function (xhr, status, error) {  
                    console.log("Failed");  
                }  
            });  
  
    }  
    else {  
        return false;  
    }  
  
}  
function getEmployeeListData() {  
    var listName = $('#listName').val();
    var fullUrl = _spPageContextInfo.webAbsoluteUrl + "/_api/web/lists/getByTitle('" + listName + "')/items";  
    $.ajax({  
        url: fullUrl,  
        type: "GET",  
        headers: {  
            "accept": "application/json;odata=verbose",  
            "content-type": "application/json;odata=verbose",  
        },  
        success: onQueryEmpSucceeded,  
        error: onQueryEmpFailed,  
    });  
}  

