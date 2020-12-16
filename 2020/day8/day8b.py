import os
import re


class Instruction:
    def __init__(self, type, value):
        self.Type = type
        self.Value = value


def run(program):
    visited = set()
    acc = 0
    ip = 0
    while ip < len(program):
        if ip in visited:
            return 0
        visited.add(ip)
        instruction = program[ip]
        type = instruction.Type
        value = instruction.Value

        if (type == "nop"):
            ip = ip + 1
        elif (type == "acc"):
            acc = acc + value
            ip = ip + 1
        elif (type == "jmp"):
            ip = ip + value
        else:
            raise Exception("Unknown instruction")
    return acc


use_sample = 0
input_file = os.path.join(os.path.dirname(
    __file__), "sample.txt" if use_sample else "input.txt")

f = open(input_file, "r")
program = []
lines = f.read().splitlines()
for line in lines:
    split = line.split(" ")
    program.append(Instruction(split[0], int(split[1])))

test = 0
result = 0


def swap(arg):
    alts = {
        "jmp": "nop",
        "nop": "jmp"
    }
    return alts.get(arg, arg)


while result == 0:
    newprogram = [i for i in program]
    oldinstruction = newprogram[test]
    newprogram[test] = Instruction(
        swap(oldinstruction.Type), oldinstruction.Value)
    result = run(newprogram)
    test = test + 1

print(f"Result: {result}")
