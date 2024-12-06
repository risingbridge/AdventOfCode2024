using System.ComponentModel.DataAnnotations;

List<string> input = File.ReadAllLines("input.txt").ToList();

foreach(string s in input){
    Console.WriteLine(s);
}

int xSize = input[0].Length;
int ySize = input.Count;
char[,] charMap = new char[xSize, ySize];
for(int y = 0; y < ySize; y++){
    for(int x = 0; x < xSize; x++){
        char c = input[y][x];
        charMap[y,x] = c;
    }
}
List<Vector2> Positions = new List<Vector2>();
for(int y = 0; y < charMap.GetLength(0); y++){
    for(int x = 0; x < charMap.GetLength(1); x++){
        Positions.Add(new Vector2(x,y));
    }
}

//XMAS
//Søkemønster:


Dictionary<string, Vector2> movements = new Dictionary<string, Vector2>();

// Legger til bevegelsene med retninger som nøkler
movements.Add("right", new Vector2(1, 0));     // Fremover
movements.Add("left", new Vector2(-1, 0));     // Bakover
movements.Add("down", new Vector2(0, 1));      // Nedover
movements.Add("up", new Vector2(0, -1));       // Oppover
movements.Add("downright", new Vector2(1, 1)); // Skrå ned høyre
movements.Add("downleft", new Vector2(-1, 1)); // Skrå ned venstre
movements.Add("upright", new Vector2(1, -1));  // Skrå opp høyre
movements.Add("upleft", new Vector2(-1, -1));  // Skrå opp venstre

string searchWord = "XMAS";
int searchCount = 0;
for (int y = 0; y < charMap.GetLength(0); y++) {
    for (int x = 0; x < charMap.GetLength(1); x++)
    {
        Vector2 currentPos = new Vector2(x,y);
        Vector2 searchPos = new Vector2(x, y);
        
        foreach (KeyValuePair<string, Vector2> dir in movements)
        {
            string searchString = charMap[y,x].ToString();
            for (int i = 1; i < searchWord.Length; i++)
            {
                //Console.WriteLine($"Searching for {searchString} at {dir.Key}");
                searchPos = AddVector2(currentPos, MultiplyVector2(dir.Value, i));
                if (searchPos.x < 0 || searchPos.x >= xSize || searchPos.y < 0 || searchPos.y >= ySize)
                {
                    break;
                }
                searchString += charMap[searchPos.y, searchPos.x];
            }

            if (searchString == searchWord)
            {
                searchCount++;
            }
        }
    }
}

Console.WriteLine($"Part One: {searchCount}");

//PART TWO
movements.Clear();
movements.Add("downright", new Vector2(1, 1)); // Skrå ned høyre
movements.Add("downleft", new Vector2(-1, 1)); // Skrå ned venstre
movements.Add("upright", new Vector2(1, -1));  // Skrå opp høyre
movements.Add("upleft", new Vector2(-1, -1));  // Skrå opp venstre

searchWord = "MAS";
searchCount = 0;
for (int y = 1; y < charMap.GetLength(0) - 1; y++)
{
    for (int x = 1; x < charMap.GetLength(1) - 1; x++)
    {
        Vector2 currentPos = new Vector2(x, y);
        string searchStringA = string.Empty;
        string searchStringB = string.Empty;
        List<Vector2> searchPositionsA = new List<Vector2>();
        searchPositionsA.Add(AddVector2(currentPos, movements["upright"]));
        searchPositionsA.Add(currentPos);
        searchPositionsA.Add(AddVector2(currentPos, movements["downleft"]));
        List<Vector2> searchPositionsB = new List<Vector2>();
        searchPositionsB.Add(AddVector2(currentPos, movements["downright"]));
        searchPositionsB.Add(currentPos);
        searchPositionsB.Add(AddVector2(currentPos, movements["upleft"]));
        foreach (Vector2 searchPosition in searchPositionsA)
        {
            searchStringA += charMap[searchPosition.y, searchPosition.x].ToString();
        }

        foreach (Vector2 searchPosition in searchPositionsB)
        {
            searchStringB += charMap[searchPosition.y, searchPosition.x].ToString();
        }
        if (searchStringA == searchWord && searchStringB == searchWord)
        {
            searchCount++;
        }else if (ReverseString(searchStringA) == searchWord && searchStringB == searchWord)
        {
            searchCount++;
        }else if (searchStringA == searchWord && ReverseString(searchStringB) == searchWord)
        {
            searchCount++;
        }else if (ReverseString(searchStringA) == searchWord && ReverseString(searchStringB) == searchWord)
        {
            searchCount++;
        }
    }
}
Console.WriteLine($"Part Two: {searchCount}");

Vector2 MultiplyVector2(Vector2 vector, int multiplier)
{
    return new Vector2(vector.x * multiplier, vector.y * multiplier);
}

Vector2 AddVector2(Vector2 a, Vector2 b)
{
    return new Vector2(a.x + b.x, a.y + b.y);
}

String ReverseString(String s)
{
    char[] charArray = s.ToCharArray();
    Array.Reverse(charArray);
    return new string(charArray);
}

class Vector2{
    public Vector2(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    public int x { get; set; }
    public int y { get; set; }
}