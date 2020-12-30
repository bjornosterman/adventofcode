const { match } = require('assert');
const { setupMaster } = require('cluster');
const fs = require('fs');
const { parse } = require('path');

var use_sample = 0;

var text = fs.readFileSync(use_sample ? "sample.txt" : "input.txt", 'utf8');
var o = JSON.parse(text);

function summarize(o) {
    var type = typeof (o);
    if (type == 'number') {
        return o;
    }

    if (type == 'string') {
        x = parseInt(o);
        if (!isNaN(x)) {
            return x;
        } else {
            return 0;
        }
    }

    // Section for fixing Part 2
    if (!Array.isArray(o)) {
        for (child in o) {
            if (!Array.isArray(o[child]) && o[child] == 'red') {
                console.log(o[child]);
                return 0;
            }
        }
    }

    sum = 0;
    for (child in o) {
        sum += summarize(o[child]);
    }
    return sum;

    // if (Array.isArray(o)) {
    //     sum = 0;
    //     for (child in o) {
    //         sum += summerizse(o[child]);
    //     }
    //     return sum;
    // } else if (type=='object'){
    //     sum = 0;
    //     for (child in o) {
    //         sum += summerizse(o[child]);
    //     }
    //     return sum;
    // }
    // else  {
    //     x = parseInt(o);
    //     if (!isNaN(x)) {
    //         return x;
    //     } else {
    //         return 0;
    //     }
    // } 

    // else {
    //     sum = 0;
    //     o.forEach(child => {
    //         sum += summerizse(child);
    //     });
    //     // for (var child of Object.entries(o)) {
    //     //     sum += summerizse(child);
    //     // }
    //     // o.children.forEach(sub => {
    //     // });
    //     return sum;
    // }
}

console.log("Test1 (6) -->", summarize(JSON.parse("[1,2,3]")));
console.log("Test1 (4) -->", summarize(JSON.parse('[1,{"c":"red","b":2},3]')));
console.log("Test1 (0) -->", summarize(JSON.parse('{"d":"red","e":[1,2,3,4],"f":5}')));
console.log("Test1 (6) -->", summarize(JSON.parse('[1,"red",5]')));

answer = summarize(o);
console.log("Answer: " + answer);
