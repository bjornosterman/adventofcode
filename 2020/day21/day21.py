import os
import re
from datetime import datetime

use_sample = 0
input_file = os.path.join(os.path.dirname(
    __file__), "sample.txt" if use_sample else "input.txt")

f = open(input_file, "r")
lines = f.read().splitlines()

starttime = datetime.now()

ingredients_by_allergens = {}
all_ingredients = set()

recepies = []

for line in lines:
    parts = line.split(" (contains ")
    ingredients = parts[0].split(" ")
    recepies.append(ingredients)
    all_ingredients.update(ingredients)
    allergens = parts[1][:-1].split(", ")
    for allergen in allergens:
        if not allergen in ingredients_by_allergens:
            ingredients_by_allergens[allergen] = set(ingredients)
        else:
            ingredients_by_allergens[allergen] = ingredients_by_allergens[allergen].intersection(ingredients)

allergenic_ingredients = set()

dirty = 1
while dirty:
    dirty = 0
    for allergen in [x for x in ingredients_by_allergens if len(ingredients_by_allergens[x]) == 1]:
        for i in ingredients_by_allergens[allergen]:
            allergenic_ingredients.add(i)
            ingredient = i
        for a2 in ingredients_by_allergens:
            if a2 != allergen:
                if ingredient in ingredients_by_allergens[a2]:
                    dirty = 1
                    ingredients_by_allergens[a2].remove(ingredient)



for allergen in ingredients_by_allergens:
    print(allergen + ": " + str(ingredients_by_allergens[allergen]))

non_allergenic_ingredients = [x for x in all_ingredients if x not in allergenic_ingredients]

answer = sum([len([x for x in r if not x in allergenic_ingredients]) for r in recepies])

# answer = len(non_allergenic_ingredients)


print(f"Answer1: {answer}")

answer2 = []
for allergen in sorted(ingredients_by_allergens):
    for i in ingredients_by_allergens[allergen]:
        answer2.append(i)

print(f'Answer2: {",".join(answer2)}')


endtime = datetime.now()
spent = endtime-starttime
print(f"Time taken: {spent}")
