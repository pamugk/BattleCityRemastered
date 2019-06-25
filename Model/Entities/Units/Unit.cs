using System;
using Model.Basics;
using Model.Interfaces;

namespace Model.Entities.Units
{
    [Serializable]
    public class Unit : Entity, IDamagable, IMovable, IShooting, IUpgradable
    {
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

        public Unit(Point _coordinates) : base(_coordinates)
        {
        }

        public Unit(int x, int y) : base(x, y)
        {
        }

        public event EventHandler<IDamagable> ObjectWasDamaged;
        public event EventHandler<IDamagable> ObjectWasHealed;
        public event EventHandler<IDamagable> ObjectWasDestroyed;

        public override string GetDescription() => "";

        private void OnObjectDamaging() => ObjectWasDamaged?.Invoke(this, this);
        private void OnObjectHealing() => ObjectWasHealed?.Invoke(this, this);
        private void OnObjectDestruction() => ObjectWasDestroyed?.Invoke(this, this);
    }
}