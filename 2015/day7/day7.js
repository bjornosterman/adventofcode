const { setupMaster } = require('cluster');
const fs = require('fs');
const { parse } = require('path');

var use_sample = 0;
var part = 2;

var text = fs.readFileSync(use_sample ? "sample.txt" : "input.txt", 'utf8');
var lines = text.split('\r\n');

circuits = {}
var depth = 0;

cache = {}

function Circuit(line) {

    this.and = () => this.getvalue(this.a) & this.getvalue(this.b);
    this.or = () => this.getvalue(this.a) | this.getvalue(this.b);
    this.rshift = () => this.getvalue(this.a) >> this.getvalue(this.b);
    this.lshift = () => this.getvalue(this.a) << this.getvalue(this.b);
    this.not = () => 65535 - this.getvalue(this.v);
    this.value = () => this.getvalue(this.v);


    this.value_or_cache = () => {
        if (isNaN(this.cached)) {
            this.cached = this.func();
        }
        return this.cached;
    }


    this.getvalue = o => {
        // console.log(o);
        // if (o == 'i') {
        //     debugger;
        // }
        i = parseInt(o);
        if (isNaN(i)) {
            // depth++;
            // console.log('>'.repeat(depth));
            i = circuits[o].value_or_cache();
            // depth--;
        }
        return i;
    }

    parts = line.split(' ');
    if (parts[0] == 'NOT') { this.v = parts[1]; this.func = this.not; }
    else if (parts.length == 1) { this.v = parts[0]; this.func = this.value; }
    else {
        this.a = parts[0];
        this.b = parts[2];
        if (parts[1] == 'AND') { this.func = this.and; }
        else if (parts[1] == 'OR') { this.func = this.or; }
        else if (parts[1] == 'RSHIFT') { this.func = this.rshift; }
        else if (parts[1] == 'LSHIFT') { this.func = this.lshift; }
    }
}

lines.forEach(line => {
    split = line.split(" -> ");
    circuits[split[1]] = new Circuit(split[0]);
});

if (use_sample) {
    console.log("Answer d: " + circuits["d"].func());
    console.log("Answer e: " + circuits["e"].func());
    console.log("Answer f: " + circuits["f"].func());
    console.log("Answer g: " + circuits["g"].func());
    console.log("Answer h: " + circuits["h"].func());
    console.log("Answer i: " + circuits["i"].func());
    console.log("Answer x: " + circuits["x"].func());
    console.log("Answer y: " + circuits["y"].func());
}
else {
    if (part == 2) {
        circuits["b"].v = 16076;
    }
    console.log("Answer part " + part + ": " + circuits["a"].func());

}

