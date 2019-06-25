using Model.Basics;
using Model.Interfaces;

namespace Model.Entities.Ammos
{
    public enum Types { Bullet, Shell, HEShell }

    [System.Serializable]
    public class Ammo : Entity, IMovable
    {
        public Types Type { get; }

        public Ammo(int x, int y, Types type) : base(x, y) => Type = type;
        public Ammo(Point _coordinates, Types type) : base(_coordinates) => Type = type;

        public override string GetDescription() => $"{Type}";
    }
}