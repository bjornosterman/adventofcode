import os
import re
from datetime import datetime

use_sample = 0
input_file = os.path.join(os.path.dirname(
    __file__), "sample.txt" if use_sample else "input.txt")

f = open(input_file, "r")
lines = f.read().splitlines()

starttime = datetime.now()

# input = [7,13,'x','x',59,'x',31,19]

input = [(0, 17), (1, 'x'), (2, 13), (3, 19)]
input = filter(lambda x: x[1] != 'x', input)
input = sorted(input, key=lambda x: x[1])

a = 0
m = 1


for (rest, busid) in input:
    if busid != 'x':
        while a % busid != ((busid-rest)%busid):
            a = a + m
        print(f"rest = {rest}, a = {a}, m = {m}")
        last = a
        a = a + m
        while a % busid != ((busid-rest)%busid):
            a = a + m
        print(f"rest = {rest}, a = {a}, m = {m}, last={last}")
        m = a - last
        a = last


endtime = datetime.now()
spent = endtime-starttime
print(f"Time taken: {spent}")
