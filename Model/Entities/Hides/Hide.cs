using Model.Basics;

namespace Model.Entities.Hides
{
    public enum Kinds { Forest, Fog }

    [System.Serializable]
    public class Hide : Entity
    {
        public Kinds Kind { get; }

        public Hide(int x, int y, Kinds kind) : base(x, y) => Kind = kind;
        public Hide(Point _coordinates, Kinds kind) : base(_coordinates) => Kind = kind;

        public override string GetDescription() => $"{Kind}";
    }
}