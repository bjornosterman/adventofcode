import os
import re
from datetime import datetime

use_sample = 0
input_file = os.path.join(os.path.dirname(
    __file__), "sample.txt" if use_sample else "input.txt")

f = open(input_file, "r")
lines = f.read().splitlines()

starttime = datetime.now()

def parseBin(line):
    pow = 1
    r = 0
    for i in range(10):
        if line[9-i] == '#':
            r = r + (pow)
        pow = pow*2
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

    def edges(self):
        if len(self._edges) == 0:
            self._edges = self.calcEdges()
        return self._edges

    def calcEdges(self):
        edgeIds = []
        for _ in range(4):
            edgeIds.append(parseBin(self.Pixels[0]))
            self.turn()
        return edgeIds

    def revedges(self):
        if len(self._revedges) == 0:
            self._revedges = self.calcRevEdges()
        return self._revedges

    def calcRevEdges(self):
        edgeIds = []
        for _ in range(4):
            edgeIds.append(parseBin(self.Pixels[0][::-1]))
            self.turn()
        return edgeIds

    def alledges(self):
        if len(self._alledges) == 0:
            self._alledges = self.calcAllEdges()
        return self._alledges

    def calcAllEdges(self):
        edgeIds = []
        for _ in range(4):
            edgeIds.append(parseBin(self.Pixels[0]))
            edgeIds.append(parseBin(self.Pixels[0][::-1]))
            self.turn()
        return edgeIds


    def turn(self):
        new_pixels = []
        for a in range(10):
            new_pixels.append("".join(row[9-a] for row in self.Pixels))
        self.Pixels = new_pixels


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

answer = 1

for tile in tiles:
    edges = set()
    for t in tiles:
        if (t != tile):
            for edge in tiles[t].alledges():
                edges.add(edge)

    tile_edges = tiles[tile].alledges()
    connection_edges = [1 if edge in edges else 0 for edge in tiles[tile].edges()]
    number_of_connecting_edges = sum(connection_edges) # 4 is itself, but could be more....
    # print(f"Tile {tile}: {number_of_connecting_edges} connecting edges")
    if number_of_connecting_edges == 2:
        print(f"Tile {tile} is corner")
        answer = answer * int(tile)
        tiles[tile].IsCorner = 1
    if number_of_connecting_edges == 3:
        # print(f"Tile {tile} is edge or corner")
        tiles[tile].IsEdgeOrCorner = 1


print(f"Answer: {answer}")


endtime = datetime.now()
spent = endtime-starttime
print(f"Time taken: {spent}")
