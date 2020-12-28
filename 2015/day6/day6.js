const { setupMaster } = require('cluster');
const fs = require('fs');

var text = fs.readFileSync("input.txt", 'utf8');
text = text.replace(/turn\s/g, '');

function getFunc(word) {
    switch (word) {
        case "on": return x => 1;
        case "off": return x => 0;
        case "toggle": return x => 1 - x;
    }
}

var lights = Array(1000000);
// var lights = int[1000000];
for (let i = 0; i < lights.length; i++) {
    lights[i] = 0;
}

text.split('\n').forEach(line => {
    split = line.split(' ');
    func = getFunc(split[0]);
    from = split[1].split(',')
    from_x = parseInt(from[0]);
    from_y = parseInt(from[1]);
    to = split[3].split(',')
    to_x = parseInt(to[0]);
    to_y = parseInt(to[1]);
    for (let x = from_x; x <= to_x; x++) {
        for (let y = from_y; y <= to_y; y++) {
            pos = y*1000+x;
            lights[pos] = func(lights[pos]);
        }
    }
});

console.log("Answer 1: " + lights.filter(x=>x==1).length);
