import os
import re
from datetime import datetime

use_sample = 0
input_file = os.path.join(os.path.dirname(__file__), "sample.txt" if use_sample else "input.txt")

f = open(input_file, "r")
lines = f.read().splitlines()

starttime = datetime.now()

x = 0
y = 0
d = 0
directions = ['E','S','W','N']

def do_move(op, value):
    global x,y
    if op == 'N':
        y = y + value
    if op == 'E':
        x = x + value
    if op == 'S':
        y = y - value
    if op == 'W':
        x = x - value


for line in lines:
    op = line[0]
    value = int(line[1:])
    if op in directions:
        do_move(op, value)
    if op == 'R':
        d = int((d*90+value)/90)%4
    if op == 'L':
        d = int((d*90+360-value)/90)%4
    if op == 'F':
        do_move(directions[d], value)
    print(f"{line:8} -{directions[d]}-> {x},{y}")

print(f"Manhattan: {abs(x)+abs(y)}") 

endtime = datetime.now()
spent = endtime-starttime
print(f"Time taken: {spent}")
