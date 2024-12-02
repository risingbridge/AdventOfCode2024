using System.ComponentModel.Design.Serialization;
using ConsoleTables;

List<string> input = File.ReadAllLines("input.txt").ToList();

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
    Reports.Add(report);
}

//Solve part 1
int safeCount = 0;
ConsoleTable table = new ConsoleTable("Report", "Status");
List<Report> unsafeReports = new List<Report>();
foreach (Report report in Reports)
{
    bool safe = IsSafe(report);
    string status = "Safe";
    if(safe == false)
    {
        status = "Unsafe";
        unsafeReports.Add(report);
    }
    else
    {
        safeCount++;
    }
    table.AddRow(PrintReport(report), status);
}
//table.Write();
Console.WriteLine($"Part one: {safeCount}");


//Solve part 2
List<Report> DampedReports = new List<Report>();
foreach(Report report in unsafeReports)
{
    //Test to check if i can remove one number to make it safe
    for (int i = 0; i < report.Levels.Count; i++)
    {
        Report testReport = new Report();
        testReport.Levels = new List<int>(report.Levels);
        testReport.Levels.RemoveAt(i);
        if (IsSafe(testReport))
        {
            DampedReports.Add(testReport);
            break;
        }
    }
}

Console.WriteLine($"Part two: {safeCount + DampedReports.Count}");



string PrintReport(Report report)
{
    string output = string.Empty;

    foreach(int i in report.Levels)
    {
        output += i.ToString() + " ";
    }
    output = output.Trim();
    return output;
}

bool IsSafe(Report report)
{
    bool isSafe = true;
    int largestGap = 0;
    for(int i = 0; i < report.Levels.Count - 1; i++)
    {
        int a = report.Levels[i];
        int b = report.Levels[i + 1];
        int gap = Math.Abs(a - b);
        if(gap > largestGap)
        {
            largestGap = Math.Abs(a - b);
        }
    }
    if(largestGap > 3)
    {
        return false;
    }

    bool isIncreasing = false;
    if (report.Levels[0] < report.Levels[1])
    {
        isIncreasing = true;
    }
    for(int i = 0; i < report.Levels.Count - 1; i++)
    {
        int a = report.Levels[i];
        int b = report.Levels[i + 1];
        if (isIncreasing)
        {
            if(a > b || a == b){
                return false;
            }
        }
        else
        {
            if(a < b || a == b)
            {
                return false;
            }
        }
    }

    return isSafe;
}

class Report
{
    public List<int> Levels { get; set; }
}