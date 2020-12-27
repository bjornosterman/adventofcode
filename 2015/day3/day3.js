

const fs = require('fs');
const collect = require('collect.js');

var text = fs.readFileSync("input.txt", 'utf8');

function mov(c) {
    switch (c) {
        case '>': return 1;
        case '<': return -1;
        case '^': return 1000;
        case 'v': return -1000;
        default:
            throw "Huh?"
    }
};

var houses = new Set();
var pos = 0;
houses.add(pos);
text.split("").forEach(c => {
    pos += mov(c);
    houses.add(pos);
});

console.log("Number houses that gets presents: " + houses.size);


houses = new Set();
pos1 = 0;
pos2 = 0;
houses.add(pos1);

for (let i = 0; i < text.length; i += 2) {
    pos1 += mov(text[i]);
    houses.add(pos1);
    if (i + 1 < text.length) {
        pos2 += mov(text[i + 1]);
        houses.add(pos2);
    }
}

console.log("Number houses that gets presents next year: " + houses.size);
