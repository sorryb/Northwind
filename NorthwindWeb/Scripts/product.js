
﻿/*find correct pathc for search*/
function searchControllerPath() {
    var path = window.location.href;
    var a = path.split("/");
    if (path.search("http://") + 1) {
        return a[0] + '/' + a[1] + '/' + a[2] + '/' + a[3];
    }
    else {
        return a[0] + '/' + a[1];
    }
}

$(document).ready(function () {

    /*datatable handler with server side implementation for product*/
    $('#Product').DataTable({
        "processing": true,
        "serverSide": true,
        "responsive": true,
        "autoWidth": false,
        "columnDefs": [
            { responsivePriority: 1, targets: 0 },
            { responsivePriority: 2, targets: -1 }
        ],
        "ajax": {
            "type": "GET",
            "url": searchControllerPath() + "/JsonTableFill",
            "dataSrc": function (json) {
                //Make your callback here.
                $.each(json.data, function (index, item) {
                    item.DeleteLink = '<a href= "' + searchControllerPath() + '/Delete?id=' + item.ID + '"/> <i class="fa fa-remove"></i></a >';
                    item.ProductName = '<a href= "' + searchControllerPath() + '/Details?id=' + item.ID + '"/>' + item.ProductName + '</a >';
                })
                return json.data;
            }
        },
        "columns": [
            { 'data': 'ProductName' },
            { 'data': 'Price' },
            { 'data': 'InStock' },
            { 'data': 'OnOrders' },
            { 'data': 'ReorderLevel' },
            { 'data': 'Discontinued' },
            { 'data': 'DeleteLink' }
        ]
    });
});

