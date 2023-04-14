using IntVector2;

namespace SeaBattle.scripts
{
    public struct Map
    {
        private bool[,] _shipMap;
        private bool[,] _shotMap;

        public const int Width = 10;
        public const int Height = 10;

        public Map()
        {
            _shipMap = new bool[Width, Height];
            _shotMap = new bool[Width, Height];

            _shipMap = BaseMap();
        }

        private bool[,] BaseMap()
        {
            var map = new bool[10, 10]
            {
                {false, false, false, true, true, true, false, false, true, false},
                {false, false, false, false, false, false, false, false, true, false},
                {true, false, false, false, false, false, false, false, false, false},
                {false, false, true, false, false, false, false, false, false, false},
                {false, false, false, false, true, false, false, false, true, false},
                {true, false, false, false, true, false, false, false, true, false},
                {true, false, false, false, false, false, false, false, true, false},
                {true, false, false, false, false, true, false, true, false, false},
                {true, false, false, false, false, true, false, true, false, false},
                {false, false, false, false, false, false, false, true, false, false},
            };



            return map;
        }

        public bool Shoot(Vector2 point)
        {
            _shotMap[point.x, point.y] = true;

            return _shipMap[point.x, point.y];
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

        public bool this[int x, int y, mapType type]
            => type == mapType.shot ? _shotMap[x, y] : _shipMap[x, y];

        public bool this[Vector2 vector, mapType type]
            => type == mapType.shot ? _shotMap[vector.x, vector.y] : _shipMap[vector.x, vector.y];
    }
}
