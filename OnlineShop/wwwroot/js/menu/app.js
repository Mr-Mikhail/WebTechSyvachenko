let selectedDishes = [];

function addToOrder(dish) {
    // Add logic to handle adding the dish with the specified ID to the order
    let object = JSON.parse(dish);
    selectedDishes.push(object);
    console.log("Added dish with ID: " + object.Id + " to order.");
}