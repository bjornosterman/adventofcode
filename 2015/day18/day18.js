const { setupMaster } = require('cluster');
const fs = require('fs');

var use_sample = 0;
var part = 2;
var steps = use_sample ? 4 : 100;
var text = fs.readFileSync(use_sample ? "sample.txt" : "input.txt", 'utf8');
var lines = text.split("\r\n")

var width = lines.length;

var lights = Array(width * width);

for (let y = 0; y < width; y++) {
    var line = lines[y];
    for (let x = 0; x < line.length; x++) {
        lights[y * width + x] = line[x] == '#' ? true : false;
    }
}

for (let i = 0; i < steps; i++) {

    var lights2 = Array(width * width);
    var lights2 = Array(width * width);
    for (let y = 0; y < width; y++) {
        for (let x = 0; x < width; x++) {
            p = y * width + x;
            n = 0;
            n += x < width - 1 && lights[p + 1];
            n += x < width - 1 && y < width - 1 && lights[p + 1 + width];
            n += y < width - 1 && lights[p + width];
            n += x > 0 && y < width - 1 && lights[p - 1 + width];
            n += x > 0 && lights[p - 1];
            n += x > 0 && y > 0 && lights[p - 1 - width];
            n += y > 0 && lights[p - width];
            n += x < width - 1 && y > 0 && lights[p - width + 1];
            if (lights[p]) {
                lights2[p] = n == 2 || n == 3;
            }
            else {
                lights2[p] = n == 3;
            }
        }
    }
    if (part == 2) {
        lights2[0] = true;
        lights2[width - 1] = true;
        lights2[width * width - 1] = true;
        lights2[width * width - width] = true;
    }
    lights = lights2;

    // console.log("=== STEP " + (i + 1) + " ===");
    // for (let y = 0; y < width; y++) {
    //     line = "";
    //     for (let x = 0; x < width; x++) {
    //         line += lights[y * width + x] ? '#' : '.';
    //     }
    //     console.log(line);
    // }
    // console.log();

}

var lights_count = lights.map(x => x ? 1 : 0).reduce((a, b) => a + b);
console.log("Count: " + lights_count);
