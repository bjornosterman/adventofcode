"use strict";

function GetIterations(row, col) {
    return (row + col - 1) * (row + col - 1 - 1) / 2 + col;
}

let row = 2947;
let col = 3029;
let iterations = GetIterations(row, col);

let current = 20151125;
while (--iterations > 0) {
    current = current * 252533 % 33554393;
}
console.log("Code for position row: " + row + " and col: " + col + " is " + current);