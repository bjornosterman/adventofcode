import os
import re
from datetime import datetime

use_sample = 0
input_file = os.path.join(os.path.dirname(
    __file__), "sample2.txt" if use_sample else "input.txt")

f = open(input_file, "r")
lines = f.read().splitlines()

starttime = datetime.now()

class Field:
    def __init__(self, name, rules):
        self.Name = name
        self.Rules = rules
        self.Positions = []
    def IsValid(self, value):
        test1 = value >= self.Rules[0][0] and value <= self.Rules[0][1]
        test2 = value >= self.Rules[1][0] and value <= self.Rules[1][1]
        return test1 or test2

rules = []
my_ticket = []
tickets = []
fieldnames = []
section = 0

fields = []

for line in lines:
    if line == "":
        section = section + 1
    elif line == "your ticket:" or line == "nearby tickets:":
        section = section  # do nothing
    elif section == 0:
        (k, rest) = line.split(": ", 1)
        fieldnames.append(k)
        (rule1, rule2) = rest.split(" or ", 1)
        fields.append(Field(k, ([int(x) for x in rule1.split("-")],[int(x) for x in rule2.split("-")])))
    elif section == 1:
        my_ticket = [int(x) for x in line.split(",")]
    else:
        tickets.append([int(x) for x in line.split(",")])

valid_tickets = []

for ticket in tickets:
    tests = [any([field.IsValid(value) for field in fields]) for value in ticket]
    if all(tests):
        valid_tickets.append(ticket)


number_of_fields = len(valid_tickets[0])
i = 0

for ticket_field in range(number_of_fields):
    for field in fields:
        values = [ticket[ticket_field] for ticket in valid_tickets]
        if all([field.IsValid(value) for value in values]):
            field.Positions.append(ticket_field)
            print(f"Field {field.Name} in position {ticket_field}")

while True:
    dirty = 0
    for field in fields:
        if len(field.Positions) == 1:
            found_position = field.Positions[0]
            for field2 in fields:
                if field2 != field and found_position in field2.Positions:
                    dirty = 1
                    field2.Positions.remove(found_position)
    if not dirty:
        break

for field in fields:
    print(f"{field.Name}: {field.Positions}")

sum = 1
for field in fields:
    if field.Name.startswith("departure"):
        sum = sum * my_ticket[field.Positions[0]]

print(f"Answer: {sum}")

endtime = datetime.now()
spent = endtime-starttime
print(f"Time taken: {spent}")
