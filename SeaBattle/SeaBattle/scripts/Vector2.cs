namespace IntVector2
{
    #pragma warning disable CS0661 // Тип определяет оператор == или оператор !=, но не переопределяет Object.GetHashCode()
    #pragma warning disable CS0660 // Тип определяет оператор == или оператор !=, но не переопределяет Object.Equals(object o)
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
}