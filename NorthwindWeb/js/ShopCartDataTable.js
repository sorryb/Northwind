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
function CreateShopCartDataTable(tableId) {
    $(document).ready(function () {
        $('#' + tableId + '').DataTable({
            "processing": true,
            "serverSide": true,
            "responsive": true,
            "autoWidth": false,
            "paging": false,
            "searching": false,
            "info": false,
            "columnDefs": [
                { responsivePriority: 1, targets: 0 },
                { responsivePriority: 2, targets: -1 }
            ],
            "ajax": {
                "type": "GET",
                "url": searchControllerPath() + "/JsonTableFill",

                "data": {
                    "json": localStorage.getItem("cart"),
                },
                "dataSrc": function (json) {
                    //Make your callback here.
                    $.each(json.data, function (index, item) {
                        item.ProductName = '<img src="/images/' + item.Category + '/' + item.ID + '.jpg" class="shopCartImage"/> <span>' + item.ProductName + '</span>';
                        item.Image = '<img src="~/images/' + item.Category + '/' + item.ID + '.jpg"/>';
                        item.Remove = '<a href="javascript:" onclick="RemoveFromCart(' + item.ID + ')" /> <i class="fa fa-remove"></i></a >';
                        item.TotalPrice = item.UnitPrice * item.Quantity;
                        item.Quantity = '<input type="number" min="1" value=' + item.Quantity + ' onchange="ChangeQuantity(' + item.ID + ',value)"/> ';
                    })
                    return json.data;
                },
            },

            "columns": [
                { 'data': 'ProductName' },
                { 'data': 'Quantity' },
                { 'data': 'UnitPrice' },
                { 'data': 'TotalPrice' },
                {
                    'data': 'Remove',
                    'orderable':false,
                },
            ]

        });

    });
}
CreateShopCartDataTable("ShopCartTable");