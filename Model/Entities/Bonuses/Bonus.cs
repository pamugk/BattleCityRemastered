using Model.Basics;

namespace Model.Entities.Bonuses
{
    public enum Specifications
    { Shield, TimeFreeze, Fortification, Upgrade, Extermination, Live, FullUpgrade }

    [System.Serializable]
    public class Bonus : Entity
    {
        public Specifications Specification { get; }

        public Bonus(int x, int y, Specifications specification) : base(x, y) => Specification = specification;
        public Bonus(Point _coordinates, Specifications specification) : base(_coordinates) => Specification = specification;

        public override string GetDescription() => $"{Specification}";
    }
}