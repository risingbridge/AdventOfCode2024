using System.Numerics;

List<string> input = File.ReadAllLines("./test.txt").ToList();
char[,] map = new char[input.Count(), input[0].Length];
int xMax = map.GetLength(1);
int yMax = map.GetLength(0);

for(int y  = 0; y < input.Count; y++)
{
    for(int x = 0; x < input[0].Length; x++)
    {
        map[y,x] = input[y][x];
    }
}

PrintMap(map);
Dictionary<char, List<Vector2>> antennaLocations = new Dictionary<char, List<Vector2>>();
for(int y = 0; y < map.GetLength(0); y++)
{
    for(int x = 0; x < map.GetLength(1); x++)
    {
        char id = map[y,x];
        Vector2 pos = new Vector2(x, y);
        if(id != '.')
        {
            if (antennaLocations.ContainsKey(id))
            {
                antennaLocations[id].Add(pos);
            }
            else
            {
                antennaLocations.Add(id, new List<Vector2>());
                antennaLocations[id].Add(pos);
            }
        }
    }
}

//Calculate the antinodes
List<Vector2> antinodePositions = new List<Vector2>();
foreach(KeyValuePair<char,List<Vector2>> antenna in antennaLocations)
{
    for (int i = 0; i < antenna.Value.Count; i++)
    {
        Vector2 currentPos = antenna.Value[i];
        for (int j = 0; j < antenna.Value.Count; j++)
        {
            Vector2 pos2 = antenna.Value[j];
            if (pos2 != currentPos)
            {
                Vector2 antiMove = pos2 - currentPos;
            }
        }
    }
}

void PrintMap(char[,] map)
{
    for(int y = 0;y < map.GetLength(0); y++)
    {
        for(int x = 0; x < map.GetLength(1); x++)
        {
            Console.Write(map[y,x]);
        }
        Console.WriteLine();
    }
}