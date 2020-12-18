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
precalced = {}

def getCombos(list):
    if len(list) == 1:
        return 1
    a = list[0]
    if a in precalced:
        return precalced[a]
    count = 0
    for start in range(1,4):
        if start >= len(list):
            break
        rest = list[start:]
        if rest[0] > (a+3):
            break
        count = count + getCombos(rest)
    precalced[a] = count
    return count

number_of_combos = getCombos(numbers)
print(f"Number of combos = {number_of_combos}")