"use strict";

const fs = require('fs');

const part = 2;
const presents_per_elf = part == 1 ? 10 : 11;

let houses = []
for (let i = 0; i < 1000000; i++) {
    houses[i] = 0;
}

for (let elf = 1; elf < 1000000; elf++) {
    for (let house = elf; (part == 1 || house <= elf * 50) && house < 1000000; house += elf) {
        houses[house] += presents_per_elf * elf;
    }
}

for (let house = 1; house < 1000000; house++) {
    if (houses[house] > 29000000) {
        console.log(house + ": " + houses[house]);
        break;
    }
}
