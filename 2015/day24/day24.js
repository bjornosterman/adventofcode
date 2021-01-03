"use strict";

const fs = require('fs');

const use_sample = false;
const part = 2;
const text = fs.readFileSync(use_sample ? "sample.txt" : "input.txt", 'utf8');
const lines = text.split("\r\n")

let allpackets = [];

lines.reverse().forEach(line => {
    allpackets.push(parseInt(line));
});

let totalWeight = allpackets.reduce((a, b) => a + b);
let groupWeight = totalWeight / (part == 2 ? 4 : 3);

let minPacketsUsed = 5;
let minQe = -1;

function FindGroup1(packetsUsed, packetsLeft, weight) {
    if (weight < 0) return;
    if (packetsUsed.length > minPacketsUsed) return;
    if (weight == 0) {
        if (AnyGroup(packetsUsed, allpackets.filter(x => !(x in packetsUsed)), groupWeight, part == 2 ? 2 : 1)) {
            console.log("Packets used: " + packetsUsed);
            let qe = packetsUsed.reduce((a, b) => a * b);
            if (packetsUsed.length < minPacketsUsed || minQe == -1 || qe < minQe) {
                minPacketsUsed = packetsUsed.length;
                minQe = qe;

                console.log("Min Packets Used: " + minPacketsUsed);
                console.log("Min QE: " + qe);
            }
        }
    }
    if (packetsUsed.length < minPacketsUsed) {
        for (let i = 0; i < packetsLeft.length; i++) {
            FindGroup1(packetsUsed.concat(packetsLeft[i]), packetsLeft.slice(i + 1), weight - packetsLeft[i]);
        }
    }
}

function AnyGroup(packetsUsed, packetsLeft, weight, groupsToFind) {
    if (weight < 0) return false;
    if (weight == 0) return true;
    for (let i = 0; i < packetsLeft.length; i++) {
        let newPacketsUsed = packetsUsed.concat(packetsLeft[i]);
        if (AnyGroup(newPacketsUsed, packetsLeft.slice(i + 1), weight - packetsLeft[i], groupsToFind)) {
            if (groupsToFind == 1) {
                return true;
            }
            if (AnyGroup(newPacketsUsed, packetsLeft.filter(x => !(x in newPacketsUsed)), groupWeight), groupsToFind - 1) {
                return true;
            }
        }
    }
    return false;
}

FindGroup1([], allpackets, groupWeight);

console.log("minPacketsUsed: " + minPacketsUsed);
console.log("minQe: " + minQe);