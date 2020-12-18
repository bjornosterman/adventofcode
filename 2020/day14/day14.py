import os
import re
from datetime import datetime

use_sample = 0
input_file = os.path.join(os.path.dirname(
    __file__), "sample.txt" if use_sample else "input.txt")

f = open(input_file, "r")
lines = f.read().splitlines()

starttime = datetime.now()

mem = {}
mask = ""


def mask_value(value):
    b = f"{value:b}"
    b = '0'*(36-len(b)) + b
    r = ""
    for i in range(36):
        r = r + (mask[i] if mask[i] != 'X' else b[i])
    return int(r, 2)


for line in lines:
    (key, value) = line.split(" = ", 1)
    if key == "mask":
        mask = value
    else:
        mem_pos = int(key[4:-1])
        masked_value = mask_value(int(value))
        mem[mem_pos] = masked_value

result = sum([mem[x] for x in mem])

print(f"Result: {result}")

endtime = datetime.now()
spent = endtime-starttime
print(f"Time taken: {spent}")
