using Model.Basics;

namespace Model.Entities
{
    [System.Serializable]
    public abstract class Entity
    {
        protected Point coordinates;
        public Point Coordinates => coordinates;

        protected Size size;
        public Size Size => size;

        protected Entity(int x, int y) => coordinates = new Point(x, y);
        protected Entity(Point _coordinates) => coordinates = _coordinates;

        public abstract string GetDescription();
    }
}