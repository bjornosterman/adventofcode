import os
import re
from datetime import datetime

use_sample = 0
input_file = os.path.join(os.path.dirname(
    __file__), "sample.txt" if use_sample else "input.txt")

f = open(input_file, "r")
lines = f.read().splitlines()

starttime = datetime.now()

deck1 = []
deck2 = []

for line in lines[1:]:
    if line == "":
        break
    deck1.append(int(line))

for line in lines[(len(deck1)+3):]:
    deck2.append(int(line))

deck1 = list(reversed(deck1))
deck2 = list(reversed(deck2))

game = 0
turns = 0
stopwatch = datetime.now()


def play(deck1, deck2, depth):
    global turns, stopwatch, game

    deck_combos = set()

    game = game + 1
    this_game = game
    round = 0

    # print()
    # print(f"=== Game {this_game} ===")

    while len(deck1) != 0 and len(deck2) != 0:
        round = round + 1
        turns = turns + 1
        # print()
        # print(f"-- Round {round} (Game {this_game}) --")
        # print(f"Player 1's deck: {', '.join(reversed([str(x) for x in deck1]))}")
        # print(f"Player 2's deck: {', '.join(reversed([str(x) for x in deck2]))}")

        if (turns % 1000000) == 0:
            turn_time = datetime.now()-stopwatch
            print(f"Turns: {turns}: Set:{len(deck_combos)}: Turntime/mill {turn_time}")
            stopwatch = datetime.now()

        deck_combo_id = ",".join([str(x) for x in deck1]) + \
            "|" + ",".join([str(x) for x in deck2])
        if deck_combo_id in deck_combos:
            return True
        deck_combos.add(deck_combo_id)

        c1 = deck1.pop()
        # print(f"Player 1 plays: {c1}")

        c2 = deck2.pop()
        # print(f"Player 2 plays: {c2}")

        if (c1 <= len(deck1) and c2 <= len(deck2)):
            # print(f"Playing a sub-game to determine the winner...")
            deck1_wins = play(deck1[-c1:].copy(), deck2[-c2:].copy(), depth+1)
            # print(f"...anyway, back to game {this_game}")
        else:
            deck1_wins = c1 > c2

        # print(f"Player {1 if deck1_wins else 2} wins round {round} of game {this_game}")

        if (deck1_wins):
            deck1.insert(0, c1)
            deck1.insert(0, c2)
        else:
            deck2.insert(0, c2)
            deck2.insert(0, c1)

    if depth == 0:
        winning_deck = deck1 if len(deck1) > 0 else deck2
        answer = 0
        for i in range(len(winning_deck)):
            answer = answer + (winning_deck[i] * (i+1))

        print(f"Answer: {answer}")

    # return deck1_wins
    # print()
    if len(deck1) == 0:
        # print(f"The winner of game {this_game} is player Player2!")
        # print()
        return False
    else:
        # print(f"The winner of game {this_game} is player Player1!")
        # print()
        return True


play(deck1, deck2, 0)


endtime = datetime.now()
spent = endtime-starttime
print(f"Time taken: {spent}")
