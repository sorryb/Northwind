
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

/*add from json (product/jsontest) in table, when we search, a list of all products (that contain search.value) come to table and local we make pagedlist*/
$(document).ready(function () {
    $('#MyTable').DataTable({
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
                $.each(json, function (index, item) {
                    item.DeleteLink = '<a href= "' + searchControllerPath() + '/Delete?id=' + item.ID + '"/> <i class="fa fa-remove"></i></a >';
                    item.ProductName = '<a href= "' + searchControllerPath() + '/Details?id=' + item.ID + '"/>' + item.ProductName + '</a >';
                })
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
            { 'data': 'DeleteLink' }
        ]

    });
});


/*add from json in table Employees*/
$(document).ready(function () {
    $('#EmployeesTable').DataTable({
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
                $.each(json, function (index, item) {
                    item.DeleteLink = '<a href= "' + searchControllerPath() + '/Delete?id=' + item.ID + '"/> <i class="fa fa-remove"></i></a >';
                    item.LastName = '<a href= "' + searchControllerPath() + '/Details?id=' + item.ID + '"/>' + item.LastName + '</a >';
                })
                return json;
            }
        },
        "columns": [
            { 'data': 'LastName' },
            { 'data': 'FirstName' }, 
            { 'data': 'Title' },
            { 'data': 'City' },
            { 'data': 'Country' },
            { 'data': 'HomePhone' },
            { 'data': 'DeleteLink' }
        ]

    });
});


/*add from json in table Customers*/
$(document).ready(function () {
    $('#CustomersTable').DataTable({
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
                $.each(json, function (index, item) {
                    item.DeleteLink = '<a href= "' + searchControllerPath() + '/Delete?id=' + item.ID + '"/> <i class="fa fa-remove"></i></a >';
                    item.CompanyName = '<a href= "' + searchControllerPath() + '/Details?id=' + item.ID + '"/>' + item.CompanyName + '</a >';
                })
                return json;
            }
        },
        "columns": [
            { 'data': 'CompanyName' },
            { 'data': 'ContactName' },
            { 'data': 'ContactTitle' },
            { 'data': 'City' },
            { 'data': 'Country' },
            { 'data': 'Phone' },
            { 'data': 'DeleteLink' }
        ]

    });
});

/*add from json in table Orders*/
$(document).ready(function () {
    $('#OrdersTable').DataTable({
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
                $.each(json, function (index, item) {
                    item.DeleteLink = '<a href= "' + searchControllerPath() + '/Delete?id=' + item.ID + '"/> <i class="fa fa-remove"></i></a >';
                    item.ID = '<a href= "' + searchControllerPath() + '/Details?id=' + item.ID + '"/>' + item.ID + '</a >';
                })
                return json;
            }
        },
        "columns": [
            { 'data': 'ID' },
            { 'data': 'LastName' },
            { 'data': 'CompanyName' },
            { 'data': 'ShippedDate' },
            { 'data': 'ShipName' },
            { 'data': 'ShipAddress' },
            { 'data': 'DeleteLink' }
        ]

    });
});
