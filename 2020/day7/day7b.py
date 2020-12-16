import os
import re
use_sample = 0
input_file = os.path.join(os.path.dirname(
    __file__), "sample2.txt" if use_sample else "input.txt")

f = open(input_file, "r")

childs = {}

lines = f.read().splitlines()
for line in lines:
    split1 = line.rstrip(".").split(" contain ")
    outer_bag = split1[0][0:-1]
    child = []
    if (split1[1] != "no other bags"):
        for inner_bag_and_count in split1[1].split(", "):
            split2 = inner_bag_and_count.split(' ', 1)
            count = int(split2[0])
            inner = split2[1].rstrip("s")
            child.append((count, inner))
    childs[outer_bag] = child


def doCount(bagType):
    sum = 1
    for (count, type) in bagType:
        sum = sum + (count * doCount(childs[type]))
    return sum


number_of_inner_bags = doCount(childs["shiny gold bag"])-1

print(f"Number inner bags: {number_of_inner_bags}")
