$(function () {
    $.ajax({
        url: 'Graph1',
        dataType: 'json',
        cache: false,
        success: function (data) {
            Morris.Area({
                element: 'morris-area-chart',
                data: data,
                xkey: ['Year'],
                ykeys: ['Sales'],
                labels: ['Sales'],
                pointSize: 2,
                hideHover: 'auto',
                resize: true
            })

        }
    });
    $.ajax({
        url: 'Graph3',
        dataType: 'json',
        cache: false,
        success: function (data) {
            Morris.Donut({
                element: 'morris-donut-chart',
                data:data,
                resize: true
            })
        }

    })
    $.ajax({
        url: 'Graph2',
        dataType: 'json',
        cache: false,
        success: function (data) {
            Morris.Bar({
                element: 'morris-bar-chart',
                data: data,
                xkey: ['Year'],
                ykeys: ['a', 'b', 'c'],
                labels: ['Aprilie', 'August', 'Decembrie'],
                hideHover: 'auto',
                resize: true
            })
        }

    })
});
