const fs = require('fs');

var use_sample = 0;

var text = fs.readFileSync(use_sample ? "sample.txt" : "input.txt", 'utf8');
var lines = text.split("\r\n");

var sue = 0;

var corrects = {
    children: 3,
    cats: 7,
    samoyeds: 2,
    pomeranians: 3,
    akitas: 0,
    vizslas: 0,
    goldfish: 5,
    trees: 3,
    cars: 2,
    perfumes: 1
};

// Part 1
lines.forEach(line => {
    var matches = line.matchAll(/(?<key>[a-z]+): (?<value>\d+)/g);
    var score = 0;
    for (const match of matches) {
        var key = match.groups.key;
        var value = parseInt(match.groups.value);
        score += corrects[key] == value ? 1 : 0;
    }

    if (score == 3) {
        console.log(line);
    }
});

// Part 2
lines.forEach(line => {
    var matches = line.matchAll(/(?<key>[a-z]+): (?<value>\d+)/g);
    var score = 0;
    for (const match of matches) {
        var key = match.groups.key;
        var value = parseInt(match.groups.value);
        switch (key) {
            case "cats":
            case "tree":
                score += corrects[key] < value ? 1 : 0;
                break;
            case "pomeranians":
            case "goldfish":
                score += corrects[key] > value ? 1 : 0;
                break;
            default:
                score += corrects[key] == value ? 1 : 0;
                break;
        }
    }

    if (score == 3) {
        console.log(line);
    }
});
