using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Amplifier
{
    class Program
    {
        static void Main(string[] args)
        {
            //var program = "3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0";
            //var program = "3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0";
            //var program = "3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0";
            var program = "3,8,1001,8,10,8,105,1,0,0,21,34,43,60,81,94,175,256,337,418,99999,3,9,101,2,9,9,102,4,9,9,4,9,99,3,9,102,2,9,9,4,9,99,3,9,102,4,9,9,1001,9,4,9,102,3,9,9,4,9,99,3,9,102,4,9,9,1001,9,2,9,1002,9,3,9,101,4,9,9,4,9,99,3,9,1001,9,4,9,102,2,9,9,4,9,99,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,99,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,1,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,1001,9,2,9,4,9,99,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,2,9,4,9,99,3,9,101,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,101,1,9,9,4,9,3,9,101,1,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1002,9,2,9,4,9,99,3,9,1001,9,1,9,4,9,3,9,1001,9,1,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,99";

            long max_value = 0;
            long[][] max_input = new[] { new long[] { } };

            //foreach (var input in GenerateInputs(0))
            //{
            //    var result = Run(Parse(program), input);
            //    if (result > max_value)
            //    {
            //        max_value = result;
            //        max_input = input;
            //    }
            //}

            foreach (var input in GenerateInputs(5))
            {
                var result = Run2(Parse(program), input);
                if (result > max_value)
                {
                    max_value = result;
                    max_input = input;
                }
            }

            Console.WriteLine("Result: " + max_value + ": " + string.Join(", ", max_input.Select(x => x[0])));
        }

        public static IEnumerable<long[][]> GenerateInputs(long offset)
        {
            for (int a = 0; a < 5; a++)
                for (int b = 0; b < 4; b++)
                    for (int c = 0; c < 3; c++)
                        for (int d = 0; d < 2; d++)
                        {
                            var values = new List<long> { 0, 1, 2, 3, 4 };
                            var i1 = values[a] + offset;
                            values.RemoveAt(a);
                            var i2 = values[b] + offset;
                            values.RemoveAt(b);
                            var i3 = values[c] + offset;
                            values.RemoveAt(c);
                            var i4 = values[d] + offset;
                            var i5 = values[1 - d] + offset;

                            yield return new[] {
                                new[] { i1, 0 },
                                new[] { i2 },
                                new[] { i3 },
                                new[] { i4 },
                                new[] { i5} };

                        }
        }

        private static long Run(long[] program, long[][] values)
        {
            var engines = new List<Engine>();
            var pipes = new List<Queue<long>>();

            foreach (var value in values)
            {
                pipes.Add(new Queue<long>(value));
                engines.Add(new Engine(Clone(program)));
            }
            var return_pipe = new Queue<long>();
            pipes.Add(return_pipe);

            for (int i = 0; i < engines.Count; i++)
            {
                engines[i].Input = pipes[i].Dequeue;
                engines[i].Output = pipes[i + 1].Enqueue;
                engines[i].Run();
            }

            return pipes[pipes.Count - 1].Dequeue();
        }

        private static  long Run2(long[] program, long[][] values)
        {
            var engines = new List<Engine>();
            var pipes = new List<Pipe>();

            foreach (var value in values)
            {
                pipes.Add(new BlockingPipe(value));
                engines.Add(new Engine(Clone(program)));
            }

            for (int i = 0; i < engines.Count; i++)
            {
                engines[i].Input = pipes[i].Output;
                engines[i].Output = pipes[(i + 1) % engines.Count].Input;
            }

            var tasks = engines.Select(x => Task.Run(x.Run)).ToArray();
             Task.WhenAll(tasks).Wait();

            var result = pipes[0].Output();
            foreach (var pipe in pipes) pipe.Dispose();
            return result;
        }

        private static long[] Clone(long[] program)
        {
            var clone = new long[program.Length];
            program.CopyTo(clone, 0);
            return clone;
        }

        private static long[] Parse(string text)
        {
            return text.Split(",").Select(long.Parse).ToArray();
        }
    }

    public abstract class Pipe:IDisposable
    {
        public abstract long Output();
        public abstract void Input(long value);

        public virtual void Dispose()
        {
        }
    }
    public class QueuePipe : Pipe
    {
        private Queue<long> Queue = new Queue<long>();
        public override void Input(long item) => Queue.Enqueue(item);
        public override long Output() => Queue.Dequeue();
        public QueuePipe(params long[] values)
        {
            foreach (var value in values) Input(value);
        }
    }

    public class BlockingPipe : Pipe
    {
        private BlockingCollection<long> Queue = new BlockingCollection<long>();
        public override void Input(long item) => Queue.Add(item);
        public override long Output() => Queue.Take();
        public BlockingPipe(params long[] values)
        {
            foreach (var value in values) Input(value);
        }
        public override void Dispose()
        {
            Queue.Dispose();
        }
    }

    public class Engine
    {
        long[] _data;
        int _opIndex = 0;
        int _base = 0;

        public Engine(long[] data)
        {
            _data = data;
        }

        public Action<long> Output;
        public Func<long> Input;

        public void Run()
        {
            for (; _opIndex < _data.Length;)
            {
                var opData = (int)_data[_opIndex];
                if (opData == 203) ;
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
                        _base += (int)Get(_opIndex + 1, p1mode);
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
                    SetData(_base + index, value);
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

        private long Get(int index, int mode)
        {
            switch (mode)
            {
                case 0:
                    return GetData((int)GetData(index));
                case 1:
                    return GetData(index);
                case 2:
                    return GetData(_base + index);
                default:
                    throw new ArgumentOutOfRangeException();
            }
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

    //public class Engine
    //{
    //    int[] _data;
    //    int _opIndex = 0;
    //    public Engine(int[] data)
    //    {
    //        _data = data;
    //    }

    //    public Action<int> Output;
    //    public Func<int> Input;

    //    public void Run()
    //    {
    //        for (; _opIndex < _data.Length;)
    //        {
    //            var opData = _data[_opIndex];
    //            var opCode = opData % 100;
    //            opData /= 100;
    //            var p1mode = opData % 10;
    //            opData /= 10;
    //            var p2mode = opData % 10;
    //            opData /= 10;
    //            var p3mode = opData % 10;

    //            int p1, p2, p3;

    //            switch (opCode)
    //            {
    //                case 1: // add
    //                    p1 = Get(_opIndex + 1, p1mode);
    //                    p2 = Get(_opIndex + 2, p2mode);
    //                    p3 = Get(_opIndex + 3, 1);
    //                    Set(p3, p1 + p2);
    //                    StepOp(4);
    //                    break;
    //                case 2: // multiply
    //                    p1 = Get(_opIndex + 1, p1mode);
    //                    p2 = Get(_opIndex + 2, p2mode);
    //                    p3 = Get(_opIndex + 3, 1);
    //                    Set(p3, p1 * p2);
    //                    StepOp(4);
    //                    break;
    //                case 3: // input
    //                    p1 = Get(_opIndex + 1, 1);
    //                    var value = In();
    //                    Set(p1, value);
    //                    StepOp(2);
    //                    break;
    //                case 4: // input
    //                    Out(Get(_opIndex + 1, 0));
    //                    StepOp(2);
    //                    break;
    //                case 5: // if-true
    //                case 6: // if-false
    //                    p1 = Get(_opIndex + 1, p1mode);
    //                    p2 = Get(_opIndex + 2, p2mode);
    //                    if (opCode == 5 && p1 != 0 || (opCode == 6 && p1 == 0))
    //                    {
    //                        _opIndex = p2;
    //                    }
    //                    else
    //                    {
    //                        StepOp(3);
    //                    }
    //                    break;
    //                case 7: // less-then
    //                case 8: // equals
    //                    p1 = Get(_opIndex + 1, p1mode);
    //                    p2 = Get(_opIndex + 2, p2mode);
    //                    p3 = Get(_opIndex + 3, 1);
    //                    Set(p3, (opCode == 7 && p1 < p2) || (opCode == 8 && p1 == p2) ? 1 : 0);
    //                    StepOp(4);
    //                    break;
    //                case 99:
    //                    return;
    //                default:
    //                    throw new Exception("Unknown Operation " + _data[_opIndex]);
    //            }
    //        }
    //    }

    //    private void Out(int value)
    //    {
    //        if (Output == null)
    //        {
    //            Console.WriteLine("--> : " + value);
    //        }
    //        else
    //        {
    //            Output(value);
    //        }
    //    }

    //    private int In()
    //    {
    //        if (Input == null)
    //        {
    //            Console.Write("Give me number: ");
    //            var line = Console.ReadLine();
    //            return int.Parse(line);
    //        }
    //        return Input();
    //    }

    //    private void StepOp(int length)
    //    {
    //        _opIndex += length;
    //    }

    //    private void Set(int index, int value)
    //    {
    //        _data[index] = value;
    //    }

    //    private int Get(int index, int mode)
    //    {
    //        return mode == 0
    //                ? _data[_data[index]]
    //                : _data[index];
    //    }
    //}
}
