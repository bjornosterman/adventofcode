import os, re
use_sample = 0
input_file = os.path.join(os.path.dirname(__file__), "sample.txt" if use_sample else "input.txt")

f = open(input_file, "r")

parents = {}

lines = f.read().splitlines()
for line in lines:
    split1 = line.rstrip(".").split(" contain ")
    outer_bag = split1[0][0:-1]
    for inner_bag_and_count in split1[1].split(", "):
        split2 = inner_bag_and_count.split(' ', 1)
        count = split2[0]
        inner = split2[1].rstrip("s")
        if not inner in parents:
            parents[inner] = []
        parents[inner].append(outer_bag)

found = []
interestings = []
interestings.extend(parents["shiny gold bag"])

for interesting in interestings:
    if not interesting in found:
        found.append(interesting)
        if interesting in parents:
            interestings.extend(parents[interesting])

for item in found:
    print("Found: " + item)

print(f"Number of bag-types that can eventually carry shiny cold bags: {len(found)}")