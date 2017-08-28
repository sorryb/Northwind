$(document).ready(function () {

    $("#searchTextBox").autocomplete({
        //To empty the selected Result Div
        
        //The function to be called for autocomplete
        source: function (request, response) {
            $.ajax({
                url: "/SearchEngine/GetAutoCompleteData", type: "POST", dataType: "json",
                data: { searchText: request.term, maxResults: 7 },
                success: function (data) {
                    $('#selectedResult').empty();
                    response($.map(data, function (item) {
                        return { label: item.text, value: item.text, id: item.value }
                    }))
                }
            })
        },
        //The function to be called for populating the selected Result
        select: function (event, ui) {
            populateSelectedResult(ui.item.id)
        }
    });
});


function populateSelectedResult(selectedId) {
    $.ajax({
        type: 'get',
        url: '/SearchEngine/GetSelectedResultData', //JSON Method to be called
        data: {id: selectedId},
        success: function (data) {

            //The Div to be populated
            $('#selectedResult').empty();
            content = "";
            //Looping thru each record
            $.each(data, function (i, record) {
                //Properties available in Model
                //We need to specify the properties in our model
                content += "<tr><td><b>" + record.Title + "</b></td></tr>";
                content += "<tr><td>" + record.Description + "</td></tr>"

            });
            table = "<table>" + content + "</table>"
            $(table).appendTo('#selectedResult');
        },
        error: function (response) {
            alert(response.Message);
        },
        dataType: 'json'
    });
}

function populateAutoCompleteData(request,response) {
   
}