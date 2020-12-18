import os
import re
from datetime import datetime

use_sample = 0
input_file = os.path.join(os.path.dirname(
    __file__), "sample2.txt" if use_sample else "input.txt")

f = open(input_file, "r")
lines = f.read().splitlines()

starttime = datetime.now()

mem = {}
mask = ""


def set_mem(mem_pos, value):
    m = f"{mem_pos:b}"
    m = '0'*(36-len(m)) + m
    # print(f"address: " + m)
    # print(f"mask:    " + mask)
    floats = []
    m2 = ""
    for i in range(36):
        if mask[i] == '0':
            m2 = m2+m[i]
        elif mask[i] == '1':
            m2 = m2+'1'
        else:
            m2 = m2+'0'
            floats.append(36-i-1)
    print("result:  " + m2)
    set_mem2(int(m2, 2), value, floats)


def set_mem2(mem_pos, value, floats):
    # print(floats)
    if len(floats) == 0:
        m = f"{mem_pos:b}"
        m = '0'*(36-len(m)) + m
        # print(f"address: " + m)
        # print(f"mem[{mem_pos}] <= {value}")
        mem[mem_pos] = value
    else:
        set_mem2(mem_pos, value, floats[1:])
        adder = pow(2,floats[0])
        set_mem2(mem_pos+adder, value, floats[1:])


for line in lines:
    (key, value) = line.split(" = ", 1)
    if key == "mask":
        mask = value
    else:
        mem_pos = int(key[4:-1])
        set_mem(mem_pos, int(value))

result = sum([mem[x] for x in mem])

print(f"Result: {result}")

endtime = datetime.now()
spent = endtime-starttime
print(f"Time taken: {spent}")
