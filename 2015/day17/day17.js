const fs = require('fs');

var use_sample = 0;

var containers = use_sample
    ? [20, 15, 10, 5, 5]
    : [33, 14, 18, 20, 45, 35, 16, 35, 1, 13, 18, 13, 50, 44, 48, 6, 24, 41, 30, 42];

containers = containers.sort(x => x);

var foundCombos = new Set();
var foundCount = 0;
var foundByCount = {}

function search(using, left, liters) {
    if (liters == 0) {
        if (!(using.length in foundByCount)) {
            foundByCount[using.length] = 0;
        }
        foundByCount[using.length]++;
        foundCount++;
        foundCombos.add(using.reduce((a, b) => a + "," + b));
    }
    if (liters <= 0) {
        return;
    }
    for (let i = 0; i < left.length; i++) {
        const use = left[i];
        search(using.concat(use), left.slice(i + 1), liters - use);
    }
}

search([], containers, use_sample ? 25 : 150);
console.log("Number of unique combos: " + foundCombos.size);
console.log("Number of combos: " + foundCount);
for (item in foundByCount) {
    console.log(item + ": " + foundByCount[item]);
}