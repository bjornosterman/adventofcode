const { match } = require('assert');
const { setupMaster } = require('cluster');
const fs = require('fs');
const { parse } = require('path');
const { stringify } = require('querystring');

function LookAndSay(input) {
    var match = input.match(/(\d)\1*/g);
    return match.map(x => x.length + x[0]).join('');
}

// var text = "1";
// var repeat = 5;
var text = "1113122113"
//var repeat = 40;
var repeat = 50;

Array(repeat).fill('x').forEach(x  => {
    text = LookAndSay(text);
    console.log(text.length);
    // console.log(text);
});

