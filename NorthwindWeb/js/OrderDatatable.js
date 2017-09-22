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
    $('#Home').DataTable({
        "processing": true,
        "serverSide": true,
        "responsive": true,
        "autoWidth": false,
        "bFilter": false,
        "bLengthChange": false,
        "aLengthMenu": false,
        "paging": true,
        "columnDefs": [
            { responsivePriority: 1, targets: 0 },
            { responsivePriority: 2, targets: -1 }
        ],
        "ajax": {
            "type": "GET",
            "url": searchControllerPath() + "/JsonTableFill",
            "dataSrc": function (json) {
                //Make your callback here.
                //$.each(json.data, function (index, item) {
                //    item.Delete = '<a href= "' + searchControllerPath() + '/DeleteFromRole?userName=' + item.UserName + '&roleName=' + json.roleName + '" onclick="if (!confirm(' + "'Doriti sa stergeti ?'" + ')) return false;"/> <i class="fa fa-remove"></i></a >';

                //});

                return json.data;
            },
            "pageLength": function (json) {
                return json.pageLength;
            },
            "recordsFiltered": function (json) {
                return json.recordsFiltered;
            },
            "recordsTotal": function (json) {
                return json.recordsTotal;
            }

        },
        "columns": [
            { 'data': 'OrderID' },
            { 'data': 'OrderDate' },
            { 'data': 'CompanyName' },
            { 'data': 'ShipperName' }

        ]

    });
})