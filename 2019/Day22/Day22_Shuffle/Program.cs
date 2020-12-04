using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Numerics;

namespace Day22_Shuffle
{
    class Program
    {
        static void Main(string[] args)
        {
            //var deck = new ForwardMathDeck(10007, 2019);
            ////deck.RunWithInput(Input.Puzzle);
            //deck.Run();
            //Console.WriteLine("Mul: " + deck.Multiplier + ", Shifter: " + deck.Shifter);
            //deck.Run();
            //Console.WriteLine("Mul: " + deck.Multiplier + ", Shifter: " + deck.Shifter);
            //deck.Run();
            //Console.WriteLine("Mul: " + deck.Multiplier + ", Shifter: " + deck.Shifter);
            //deck.Run();
            //Console.WriteLine("Mul: " + deck.Multiplier + ", Shifter: " + deck.Shifter);
            //Console.WriteLine(deck);
            ////Console.WriteLine("Origin: " + deck.GetOrigin(deck.Pos));

            //deck = new ForwardMathDeck(10007, 2019);
            //deck.Run();
            //Console.WriteLine("Mul: " + deck.Multiplier + ", Shifter: " + deck.Shifter);
            //deck.Multiply(4);
            //Console.WriteLine("Mul: " + deck.Multiplier + ", Shifter: " + deck.Shifter);
            //Console.WriteLine(deck);


            //deck = new ForwardMathDeck(10007, 2019);
            //deck.Run();
            //Console.WriteLine("Mul: " + deck.Multiplier + ", Shifter: " + deck.Shifter);
            //deck.Multiply(2);
            //Console.WriteLine("Mul: " + deck.Multiplier + ", Shifter: " + deck.Shifter);
            //deck.Apply(deck.Multiplier, deck.Shifter);
            //Console.WriteLine("Mul: " + deck.Multiplier + ", Shifter: " + deck.Shifter);

            //Console.WriteLine(deck);

            //var deck2 = new ForwardDeck(10007, 2019, Input.Puzzle);
            //deck2.Run();
            //Console.WriteLine(deck2.Pos);
            //deck2.Run();
            //Console.WriteLine(deck2.Pos);

            //var digits = 10;
            //var multipliers = new BigInteger[digits];
            //var shifters = new BigInteger[digits];

            //deck = new ForwardMathDeck(10007, 2019);
            //deck.Run();
            //for (int i = 0; i < digits; i++)
            //{
            //    multipliers[i] = deck.Multiplier;
            //    shifters[i] = deck.Shifter;
            //    deck.Multiply(10);
            //}

            //Console.WriteLine("Run 123");
            //deck = new ForwardMathDeck(10007, 2019);
            //for(int i = 0;i<123;i++)
            //{
            //    deck.Run();
            //}
            //Console.WriteLine("Mul: " + deck.Multiplier + ", Shifter: " + deck.Shifter);
            //Console.WriteLine(deck);

            //Console.WriteLine("Apply 123");
            //deck = new ForwardMathDeck(10007, 2019);
            //deck.Apply(multipliers[2], shifters[2]);
            //deck.Apply(multipliers[1], shifters[1]);
            //deck.Apply(multipliers[1], shifters[1]);
            //deck.Apply(multipliers[0], shifters[0]);
            //deck.Apply(multipliers[0], shifters[0]);
            //deck.Apply(multipliers[0], shifters[0]);
            //Console.WriteLine("Mul: " + deck.Multiplier + ", Shifter: " + deck.Shifter);
            //Console.WriteLine(deck);

            //Console.WriteLine("LoopApply 123");
            //deck = new ForwardMathDeck(10007, 2019);
            //var runs = 123;
            //var index = 0;
            //while(runs > 0)
            //{
            //    var applies = runs % 10;
            //    for(int i = 0;i<applies;i++)
            //    {
            //        deck.Apply(multipliers[index], shifters[index]);
            //    }
            //    index++;
            //    runs /= 10;
            //}
            //Console.WriteLine("Mul: " + deck.Multiplier + ", Shifter: " + deck.Shifter);
            //Console.WriteLine(deck);

            //Console.WriteLine("Mul: " + deck.Multiplier);
            //Console.WriteLine("Shifter: " + deck.Shifter);

            //Console.WriteLine($"Origin for {deck.Pos} is {deck.GetOrigin(deck.Pos)}");

            //Console.WriteLine("Should be 2019: " + ReverseFollowDeck.Origin(3995, 1023, 10007));


            //var deckMulti = new ForwardMathDeck(10007, 2019);
            //deckMulti.RunWithInput(Input.Puzzle);
            //deckMulti.Multiply(2);
            //Console.WriteLine("Mul2: " + deck);
            //Console.WriteLine("Origin: " + ReverseFollowDeck.Origin((deckMulti.Pos+deckMulti.Shifter).Mod(deckMulti.s), deckMulti.Multiplier, deckMulti.Size));






            var digits = 16;
            var multipliers = new BigInteger[digits];
            var shifters = new BigInteger[digits];

            var deck = new ForwardMathDeck(119315717514047, 2019);
            deck.Run();
            for (int i = 0; i < digits; i++)
            {
                multipliers[i] = deck.Multiplier;
                shifters[i] = deck.Shifter;
                deck.Multiply(10);
            }

            var deckPuzzle = new ForwardMathDeck(119315717514047, 87570316820120);
            var runs = 101741582076661;
            var index = 0;
            while (runs > 0)
            {
                var applies = runs % 10;
                for (int i = 0; i < applies; i++)
                {
                    deckPuzzle.Apply(multipliers[index], shifters[index]);
                }
                index++;
                runs /= 10;
            }
            //deckPuzzle.Multiply(101741582076661);
            Console.WriteLine("DeckPuzzle: " + deckPuzzle);
            Console.WriteLine("Answer: " + deckPuzzle.GetOrigin(2020));
            Console.WriteLine("Check: " + deckPuzzle.GetPos(deckPuzzle.GetOrigin(2020)));

            //var rev = GetLines(Input.Tmp).Reverse();
            //Console.WriteLine(string.Concat(rev.Select(x => x + "\n")));
            //return;
            //var deck = new ForwardDeck(10, 1, Input.TestAll);
            //deck.Run();
            //Console.WriteLine(deck.Pos);

            //var deck = new ForwardDeck(10007, 2019, Input.Puzzle);
            //deck.Run();
            //Console.WriteLine(deck.Pos);

            //var deck2 = new ReverseFollowDeck(10007, 3143, Input.Puzzle);
            //deck2.Run();
            //Console.WriteLine(deck2.Pos);

            //var timer = Stopwatch.StartNew();
            //long last = 0;
            //for (long i = 0; i < 10; i++)
            //{
            //    var deck2 = new ForwardDeck(119315717514047, last, Input.Puzzle);
            //    deck2.Run();
            //    //Console.WriteLine(deck2.Pos + ": diff = " + (deck2.Pos-last) );
            //    last = deck2.Pos;
            //    Console.WriteLine($"Found low number ({last}) Whoho at interval: {i}");
            //    if (last < 1_000_000)
            //    {
            //        Console.WriteLine($"Found low number ({last}) Whoho at interval: {i}");
            //    }
            //}
            //timer.Stop();
            //Console.WriteLine("Time: " + timer.ElapsedMilliseconds);

            //var deck = new ForwardDeck(119315717514047, 87570316820120, Input.Puzzle);
            //deck.Run();
            //Console.WriteLine(deck.Pos);

        }

    }


}
