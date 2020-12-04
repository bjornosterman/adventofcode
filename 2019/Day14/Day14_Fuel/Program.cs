using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day14_Fuel
{
    class Program
    {
        static void Main(string[] args)
        {
            //            var input = @"9 ORE => 2 A
            //8 ORE => 3 B
            //7 ORE => 5 C
            //3 A, 4 B => 1 AB
            //5 B, 7 C => 1 BC
            //4 C, 1 A => 1 CA
            //2 AB, 3 BC, 4 CA => 1 FUEL";

            var input = File.ReadAllText("input.txt");

            //            var input = @"157 ORE => 5 NZVS
            //165 ORE => 6 DCFZ
            //44 XJWVT, 5 KHKGT, 1 QDVJ, 29 NZVS, 9 GPVTF, 48 HKGWZ => 1 FUEL
            //12 HKGWZ, 1 GPVTF, 8 PSHF => 9 QDVJ
            //179 ORE => 7 PSHF
            //177 ORE => 5 HKGWZ
            //7 DCFZ, 7 PSHF => 2 XJWVT
            //165 ORE => 2 GPVTF
            //3 DCFZ, 7 NZVS, 5 HKGWZ, 10 PSHF => 8 KHKGT";

            //            var input = @"2 VPVL, 7 FWMGM, 2 CXFTF, 11 MNCFX => 1 STKFG
            //17 NVRVD, 3 JNWZP => 8 VPVL
            //53 STKFG, 6 MNCFX, 46 VJHF, 81 HVMC, 68 CXFTF, 25 GNMV => 1 FUEL
            //22 VJHF, 37 MNCFX => 5 FWMGM
            //139 ORE => 4 NVRVD
            //144 ORE => 7 JNWZP
            //5 MNCFX, 7 RFSQX, 2 FWMGM, 2 VPVL, 19 CXFTF => 3 HVMC
            //5 VJHF, 7 MNCFX, 9 VPVL, 37 CXFTF => 6 GNMV
            //145 ORE => 6 MNCFX
            //1 NVRVD => 8 CXFTF
            //1 VJHF, 6 MNCFX => 4 RFSQX
            //176 ORE => 6 VJHF";

            //            var input = @"171 ORE => 8 CNZTR
            //7 ZLQW, 3 BMBT, 9 XCVML, 26 XMNCP, 1 WPTQ, 2 MZWV, 1 RJRHP => 4 PLWSL
            //114 ORE => 4 BHXH
            //14 VRPVC => 6 BMBT
            //6 BHXH, 18 KTJDG, 12 WPTQ, 7 PLWSL, 31 FHTLT, 37 ZDVW => 1 FUEL
            //6 WPTQ, 2 BMBT, 8 ZLQW, 18 KTJDG, 1 XMNCP, 6 MZWV, 1 RJRHP => 6 FHTLT
            //15 XDBXC, 2 LTCX, 1 VRPVC => 6 ZLQW
            //13 WPTQ, 10 LTCX, 3 RJRHP, 14 XMNCP, 2 MZWV, 1 ZLQW => 1 ZDVW
            //5 BMBT => 4 WPTQ
            //189 ORE => 9 KTJDG
            //1 MZWV, 17 XDBXC, 3 XCVML => 2 XMNCP
            //12 VRPVC, 27 CNZTR => 2 XDBXC
            //15 KTJDG, 12 BHXH => 5 XCVML
            //3 BHXH, 2 VRPVC => 7 MZWV
            //121 ORE => 7 VRPVC
            //7 XCVML => 6 RJRHP
            //5 BHXH, 4 VRPVC => 5 LTCX";

            var recepies = Parse(input);
            var r = new Refinery(recepies);
            r.Get("FUEL", 3209253);
            Console.WriteLine("Used ORE: " + r.UsedOre);
            r.Get("FUEL", 1);
            r.Get("FUEL", 1);
            r.Get("FUEL", 1);
            r.Get("FUEL", 1);
            r.Get("FUEL", 1);
            r.Get("FUEL", 1);
            r.Get("FUEL", 1);
            r.Get("FUEL", 1);
            r.Get("FUEL", 1);
            Console.WriteLine("Used ORE: " + r.UsedOre);

            var refinery = new Refinery(recepies);
            var fuel = 0;
            try
            {
                while (true)
                {
                    //var multi = 1;
                    var multi = refinery.UsedOre < 990000000000 ? 200 : 1;
                    refinery.Get("FUEL", multi);
                    fuel += multi;
                    if (refinery.Fuel % 1000 == 0)
                    {
                        Console.WriteLine("Fuel: " + fuel + ", Ore: + " + refinery.UsedOre);
                    }
                }
            }
            catch
            {
                Console.WriteLine("Produced FUEL: " + fuel);
                Console.WriteLine("Used ORE: " + refinery.UsedOre);
            }
        }



        public static List<Recepie> Parse(string input)
        {
            string line;
            var sr = new StringReader(input);
            var recepies = new List<Recepie>();
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Trim().Length == 0) continue;
                var recepie = new Recepie();
                var split1 = line.Split("=>");
                var split2 = split1[1].Trim().Split(" ");
                recepie.Quantity = long.Parse(split2[0]);
                recepie.Name = split2[1].Trim();
                foreach (var part in split1[0].Split(","))
                {
                    var part2 = part.Trim().Split(" ");
                    recepie.Ingridients.Add(new Ingridient() { Quantity = long.Parse(part2[0]), Type = part2[1].Trim() });
                }
                recepie.Ingridients.Reverse();
                recepies.Add(recepie);
            }
            return recepies;
        }
    }

    public class Refinery
    {
        public Dictionary<string, Recepie> RecepiesByName;
        private Dictionary<string, long> _shelf;
        public long UsedOre;
        public long Fuel => _shelf["FUEL"];


        public Refinery(List<Recepie> recepies)
        {
            RecepiesByName = recepies.ToDictionary(x => x.Name);
            _shelf = recepies.ToDictionary(x => x.Name, x => (long)0);
        }
        public void Get(string name, long quantity)
        {
            if (name == "ORE")
            {
                if (UsedOre + quantity > 1000000000000) throw new Exception();
                UsedOre += quantity;
                return;
            }

            if (_shelf[name] < quantity)
            {
                var recepie = RecepiesByName[name];

                var multiple = ((quantity - _shelf[name]) / recepie.Quantity) + 1L;

                foreach (var ingridient in recepie.Ingridients.OrderByDescending(x=>x.Quantity))
                {
                    Get(ingridient.Type, ingridient.Quantity * multiple);
                }
                _shelf[name] += (recepie.Quantity * multiple);
            }
            _shelf[name] -= quantity;
        }
    }

    public class Recepie
    {
        public string Name;
        public long Quantity;
        public List<Ingridient> Ingridients = new List<Ingridient>();
    }

    public class Ingridient
    {
        public long Quantity;
        public string Type;
    }
}
