"use strict";

let part = 0;

let turns = 0;

function HeroTurn(heroHP, heroManaLeft, heroManaSpent, bossHP, shieldTurnsLeft, poisonTurnsLeft, rechargeTurnsLeft) {
    turns++;
    if (part == 2) heroHP--;
    if (heroHP <= 0) return;
    if (poisonTurnsLeft > 0) bossHP -= 3;
    if (bossHP <= 0) {
        if (heroManaSpent < minManaSpent) {
            minManaSpent = heroManaSpent;
        }
        return;
    }
    if (rechargeTurnsLeft > 0) heroManaLeft += 101;
    if (heroManaLeft <= 0) return;
    
    // Recharge
    if (rechargeTurnsLeft <= 1) {
        BossTurn(heroHP, heroManaLeft - 229, heroManaSpent + 229, bossHP, shieldTurnsLeft - 1, poisonTurnsLeft - 1, 5);
    }

    // Shield
    if (shieldTurnsLeft <= 1) {
        BossTurn(heroHP, heroManaLeft - 113, heroManaSpent + 113, bossHP, 6, poisonTurnsLeft - 1, rechargeTurnsLeft - 1);
    }

    // Poison
    if (poisonTurnsLeft <= 1) {
        BossTurn(heroHP, heroManaLeft - 173, heroManaSpent + 173, bossHP, shieldTurnsLeft - 1, 6, rechargeTurnsLeft - 1);
    }

    // Magic Missile
    BossTurn(heroHP, heroManaLeft - 53, heroManaSpent + 53, bossHP - 4, shieldTurnsLeft - 1, poisonTurnsLeft - 1, rechargeTurnsLeft - 1);

    // Drain
    BossTurn(heroHP + 2, heroManaLeft - 73, heroManaSpent + 73, bossHP - 2, shieldTurnsLeft - 1, poisonTurnsLeft - 1, rechargeTurnsLeft - 1);
}

let minManaSpent = 1000000;

function BossTurn(heroHP, heroManaLeft, heroManaSpent, bossHP, shieldTurnsLeft, poisonTurnsLeft, rechargeTurnsLeft) {
    turns++;
    if (heroManaSpent > minManaSpent) return; // this line took numbers of turns from halv a billion to halv a million. :-)
    if (heroManaLeft < 0) return; // could actually not cast that spell :-(
    if (rechargeTurnsLeft > 0) heroManaLeft += 101;
    if (poisonTurnsLeft > 0) bossHP -= 3;
    if (bossHP <= 0) {
        if (heroManaSpent < minManaSpent) {
            minManaSpent = heroManaSpent;
        }
        return;
    }
    HeroTurn(heroHP - (shieldTurnsLeft > 0 ? (8 - 7) : 8), heroManaLeft, heroManaSpent, bossHP, shieldTurnsLeft - 1, poisonTurnsLeft - 1, rechargeTurnsLeft - 1);
}

HeroTurn(50, 500, 0, 55, 0, 0, 0);

console.log("Min Mana Spent: " + minManaSpent);
console.log("Test Turns: " + turns);
