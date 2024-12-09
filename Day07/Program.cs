using System.Data;
using System.Numerics;

List<string> input = File.ReadAllLines("input.txt").ToList();
List<CalibrationOutput> Outputs = new List<CalibrationOutput>();
foreach (string s in input)
{
    string[] split = s.Split(": ");
    long value = long.Parse(split[0]);
    List<int> numbers = new List<int>();
    string[] numbersSplit = split[1].Split(" ");
    foreach (string number in numbersSplit)
    {
        numbers.Add(int.Parse(number));
    }
    Outputs.Add(new CalibrationOutput(value, numbers));
}
List<char> PossibleOperators = new List<char> { '+', '*' };

long sum = 0;
foreach(CalibrationOutput output in Outputs)
{
    if (IsValidCalibration(output))
    {
        //Console.WriteLine(output.Sum);
        sum += output.Sum;
    }
}
Console.WriteLine($"Part One: {sum}");

PossibleOperators.Add('|');
sum = 0;
foreach (CalibrationOutput output in Outputs)
{
    if (IsValidCalibration(output))
    {
        //Console.WriteLine(output.Sum);
        sum += output.Sum;
    }
}
Console.WriteLine($"Part Two: {sum}");

bool IsValidCalibration(CalibrationOutput o)
{
    //Generer alle mulige løsninger
    bool isValid = false;
    int numberOfOperators = o.Values.Count - 1;
    List<string> possibleSolutions = CreatePosibleSolutions(PossibleOperators, numberOfOperators);
    //Test alle mulige løsninger
    foreach (string possibleSolution in possibleSolutions) {
        string equation = string.Empty;
        List<string> equationList = new List<string>();
        for (int i = 0; i < o.Values.Count; i++)
        {
            equation += o.Values[i].ToString();
            equationList.Add(o.Values[i].ToString());
            if(i < possibleSolution.Length)
            {
                equation += possibleSolution[i].ToString();
                equationList.Add(possibleSolution[i].ToString());
            }
        }
        //Console.Write($"{o.Sum}: {equation} = {EvaluateEquation(equationList)}");
        if (EvaluateEquation(equationList) == o.Sum)
        {
            //Console.WriteLine(" True");
            isValid = true;
        }
        else
        {
            //Console.WriteLine(" False");
        }
    }
    return isValid;
}

long EvaluateEquation(List<string> eq)
{
    long sum = long.Parse(eq[0]);
    for(int i = 1; i < eq.Count; i++)
    {
        string s = eq[i].ToString();
        if (PossibleOperators.Contains(s[0]))
        {
            switch (s[0])
            {
                case '*':
                    sum *= long.Parse(eq[i + 1].ToString());
                    break;
                case '+':
                    sum += long.Parse(eq[i + 1].ToString());
                    break;
                case '|':
                    string A = sum.ToString();
                    string B = eq[i + 1].ToString();
                    sum = long.Parse(A + B);
                    break;
                default:
                    break;
            }
        }
    }
    return sum;
}

List<string> CreatePosibleSolutions(List<char> operators, int n)
{
    List<string> output = new List<string>();
    Generate(operators, "", n, output);


    return output;
}
void Generate(List<char> operators, string current, int n, List<string> output)
{
    string returnString = current;

    if(returnString.Length == n)
    {
        output.Add(returnString);
        return;
    }

    foreach (char op in operators) {
        Generate(operators, returnString + op, n, output);
    }
}


class CalibrationOutput
{
    public CalibrationOutput(long _value, List<int> list)
    {
        Sum = _value;
        Values = new List<int>(list);
    }
    public long Sum { get; set; }
    public List<int> Values { get; set; }
}