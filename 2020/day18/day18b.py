import os
import re
from datetime import datetime

use_sample = 0
input_file = os.path.join(os.path.dirname(
    __file__), "sample.txt" if use_sample else "input.txt")

f = open(input_file, "r")
lines = f.read().splitlines()

starttime = datetime.now()

def solveParentheses(expr):
    return solve(expr.group()[1:-1])

def solveAddiion(expr):
    (a, b) = expr.group().split('+', 1)
    return str(int(a)+int(b))

def solveMultiplication(expr):
    (a, b) = expr.group().split('*', 1)
    return str(int(a)*int(b))

def solve(expr):
    # print("in:  " + expr)
    while '(' in expr:
        expr = re.sub(r"\([^()]+\)", solveParentheses, expr)
    while '+' in expr:
        expr = re.sub(r"\d+\+\d+", solveAddiion, expr)
    while '*' in expr:
        expr = re.sub(r"\d+\*\d+", solveMultiplication, expr)
    # print("out: " + expr)
    return expr

answer = 0
for line in lines:
    solution = int(solve(line.replace(" ", "")))
    print(f"Solution: {solution:10} <-- {line}")
    answer = answer + solution

print(f"Answer: {answer}")


endtime = datetime.now()
spent = endtime-starttime
print(f"Time taken: {spent}")
