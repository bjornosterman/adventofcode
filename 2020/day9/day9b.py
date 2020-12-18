import os
import re

use_sample = 0
input_file = os.path.join(os.path.dirname(
    __file__), "sample.txt" if use_sample else "input.txt")
weakness = 127 if use_sample else 10884537

f = open(input_file, "r")
lines = f.read().splitlines()
numbers = [int(line) for line in lines]

for i in range(1, len(numbers)-1):
    sum = 0
    for a in range(i, len(numbers)-1):
        sum = sum + numbers[a]
        if sum != numbers[a] and sum == weakness:
            print(f"Found {min(numbers[i:a])} + {max(numbers[i:a])} = {min(numbers[i:a])+max(numbers[i:a])}")
        else:
            if sum > weakness:
                break;

