using System;

namespace IntMachineTest
{
    public class Engine
    {
        int[] _data;
        int _opIndex = 0;
        public Engine(int[] data)
        {
            _data = data;
        }

        public void Run()
        {
            for (; _opIndex < _data.Length;)
            {
                var opData = _data[_opIndex];
                var opCode = opData % 100;
                opData /= 100;
                var p1mode = opData % 10;
                opData /= 10;
                var p2mode = opData % 10;
                opData /= 10;
                var p3mode = opData % 10;

                int p1, p2, p3;

                switch (opCode)
                {
                    case 1: // add
                        p1 = Get(_opIndex + 1, p1mode);
                        p2 = Get(_opIndex + 2, p2mode);
                        p3 = Get(_opIndex + 3, 1);
                        Set(p3, p1 + p2);
                        StepOp(4);
                        break;
                    case 2: // multiply
                        p1 = Get(_opIndex + 1, p1mode);
                        p2 = Get(_opIndex + 2, p2mode);
                        p3 = Get(_opIndex + 3, 1);
                        Set(p3, p1 * p2);
                        StepOp(4);
                        break;
                    case 3: // input
                        p1 = Get(_opIndex + 1, 1);
                        Console.Write("Give me number: ");
                        var line = Console.ReadLine();
                        var value = int.Parse(line);
                        Set(p1, value);
                        StepOp(2);
                        break;
                    case 4: // input
                        Console.WriteLine("--> : " + Get(_opIndex + 1, 0));
                        StepOp(2);
                        break;
                    case 5: // if-true
                    case 6: // if-false
                        p1 = Get(_opIndex + 1, p1mode);
                        p2 = Get(_opIndex + 2, p2mode);
                        if (opCode == 5 && p1 != 0 || (opCode == 6 && p1 == 0))
                        {
                            _opIndex = p2;
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
                        p3 = Get(_opIndex + 3, 1);
                        Set(p3, (opCode == 7 && p1 < p2) || (opCode == 8 && p1 == p2) ? 1 : 0);
                        StepOp(4);
                        break;
                    case 99:
                        return;
                    default:
                        throw new Exception("Unknown Operation " + _data[_opIndex]);
                }
            }
        }

        private void StepOp(int length)
        {
            _opIndex += length;
        }

        private void Set(int index, int value)
        {
            _data[index] = value;
        }

        private int Get(int index, int mode)
        {
            return mode == 0
                    ? _data[_data[index]]
                    : _data[index];
        }
    }

}
