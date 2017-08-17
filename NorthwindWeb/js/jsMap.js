function myMap() {
    var mapCanvas = document.getElementById("googleMap");
    var myCenter = new google.maps.LatLng(44.480104, 26.108327199999962);
    var mapOptions = { center: myCenter, zoom: 5, mapTypeId: google.maps.MapTypeId.HYBRID };
    var map = new google.maps.Map(mapCanvas, mapOptions);
    map.setTilt(45);
    var marker = new google.maps.Marker({
        position: myCenter,
        animation: google.maps.Animation.BOUNCE
    });
    marker.setMap(map);
}