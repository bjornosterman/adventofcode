import os
import re
from datetime import datetime

part = 2
use_sample = 0

input = "389125467" if use_sample else "583976241"

starttime = datetime.now()

length = 9 if part == 1 else (1000*1000)
iterations = 100 if part == 1 else (10*1000*1000)


class Cup:
    def __init__(self, id):
        self.Id = id
        self.Before = self
        self.After = self

def printCups(cup):
    
    for _ in range(9):
        if cup == cc:
            print(f"({cup.Id})", end="")
        else:
            print(f" {cup.Id} ", end="")
        cup = cup.After
    print("")

before = Cup(-1)
tmp = before

cups_by_id = {}

for x in [int(c) for c in (input)] + [x for x in range(10, length+1)]:
    cup = Cup(x)
    cups_by_id[x] = cup
    before.After = cup
    before = cup




first = cups_by_id[int(input[0])]
last = cups_by_id[int(input[8]) if part == 1 else length]
last.After = first

cc = first
for i in range(iterations):
    # printCups(first)
    y1 = cc.After
    y2 = y1.After
    y3 = y2.After

    # print(f"pick up: {y1.Id}, {y2.Id}, {y3.Id}")

    dcv = cc.Id
    while dcv == cc.Id or dcv == y1.Id or dcv == y2.Id or dcv == y3.Id:
        dcv = dcv-1
        if dcv == 0:
            dcv = length
    dc = cups_by_id[dcv]
    
    # print(f"destination: {dcv}")

    cc.After = y3.After
    y3.After = dc.After
    dc.After = y1

    cc = cc.After


endtime = datetime.now()

one = cups_by_id[1]

printCups(one)

print(f"{one.After.Id} * {one.After.After.Id} = {one.After.Id * one.After.After.Id}")

spent = endtime-starttime
print(f"Time taken: {spent}")
