using Model.Basics;

namespace Model.Entities.Surfaces
{
    public class Surface : Entity
    {
        public enum Kinds
        {
            Stone,
            Water,
            Ice,
            Sand,
            Lava
        }

        public Kinds Kind { get; }

        public Surface(Point _coordinates, Kinds surfaceKind) : base(_coordinates)
        {
            Kind = surfaceKind;
        }

        public Surface(int x, int y, Kinds surfaceKind) : base(x, y)
        {
            Kind = surfaceKind;
        }

        public override string GetDescription()
        {
            return $"{Kind}_0";
        }
    }
}
