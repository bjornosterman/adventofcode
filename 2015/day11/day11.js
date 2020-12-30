function TestDoubles(chars) {
    for (let i = 0; i < chars.length - 3; i++) {
        if (chars[i] == chars[i + 1]) {
            for (let j = i + 2; j < chars.length - 1; j++) {
                if (chars[j] == chars[j + 1] && chars[i] != chars[j]) {
                    return true;
                }
            }
        }
    }
    return false;
}

var alphabet = "abcdefghijklmnopqrstuvwxyz";

function Increment(chars, index) {
    do {
        var char = chars[index];
        char++;

        if (char == alphabet.length) {
            char = 0
            Increment(chars, index - 1);
        }
        chars[index] = char;
    } while (char == 105 - 97 || char == 111 - 97 || char == 108 - 97);
}

function TestStreak(chars) {
    for (let i = 0; i < chars.length - 2; i++) {
        if (chars[i] == chars[i + 1] - 1 && chars[i + 1] == chars[i + 2] - 1) return true;
    }
    return false;
}

// Part 1
// var item = Array.from("vzbxkghb").map(x => x.charCodeAt(0) - 97);
// Part 2
var item = Array.from("vzbxxyzz").map(x => x.charCodeAt(0) - 97);

do {
    Increment(item, item.length - 1);
} while (!TestDoubles(item) || !TestStreak(item));

console.log("Next password: " + item.map(x => String.fromCharCode(x + 97)).join(""));


