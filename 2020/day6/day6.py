import os
use_sample = 0
input_file = os.path.join(os.path.dirname(
    __file__), "sample.txt" if use_sample else "input.txt")

f = open(input_file, "r")
lines = f.read().splitlines()

yess = set()
group_results = [yess]
ab = "abcdefghijklmnopqrstuvxyz"


for line in lines:
    if line == "":
        yess = set()
        group_results.append(yess)
    else:
        for c in line:
            yess.add(c)

group_sum = 0
for s in group_results:
    group_sum = group_sum + len(s)

print(f"Group sum: {group_sum}")
