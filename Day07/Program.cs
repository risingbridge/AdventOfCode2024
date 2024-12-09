List<string> input = File.ReadAllLines("test.txt").ToList();
List<CalibrationOutput> output = new List<CalibrationOutput>();
foreach (string s in input)
{
    string[] split = s.Split(": ");
    int value = int.Parse(split[0]);
    List<int> numbers = new List<int>();
    string[] numbersSplit = split[1].Split(" ");
    foreach (string number in numbersSplit)
    {
        numbers.Add(int.Parse(number));
    }
    output.Add(new CalibrationOutput(value, numbers));
}
char[] operators = new char[] { '+', '*'};
bool IsValidCalibration(CalibrationOutput output)
{
    char[] possibleCombinations = GenerateCombinations(output.Operators.Count, operators);
    return false;
}

char[] GenerateCombinations(int n, char[] operators)
{
    //Function to generate an array with all possible combinations of the operators, with array length of n
    char[] combinations = new char[n];
    

}

class CalibrationOutput
{
    public CalibrationOutput(int _value, List<int> list)
    {
        TestValue = _value;
        Operators = new List<int>(list);
    }
    public int TestValue { get; set; }
    public List<int> Operators { get; set; }
}