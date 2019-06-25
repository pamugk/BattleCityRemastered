using Model.Basics;

namespace Model.Entities.Effects
{
    public enum Kinds { Boom, Shield, Respawn }

    [System.Serializable]
    public class Effect : Entity
    {
        public Kinds Kind { get; }

        public Effect(int x, int y, Kinds kind) : base(x, y) => Kind = kind;
        public Effect(Point _coordinates, Kinds kind) : base(_coordinates) => Kind = kind;

        public override string GetDescription() => $"{Kind}";
    }
}