def downhill(down,right):
    hits = 0
    col = 0
    for row in range(0, len(lines), right):
        if (lines[row][col] == "#"):
            hits = hits + 1
        col = (col + down) % width
    print(f"{down},{right} => {hits}")
    return hits


import os
use_sample = 0
input_file = os.path.join(os.path.dirname(__file__), "sample.txt" if use_sample else "input.txt")

f = open(input_file, "r")
lines = f.read().splitlines()

width = len(lines[0])

strats = [
    [1,1],
    [3,1],
    [5,1],
    [7,1],
    [1,2],
]

combined = 1
for strat in strats:
    combined = combined * downhill(strat[0], strat[1])

print(f"combined = {combined}")

# #my answer = 1718180100
