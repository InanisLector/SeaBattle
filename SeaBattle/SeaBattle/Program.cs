using System;
using System.Runtime.InteropServices;

class Program
{
    /*
         *  "\x1b[48;5;" + s + "m" - set background color by index in table (0-255)
         *
         *  "\x1b[38;5;" + s + "m" - set foreground color by index in table (0-255)
         *
         *  "\x1b[48;2;" + r + ";" + g + ";" + b + "m" - set background by r,g,b values
         *
         *  "\x1b[38;2;" + r + ";" + g + ";" + b + "m" - set foreground by r,g,b values
         * 
         */

    #region DLL

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool SetConsoleMode(IntPtr hConsoleHandle, int mode);
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool GetConsoleMode(IntPtr handle, out int mode);
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr GetStdHandle(int handle);
    
    #endregion

    static void Main(string[] args)
    {
        var handle = GetStdHandle(-11);
        int mode;
        GetConsoleMode(handle, out mode);
        SetConsoleMode(handle, mode | 0x4);

        while (true)
        {
            SeaBattle game = new();
            game.Start();
        }
    }
}

class SeaBattle
{
    public void Start()
    {
        Init();

        GameCycle();
    }

    private void Init()
    {

    }

    private void GameCycle()
    {
        while (!CheckForWin())
        {
            Render();
        }
    }

    private bool CheckForWin()
    {
        return false;
    }

    private void Render()
    {

    }
}

struct Map
{
    private bool[,] _shipMap;
    private bool[,] _shotMap;

    public Map()
    {
    _shipMap = new bool[8, 8];
    _shotMap = new bool[8, 8];
}

    public enum mapType
    {
        shot,
        ship
    }

    public (bool ship, bool shot) this[int x, int y]
        => (_shipMap[x, y], _shotMap[x, y]);

    public (bool ship, bool shot) this[Vector2 vector]
        => (_shipMap[vector.x, vector.y], _shotMap[vector.x, vector.y]);

    public (bool ship, bool shot) this[int x, int y, mapType type]
        => (_shipMap[x, y], _shotMap[x, y]);

    public (bool ship, bool shot) this[Vector2 vector, mapType type]
        => (_shipMap[vector.x, vector.y], _shotMap[vector.x, vector.y]);
}

struct Vector2
{
    public int x;
    public int y;

    public static Vector2 Zero => new Vector2(0, 0);
    public static Vector2 Up => new Vector2(0, -1);
    public static Vector2 Down => new Vector2(0, 1);
    public static Vector2 Left => new Vector2(-1, 0);
    public static Vector2 Right => new Vector2(1, 0);


    public Vector2(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public Vector2 RotateByNinetyDegrees()
        => new Vector2(y, -x);

    public Vector2 RotateByNinetyTimesDegrees(int times)
    {
        times = (times + 4) % 4;

        return times switch
        {
            1 => new Vector2(y, -x),
            2 => new Vector2(-x, -y),
            3 => new Vector2(-y, x),
            _ => this
        };
    }

    public static Vector2 operator +(Vector2 v1, Vector2 v2)
        => new Vector2(v1.x + v2.x, v1.y + v2.y);

    public static Vector2 operator -(Vector2 v1, Vector2 v2)
        => new Vector2(v1.x - v2.x, v1.y - v2.y);

    public static Vector2 operator *(Vector2 v1, int multiplier)
        => new Vector2(v1.x * multiplier, v1.y * multiplier);

    public static bool operator ==(Vector2 v1, Vector2 v2)
        => v1.x == v2.x && v1.y == v2.y;

    public static bool operator !=(Vector2 v1, Vector2 v2)
        => !(v1 == v2);

    public override string ToString()
        => $"({x}, {y})";

    public void Deconstruct(out int x, out int y)
    {
        x = this.x;
        y = this.y;
    }
}
