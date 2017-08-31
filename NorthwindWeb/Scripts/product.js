
﻿/*find correct pathc for search*/
function searchControllerPath() {
    var path = window.location.href;
    var a = path.split("/");
    if (path.indexOf("http://") + 1) {
        return a[0] + '//' + a[2] + '/' + (a[3].split("?"))[0];
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

    $('#EmployeesTable').DataTable({
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
                    item.LastName = '<a href= "' + searchControllerPath() + '/Details?id=' + item.ID + '"/>' + item.LastName + '</a >';
                });
                return json.data;
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


    /*add from json in table Customers*/
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

    /*add from json in table Orders*/
    $('#OrdersTable').DataTable({
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
                    item.ShippedDate = item.ShippedDate.replace("12:00AM", "");
                    item.DeleteLink = '<a href= "' + searchControllerPath() + '/Delete?id=' + item.ID + '"/> <i class="fa fa-remove"></i></a >';
                    item.ID = '<a href= "' + searchControllerPath() + '/Details?id=' + item.ID + '"/>' + item.ID + '</a >';
                });
                return json.data;
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

    /*add from json in table Shippers*/
    
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
    

    /*add from json in table Categories*/
    
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
        /*add from json in table User*/
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
                        var date = Date.parse(item.LastActiveString);

                        item.LastActiveDate = new Date(date);
                        item.DeleteLink = '<a href= "' + searchControllerPath() + '/DeleteUser?userName=' + item.UserName + '"/> <i class="fa fa-remove"></i></a >';
                        item.Manage = '<a href= "' + searchControllerPath() + '/ChangeUser?userName=' + item.UserName + '"/>Manage</a >';
                    })

                    return json;
                }
            },
            "columns": [
                { 'data': 'Manage' },
                { 'data': 'UserName' },
                { 'data': 'Email' },
                { 'data': 'LastActiveDate' },
                { 'data': 'IsLockedOut' },
                { 'data': 'IsOnline' },
                { 'data': 'DeleteLink' }
            ]

        });

   

    /*add from json in table Regions*/

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
     /*add from json in table Role*/
        $('#RolesTable').DataTable({
            //"processing": true,
            //"serverSide": true,
            "responsive": true,
            "autoWidth": false,
            "columnDefs": [
                { responsivePriority: 1, targets: 0 },
                { responsivePriority: 2, targets: -1 }
            ],
            "ajax": {
                "type": "GET",
                "url": searchControllerPath() + "/JsonTableRolesFill",
                "dataSrc": function (json) {
                    //Make your callback here.
                    $.each(json, function (index, item) {
                        item.Delete = '<a onclick="alert('+"'Nu aveti dreptul de a efectua aceasta operatie!'"+')"/> <i class="fa fa-remove"></i></a >';
                        item.Membership = '<a href= "' + searchControllerPath() + '/RoleMembership?roleName ='+ item.Name+'&name='+item.Name+'"/>Membrii</a >';
                    })
                   
                    return json;
                }
            },
            "columns": [
                { 'data': 'Delete' },
                { 'data': 'Name' },
                { 'data': 'Membership' }
            ]

        });
        $(".alerte").click(function () {
            alert("Nu aveti dreptul de a efectua aceasta operatie!");
        })
        function testalert() {
            alert("Nu aveti dreptul de a efectua aceasta operatie!");
        }
});
