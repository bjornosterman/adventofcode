f = open('2020/day1/input1.txt', "r")
lines = f.readlines()
ints = [int(x) for x in lines]

for a in ints:
    for b in ints:
        if a+b==2020:
            print(f"{a} + {b} = {a*b}")
            break

# print(ints)


#my answer = 756 + 1264 = 955584