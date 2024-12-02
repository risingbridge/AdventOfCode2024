List<string> input = File.ReadAllLines("test.txt").ToList();

//Parser input
List<Report> Reports = new List<Report>();
foreach (string line in input)
{
    Report report = new Report();
    report.Levels = new List<int>();
    string[] data = line.Split(' ');
    foreach (string s in data)
    {
        if (int.TryParse(s, out int i))
        {
            report.Levels.Add(i);
        }
        else
        {
            Console.WriteLine("Error parsing");
        }
    }
}

//Solve part 1
foreach (Report report in Reports)
{
    Console.WriteLine(IsSafe(report));
}

bool IsSafe(Report report)
{
    bool isSafe = true;
    bool increasing = true;
    if (report.Levels[0] < report.Levels[1])
    {
        increasing = false;
    }
    for (int i = 0; i < report.Levels.Count - 1; i++)
    {
        int checkA = report.Levels[i];
        int checkB = report.Levels[i + 1];
        if (increasing)
        {
            if (checkA < checkB && checkA + 3 > checkB)
            {
                isSafe = true;
            }
            else
            {
                isSafe = false;
                break;
            }
        }
    }
    return isSafe;
}

class Report
{
    public List<int> Levels { get; set; }
}