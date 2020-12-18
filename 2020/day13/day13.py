import os
import re
from datetime import datetime

use_sample = 0
input_file = os.path.join(os.path.dirname(
    __file__), "sample.txt" if use_sample else "input.txt")

f = open(input_file, "r")
lines = f.read().splitlines()

starttime = datetime.now()

class bus:
    def __init__(self, busid, wait):
        self.BusId = busid
        self.Wait = wait

arrived = int(lines[0])
busids = list(map(lambda x: int(x), filter(
    lambda x: x != 'x', lines[1].split(','))))
withwait = list(map(lambda x: bus(x, (x-arrived % x) % x), busids))
smallest = sorted(withwait, key=lambda x: x.Wait)[0]


print(f"Mindiff: {smallest.Wait}, Busid: {smallest.BusId}, Answer = {smallest.BusId*smallest.Wait}")

endtime = datetime.now()
spent = endtime-starttime
print(f"Time taken: {spent}")
