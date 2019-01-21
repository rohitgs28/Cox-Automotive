$(document).ready(function () {
    $("#givenTable").hide();
    $('#btnUploadFile').on('click', function () {

        var data = new FormData();

        var files = $("#fileUpload").get(0).files;

        // Add the uploaded image content to the form data collection
        if (files.length > 0) {
            data.append("files", files[0]);
        }
      
        // Make Ajax request with the contentType = false, and procesDate = false
        var ajaxRequest = $.ajax({
            type: "POST",
            url: "/Uploader",
            contentType: false,
            processData: false,
            data: data
        });

        ajaxRequest.done(function (result) {
        
            if (result.status == true) {
                $("#givenTable").show();
                $("#displayGrid > tr").remove();
                var newAjaxRequest = $.ajax
                    ({
                        type: 'GET',
                        url: "/View/FetchRecords/",
                        dataType: 'json',
                        data: { myfile: files[0].name },
                        success: function (data) {
                            data = data.data;
                          if (data.length != 0) {
                                for (i = 0; i < data.length; ++i) {
                                    var rows = "<tr>" +
                                        "<td class='table-bordered'>" + "<div class=\"RequestDetailsTR\">" + data[i].dealNumber + "</div>" + "</td>" +
                                        "<td class='table-bordered'>" + "<div class=\"RequestDetailsTR\">" + data[i].customerName + "</div>"  + "</td>" +
                                        "<td class='table-bordered'>" + "<div class=\"RequestDetailsTR\">" + data[i].dealershipName + "</div>"  + "</td>" +
                                        "<td class='table-bordered'>" + "<div class=\"RequestDetailsTR\">" + data[i].vehicle + "</div>"  + "</td>"+
                                        "<td class='table-bordered'>" + "<div class=\"RequestDetailsTR\">" + 'CAD$' + data[i].price + "</div>"  +  "</td>" +
                                        "<td class='table-bordered'>" + "<div class=\"RequestDetailsTR\">" + data[i].date + "</div>" + "</tr>";
                                    $('#displayGrid').append(rows);
                                }
                         
                              
                            }
                        }
                    });
                  

            } else {
                alert(result.message);
            }
        });

        
    });
});