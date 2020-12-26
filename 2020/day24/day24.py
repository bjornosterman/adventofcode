import os
import re
from datetime import datetime

use_sample = 0
input_file = os.path.join(os.path.dirname(
    __file__), "sample.txt" if use_sample else "input.txt")

f = open(input_file, "r")
lines = f.read().splitlines()

starttime = datetime.now()


def neighbors(value):
    return [value+2, value+1-1000, value-1-1000, value-2, value-1+1000, value+1+1000]


black_tiles = set()
for line in lines:
    (x, y) = (0, 0)
    while line != "":
        (dx, dy) = (0, 0)
        if line.startswith("e"):
            dx = 2
        elif line .startswith("se"):
            dx = 1
            dy = -1
        elif line .startswith("sw"):
            dx = -1
            dy = -1
        elif line .startswith("w"):
            dx = -2
        elif line .startswith("nw"):
            dx = -1
            dy = 1
        elif line .startswith("ne"):
            dx = 1
            dy = 1
        line = line[1 if line[0] in "ew" else 2:]
        x = x + dx
        y = y + dy
    pos = y*1000+x
    if pos in black_tiles:
        black_tiles.remove(pos)
    else:
        black_tiles.add(pos)

answer = len(black_tiles)
print(f"Answer Part 1: {answer}")


for i in range(100):
    white_tiles = set([n for ns in [neighbors(x) for x in black_tiles] for n in ns]).difference(black_tiles)
    to_add = [x for x in white_tiles if sum([1 for n in neighbors(x) if n in black_tiles]) == 2]
    to_remove = [x for x in black_tiles if len([n for n in neighbors(x) if n in black_tiles]) not in [1,2] ]
    black_tiles.difference_update(to_remove)
    black_tiles.update(to_add)
    print(f"Day {i+1}: {len(black_tiles)}")




endtime= datetime.now()
spent= endtime-starttime
print(f"Time taken: {spent}")
