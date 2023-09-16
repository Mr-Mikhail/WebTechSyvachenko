"use strict";

function initMap() {
    const myLatLng = {
        lat: 50.43202590942383,
        lng: 30.548511505126953
    };
    const map = new google.maps.Map(document.getElementById("gmp-map"), {
        zoom: 15,
        center: myLatLng,
        fullscreenControl: false,
        zoomControl: true,
        streetViewControl: false
    });
    new google.maps.Marker({
        position: myLatLng,
        map,
        title: "My location"
    });
}