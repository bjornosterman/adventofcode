import os
use_sample = 0
input_file = os.path.join(os.path.dirname(
    __file__), "sample.txt" if use_sample else "input.txt")

f = open(input_file, "r")
lines = f.read().splitlines()
lines.append("")

yess = {}
number_of_members = 0
all_yess = 0
for line in lines:
    if line == "":
        for a in yess:
            if yess[a] == number_of_members:
                all_yess = all_yess + 1
        yess = {}
        number_of_members = 0
    else:
        number_of_members = number_of_members + 1
        for c in line:
            if c in yess:
                yess[c] = yess[c]+1
            else:
                yess[c] = 1


print(f"All Yes's: {all_yess}")
