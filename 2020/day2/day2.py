f = open('2020/day2/input.txt', "r")
lines = f.readlines()
oks = 0
for line in lines:
    parts = line.split(' ')
    char = parts[1].replace(":","")
    password = parts[2]
    parts = parts[0].split('-')
    min = int(parts[0])
    max = int(parts[1])
    count = 0
    for c in password:
        if c == char:
            count = count + 1
    if count >= min and count <= max:
        oks = oks + 1

print(f"oks = {oks}")

#my answer = oks = 548