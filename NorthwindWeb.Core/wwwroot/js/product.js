
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

//delete product dialog
function deleteIcon(id) {
    var dialog = $("#dialog").dialog();

}

//get parameter value from http query
function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

//select category for dropdown
function selectDropdownCategory() {

    $('#dropdownCategory option[value=' + $('#dropdownCategory option:contains("' + getParameterByName("category") + '")').val() + ']').attr("selected", "selected");
}

//get initial search value
function getInitialSearchValue() {
    var searchValue = getParameterByName("search");
    if (searchValue == null || searchValue == "" || searchValue == undefined) {
        return "";
    }
    return searchValue;
}

//change query link
function updateQueryStringParameter(uri, key, value) {
    var re = new RegExp("([?&])" + key + "=.*?(&|$)", "i");
    var separator = uri.indexOf('?') !== -1 ? "&" : "?";
    if (uri.match(re)) {
        if (value == null || value == "" || value == undefined) {
            return uri.replace(re, "");
        }
        else {
            return uri.replace(re, '$1' + key + "=" + value + '$2');
        }
    }
    else {
        if (value == null || value == "" || value == undefined) {
            return uri;
        }
        else {
            return uri + separator + key + "=" + value;
        }
    }
}

//change product create link
function changeProductCreateLink(search) {
    if (document.getElementById('createProductButton') != null) {
        $("#createProductButton").attr("href", updateQueryStringParameter($("#createProductButton").attr("href"), "search", search));
    }
}

$(document).ready(function () {

    /*autoselect category*/
    selectDropdownCategory();

    /*datatable handler with server side implementation for product*/
    var productTable = $('#Product').DataTable({
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
                    var category = getParameterByName("category");
                    var search = productTable.search();
                    if (category == null || category == "" || category == undefined) {
                        if (search == null || search == "" || search == undefined) {
                            item.ProductName = '<a href= "' + searchControllerPath() + '/Details?id=' + item.ID + '"/>' + item.ProductName + '</a >';
                            item.DeleteLink = '<a href= "' + searchControllerPath() + '/Delete?id=' + item.ID + '"/> <i class="fa fa-remove"></i></a >';
                        }
                        else {
                            item.ProductName = '<a href= "' + searchControllerPath() + '/Details?id=' + item.ID + '&search=' + search + '"/>' + item.ProductName + '</a >';
                            item.DeleteLink = '<a href= "' + searchControllerPath() + '/Delete?id=' + item.ID + '&search=' + search + '"/> <i class="fa fa-remove"></i></a >';
                        }

                    }
                    else {
                        if (search == null || search == "" || search == undefined) {
                            item.ProductName = '<a href= "' + searchControllerPath() + '/Details?id=' + item.ID + '&category=' + category + '"/>' + item.ProductName + '</a >';
                            item.DeleteLink = '<a href= "' + searchControllerPath() + '/Delete?id=' + item.ID + '&category=' + getParameterByName("category") + '"/> <i class="fa fa-remove"></i></a >';
                        }
                        else {
                            item.ProductName = '<a href= "' + searchControllerPath() + '/Details?id=' + item.ID + '&category=' + category + '&search=' + search + '"/>' + item.ProductName + '</a >';
                            item.DeleteLink = '<a href= "' + searchControllerPath() + '/Delete?id=' + item.ID + '&category=' + getParameterByName("category") + '&search=' + search + '"/> <i class="fa fa-remove"></i></a >';
                        }
                    }
                });
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
            {
                'data': 'DeleteLink',
                "orderable": false,
            }
        ]
    });
    //begin with search...
    productTable.search(getInitialSearchValue()).ajax.reload();
    //on search change link of create button
    productTable.on("search.dt", function () {
        changeProductCreateLink(productTable.search());
    });
    //set search on page load
    changeProductCreateLink(productTable.search());



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
                    item.deleteLink = '<a href= "' + searchControllerPath() + '/Delete?id=' + item.id + '"/> <i class="fa fa-remove"></i></a >';
                    item.lastName = '<a href= "' + searchControllerPath() + '/Details?id=' + item.id + '"/>' + item.lastName + '</a >';
                });
                return json.data;
            }
        },
        "columns": [
            { 'data': 'lastName' },
            { 'data': 'firstName' },
            { 'data': 'title' },
            { 'data': 'city' },
            { 'data': 'country' },
            { 'data': 'homePhone' },
            {
                'data': 'deleteLink',
                "orderable": false,
            }
        ]

    });


    /*datatable handler with server side implementation for customer*/
    $('#CustomersTable').DataTable({
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
                    console.log(item);
                    item.deleteLink = '<a href= "' + searchControllerPath() + '/Delete?id=' + item.id + '"/> <i class="fa fa-remove"></i></a >';
                    item.companyName = '<a href= "' + searchControllerPath() + '/Details?id=' + item.id + '"/>' + item.companyName + '</a >';
                });
                return json.data;
            }
        },
        "columns": [
            { 'data': 'companyName' },
            { 'data': 'contactName' },
            { 'data': 'contactTitle' },
            { 'data': 'city' },
            { 'data': 'country' },
            { 'data': 'phone' },
            {
                'data': 'deleteLink',
                "orderable": false,
            }
        ]

    });

    /*datatable handler with server side implementation for order*/
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
            {
                'data': 'DeleteLink',
                "orderable": false,
            }
        ]

    });

    $('#Suppliers').DataTable({
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
                    item.DeleteLink = '<a href= "' + searchControllerPath() + '/Delete?id=' + item.SupplierID + '"/> <i class="fa fa-remove"></i></a >';
                    item.CompanyName = '<a href= "' + searchControllerPath() + '/Details?id=' + item.SupplierID + '"/>' + item.CompanyName + '</a >';
                });
                return json.data;
            }
        },
        "columns": [
            { 'data': 'CompanyName' },
            { 'data': 'ContactName' },
            { 'data': 'ContactTitle' },
            { 'data': 'Address' },
            { 'data': 'City' },
            { 'data': 'Country' },
            { 'data': 'Phone' },
            {
                'data': 'DeleteLink',
                "orderable": false,
            }
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
            {
                'data': 'DeleteLink',
                "orderable": false,
            }
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
            {
                'data': 'DeleteLink',
                "orderable": false,
            }
        ]

    });
    /*add from json in table User*/
    $('#UsersTable').DataTable({
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
                    if (item.IsLockedOut) { item.IsLockedOut = "Yes"; }
                    else { item.IsLockedOut = "No"; }
                    if (item.IsOnline) { item.IsOnline = "Yes"; }
                    else { item.IsOnline = "No"; }
                    var date = Date.parse(item.LastActiveString);

                    item.LastActiveDate = new Date(date);
                    item.DeleteLink = '<a href= "' + searchControllerPath() + '/DeleteUser?userName=' + item.UserName + '"/> <i class="fa fa-remove"></i></a >';
                    item.Manage = '<a href= "' + searchControllerPath() + '/ChangeUser?userName=' + item.UserName + '"/><i>Manage</i></a >';
                    item.Image = '<img src="/images/' + item.UserName + '.jpg" onerror="this.src=' + "'/images/default.png'" + '"  style= "width:60px;height:45px;" >';
                });

                return json.data;
            }
        },
        "columns": [
            { 'data': 'Manage' },
            { 'data': 'Image' },
            { 'data': 'UserName' },
            { 'data': 'Email' },
            { 'data': 'LastActiveDate' },
            { 'data': 'IsOnline' },
            {
                'data': 'DeleteLink',
                "orderable": false,
            }
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
            {
                'data': 'DeleteLink',
                "orderable": false,
            }
        ]

    });
    /*add from json in table Role*/
    $('#RolesTable').DataTable({
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
            "url": searchControllerPath() + "/JsonTableRolesFill",
            "dataSrc": function (json) {
                //Make your callback here.
                $.each(json.data, function (index, item) {
                    item.Delete = '<a href= "' + searchControllerPath() + '/RoleDelete?roleName=' + item.Name + '" onclick="if (!confirm(' + "'Doriti sa stergeti ?'" + ')) return false;"/> <i class="fa fa-remove"></i></a >';
                    item.Membership = '<a href= "' + searchControllerPath() + '/RoleMembership?roleName=' + item.Name + '&name=' + item.Name + '"/>Membrii</a >';
                });

                return json.data;
            }
        },
        "columns": [
            {
                'data': 'Delete',
                "orderable": false,
            },
            { 'data': 'Name' },
            { 'data': 'Membership' }
        ]

    });

    /*add from json in table Role*/
    $('#UsersInRole').DataTable({
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
            "url": searchControllerPath() + "/JsonTableMembershipFill",
            "dataSrc": function (json) {
                //Make your callback here.
                $.each(json.data, function (index, item) {
                    item.Delete = '<a href= "' + searchControllerPath() + '/DeleteFromRole?userName=' + item.UserName + '&roleName=' + json.roleName + '" onclick="if (!confirm(' + "'Doriti sa stergeti ?'" + ')) return false;"/> <i class="fa fa-remove"></i></a >';

                });

                return json.data;
            }
        },
        "columns": [
            { 'data': 'Delete' },
            { 'data': 'UserName' }

        ]

    });
    $('#ErrorsTable').DataTable({
        "responsive": true,
        "autoWidth": false,
        "order": [["0", "desc"]],
        "columnDefs": [
            { responsivePriority: 1, targets: 0 },
            { responsivePriority: 2, targets: -1 }
        ]
    });
});
