
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
                });
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
                });
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
                    item.ShippedDate = item.ShippedDate.replace("12:00AM", "");
                    item.DeleteLink = '<a href= "' + searchControllerPath() + '/Delete?id=' + item.ID + '"/> <i class="fa fa-remove"></i></a >';
                    item.ID = '<a href= "' + searchControllerPath() + '/Details?id=' + item.ID + '"/>' + item.ID + '</a >';
                });
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
$(document).ready(function () {
    $('#Suppliers').DataTable({
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
                });
                return json;
            }
        },
        "columns": [
            {
                'data': 'CompanyName'
            },
            { 'data': 'ContactName' },
            { 'data': 'ContactTitle' },
            { 'data': 'Address' },
            { 'data': 'City' },
            { 'data': 'Country' },
            { 'data': 'Phone' },
            { 'data': 'DeleteLink' }
        ]

    });
});

/*add from json in table Shippers*/
$(document).ready(function () {
    $('#ShippersTable').DataTable({
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
                });
                return json;
            }
        },
        "columns": [
            { 'data': 'CompanyName' },
            { 'data': 'Phone' },
            { 'data': 'DeleteLink' }
        ]

    });
});

/*add from json in table Categories*/
$(document).ready(function () {
    $('#CategoriesTable').DataTable({
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
                    item.CategoryName = '<a href= "' + searchControllerPath() + '/Details?id=' + item.ID + '"/>' + item.CategoryName + '</a >';
                });
                return json;
            }
        },
        "columns": [
            { 'data': 'CategoryName' },
            { 'data': 'Description' },
            { 'data': 'DeleteLink' }
        ]

    });
});
/*add from json in table User*/
$(document).ready(function () {
    $('#UsersTable').DataTable({
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
                    if (item.IsLockedOut) { item.IsLockedOut = "Yes"; }
                       else { item.IsLockedOut = "No"; }
                    if (item.IsOnline) { item.IsOnline = "Yes"; }
                    else { item.IsOnline = "No"; }
                    item.DeleteLink = '<a href= "' + searchControllerPath() + '/Delete?userName=' + item.UserName + '"/> <i class="fa fa-remove"></i></a >';
                    item.Manage = '<a href= "' + searchControllerPath() + '/ChangeUser?userName=' + item.UserName + '"/>Manage</a >';
                })
                  
                return json;
            }
        },
        "columns": [
            { 'data': 'Manage' },
            { 'data': 'UserName' },
            { 'data': 'Email' },
            { 'data': 'LastActiveDateTime' },
            { 'data': 'IsLockedOut' },
            { 'data': 'IsOnline' },
            { 'data': 'DeleteLink' }
        ]

    });
});

/*add from json in table Regions*/
$(document).ready(function () {
    $('#RegionsTable').DataTable({
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
                    item.RegionDescription = '<a href= "' + searchControllerPath() + '/Details?id=' + item.ID + '"/>' + item.RegionDescription + '</a >';
                });
                return json;
            }
        },
        "columns": [
            { 'data': 'RegionDescription' },
            { 'data': 'DeleteLink' }
        ]

    });
});

