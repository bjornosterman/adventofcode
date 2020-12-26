import os
import re
from datetime import datetime

use_sample = 1

input = "389125467" if use_sample else "583976241"

starttime = datetime.now()

# cups = [int(c) for c in (input)] + [x for x in range(10,1000001)]
cups = [int(c) for c in (input)]

for i in range(100):
    # print(f"{i}: {cups}")
    cc = cups[0]
    picked_cups = cups[1:4]
    found = 0
    while True:
        cc = cc-1
        if cc <= 0:
            cc = len(cups)
        if not cc in cups[1:4]:
            break
    a = cups.index(cc,4)
    print(f"Inserting {cups[1:4]} into index {a}")
    cups = cups[4:a+1] + cups[1:4] + cups[a+1:] + cups[0:1]

i = cups.index(1)
print(f"{cups[i+1]} * {cups[i+2]} = {cups[i+1] * cups[i+2]}")
cups = cups[i+1:] + cups[0:i]
print(f"{i}: {''.join([str(x) for x in cups])}")


endtime = datetime.now()
spent = endtime-starttime
print(f"Time taken: {spent}")
