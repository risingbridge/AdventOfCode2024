List<string> input = File.ReadAllLines("input.txt").ToList();
List<string> ruleStrings = new List<string>();
List<string> contentStrings = new List<string>();
foreach (string s in input)
{
    if (s.Contains('|'))
    {
        ruleStrings.Add(s);
    }else if (s.Contains(','))
    {
        contentStrings.Add(s);
    }
}

Dictionary<int, List<int>> rules = new Dictionary<int, List<int>>();
foreach (string r in ruleStrings)
{
    int before = int.Parse(r.Split('|')[0]);
    int after = int.Parse(r.Split('|')[1]);
    if (rules.ContainsKey(before))
    {
        rules[before].Add(after);
    }
    else
    {
        rules.Add(before, new List<int> { after });
    }
}

List<List<int>> content = new List<List<int>>();
foreach (string s in contentStrings)
{
    int[] numbers = s.Split(',').Select(int.Parse).ToArray();
    content.Add(numbers.ToList());
}

//Solve part 1
List<List<int>> legalLines = new List<List<int>>();
List<List<int>> illegalLines = new List<List<int>>();
foreach (List<int> s in content)
{
    bool isLegal = true;
    for (int i = s.Count - 1; i >= 0; i--)
    {
        int check = s.ElementAt(i);
        if (rules.ContainsKey(check))
        {
            List<int> rule = rules[check];
            for (int j = i - 1; j >= 0; j--)
            {
                if (rule.Contains(s.ElementAt(j)))
                {
                    isLegal = false;
                }
            }
        }
    }
    Console.WriteLine();
    if (isLegal)
    {
        legalLines.Add(s);
    }
    else
    {
        illegalLines.Add(s);
    }
}

Console.Clear();
int sum = 0;
foreach (List<int> l in legalLines)
{
    for (int i = 0; i < l.Count; i++)
    {
        if (i == Math.Floor((float)l.Count / (float)2))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(l.ElementAt(i) + " ");
            sum += l.ElementAt(i);
            Console.ResetColor();
        }
        else
        {
            Console.Write(l.ElementAt(i) + " ");
        }
    }
    Console.WriteLine();
}

Console.WriteLine($"Part One: {sum}");

//Solve Part Two
//Sort the rule-lists
foreach (KeyValuePair<int, List<int>> kvp in rules)
{
    kvp.Value.Sort();
}

Console.WriteLine($"solving {illegalLines.Count} lines");
int counter = 0;
foreach (List<int> l in illegalLines)
{
    Console.WriteLine($"Solving line {counter}");;
    counter++;
    for (int stupidFix = 0; stupidFix < 2000; stupidFix++)
    {
        for (int i = l.Count - 1; i >= 0; i--)
        {
            int check = l.ElementAt(i);
            if (rules.ContainsKey(check))
            {
                List<int> rule = rules[check];
                for (int j = 0; j < l.Count; j++)
                {
                    if (rule.Contains(l.ElementAt(j)))
                    {
                        int index = j - 1;
                        if (index < 0)
                        {
                            index = 0;
                        }
                        l.Insert(index, l[i]);
                        l.RemoveAt(i + 1);
                        break;
                    }
                }
            }
        }
    }
    
}




int partTwoSum = 0;
foreach (List<int> l in illegalLines)
{
    for (int i = 0; i < l.Count; i++)
    {
        if (i == Math.Floor((float)l.Count / (float)2))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(l.ElementAt(i) + " ");
            partTwoSum += l.ElementAt(i);
            Console.ResetColor();
        }
        else
        {
            Console.Write(l.ElementAt(i) + " ");
        }
    }
    Console.WriteLine();
}
Console.WriteLine($"Part Two: {partTwoSum}");