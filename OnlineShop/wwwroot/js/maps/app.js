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

    restaurantLocations.forEach(function(restaurant) {
        const marker = new google.maps.Marker({
            position: { lat: restaurant.latitude, lng: restaurant.longitude },
            map: map,
            title: restaurant.name
        });
    });
}