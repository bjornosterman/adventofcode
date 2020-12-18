import os
import re
from datetime import datetime

use_sample = 0
input_file = os.path.join(os.path.dirname(
    __file__), "sample.txt" if use_sample else "input.txt")

f = open(input_file, "r")
lines = f.read().splitlines()

width = len(lines[0])
height = len(lines)

grid = [[0]*height for x in range(width)]
x = 0
y = 0
for line in lines:
    x = 0
    for c in line:
        grid[x][y] = c
        x = x + 1
    y = y + 1


def print_grid():
    for y in range(height):
        for x in range(width):
            print(grid[x][y], end="")
        print()
    print()

changes = [
    [-1, -1],
    [0, -1],
    [1, -1],
    [-1, 0],
    [1, -0],
    [-1, 1],
    [0, 1],
    [1, 1],
]

def count_used(x, y):
    count = 0
    for (dx, dy) in changes:
        tx = x
        ty = y
        while True:
            tx = tx + dx
            ty = ty + dy
            if tx < 0 or ty < 0 or tx >= width or ty >= height:
                break
            txy = grid[tx][ty]
            if txy == '#':
                count = count + 1
                break
            elif txy == 'L':
                break
    return count

starttime = datetime.now()

iteration = 0
while True:
    iteration = iteration + 1
    print(f"--- Iteration: {iteration}")
    # print_grid()
    grid2 = [[0]*height for x in range(width)]
    dirty = 0
    for x in range(width):
        for y in range(height):
            current = grid[x][y]
            if current == '.':
                grid2[x][y] = '.'
            else:
                used = count_used(x,y)
                if current == 'L' and used == 0:
                    grid2[x][y] = '#'
                    dirty = 1
                elif current == '#' and used >= 5:
                    grid2[x][y] = 'L'
                    dirty = 1
                else:
                    grid2[x][y] = current
    grid = grid2
    if dirty == 0:
        break

def is_used(x, y):
    return 1 if x >= 0 and y >= 0 and x < width and y < height and grid[x][y] == '#' else 0

used = 0
for x in range(width):
    for y in range(height):
        used = used + is_used(x, y)


print(f"Number of used seats: {used}")
endtime = datetime.now()
spent = endtime-starttime
print(f"Time taken: {spent}")
