$(document).ready(function () {
    $('#MyTable').DataTable({
        "ajax": {
            "type": "GET",
            "url": "/Product/JsonTest",
            "dataSrc": function (json) {
                //Make your callback here.
                return json;
            }
        },
        "columns": [
            { 'data': 'ProductName' },  
            { 'data': 'Price' },
            { 'data': 'InStock' },
            { 'data': 'OnOrders' },
            { 'data': 'ReorderLevel' },
            { 'data': 'Discontinued' }
        ],
        
    });
});