import os
import re
from datetime import datetime

use_sample = 0
input_file = os.path.join(os.path.dirname(
    __file__), "sample.txt" if use_sample else "input.txt")

f = open(input_file, "r")
lines = f.read().splitlines()

starttime = datetime.now()

wx = 10
wy = 1
sx = 0
sy = 0

directions = ['E', 'S', 'W', 'N']


def do_move(op, value):
    global wx, wy
    if op == 'N':
        wy = wy + value
    if op == 'E':
        wx = wx + value
    if op == 'S':
        wy = wy - value
    if op == 'W':
        wx = wx - value


def turn_left(times):
    global wx, wy
    for _ in range(times):
        wx, wy = wy, -wx


for line in lines:
    op = line[0]
    value = int(line[1:])
    if op in directions:
        do_move(op, value)
    if op == 'R':
        turn_left(int(value/90))
    if op == 'L':
        turn_left(4-int(value/90))
    if op == 'F':
        sx = sx + wx*value
        sy = sy + wy*value
    print(f"{line:5} ship: {sx},{sy} waypoint: {wx},{wy}")

print(f"Manhattan: {abs(sx)+abs(sy)}")

endtime = datetime.now()
spent = endtime-starttime
print(f"Time taken: {spent}")
