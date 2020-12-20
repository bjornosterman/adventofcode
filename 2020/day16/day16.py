import os
import re
from datetime import datetime

use_sample = 0
input_file = os.path.join(os.path.dirname(
    __file__), "sample.txt" if use_sample else "input.txt")

f = open(input_file, "r")
lines = f.read().splitlines()

starttime = datetime.now()
rules = []
my_ticket = []
tickets = []
section = 0
for line in lines:
    if line == "":
        section = section + 1
    elif line == "your ticket:" or line == "nearby tickets:":
        section = section  # do nothing
    elif section == 0:
        (k, rest) = line.split(": ", 1)
        (rule1, rule2) = rest.split(" or ", 1)
        rules.append([int(x) for x in rule1.split("-")])
        rules.append([int(x) for x in rule2.split("-")])
    elif section == 1:
        my_ticket = [int(x) for x in line.split(",")]
    else:
        tickets.append([int(x) for x in line.split(",")])

error_rate = 0
for ticket in tickets:
    for value in ticket:
        fails = [value < rule[0] or value > rule[1] for rule in rules]
        if all(fails):
            error_rate = error_rate + value

print(f"Ticket scanning error rate: {error_rate}")

endtime = datetime.now()
spent = endtime-starttime
print(f"Time taken: {spent}")
