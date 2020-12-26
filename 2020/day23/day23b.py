import os
import re
from datetime import datetime

use_sample = 0

input = "389125467" if use_sample else "583976241"

starttime = datetime.now()

# cups = [int(c) for c in (input)] + [x for x in range(10,1000001)]
length = 1000
iterations = 10000

cups = [int(c) for c in (input)] + [x for x in range(10, length+1)]

last = -1
diff = 0
hits = 0
for i in range(iterations):
    # print(f"{i}: {cups}")
    cc = cups[0]
    picked_cups = cups[1:4]
    found = 0
    shortcut = last-diff
    if shortcut > 4 and shortcut < length and cups[shortcut] == cc-1:
        hits = hits + 1
        a = shortcut
    else:
        # print(f"Miss: Last: {last}, Diff: {diff}, Shortcut: {shortcut}")
        while True:
            cc = cc-1
            if cc <= 0:
                cc = len(cups)
            if  cc not in cups[1:4]:
                break
        a = cups.index(cc, 4)
        diff = last - a
        # print(f"Correct was: {a}, Last: {last}, NewDiff: {diff}")
    print(f"Inserting {cups[1:4]} into index {a}, diff {last-a}")
    last = a
    cups = cups[4:a+1] + cups[1:4] + cups[a+1:] + cups[0:1]

endtime = datetime.now()

i = cups.index(1)
print(f"{cups[i+1]} * {cups[i+2]} = {cups[i+1] * cups[i+2]}")
cups = cups[i+1:] + cups[0:i]
print(f"{i}: {''.join([str(x) for x in cups])}")


spent = endtime-starttime
print(f"Time taken: {spent}")
print(f"Hit {hits} times of {iterations}: {hits/iterations*100}%")
