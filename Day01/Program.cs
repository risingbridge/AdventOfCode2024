using System.Collections.Generic;

List<string> input = File.ReadAllLines("./input.txt").ToList();
List<int> ListA = new List<int>();
List<int> ListB = new List<int>();

foreach (string line in input)
{
    string[] splitInput = line.Split("   ");
    if(int.TryParse(splitInput[0], out int value))
    {
        ListA.Add(value);
    }
    else
    {
        Console.WriteLine($"Error parsing the int from side A: {splitInput[0]}");
    }
    if(int.TryParse(splitInput[1], out int valueB))
    {
        ListB.Add(valueB);
    }
    else
    {
        Console.WriteLine($"Error parsing the int from side A: {splitInput[1]}");
    }
};

ListA.Sort();
ListB.Sort();
int diffSum = 0;
for(int i = 0; i <  ListA.Count; i++)
{
    int a = ListA[i];
    int b = ListB[i];
    int diff = Math.Abs(a - b);
    diffSum += diff;
}

Console.WriteLine($"Part 1: {diffSum}");

int simSum = 0;
foreach(int valueA in ListA)
{
    int simCount = 0;
    foreach(int valueB in ListB)
    {
        if(valueB == valueA)
        {
            simCount++;
        }
    }
    simSum += (valueA * simCount);
}
Console.WriteLine($"Part B: {simSum}");