f = open('2020/day2/input.txt', "r")
lines = f.readlines()
oks = 0
for line in lines:
    parts = line.split(' ')
    char = parts[1].replace(":","")
    password = parts[2]
    parts = parts[0].split('-')
    p1 = int(parts[0])-1
    p2 = int(parts[1])-1
    count = 0
    if (password[p1] == char and password[p2] != char) or (password[p2] == char and password[p1] != char):
        oks = oks + 1

print(f"oks = {oks}")

#my answer = oks = 548