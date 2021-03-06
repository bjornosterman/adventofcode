import os
import re
from datetime import datetime

use_sample = 0
input_file = os.path.join(os.path.dirname(
    __file__), "sample.txt" if use_sample else "input.txt")

f = open(input_file, "r")
lines = f.read().splitlines()

starttime = datetime.now()

input = [x if x == 'x' else int(x)  for x in lines[1].split(',')]

# input = [7,13,'x','x',59,'x',31,19]
# input = [17, 'x', 13, 19]

a = 0
m = input[0]

for diff in range(1, len(input)):
    busid = input[diff]
    if busid != 'x':
        rest = (busid-diff)%busid

        # Find next alignment
        while a % busid != rest:
            a = a + m
        print(f"rest = {rest}, a = {a}, m = {m}")

        # Look for new jump-size        
        last = a
        a = a + m
        while a % busid != rest:
            a = a + m
        print(f"rest = {rest}, a = {a}, m = {m}, last={last}")
        m = a - last
        a = last


endtime = datetime.now()
spent = endtime-starttime
print(f"Time taken: {spent}")
