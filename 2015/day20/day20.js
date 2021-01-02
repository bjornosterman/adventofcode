"use strict";

const fs = require('fs');

let house = 1;
let max_so_far = 0;
while (true) {
    let sum = house;
    let max_search = house / 2 + 1;
    for (let i = 1; i <= max_search; i++) {
        if (house % i == 0) {
            sum += i;
        }
    }
    if (sum > max_so_far) {
        max_so_far = sum;
    }
    if (sum >= 2900000) {
        console.log(house + ": " + sum);
        break;
    }
    if (house % 10000 == 0) {
        console.log(house + ": " + max_so_far);
    }
    house++;
}
