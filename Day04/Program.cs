using System.ComponentModel.DataAnnotations;

List<string> input = File.ReadAllLines("test.txt").ToList();

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
        Console.Write(charMap[y,x]);
        Positions.Add(new Vector2(x,y));
    }
    Console.WriteLine();
}

//XMAS
//Søkemønster:
//Fremover
//Bakover
//Opp
//Ned
//Skrått opp høyre
//Skrått opp venstre
//Skrått ned høyre
//Skrått ned venstre
int searchCount = 0;
foreach(Vector2 position in Positions){
    string searchString = "XMAS";
    //Søk fremover
    if(position.x + searchString.Length <= xSize){
        string search = string.Empty;
        for(int i = 0; i < searchString.Length; i++){
            search += charMap[position.x + i, position.y];
        }
        if(search == searchString){
            searchCount++;
            Console.WriteLine(position.x + " " + position.y);
        }
    }
    //Søk bakover
}

Console.WriteLine($"Search: {searchCount}");


class Vector2{
    public Vector2(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    public int x { get; set; }
    public int y { get; set; }
}