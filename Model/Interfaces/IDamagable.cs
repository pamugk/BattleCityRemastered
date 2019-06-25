using System;

namespace Model.Interfaces
{
    public interface IDamagable
    {
        double Health { get; set; }
        event EventHandler<IDamagable> ObjectWasDamaged;
        event EventHandler<IDamagable> ObjectWasHealed;
        event EventHandler<IDamagable> ObjectWasDestroyed;
    }
}