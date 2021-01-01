const fs = require('fs');

var use_sample = 0;

var text = fs.readFileSync(use_sample ? "sample.txt" : "input.txt", 'utf8');
var lines = text.split("\r\n");

var maxtime = use_sample ? 1000 : 2503;

function Deer(name, speed, flytime, resttime) {
    this.Name = name;
    this.Speed = speed;
    this.Flytime = flytime;
    this.Resttime = resttime;
    this.Time = 0;
    this.Points = 0;
    this.Distance = 0;
    this.Tick = function () {
        if (this.Time % (this.Flytime + this.Resttime) < this.Flytime) {
            this.Distance += this.Speed;
        }
        this.Time++;
    }
}

var deers = [];

lines.forEach(line => {
    var match = line.match(/(?<name>\w+) can fly (?<speed>\d+) km\/s for (?<flytime>\d+) seconds, but then must rest for (?<resttime>\d+) seconds\./);
    var speed = parseInt(match.groups.speed);
    var flytime = parseInt(match.groups.flytime);
    var name = match.groups.name;
    var resttime = parseInt(match.groups.resttime);

    deers.push(new Deer(name, speed, flytime, resttime));
});

for (let i = 0; i < maxtime; i++) {
    deers.forEach(deer => {
        deer.Tick();
    });
    var bestDistance = deers.map(x => x.Distance).reduce((a, b) => a < b ? b : a);
    // var bestDistance = Math.max.apply(deers.map(x => x.Distance));
    deers
        .filter(x => x.Distance == bestDistance)
        .forEach(deer => {
            deer.Points++;
        });
}

deers.forEach(deer => {
    console.log(deer.Name + ": " + deer.Points);
});
