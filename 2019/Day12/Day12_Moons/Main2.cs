using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day12_Moons
{
    public class MoonsCalculator
    {
        public static void Run(int[] inPos, int[] inVel)
        {
            var origPos = Clone(inPos);
            var origVel = Clone(inVel);
            var pos = Clone(inPos);
            var vel = Clone(inVel);

            long step = 0;

        forloop:
            step++;

            if (step % 10000000 == 0)
            {
                Console.WriteLine(string.Concat(pos.Select(x => $"{x,6}")));
                Console.WriteLine(string.Concat(vel.Select(x => $"{x,6}")));
            }
            if (step % 1000000 == 0)
            {
                Console.WriteLine($"Step: {step,10} {CalcEnergy(pos),6} * {CalcEnergy(vel),6} = {CalcEnergy(pos) * CalcEnergy(vel),10}");
            }

            for (int a = 0; a < 12; a += 3)
                for (int b = 0; b < 12; b += 3)
                    for (int c = 0; c < 3; c++)
                    {
                        var p1 = pos[a + c];
                        var p2 = pos[b + c];
                        vel[a + c] += p1 == p2 ? 0 : p1 > p2 ? -1 : 1;
                    }

            for (int a = 0; a < 12; a++)
                pos[a] += vel[a];

            for (int i = 0; i < 12; i++)
            {
                if (pos[i] != origPos[i]) goto forloop;
                if (vel[i] != origVel[i]) goto forloop;
            }
            Console.WriteLine("Number of steps = " + step);
        }

        internal static void RunParallel(int[] v1)
        {
            var queues = new BlockingCollection<long>[3];
            var tasks = new Task[3];

            var nextPrintStep = 0;

            for (int i = 0; i < 3; i++)
            {
                queues[i] = new BlockingCollection<long>();
                var queue = queues[i];
                var ints = new[] { v1[i], v1[i + 3], v1[i + 6], v1[i + 9] };
                var task = new Task(() => { foreach (var thing in RunOne(ints)) queue.Add(thing); });
                tasks[i] = task;
                task.Start();
            }

            long step_x = 0;
            long step_y = 0;
            long step_z = 0;
            var queue_x = queues[0];
            var queue_y = queues[1];
            var queue_z = queues[2];

            do
            {
                if (step_x < step_y && step_x < step_z)
                {
                    step_x = queue_x.Take();
                }
                else if (step_y < step_z)
                {
                    step_y = queue_y.Take();
                }
                else
                {
                    step_z = queue_z.Take();
                }

                if (step_x > nextPrintStep)
                {
                    nextPrintStep += 1000000;
                    Console.WriteLine($"{step_x,10} {step_y,10} {step_z,10} ");
                }
            }
            while (step_x != step_y || step_y != step_z);
            Console.WriteLine($"{step_x,10} {step_y,10} {step_z,10} ");
        }

        internal static IEnumerable<long> RunOne(params int[] origPos)
        {
            int founds = 0;
            var pos = new int[] { origPos[0], origPos[1], origPos[2], origPos[3] };
            var vel = new int[4];
            long step = 0;
        forloop:
            step++;

            //if (step % 10000000 == 0)
            //{
            //    Console.WriteLine(string.Concat(pos.Select(x => $"{x,6}")));
            //    Console.WriteLine(string.Concat(vel.Select(x => $"{x,6}")));
            //}
            //if (step % 1000000 == 0)
            //{
            //    Console.WriteLine($"Step: {step,10} {CalcEnergy(pos),6} * {CalcEnergy(vel),6} = {CalcEnergy(pos) * CalcEnergy(vel),10}");
            //}

            for (int a = 0; a < 4; a++)
                for (int b = 0; b < 4; b++)
                {
                    if (a != b)
                    {
                        var p1 = pos[a];
                        var p2 = pos[b];
                        vel[a] += p1 == p2 ? 0 : p1 > p2 ? -1 : 1;
                    }
                }

            for (int a = 0; a < 4; a++)
                pos[a] += vel[a];

            for (int i = 0; i < 4; i++)
            {
                if (pos[i] != origPos[i]) goto forloop;
                if (vel[i] != 0) goto forloop;
            }
            yield return step;
            goto forloop;
            //Console.WriteLine("Number of steps = " + step);
            //if (founds++ < 10) goto forloop;
        }

        private static long CalcEnergy(int[] values)
        {
            return values.Sum(x => x > 0 ? x : -x);
        }
        private static long[] Clone(long[] pos)
        {
            return (long[])pos.Clone();
        }
        private static int[] Clone(int[] pos)
        {
            return (int[])pos.Clone();
        }
    }
}
