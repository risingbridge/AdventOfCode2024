using System.Numerics;
//Load input
List<string> input = File.ReadAllLines("input.txt").ToList();
char[,] map = new char[input.Count, input[0].Length];
Vector2 playerPosition = new Vector2(0, 0);
FacingDirection playerDir = FacingDirection.Up;
FacingDirection defaultDir = FacingDirection.Up;
for (int y = 0; y < input.Count; y++)
{
    for (int x = 0; x < input[0].Length; x++)
    {
        map[y, x] = input[y][x];
        if (input[y][x] == '^')
        {
            playerPosition = new Vector2(x, y);
            map[y, x] = '.';
        }
    }
}
Vector2 playerDefaultPos = new Vector2(playerPosition.X, playerPosition.Y);
List<Vector2> distinctLocations = new List<Vector2>();
distinctLocations.Add(playerPosition);
int stepCounter = 0;
//Loop forever
while (true)
{
    //Move player forward
    //If it hits #, turn right
    //Continue
    Vector2 newPos = MoveForward(playerDir, playerPosition);
    if (newPos.X < 0 || newPos.Y < 0 || newPos.X >= map.GetLength(1) || newPos.Y >= map.GetLength(0))
    {
        //Console.WriteLine("Out of bounds!");
        break;
    }

    if (map[(int)newPos.Y, (int)newPos.X] == '#')
    {
        playerDir = TurnRightNighty(playerDir);
    }
    else
    {
        playerPosition = newPos;
        stepCounter++;
    }

    if (!distinctLocations.Contains(playerPosition))
    {
        distinctLocations.Add(playerPosition);
    }

    //Console.Clear();
    //PrintMap(map);
    //Thread.Sleep(10);
}
Console.WriteLine($"Part One: {distinctLocations.Count}");
//Console.WriteLine($"Used number of steps: {stepCounter}");

//Solve part two
int loopCount = 0;
for (int y = 0; y < map.GetLength(0); y++)
{
    for (int x = 0; x < map.GetLength(1); x++)
    {
        char c = map[y, x];
        if (c == '.')
        {
            char[,] workingMap = map.Clone() as char[,];
            playerPosition = new Vector2(playerDefaultPos.X, playerDefaultPos.Y);
            playerDir = defaultDir;
            //Try to place an obstacle
            workingMap[y, x] = '#';
            int currentStepCounter = 0;
            while (true)
            {
                //Move player forward
                //If it hits #, turn right
                //Continue
                Vector2 newPos = MoveForward(playerDir, playerPosition);
                if (newPos.X < 0 || newPos.Y < 0 || newPos.X >= workingMap.GetLength(1) || newPos.Y >= workingMap.GetLength(0))
                {
                    //Console.WriteLine("Out of bounds!");
                    //PrintMap(workingMap, new Vector2(x, y));
                    //Console.WriteLine($"Steps: {currentStepCounter}");
                    break;
                }

                if (workingMap[(int)newPos.Y, (int)newPos.X] == '#')
                {
                    playerDir = TurnRightNighty(playerDir);
                }
                else
                {
                    playerPosition = newPos;
                    currentStepCounter++;
                }

                if (!distinctLocations.Contains(playerPosition))
                {
                    distinctLocations.Add(playerPosition);
                }

                //Console.Clear();
                //PrintMap(map);
                //Thread.Sleep(10);
                if (currentStepCounter >= (stepCounter * 10))
                {
                    //Console.WriteLine("This loops!");
                    loopCount++;
                    break;
                }
            }

            //Console.WriteLine(); 
        }
    }
}
Console.WriteLine($"Part Two: {loopCount}");

//Function to visualize map
void PrintMap(char[,] map, Vector2? newObstalce)
{
    for (int y = 0; y < map.GetLength(0); y++)
    {
        for (int x = 0; x < map.GetLength(1); x++)
        {
            if (new Vector2(x, y) == playerPosition)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                switch (playerDir)
                {
                    case FacingDirection.Up:
                        Console.Write('^');
                        break;
                    case FacingDirection.Down:
                        Console.Write('v');
                        break;
                    case FacingDirection.Left:
                        Console.Write('<');
                        break;
                    case FacingDirection.Right:
                        Console.Write('>');
                        break;
                    default:
                        Console.Write('x');
                        break;
                }
                Console.ResetColor();
            }
            else
            {
                if (new Vector2(x, y) == newObstalce)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write('O');
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(map[y, x]);
                }
                
            }
        }
        Console.WriteLine();
    }
}

//Function to turn right
FacingDirection TurnRightNighty(FacingDirection currentDir)
{
    switch (currentDir)
    {
        case FacingDirection.Up:
            return FacingDirection.Right;
        case FacingDirection.Down:
            return FacingDirection.Left;
        case FacingDirection.Left:
            return FacingDirection.Up;
        case FacingDirection.Right:
            return FacingDirection.Down;
        default:
            return currentDir;
    }
}

//Function to move forward
Vector2 MoveForward(FacingDirection currentDir, Vector2 currentPos)
{
    Vector2 returnPos = new Vector2(currentPos.X, currentPos.Y);
    switch (currentDir)
    {
        case FacingDirection.Up:
            returnPos =  new Vector2(currentPos.X, currentPos.Y - 1);
            break;
        case FacingDirection.Down:
            returnPos =  new Vector2(currentPos.X, currentPos.Y + 1);
            break;
        case FacingDirection.Left:
            returnPos =  new Vector2(currentPos.X - 1, currentPos.Y);
            break;
        case FacingDirection.Right:
            returnPos =  new Vector2(currentPos.X + 1, currentPos.Y);
            break;
        default:
            return returnPos;
    }

    return returnPos;
}

public enum FacingDirection
{
    Up, Down, Left, Right
}