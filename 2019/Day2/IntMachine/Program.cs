using System;
using System.IO;
using System.Linq;

namespace IntMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            //RunWithInput(12, 2);
            var input = "1,0,0,3,1,1,2,3,1,3,4,3,1,5,0,3,2,9,1,19,1,19,5,23,1,23,5,27,2,27,10,31,1,31,9,35,1,35,5,39,1,6,39,43,2,9,43,47,1,5,47,51,2,6,51,55,1,5,55,59,2,10,59,63,1,63,6,67,2,67,6,71,2,10,71,75,1,6,75,79,2,79,9,83,1,83,5,87,1,87,9,91,1,91,9,95,1,10,95,99,1,99,13,103,2,6,103,107,1,107,5,111,1,6,111,115,1,9,115,119,1,119,9,123,2,123,10,127,1,6,127,131,2,131,13,135,1,13,135,139,1,9,139,143,1,9,143,147,1,147,13,151,1,151,9,155,1,155,13,159,1,6,159,163,1,13,163,167,1,2,167,171,1,171,13,0,99,2,0,14,0";
            var input_data = input.Split(",").Select(int.Parse).ToArray();

            for (int a = 0; a < input_data.Length; a++)
            {
                Console.WriteLine("a = " + a);
                for (int b = 0; b < input_data.Length; b++)
                {
                    var response = RunWithInput(input_data, a, b);
                    if (response == 19690720)
                    {
                        Console.WriteLine(a * 100 + b);
                        return;
                    }
                }
            }
        }

        static int RunWithInput(int[] input, int a, int b)
        {
            var data = new int[input.Length];
            try
            {
                Array.Copy(input, data, input.Length);
                Engine engine = null;
                engine = new Engine(data);
                data[1] = a; data[2] = b;
                engine.Run();
            }
            catch (Exception ex)
            {
                return -1;
            }
            return data[0];
        }
    }

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
            for (; _opIndex < _data.Length; _opIndex += 4)
            {
                switch (_data[_opIndex])
                {
                    case 1: // add
                        _data[_data[_opIndex + 3]] = _data[_data[_opIndex + 1]] + _data[_data[_opIndex + 2]];
                        break;
                    case 2: // multiply
                        _data[_data[_opIndex + 3]] = _data[_data[_opIndex + 1]] * _data[_data[_opIndex + 2]];
                        break;
                    case 99:
                        return;
                    default:
                        throw new Exception("Unknown Operation " + _data[_opIndex]);
                }
            }
        }
    }
}
