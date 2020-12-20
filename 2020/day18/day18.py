import os
import re
from datetime import datetime

use_sample = 0
input_file = os.path.join(os.path.dirname(
    __file__), "sample.txt" if use_sample else "input.txt")

f = open(input_file, "r")
lines = f.read().splitlines()

starttime = datetime.now()

def solve(expr):
    sum = 0
    op = '+'
    while len(expr) > 0:
        c = expr[0]
        expr = expr[1:]
        if c >= '0' and c <= '9':
            if op == '+':
                sum = sum + int(c)
            else:
                sum = sum * int(c)
        elif c == '(':
            (v, expr) = solve(expr)
            if op == '+':
                sum = sum + v
            else:
                sum = sum * v
        elif c == ')':
            return sum, expr
        else:
            op = c
    return sum

# expr = "1 + 2 * 3 + 4 * 5 + 6"
# expr =" 1 + (2 * 3) + (4 * (5 + 6))"
# expr = "((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2"

answer = 0
for line in lines:
    answer = answer + solve(line.replace(" ", ""))

print(f"Answer: {answer}")


endtime = datetime.now()
spent = endtime-starttime
print(f"Time taken: {spent}")
