

const fs = require('fs');
const collect = require('collect.js');

var text = fs.readFileSync("input.txt", 'utf8');
var lines = text.split('\n');

var paper_needed = 0
lines.forEach(line => {
    d = line.split("x").map(x => parseInt(x)).sort((a, b) => a - b);
    paper_needed += 3 * d[0] * d[1] + 2 * d[0] * d[2] + 2 * d[1] * d[2];
});

console.log("Number of cubic feet paper needed: " + paper_needed)

var ribbon_needed = 0
lines.forEach(line => {
    d = line.split("x").map(x => parseInt(x)).sort((a, b) => a - b);
    ribbon_needed += d[0] * d[1] * d[2] + 2 * d[0] + 2 * d[1];
});

console.log("Number of feet ribbon needed: " + ribbon_needed)
