"use strict";

const fs = require('fs');

function Item(name, cost, damage, armor) {
    this.Name = name;
    this.Cost = cost;
    this.Damage = damage;
    this.Armor = armor;
}

const weapons = [
    new Item("Dagger", 8, 4, 0),
    new Item("Shortsword", 10, 5, 0),
    new Item("Warhammer", 25, 6, 0),
    new Item("Longsword", 40, 7, 0),
    new Item("Greataxe", 74, 8, 0),
];

const armors = [
    new Item("Nothing", 0, 0, 0),
    new Item("leather", 13, 0, 1),
    new Item("Chainmail", 31, 0, 2),
    new Item("Splintermail", 53, 0, 3),
    new Item("Bandedmail", 75, 0, 4),
    new Item("Platemail", 102, 0, 5),
];

const rings = [
    new Item("Nothing", 0, 0, 0),
    new Item("Damage +1", 25, 1, 0),
    new Item("Damage +1", 50, 2, 0),
    new Item("Damage +1", 100, 3, 0),
    new Item("Defense +1", 20, 0, 1),
    new Item("Defense +1", 40, 0, 2),
    new Item("Defense +1", 80, 0, 3),
];

function Fight(heroHitpoints, heroDamage, heroArmor) {
    let bossDamage = 8;
    let bossArmor = 2;
    let bossHitpoints = 100;
    while (true) {
        bossHitpoints -= Math.max(heroDamage - bossArmor, 1);
        if (bossHitpoints <= 0) return true;
        heroHitpoints -= Math.max(bossDamage - heroArmor, 1);
        if (heroHitpoints <= 0) return false;
    }
}

let minCost = 100000;
let minSet = [];
let maxCost = 0;
let maxSet = [];

weapons.forEach(weapon => {
    armors.forEach(armor => {
        rings.forEach(left => {
            rings.forEach(right => {
                if (left != right || left.Cost + right.Cost == 0) {
                    let items = [weapon, armor, left, right];
                    const cost = items.reduce((a, b) => a + b.Cost, 0);
                    let damage = items.reduce((a, b) => a + b.Damage, 0);
                    let defence = items.reduce((a, b) => a + b.Armor, 0);
                    if (Fight(100, damage, defence)) {
                        if (cost < minCost) {
                            minCost = cost;
                            minSet = items;
                        }
                    } else {
                        if (cost > maxCost) {
                            maxCost = cost;
                            maxSet = items;
                        }
                    }
                }
            });
        });
    });
});

console.log("Min cost: " + minCost + ", Set: " + minSet.map(x => x.Name));
console.log("Max cost: " + maxCost + ", Set: " + maxSet.map(x => x.Name));
