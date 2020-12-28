const fs = require('fs');

var text = fs.readFileSync("input.txt", 'utf8');

function test2(line) {
    for (let i = 1; i < line.length; i++) {
        if (line[i] == line[i - 1]) return true;
    }
    return false;
}

function test1(line) {
    return line.split("").filter(x => "aeiou".indexOf(x) != -1).length >= 3;
}

function test3(line) {
    return line.indexOf('ab') < 0 && line.indexOf('cd') < 0 && line.indexOf('pq') < 0 && line.indexOf('xy') < 0;
}

nices = text.split('\n')
    .filter(test1)
    .filter(test2)
    .filter(test3);

console.log("Answer 1: " + nices.length);

nices2 = text.split('\n')
.filter(x=>/(\w\w).*\1/.test(x))
.filter(x=>/(\w)\w\1/.test(x));

console.log("Answer 2: " + nices2.length);
