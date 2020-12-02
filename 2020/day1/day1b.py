f = open('2020/day1/input1.txt', "r")
lines = f.readlines()
ints = [int(x) for x in lines]

for a in ints:
    for b in ints:
        for c in ints:
            if a+b+c==2020:
                print(f"{a} + {b} + {c} = {a*b*c}")
                break

# print(ints)

#my answer = 817 + 502 + 701= 287503934