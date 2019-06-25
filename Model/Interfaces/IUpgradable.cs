using System;

namespace Model.Interfaces
{
    public interface IUpgradable
    {
        void Upgrade();
        event EventHandler<IUpgradable> MaxLevelReached;
    }
}