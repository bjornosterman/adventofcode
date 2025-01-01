// See https://aka.ms/new-console-template for more information

using System.Reflection;
using System.IO;
using System.Runtime.InteropServices;

var types = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.Name.StartsWith("Day"))
                .OrderBy(x => x.Name.EndsWith("x"))
                .ThenBy(x => x.Name)
                .ToArray();

var last_day = types.Last();


#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
var day = (Day)Assembly.GetExecutingAssembly().CreateInstance(last_day.Name);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

// foreach (var type in types)
// {
//     Console.WriteLine(type);
// }

var day_name = last_day.Name.Substring(0, 5).ToLower();

var sample = File.ReadAllLines($"../../../{day_name}/sample.txt");
var input = File.ReadAllLines($"../../../{day_name}/input.txt");

#pragma warning disable CS8602 // Dereference of a possibly null reference.
day.Init(sample, input);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
var before = DateTime.Now;
day.Run();
var after = DateTime.Now;
Console.WriteLine("Time taken: " + (after - before));