import os
import re

use_sample = 0
input_file = os.path.join(os.path.dirname(
    __file__), "sample2.txt" if use_sample else "input.txt")
preamble = 5 if use_sample else 25

f = open(input_file, "r")
lines = f.read().splitlines()
numbers = [int(line) for line in lines]
numbers.sort()
numbers.insert(0,0)
numbers.append(numbers[len(numbers)-1]+3)
diffs = [0,0,0]

for i in range(0, len(numbers)-1):
    diffs[numbers[i+1]-numbers[i]-1] = diffs[numbers[i+1]-numbers[i]-1]+1

print(diffs)
print(f"{diffs[0]} * {diffs[2]} = {diffs[0]*diffs[2]}")