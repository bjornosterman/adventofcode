import os
import re
from datetime import datetime

use_sample = 0
input_file = os.path.join(os.path.dirname(
    __file__), "sample.txt" if use_sample else "input.txt")

f = open(input_file, "r")
lines = f.read().splitlines()

starttime = datetime.now()

start_numbers = [int(x) for x in lines[0].split(",")]

heard = {}

for x in range(len(start_numbers)):
    heard[start_numbers[x]] = x+1

turn = len(start_numbers)
spoken = start_numbers[-1]
first_time_spoken = 1
diff = 0

million_timer = datetime.now()
while turn < 30000000:
    turn = turn + 1
    spoken = 0 if first_time_spoken else diff
    
    if not spoken in heard:
        first_time_spoken = 1
    else:
        first_time_spoken = 0
        diff = turn - heard[spoken]

    if turn%1000000==0:
        print(f"Million-mark: {datetime.now()-million_timer}")
        million_timer = datetime.now()
        
    # if turn > 9999000:
    #     print(f"{turn}: {first_time_spoken} I say {spoken}")
    heard[spoken] = turn



print(f"Last: {spoken}")

endtime = datetime.now()
spent = endtime-starttime
print(f"Time taken: {spent}")
