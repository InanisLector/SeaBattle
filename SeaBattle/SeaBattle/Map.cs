using IntVector2;

namespace SeaBattle
{
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
}
