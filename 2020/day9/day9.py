import os
import re

use_sample = 0
input_file = os.path.join(os.path.dirname(
    __file__), "sample.txt" if use_sample else "input.txt")
preamble = 5 if use_sample else 25

f = open(input_file, "r")
lines = f.read().splitlines()
numbers = [int(line) for line in lines]

for i in range(preamble, len(numbers)-1):
    found = 0
    for a in range(i-preamble, i-1):
        if found == 1:
            break
        for b in range(a+1, i):
            if numbers[a]+numbers[b] == numbers[i]:
                found = 1
                break
    if found == 0:
        print(f"Exception found: {numbers[i]}")
