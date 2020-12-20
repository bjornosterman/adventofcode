import os
import re
from datetime import datetime

use_sample = 0
input_file = os.path.join(os.path.dirname(
    __file__), "sample.txt" if use_sample else "input.txt")

f = open(input_file, "r")
lines = f.read().splitlines()

starttime = datetime.now()

rules = {}
messages = []

section = 0
for line in lines:
    if len(line) == 0:
        section = 1
    elif section == 0:
        (k, v) = line.split(': ')
        rules[k] = v
    else:
        messages.append(line)


def makeregex(text):
    if text.startswith("\""):
        return text[1]
    if "|" in text:
        (a, b) = text.split(" | ")
        return "(" + makeregex(a) + "|" + makeregex(b) + ")"
    return "".join([makeregex(rules[x]) for x in text.split(" ")])

master_pattern="^" + makeregex(rules['0']) + "$"

matches=sum(1 for m in messages if re.match(master_pattern, m))

print(f"Answer: {matches}")


endtime=datetime.now()
spent=endtime-starttime
print(f"Time taken: {spent}")
