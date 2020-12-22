import os
import re
from datetime import datetime

use_sample = 0
input_file = os.path.join(os.path.dirname(
    __file__), "sample.txt" if use_sample else "input.txt")

f = open(input_file, "r")
lines = f.read().splitlines()

input_file = os.path.join(os.path.dirname(__file__), "seamonster.txt")
f = open(input_file, "r")
seamonster_lines = f.read().splitlines()

y = 0
seamonster_pixels = []
for line in seamonster_lines:
    x = 0
    for c in line:
        if c == '#':
            seamonster_pixels.append((x, y))
        x = x+1
    y = y+1

starttime = datetime.now()


def parseBin(line):
    pow = 1
    r = 0
    for i in range(10):
        if line[9-i] == '#':
            r = r + (pow)
        pow = pow*2
    return r


def rev(v):
    r = 0
    for _ in range(10):
        r = r * 2
        r = r + (v % 2)
        v = int(v / 2)
    return r


class Tile:
    def __init__(self, number):
        self.Number = number
        self.Pixels = []
        self.IsCorner = 0
        self.IsCornerOrEdge = 0
        self._edges = []
        self._revedges = []
        self._alledges = []

    def topEdge(self):
        return parseBin(self.Pixels[0])

    def leftEdge(self):
        self.turnClockwise()
        r = parseBin(self.Pixels[0])
        self.turnClockwise()
        self.turnClockwise()
        self.turnClockwise()
        return r

    def bottomEdge(self):
        self.turnClockwise()
        self.turnClockwise()
        r = parseBin(self.Pixels[0])
        self.turnClockwise()
        self.turnClockwise()
        return r

    def rightEdge(self):
        self.turnClockwise()
        self.turnClockwise()
        self.turnClockwise()
        r = parseBin(self.Pixels[0])
        self.turnClockwise()
        return r

    def edges(self):
        if len(self._edges) == 0:
            self._edges = self.calcEdges()
        return self._edges

    def calcEdges(self):
        edgeIds = []
        for _ in range(4):
            edgeIds.append(parseBin(self.Pixels[0]))
            self.turnClockwise()
        return edgeIds

    def revedges(self):
        if len(self._revedges) == 0:
            self._revedges = self.calcRevEdges()
        return self._revedges

    # def calcRevEdges(self):
    #     edgeIds = []
    #     for _ in range(4):
    #         edgeIds.append(parseBin(self.Pixels[0][::-1]))
    #         self.turn()
    #     return edgeIds

    def alledges(self):
        if len(self._alledges) == 0:
            self._alledges = self.calcAllEdges()
        return self._alledges

    def calcAllEdges(self):
        edgeIds = []
        for _ in range(4):
            edgeIds.append(parseBin(self.Pixels[0]))
            edgeIds.append(parseBin(self.Pixels[0][::-1]))
            self.turnClockwise()
        return edgeIds

    def flip(self):
        self.Pixels = [x[::-1] for x in self.Pixels]

    def turnClockwise(self):
        # clockwise
        new_pixels = []
        for a in range(10):
            new_pixels.append("".join(row[a] for row in reversed(self.Pixels)))
        self.Pixels = new_pixels

    def crop(self):
        self.Pixels = [line[1:-1] for line in self.Pixels[1:-1]]

    def print(self):
        for line in self.Pixels:
            print(line)


tiles = {}

for line in lines:
    if line == "":
        continue
    if line.startswith("Tile"):
        (_, id) = line[0:-1].split(" ")
        tile = Tile(id)
        tiles[id] = tile
    else:
        tile.Pixels.append(line)

print(f"Number of tiles: {len(tiles)}")

answer = 1

cornerTiles = []

for tile in tiles:
    edges = set()
    for t in tiles:
        if (t != tile):
            for edge in tiles[t].alledges():
                edges.add(edge)

    tile_edges = tiles[tile].alledges()
    connection_edges = [
        1 if edge in edges else 0 for edge in tiles[tile].edges()]
    # 4 is itself, but could be more....
    number_of_connecting_edges = sum(connection_edges)
    # print(f"Tile {tile}: {number_of_connecting_edges} connecting edges")
    if number_of_connecting_edges == 2:
        print(f"Tile {tile} is corner")
        answer = answer * int(tile)
        cornerTiles.append(tile)
        tiles[tile].IsCorner = 1
    if number_of_connecting_edges == 3:
        # print(f"Tile {tile} is edge or corner")
        tiles[tile].IsEdgeOrCorner = 1


def edgeDict(tiles):
    edges = {}
    for tileName in tiles:
        tile = tiles[tileName]
        for edge in tile.alledges():
            if not edge in edges:
                edges[edge] = []
            edges[edge].append(tile)
    return edges


print(f"Answer: {answer}")

heap = tiles.copy()

table = []

for y in range(12):
    row = []
    table.append(row)
    for x in range(12):
        if x == 0 and y == 0:
            tile = heap.pop(cornerTiles[0])
            edges = edgeDict(heap)
            while rev(tile.topEdge()) in edges or rev(tile.leftEdge()) in edges:
                tile.turnClockwise()
        elif x == 0:
            potentialEdges = edgeDict(heap)
            edges = potentialEdges[rev(table[y-1][0].bottomEdge())]
            if len(edges) != 1:
                raise "More then one potential found"
            tile = heap.pop(edges[0].Number)
            tries = 0
            while tries < 4 and tile.topEdge() != rev(table[y-1][x].bottomEdge()):
                tries = tries + 1
                tile.turnClockwise()
            if tile.topEdge() != rev(table[y-1][x].bottomEdge()):
                tile.flip()
                while tile.topEdge() != rev(table[y-1][x].bottomEdge()):
                    tile.turnClockwise()
        else:
            potentialEdges = edgeDict(heap)
            edges = potentialEdges[rev(tile.rightEdge())]
            if len(edges) != 1:
                raise "More then one potential found"
            tile = heap.pop(edges[0].Number)
            tries = 0
            while tries < 4 and tile.leftEdge() != rev(table[y][x-1].rightEdge()):
                tries = tries + 1
                tile.turnClockwise()
            if tile.leftEdge() != rev(table[y][x-1].rightEdge()):
                tile.flip()
                while tile.leftEdge() != rev(table[y][x-1].rightEdge()):
                    tile.turnClockwise()
        table[y].append(tile)

for y in range(12):
    for x in range(11):
        leftTile = table[y][x]
        rightTile = table[y][x+1]
        if leftTile.rightEdge() != rev(rightTile.leftEdge()):
            raise "Does not match!!!!"

for y in range(11):
    for x in range(12):
        topTile = table[y][x]
        bottomTile = table[y+1][x]
        if topTile.bottomEdge() != rev(bottomTile.topEdge()):
            raise "Does not match!!!!"


for y in range(12):
    for l in range(10):
        for x in range(12):
            print(table[y][x].Pixels[l] + " ", end="")
        print()
    print()

for tileNumber in tiles:
    tiles[tileNumber].crop()

map = []
for y in range(12):
    for l in range(8):
        line = ""
        for x in range(12):
            line = line + table[y][x].Pixels[l]
        map.append(line)

print()
print(f"Map: {len(map)}x{len(map[0])}")
print()

for line in map:
    print(line)

seamonster_dots = set()

for _ in range(2):
    for i in range(4):
        for y in range(96-3):
            for x in range(96-max([a for (a, b) in seamonster_pixels])):
                if all([map[y+sy][x+sx] == '#' for (sx, sy) in seamonster_pixels]):
                    print(f"Found seamonster at {x},{y}")
                    for w in [(y+sy)*1000+x+sx for (sx, sy) in seamonster_pixels]:
                        seamonster_dots.add(w)
        if len(seamonster_dots) > 0:
            print(f"Seamonster-dots found: {len(seamonster_dots)}")
            roughness = sum([len(line.replace(".","")) for line in map])-len(seamonster_dots)
            print(f"Roughness: {roughness}")
        print("Turning")
        new_map = []
        for a in range(96):
            new_map.append("".join(row[a] for row in reversed(map)))
        map = new_map
    print("Flipping")
    map = [line for line in reversed(map)]

endtime = datetime.now()
spent = endtime-starttime
print(f"Time taken: {spent}")
