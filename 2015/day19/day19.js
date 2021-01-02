"use strict";

const fs = require('fs');

const use_sample = false;
const part = 1;
const text = fs.readFileSync(use_sample ? "sample.txt" : "input.txt", 'utf8');
const lines = text.split("\r\n")

let searchline = "";

const substitutions = {}
const backsubstitutions = {}

lines.forEach(line => {
    if (line == "") {
        // do nothing
    } else if (!line.match(/=>/)) {
        searchline = line;
    } else {
        const split = line.split(" => ");
        if (!(split[0] in substitutions)) substitutions[split[0]] = [];
        substitutions[split[0]].push(split[1]);
        backsubstitutions[split[1]] = split[0];
    }
});

let molecules = new Set();

let finds = 0;

for (const key in substitutions) {
    let index = 0;
    while ((index = searchline.indexOf(key, index + 1)) != -1) {
        for (const sub of substitutions[key]) {
            finds++;
            molecules.add(searchline.substr(0, index) + sub + searchline.substr(index + key.length));
        }
    }
}

let reduction = searchline;
let number_of_reductions = 0;
var sorted_keys = Object.keys(backsubstitutions).sort(x => 100 - x.length);
while (reduction != 'e') {
    for (const key of sorted_keys) {
        const new_part = backsubstitutions[key];
        const new_string = reduction.replace(key, new_part);
        if (new_string != reduction) {
            number_of_reductions++;
            reduction = new_string;
            break;
        }
    }
}

console.log("Number of replacements: " + finds);
console.log("Number of unique molecules: " + molecules.size);
console.log("Number of reductions: " +number_of_reductions);
