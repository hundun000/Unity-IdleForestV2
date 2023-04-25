using hundun.idleshare.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DemoGameCore.logic
{
    internal class IdleForestOutputComponent : BaseAutoOutputComponent
    {


        public IdleForestOutputComponent(BaseConstruction construction) : base(construction)
        {
        }

        override public long calculateModifiedOutputGain(long baseValue, int level, int proficiency)
        {
            return (long)((baseValue * level) * (0.5 + proficiency / 100.0 * 0.5));
        }

        override public long calculateModifiedOutputCost(long baseValue, int level, int proficiency)
        {
            return (long)((baseValue * level) * (0.5 + proficiency / 100.0 * 0.5));
        }
    }
}
