using Model.Basics;
using Model.Interfaces;
using System;
using System.Collections.Generic;

namespace Model.Entities.Walls
{
    public enum Materials { Brick, Concrete }

    [Serializable]
    public sealed class Wall : Entity, IDamagable
    {
        private static Dictionary<Materials, double> startHealth = new Dictionary<Materials, double>
            { { Materials.Brick, 1.0 }, { Materials.Concrete, 2.0 } };

        private Materials material;
        public Materials Material
        {
            get => material;
            set
            {
                material = value;
                health = startHealth[material];
            }
        }

        private double health;
        public double Health
        {
            get => health;
            set
            {
                if (health == 0 || value == health)
                    return;
                double newHealth = 0;
                if (value > health)
                {
                    OnObjectHealing();
                    newHealth = value;
                }
                else
                {
                    OnObjectDamaging();
                    newHealth = value > 0 ? value : 0;
                }
                health = newHealth;
                if (health == 0)
                    OnObjectDestruction();
            }
        }

        public event EventHandler<IDamagable> ObjectWasDamaged;
        public event EventHandler<IDamagable> ObjectWasHealed;
        public event EventHandler<IDamagable> ObjectWasDestroyed;

        public Wall(int x, int y, Materials type) : base(x, y) => Material = type;
        public Wall(Point _coordinates, Materials type) : base(_coordinates) => Material = type;

        public override string GetDescription() => $"{material}Wall";

        private void OnObjectDamaging() => ObjectWasDamaged?.Invoke(this, this);
        private void OnObjectHealing() => ObjectWasHealed?.Invoke(this, this);
        private void OnObjectDestruction() => ObjectWasDestroyed?.Invoke(this, this);
    }
}