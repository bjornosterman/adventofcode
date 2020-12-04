using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class IntMachine
    {
        long[] _data;
        int _opIndex = 0;
        long _base = 0;

        public IntMachine(long[] data)
        {
            _data = data;
        }

        public IntMachine(string program)
        {
            _data = program.Split(",").Select(long.Parse).ToArray();
        }

        public Action<long> Output;
        public Func<long> Input;
        public IEnumerable<long> EnumerableInput;

        public void Run()
        {
            for (; _opIndex < _data.Length;)
            {
                var opData = (int)_data[_opIndex];
                var opCode = (opData % 100);
                opData /= 100;
                var p1mode = opData % 10;
                opData /= 10;
                var p2mode = opData % 10;
                opData /= 10;
                var p3mode = opData % 10;

                long p1, p2, p3;

                switch (opCode)
                {
                    case 1: // add
                        p1 = Get(_opIndex + 1, p1mode);
                        p2 = Get(_opIndex + 2, p2mode);
                        Set(_opIndex + 3, p3mode, p1 + p2);
                        StepOp(4);
                        break;
                    case 2: // multiply
                        p1 = Get(_opIndex + 1, p1mode);
                        p2 = Get(_opIndex + 2, p2mode);
                        Set(_opIndex + 3, p3mode, p1 * p2);
                        StepOp(4);
                        break;
                    case 3: // input
                        var value = In();
                        Set(_opIndex + 1, p1mode, value);
                        StepOp(2);
                        break;
                    case 4: // output
                        Out(Get(_opIndex + 1, p1mode));
                        StepOp(2);
                        break;
                    case 5: // if-true
                    case 6: // if-false
                        p1 = Get(_opIndex + 1, p1mode);
                        p2 = Get(_opIndex + 2, p2mode);
                        if (opCode == 5 && p1 != 0 || (opCode == 6 && p1 == 0))
                        {
                            _opIndex = (int)p2;
                        }
                        else
                        {
                            StepOp(3);
                        }
                        break;
                    case 7: // less-then
                    case 8: // equals
                        p1 = Get(_opIndex + 1, p1mode);
                        p2 = Get(_opIndex + 2, p2mode);
                        Set(_opIndex + 3, p3mode, (opCode == 7 && p1 < p2) || (opCode == 8 && p1 == p2) ? 1 : 0);
                        StepOp(4);
                        break;
                    case 9:
                        _base += Get(_opIndex + 1, p1mode);
                        StepOp(2);
                        break;
                    case 99:
                        return;
                    default:
                        throw new Exception("Unknown Operation " + _data[_opIndex]);
                }
            }
        }

        private void Out(long value)
        {
            if (Output == null)
            {
                Console.WriteLine("--> : " + value);
            }
            else
            {
                Output(value);
            }
        }

        private long In()
        {
            if (Input == null)
            {
                Console.Write("Give me number: ");
                var line = Console.ReadLine();
                return long.Parse(line);
            }
            return Input();
        }

        private void StepOp(int length)
        {
            _opIndex += length;
        }

        private void Set(long index, int mode, long value)
        {
            switch (mode)
            {
                case 0:
                    SetData((int)GetData((int)index), value);
                    break;
                case 1:
                    SetData(index, value);
                    break;
                case 2:
                    SetData(_base + GetData((int)index), value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetData(long index, long value)
        {
            EnsureLength((int)index);
            _data[index] = value;
        }

        private void EnsureLength(int index)
        {
            if (index >= _data.Length)
            {
                var newData = new long[(index + 1) * 2];
                Array.Copy(_data, newData, _data.Length);
                _data = newData;
            }
        }

        private long Get(long index, int mode)
        {
            return mode switch
            {
                0 => GetData((int)GetData((int)index)),
                1 => GetData((int)index),
                2 => GetData((int)(_base + GetData((int)index))),
                _ => throw new ArgumentOutOfRangeException(),
            };
            //return mode == 0
            //        ? _data[_data[index]]
            //        : _data[index];
        }

        private long GetData(int index)
        {
            if (index < 0) throw new IndexOutOfRangeException();
            EnsureLength(index);
            return _data[index];
        }
    }
}
