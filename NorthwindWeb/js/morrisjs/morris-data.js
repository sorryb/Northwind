$(function () {
   $.ajax({
       url: 'Graph1',
        dataType: 'json',
        cache: false,
        success: function (data) {
            console.log(data),

            Morris.Area({
                element: 'morris-area-chart',
                data: data,
                xkey: ['Year'],
                ykeys: ['Sales'],
                labels: ['Sales'],
                pointSize: 5,
                hideHover: 'auto',
                resize: true
            })

        }
    });
    //Morris.Donut({
    //    element: 'morris-donut-chart',
    //    data: [{
    //        label: "Download Sales",
    //        value: 12
    //    }, {
    //        label: "In-Store Sales",
    //        value: 30
    //    }, {
    //        label: "Mail-Order Sales",
    //        value: 20
    //    }],
    //    resize: true
    //});

    //Morris.Bar({
    //    element: 'morris-bar-chart',
    //    data: [{
    //        y: '2006',
    //        a: 100,
    //        b: 90
    //    }, {
    //        y: '2007',
    //        a: 75,
    //        b: 65
    //    }, {
    //        y: '2008',
    //        a: 50,
    //        b: 40
    //    }, {
    //        y: '2009',
    //        a: 75,
    //        b: 65
    //    }, {
    //        y: '2010',
    //        a: 50,
    //        b: 40
    //    }, {
    //        y: '2011',
    //        a: 75,
    //        b: 65
    //    }, {
    //        y: '2012',
    //        a: 100,
    //        b: 90
    //    }],
    //    xkey: 'y',
    //    ykeys: ['a', 'b'],
    //    labels: ['Series A', 'Series B'],
    //    hideHover: 'auto',
    //    resize: true
    //});

});
