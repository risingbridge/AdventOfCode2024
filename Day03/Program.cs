string input = File.ReadAllText("input.txt");

List<int> validInstructionPositions = new List<int>();
for(int i = 0; i < input.Length - 3; i++){
    string testString = input.Substring(i, 4);
    if(testString == "mul("){
        validInstructionPositions.Add(i);
    }
}

List<string> validInstructions = new List<string>();
foreach(int i in validInstructionPositions){
    int startPos = i + 3;
    int endPos = startPos;
    for(int j = startPos; j < input.Length;j++){
        if(input[j] == ')'){
            endPos = j;
            break;
        }
    }
    string instruction = input.Substring(startPos, endPos - startPos);
    string reducedinstruction = "mul" + instruction + ")";
    reducedinstruction = reducedinstruction.Replace("mul(","").Replace(")","");
    if(reducedinstruction.Split(',').Length == 2){
        validInstructions.Add("mul(" + reducedinstruction + ")");
    }
}

int sum = 0;
foreach(string inst in validInstructions){
    sum += mul(inst);
}
Console.WriteLine($"Part One: {sum}");

//PART TWO
Dictionary<int, string>instructinoSet = new Dictionary<int, string>();
validInstructions.Clear();
foreach(int i in validInstructionPositions){
    int startPos = i + 3;
    int endPos = startPos;
    for(int j = startPos; j < input.Length;j++){
        if(input[j] == ')'){
            endPos = j;
            break;
        }
    }
    string instruction = input.Substring(startPos, endPos - startPos);
    string reducedinstruction = "mul" + instruction + ")";
    reducedinstruction = reducedinstruction.Replace("mul(","").Replace(")","");
    if(reducedinstruction.Split(',').Length == 2){
        validInstructions.Add("mul(" + reducedinstruction + ")");
        instructinoSet.Add(i, "mul(" + reducedinstruction + ")");
    }
}

for(int i = 0; i < input.Length; i++){
    // do()
    // don't()
    if(i < input.Length - 4){
        if(input.Substring(i,4) == "do()"){
            instructinoSet.Add(i, "do()");
        }
    }
    if(i < input.Length - 7){
        if(input.Substring(i,7) == "don't()"){
            instructinoSet.Add(i, "don't()");
        }
    }
}

Dictionary<int, string> sortedInstructions = instructinoSet.OrderBy(x => x.Key).ToDictionary();
sum = 0;
bool isEnabled = true;
foreach(KeyValuePair<int, string> inst in sortedInstructions){
    if(inst.Value == "don't()"){
        isEnabled = false;
    }else if(inst.Value == "do()"){
        isEnabled = true;
    }else{
        if(isEnabled){
            sum += mul(inst.Value);
        }
    }
}

Console.WriteLine("Part two: " + sum);


//Functions
int mul(string instruction){
    string reduced = instruction.Replace("mul(","").Replace(")","");
    int a = 0;
    int b = 0;
    if(int.TryParse(reduced.Split(',')[0], out int aa)){
        a = aa;
    }
    if(int.TryParse(reduced.Split(',')[1], out int bb)){
        b = bb;
    }
    return a*b;
}