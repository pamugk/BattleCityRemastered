using Model.Basics;

namespace Model.Entities.Surfaces
{
    public enum Types { Stone, Water, Ice, Sand, Lava }

    [System.Serializable]
    public class Surface : Entity
    {
        public Types Type { get; }

        public Surface(int x, int y, Types type) : base(x, y) => Type = type;
        public Surface(Point _coordinates, Types type) : base(_coordinates) => Type = type;

        public override string GetDescription() => $"{Type}";
    }
}