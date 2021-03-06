import os
import re
from datetime import datetime

use_sample = 0
input_file = os.path.join(os.path.dirname(
    __file__), "sample.txt" if use_sample else "input.txt")

f = open(input_file, "r")
lines = f.read().splitlines()

starttime = datetime.now()


def index(x, y, z):
    return z*1000000 + y*1000 + x


def get_cordinates(value):
    return (value % 1000, (int(value/1000) % 1000), int(value/1000000))


def get_neighbors(value):
    r = []
    for x in [-1, 0, 1]:
        for y in [-1000, 0, 1000]:
            for z in [-1000000, 0, 1000000]:
                if x+y+z != 0:
                    r.append(x+y+z+value)
    return r

    # return [
    #     value - 1 - 1000 - 1000000,
    #     value + 0 - 1000 - 1000000,
    #     value + 1 - 1000 - 1000000,
    #     value - 1 - 0000 - 1000000,
    #     value + 0 - 0000 - 1000000,
    #     value + 1 - 0000 - 1000000,
    #     value - 1 + 1000 - 1000000,
    #     value + 0 + 1000 - 1000000,
    #     value + 1 + 1000 - 1000000,

    #     value - 1 - 1000 - 0000000,
    #     value + 0 - 1000 - 0000000,
    #     value + 1 - 1000 - 0000000,
    #     value - 1 - 0000 - 0000000,
    #     # value + 0 - 0000 - 0000000,  # This i me
    #     value + 1 - 0000 - 0000000,
    #     value - 1 + 1000 - 0000000,
    #     value + 0 + 1000 - 0000000,
    #     value + 1 + 1000 - 0000000,

    #     value - 1 - 1000 + 1000000,
    #     value + 0 - 1000 + 1000000,
    #     value + 1 - 1000 + 1000000,
    #     value - 1 - 0000 + 1000000,
    #     value + 0 - 0000 + 1000000,
    #     value + 1 - 0000 + 1000000,
    #     value - 1 + 1000 + 1000000,
    #     value + 0 + 1000 + 1000000,
    #     value + 1 + 1000 + 1000000,
    # ]


actives = set()

for y in range(len(lines)):
    for x in range(len(lines[0])):
        if lines[y][x] == '#':
            actives.add(index(x, y, 0))



# for cell in actives:
#     (x, y, x) = get_cordinates(cell)
#     print(get_cordinates(cell))

for turn in range(1,7):

    interrests = set()
    for active in actives:
        for neighbor in get_neighbors(active):
            interrests.add(neighbor)

    next_actives = set()

    for cell in interrests:
        active_neighbors = [(1 if x in actives else 0) for x in get_neighbors(cell)]
        number_of_active_neighbors = sum(active_neighbors)
        if cell in actives:
            if number_of_active_neighbors == 2 or number_of_active_neighbors == 3:
                next_actives.add(cell)
        else:
            if number_of_active_neighbors == 3:
                next_actives.add(cell)

    actives = next_actives
    print(f"Turn {turn}: Number of actives: {len(actives)}")

    # for cell in next_actives:
    #     (x, y, x) = get_cordinates(cell)
    #     print(get_cordinates(cell))


endtime = datetime.now()
spent = endtime-starttime
print(f"Time taken: {spent}")
