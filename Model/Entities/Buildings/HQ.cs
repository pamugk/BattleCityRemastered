using System;
using Model.Basics;
using Model.Interfaces;

namespace Model.Entities.Buildings
{
    [Serializable]
    public sealed class HQ : Entity, IDamagable
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

        public event EventHandler<IDamagable> ObjectWasDamaged;
        public event EventHandler<IDamagable> ObjectWasHealed;
        public event EventHandler<IDamagable> ObjectWasDestroyed;

        private const double startHealth = 3;

        public HQ(int x, int y) : base(x, y) => health = startHealth;
        public HQ(Point _coordinates) : base(_coordinates) => health = startHealth;

        public override string GetDescription() => health > 0 ? "HQ" : "DestroyedHQ";

        private void OnObjectDamaging() => ObjectWasDamaged?.Invoke(this, this);
        private void OnObjectHealing() => ObjectWasHealed?.Invoke(this, this);
        private void OnObjectDestruction() => ObjectWasDestroyed?.Invoke(this, this);
    }
}