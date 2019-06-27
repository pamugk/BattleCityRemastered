using Model.Basics;

namespace Model.Entities.Surfaces
{
    public class Surface : Entity
    {
        public Surfaces Kind { get; }

        public Surface(Point _coordinates, Surfaces surfaceKind) : base(_coordinates)
        {
            Kind = surfaceKind;
        }

        public Surface(int x, int y, Surfaces surfaceKind) : base(x, y)
        {
            Kind = surfaceKind;
        }

        public override string GetDescription()
        {
            return $"{Kind}_0";
        }
    }
}
