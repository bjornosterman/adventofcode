import os
import re
from datetime import datetime

use_sample = 0

pub1 = 5764801 if use_sample else 2084668
pub2 = 17807724 if use_sample else 3704642

starttime = datetime.now()

mod = 20201227

def GetLoopSize(subject_number, result):
    v = 1
    loop_size = 0
    while v != result:
        v = (v * subject_number) % mod
        loop_size = loop_size + 1
    return loop_size

def Transform(subject_number, loop):
    v = 1
    for _ in range(loop):
        v = (v * subject_number) % mod
    return v

ls1 = GetLoopSize(7, pub1)
ls2 = GetLoopSize(7, pub2)

print(f"Loop Size 1: {ls1}")
print(f"Loop Size 2: {ls2}")

print(f"Encryption Key: {Transform(pub1, ls2)}")
print(f"Encryption Key: {Transform(pub2, ls1)}")

endtime = datetime.now()
spent = endtime-starttime
print(f"Time taken: {spent}")
