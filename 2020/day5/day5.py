import os
use_sample = 0
input_file = os.path.join(os.path.dirname(
    __file__), "sample.txt" if use_sample else "input.txt")

f = open(input_file, "r")
lines = f.read().splitlines()

seat_ids = set()
max_seat_id = 0
for line in lines:
    rf = 0
    cf = 0
    rt = 128
    ct = 8
    for c in line:
        if c == "F":
            rt = (rt+rf)/2
        elif c == "B":
            rf = (rt+rf)/2
        elif c == "L":
            ct = (ct+cf)/2
        elif c == "R":
            cf = (ct+cf)/2
    seat_id = rf*8+cf
    seat_ids.add(seat_id)
    if seat_id > max_seat_id:
        max_seat_id = seat_id

print(f"Max Seat id: {max_seat_id}")

for i in range(8*128):
    if not i in seat_ids and (i-1) in seat_ids and (i+1) in seat_ids:
        print(f"Found seat!: {i}")
        break


