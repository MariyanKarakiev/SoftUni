function solve(fruit, grams, pricePerKg) {
    let kg = grams/1000;
    let priceTotal = pricePerKg * kg;

    console.log(`I need $${priceTotal.toFixed(2)} to buy ${kg.toFixed(2)} kilograms ${fruit}.`);
}

solve('orange', 2500, 1.80);