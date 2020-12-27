

const md5 = require('md5');

input = 'abcdef'; // sample
input = 'iwrupvqb';

var mine = 0;
do {
    mine++;
    hash = md5(input + mine);
} while (!hash.startsWith('00000'));

console.log("Mine: " + mine);

mine = 0;
do {
    mine++;
    hash = md5(input + mine);
} while (!hash.startsWith('000000'));

console.log("Mine2: " + mine);
