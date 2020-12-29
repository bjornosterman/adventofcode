const { match } = require('assert');
const { setupMaster } = require('cluster');
const fs = require('fs');
const { parse } = require('path');

var use_sample = 0;

var text = fs.readFileSync(use_sample ? "sample.txt" : "input.txt", 'utf8');
var lines = text.split('\r\n');

var towns = new Set();
var roads = {};

lines.forEach(line => {
    var match = line.match(/(?<from>\w+) to (?<to>\w+) = (?<cost>\d+)/);
    var from = match.groups.from;
    var to = match.groups.to;
    var cost = parseInt(match.groups.cost);
    towns.add(from);
    towns.add(to);
    roads[from + "-->" + to] = cost;
    roads[to + "-->" + from] = cost;
});

function walkShortest(from, list) {
    if (list.length == 0) return 0;
    var min = undefined;
    list.forEach(item => {
        var cost = roads[from + "-->" + item];
        if (cost !== undefined) {
            distance = walkShortest(item, list.filter(x => x != item));
            if (distance !== undefined) {
                var new_distance = distance + cost;
                if (min === undefined || new_distance < min) {
                    min = new_distance;
                }
            }
        }
    });
    return min;
}

function walkLongest(from, list) {
    if (list.length == 0) return 0;
    var max = undefined;
    list.forEach(item => {
        var cost = roads[from + "-->" + item];
        if (cost !== undefined) {
            distance = walkLongest(item, list.filter(x => x != item));
            if (distance !== undefined) {
                var new_distance = distance + cost;
                if (max === undefined || new_distance > max) {
                    max = new_distance;
                }
            }
        }
    });
    return max;
}

Array.from(towns).forEach(town => {
    console.log(town + ": " + walkShortest(town, Array.from(towns).filter(x => x != town)));
});

Array.from(towns).forEach(town => {
    console.log(town + ": " + walkLongest(town, Array.from(towns).filter(x => x != town)));
});

