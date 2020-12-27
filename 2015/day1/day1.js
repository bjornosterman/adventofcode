

const fs = require('fs');
const collect = require('collect.js');

var text = fs.readFileSync("day1/input.txt", 'utf8');
var chars = collect(Array.from(text))

var ups = chars.filter(x => x == '(').count();
var downs = chars.filter(x => x == ')').count();
var current_floor = ups - downs;

console.log(current_floor);

// text = "()())";

var floor = 0;
for (let i = 0; i < text.length; i++) {
    const c = text[i];
    if (c == '(') { floor++ } else { floor-- }
    if (floor == -1) {
        console.log("position: " + (i + 1));
        break;
    }

}


// console.log(text);

