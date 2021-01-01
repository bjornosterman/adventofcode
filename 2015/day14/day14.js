const fs = require('fs');

var use_sample = 0;

var text = fs.readFileSync(use_sample ? "sample.txt" : "input.txt", 'utf8');
var lines = text.split("\r\n");

var maxtime = use_sample ? 1000 : 2503;


lines.forEach(line => {
    var match = line.match(/(?<name>\w+) can fly (?<speed>\d+) km\/s for (?<flytime>\d+) seconds, but then must rest for (?<resttime>\d+) seconds\./);
    var speed = parseInt(match.groups.speed);
    var flytime = parseInt(match.groups.flytime);
    var name = match.groups.name;
    var resttime = parseInt(match.groups.resttime);

    distance = 0;
    time = 0;
    while (time < maxtime) {
        this_flytime = Math.min(flytime, (maxtime - time));
        time += this_flytime;
        distance += this_flytime * speed;
        time += resttime;
    }
    console.log(name + " flew " + distance + " km");
});

