"use strict";

const fs = require('fs');

const use_sample = false;
const part = 2;
const text = fs.readFileSync(use_sample ? "sample.txt" : "input.txt", 'utf8');
const lines = text.split("\r\n")

let instructions = [];

lines.forEach(line => {
    instructions.push(line.split(/,?\s/));
});

let ip = 0;
let reg = { a: 0, b: 0 }
if (part == 2) reg.a = 1;

while (ip < instructions.length) {
    let instruction = instructions[ip];
    let op = instruction[0];
    let p1 = instruction[1];
    let p2 = instruction.length >= 3 ? instruction[2] : null;

    ip++;
    switch (op) {
        case "hlf": reg[p1] /= 2; break;
        case "tpl": reg[p1] *= 3; break;
        case "inc": reg[p1]++; break;
        case "jmp": ip = ip - 1 + (parseInt(p1)); break;
        case "jie": if (reg[p1] % 2 == 0) ip = ip - 1 + parseInt(p2); break;
        case "jio": if (reg[p1] == 1) ip = ip - 1 + parseInt(p2); break;
        default: throw "What?";
    }
}

console.log("a: " + reg["a"]);
console.log("b: " + reg["b"]);