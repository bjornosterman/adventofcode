const { setupMaster } = require('cluster');
const fs = require('fs');
const { parse } = require('path');

var use_sample = 0;

var text = fs.readFileSync(use_sample ? "sample.txt" : "input.txt", 'utf8');
var lines = text.split('\r\n');

encoded_chars = 0;
code_chars = 0;
mem_chars = 0;

lines.forEach(line => {
    code_chars += line.length;
    mem_line = line
        .substr(1, line.length - 2)
        .replace(/\\\\/g, ".")
        .replace(/\\\"/g, "\"")
        .replace(/\\x[a-f0-9]{2}/g, "-");
    mem_chars += mem_line.length;
    encoded_line = line
        .replace(/\\/g, "\\\\")
        .replace(/\"/g, "\\\"");
    encoded_chars += encoded_line.length + 2;

    console.log(line);
    // console.log(mem_line);
    console.log(encoded_line);
    console.log("");
});

console.log("Answer Part 1: " + (code_chars - mem_chars));
console.log("Answer Part 2: " + (encoded_chars - code_chars));
