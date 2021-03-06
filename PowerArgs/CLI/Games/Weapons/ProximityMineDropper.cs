﻿using PowerArgs.Cli.Physics;

namespace PowerArgs.Games
{
    public class ProximityMineDropper : Weapon
    {
        public override WeaponStyle Style => WeaponStyle.Explosive;

        public override void FireInternal()
        {
            var mine = new ProximityMine();
            mine.MoveTo(Holder.Left, Holder.Top);
            SpaceTime.CurrentSpaceTime.Add(mine);
        }
    }
}
