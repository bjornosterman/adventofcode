const fs = require('fs');

var use_sample = 0;

var text = fs.readFileSync(use_sample ? "sample.txt" : "input.txt", 'utf8');
var lines = text
    .replace(/ would lose /g, ";-")
    .replace(/ would gain /g, ";+")
    .replace(/ happiness units by sitting next to /g, ";")
    .replace(/\./g, "")
    .split("\r\n");

var happies = {};
var people = new Set();

lines.forEach(line => {
    var split = line.split(";");
    var p1 = split[0];
    var p2 = split[2];
    var happy = parseInt(split[1]);
    happies[p1 + "<-->" + p2] = happy;
    people.add(p1);
    people.add(p2);
});

people.forEach(person => {
    happies["self" + "<-->" + person] = 0;
    happies[person + "<-->" + "self"] = 0;
});

people.add("self");

function GenerateSeatings(from, to) {
    if (from.length == 0) {
        CalculateSeatings(to);
    }

    from.forEach(item => {
        GenerateSeatings(from.filter(x => x != item), to.concat([item]));
    });
}

var optimalSeating = null;
var optimalHappiness = 0;

function CalculateSeatings(list) {
    var happiness = 0;
    for (let i = 0; i < list.length; i++) {
        happiness += happies[list[i] + "<-->" + list[(i + 1) % list.length]];
        happiness += happies[list[i] + "<-->" + list[(i + list.length - 1) % list.length]];
    }

    if (happiness > optimalHappiness) {
        optimalHappiness = happiness;
        optimalSeating = list;
    }
    // console.log(list + ": " + happiness);
}

GenerateSeatings(Array.from(people), [])

console.log("Optimal Seating: " + optimalSeating);
console.log("Optimal Happiness: " + optimalHappiness);
PrintSeating(optimalSeating);

function PrintSeating(list) {
    for (let i = 0; i < list.length; i++) {
        console.log("Person: " + list[i])
        console.log(" * " + happies[list[i] + "<-->" + list[(i + 1) % list.length]]);
        console.log(" * " + happies[list[i] + "<-->" + list[(i + list.length - 1) % list.length]]);
    }
}
