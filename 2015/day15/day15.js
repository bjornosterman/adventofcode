const fs = require('fs');

var use_sample = 0;

var text = fs.readFileSync(use_sample ? "sample.txt" : "input.txt", 'utf8');
var lines = text.split("\r\n");

ingredients = [];

lines.forEach(line => {
    var match = line.match(/(?<name>\w+): capacity (?<capacity>-?\d+), durability (?<durability>-?\d+), flavor (?<flavor>-?\d+), texture (?<texture>-?\d+), calories (?<calories>-?\d+)/);
    var ingredient = new Object();
    ingredient.Name = match.groups.name;
    ingredient.Capacity = parseInt(match.groups.capacity);
    ingredient.Durability = parseInt(match.groups.durability);
    ingredient.Flavor = parseInt(match.groups.flavor);
    ingredient.Texture = parseInt(match.groups.texture);
    ingredient.Calories = parseInt(match.groups.calories);
    ingredients.push(ingredient);
});

var maxScore = 0;

if (use_sample) {
    for (let a = 1; a <= 100; a++) {
        var b = 100 - a;
        i1 = ingredients[0].Capacity * a + ingredients[1].Capacity * b;
        i2 = ingredients[0].Durability * a + ingredients[1].Durability * b;
        i3 = ingredients[0].Flavor * a + ingredients[1].Flavor * b;
        i4 = ingredients[0].Texture * a + ingredients[1].Texture * b;
        if (i1 <= 0 || i2 <= 0 || i3 <= 0 || i4 <= 0) continue;
        res = i1 * i2 * i3 * i4;
        maxScore = Math.max(maxScore, res);
    }
} else {
    for (let a = 1; a <= 97; a++) {
        for (let b = 1; a + b <= 98; b++) {
            for (let c = 1; a + b + c <= 99; c++) {
                var d = 100 - a - b - c;
                i1 = ingredients[0].Capacity * a + ingredients[1].Capacity * b + ingredients[2].Capacity * c + ingredients[3].Capacity * d;
                i2 = ingredients[0].Durability * a + ingredients[1].Durability * b + ingredients[2].Durability * c + ingredients[3].Durability * d;
                i3 = ingredients[0].Flavor * a + ingredients[1].Flavor * b + ingredients[2].Flavor * c + ingredients[3].Flavor * d;
                i4 = ingredients[0].Texture * a + ingredients[1].Texture * b + ingredients[2].Texture * c + ingredients[3].Texture * d;
                if (i1 <= 0 || i2 <= 0 || i3 <= 0 || i4 <= 0) continue;
                res = i1 * i2 * i3 * i4;
                calories = ingredients[0].Calories * a + ingredients[1].Calories * b + ingredients[2].Calories * c + ingredients[3].Calories * d;
                if (calories == 500) {
                    maxScore = Math.max(maxScore, res);
                }
            }
        }
    }
}
console.log("Answer: " + maxScore);