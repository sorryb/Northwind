
$(document).ready(function () {
    $('#MyTable').DataTable({
        "ajax": {
            "type": "GET",
            "url": "/Product/JsonTest",
            "dataSrc": function (json) {
                //Make your callback here.
                $.each(json, function (index, item){
                    item.DeleteLink = '<a href= "/Product/Delete?id=' + item.ID + '"/> <i class="fa fa-remove"></i></a >'
                });
                return json;
            }
        },
        "columns": [
            { 'data': 'ProductName' },
            { 'data': 'Price' },
            { 'data': 'InStock' },
            { 'data': 'OnOrders' },
            { 'data': 'ReorderLevel' },
            { 'data': 'Discontinued' },
            { 'data': 'DeleteLink'}
        ]

    });
});