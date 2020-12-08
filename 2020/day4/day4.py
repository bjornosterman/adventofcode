import os, re
use_sample = 0
input_file = os.path.join(os.path.dirname(
    __file__), "sample.txt" if use_sample else "input.txt")

f = open(input_file, "r")
lines = f.read().splitlines()


passport = {}
passports = [passport]
for line in lines:
    if line == "":
        passport = {}
        passports.append(passport)
    else:
        for kv in line.split(' '):
            k_v = kv.split(':')
            passport[k_v[0]] = k_v[1]

reqs = ['byr', 'iyr', 'eyr', 'hgt', 'hcl', 'ecl', 'pid']

valid_passports = 0
for passport in passports:
    invalid = 0
    for req in reqs:
        if not req in passport:
            invalid = 1
        else:
            v = passport[req]
            if req == 'byr':
                i = int(v)
                if i < 1920 or i > 2002:
                    invalid = 1
            elif req == 'iyr':
                i = int(v)
                if i < 2010 or i > 2020:
                    invalid = 1
            elif req == 'eyr':
                i = int(v)
                if i < 2020 or i > 2030:
                    invalid = 1
            elif req == 'hgt':
                if not re.match("^\d+(cm|in)$", v):
                    invalid = 1
                else:
                    i = int(v[:-2])
                    if v[-2:] == "cm":
                        if i < 150 or i > 193:
                            invalid = 1
                    else:
                        if i < 59 or i > 76:
                            invalid = 1
            elif req == 'hcl':
                if not re.match("^#[0-9a-f]{6}$", v):
                    invalid = 1
            elif req == 'ecl':
                if not re.match("^(amb|blu|brn|gry|grn|hzl|oth)$", v):
                    invalid = 1
            elif req == 'pid':
                if not re.match("^\d{9}$", v):
                    invalid = 1

    if invalid == 0:
        valid_passports = valid_passports + 1

print(f"Valid password = {valid_passports}")
# #my first answer = 202
