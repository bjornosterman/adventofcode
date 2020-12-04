using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Day16_FFT
{
    class Program
    {
        static void Main(string[] args)
        {
            //var input = "12345678".Select(x => x - '0').ToArray();
            //var input = "80871224585914546619083218645595".Select(x => x - '0').ToArray();
            var input = "59750530221324194853012320069589312027523989854830232144164799228029162830477472078089790749906142587998642764059439173975199276254972017316624772614925079238407309384923979338502430726930592959991878698412537971672558832588540600963437409230550897544434635267172603132396722812334366528344715912756154006039512272491073906389218927420387151599044435060075148142946789007756800733869891008058075303490106699737554949348715600795187032293436328810969288892220127730287766004467730818489269295982526297430971411865028098708555709525646237713045259603175397623654950719275982134690893685598734136409536436003548128411943963263336042840301380655801969822".Select(x => x - '0').ToArray();
            var tmpinput = new int[input.Length * 10000];
            for (int i = 0; i < 10000; i++) Array.Copy(input, 0, tmpinput, i * input.Length, input.Length);
            input = tmpinput;


            var alg = new FttAlgo(input);
            alg.Run2();

            //Console.ReadKey();

            for (int i = 0; i < 100; i++)
            {
                //alg.Print();
                alg.Iterate();
            }
            var pattern = new[] { 1, 0, -1, 0 };
            for (int a = 0; a < 100; a++)
            {
                Console.WriteLine(a);
                var output = new int[input.Length];
                //Print(input);
                for (int i = 0; i < input.Length; i++)
                {
                    //Print(GetMultipliers(i, pattern).Take(input.Length));
                    output[i] = Math.Abs(input.Zip(GetMultipliers(i, pattern)).Sum(x => x.First * x.Second)) % 10;
                    Console.Write(".");
                }
                input = output;
            }
            Print(input);
            Console.ReadKey();
        }

        private static void Print(IEnumerable<int> input)
        {
            Console.WriteLine(new string(input.Select(x => (char)(x + '0')).ToArray()));
        }

        static IEnumerable<int> GetMultipliers(int index, int[] pattern)
        {
            return Enumerable.Repeat(0, index).Concat(MultiplyPattern(index + 1, pattern));
            //var enumerator = MultiplyPattern(index+1, pattern);
            //foreach (var multiplyer in enumerator.Skip(index))
            //{
            //    yield return multiplyer;
            //}
        }
        static IEnumerable<int> MultiplyPattern(int count, int[] pattern)
        {
            for (int i = 0; ; i = (i + 1) % pattern.Length)
            {
                for (int b = 0; b < count; b++)
                {
                    yield return pattern[i];
                }
            }
        }
    }
    public class FttAlgo
    {
        private int[] input;

        public FttAlgo(int[] input)
        {
            this.input = input;
        }

        public void Iterate()
        {
            var length = input.Length;
            var output = new int[length];
            var watch = Stopwatch.StartNew();
            for (int a = 5975053 - 200; a < length; a++)
            {
                int o = 0;
                int d = 0;
                for (int b = a; b < length;)
                {
                    int length2 = b + a + 1 > length ? length : (b + a + 1);
                    for (; b < length2;) o += input[b++];
                    b += (a + 1);
                    if (b < length)
                    {
                        length2 = b + a + 1 > length ? length : (b + a + 1);
                        for (; b < length2;) o -= input[b++];
                    }
                    b += (a + 1);
                }
                if (a % 6500 == 0)
                {
                    Console.WriteLine(watch.Elapsed);
                    watch = Stopwatch.StartNew();
                }
                output[a] = (o < 0 ? -o : o) % 10;
            }
            input = output;
        }

        public void Run2()
        {
            for (int a = 0; a < 100; a++)
            {
                var sum = 0;
                for (int i = 6500000 - 1; i >= 5500000; i--)
                {
                    sum += input[i];
                    input[i] = sum % 10;
                }
                Console.Write(".");
            }
            int[] output = new int[8];
            Array.Copy(input, 5975053, output, 0, 8);
            
            Console.WriteLine(new string(output.Select(x => (char)(x + '0')).ToArray()));
            Console.ReadLine();
        }

        public void Run()
        {
            //5975053
            var sections = GetSections(5975053, 5975059).ToList();
            sections = ConsolidateSections(sections);
            var sections2 = ConsolidateSections(sections.SelectMany(x => GetSections(x.StartIndex, x.EndIndex)));
            var sections3 = ConsolidateSections(sections2.SelectMany(x => GetSections(x.StartIndex, x.EndIndex)));
        }

        public List<Section> ConsolidateSections(IEnumerable<Section> sections)
        {
            var items = sections.OrderBy(x => x.StartIndex).ThenBy(x => x.EndIndex).ToList();
            int i = 0;
            while (i < items.Count - 1)
            {
                var item1 = items[i];
                var item2 = items[i + 1];
                if (item1._multiplier == item2._multiplier && item1.EndIndex + 1 >= item2.StartIndex)
                {
                    items[i] = new Section(item1.StartIndex, item2.EndIndex, item1._multiplier);
                    items.RemoveAt(i + 1);
                }
                else
                {
                    i++;
                }
            }
            return items;
        }

        public class Section
        {
            public int StartIndex;
            public int EndIndex;
            public int _multiplier;
            public int _length;

            public Section(int startIndex, int endIndex, int multiplier)
            {
                StartIndex = startIndex;
                EndIndex = endIndex;
                _multiplier = multiplier;
                _length = EndIndex - StartIndex + 1;
            }


        }

        public IEnumerable<Section> GetSections(int startIndex, int endIndex)
        {
            var length = input.Length;
            for (int a = startIndex; a <= endIndex; a++)
            {
                for (int b = a; b < length;)
                {
                    int length2 = b + a + 1 > length ? length : (b + a + 1);

                    yield return new Section(b, length2 - 1, 1);
                    b += length2;
                    b += (a + 1);
                    if (b < length)
                    {
                        length2 = b + a + 1 > length ? length : (b + a + 1);
                        yield return new Section(b, length2 - 1, -1);
                        b += length2;
                    }
                    b += (a + 1);
                }
            }
        }

        internal void Print()
        {
            Console.WriteLine(new string(input.Select(x => (char)(x + '0')).ToArray()));
        }
    }
}
