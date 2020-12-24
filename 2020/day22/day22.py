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

turns = 0
while len(deck1) != 0 and len(deck2) != 0:
    turns = turns + 1
    c1 = deck1.pop()
    c2 = deck2.pop()
    if (c1 > c2):
        deck1.insert(0, c1)
        deck1.insert(0, c2)
    else:
        deck2.insert(0, c2)
        deck2.insert(0, c1)

winning_deck = deck1 if len(deck1) > 0 else deck2
answer = 0
for i in range(len(winning_deck)):
    answer = answer + (winning_deck[i] * (i+1))

print(f"Answer: {answer}")

endtime = datetime.now()
spent = endtime-starttime
print(f"Time taken: {spent}")
