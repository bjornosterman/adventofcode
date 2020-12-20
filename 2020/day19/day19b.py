import os
import regex
from datetime import datetime

use_sample = 0
input_file = os.path.join(os.path.dirname(
    __file__), "sample2.txt" if use_sample else "input.txt")

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
        return "(?:" + makeregex(a) + "|" + makeregex(b) + ")"
    r = ""
    for x in text.split(" "):
        if x == '8':
            r = r + "(?:" + makeregex("42") + ")+"
        elif x == '11':
            r = r + "(" + makeregex("42") + "(?1)?" + makeregex("31") + ")" # (?1)"
        else:
            r = r + makeregex(rules[x])
    return r

precalced_rules = {}
for rule in rules:
    precalced_rules[rule] = makeregex(rules[rule])

for rule in sorted(precalced_rules, key=lambda x: int(x)):
    print(rule + ": " + precalced_rules[rule])

master_pattern="^" + makeregex(rules['0']) + "$"

matches=sum(1 for m in messages if regex.match(master_pattern, m))

print(f"Answer: {matches}")


endtime=datetime.now()
spent=endtime-starttime
print(f"Time taken: {spent}")
