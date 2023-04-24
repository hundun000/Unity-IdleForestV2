using hundun.idleshare.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DemoGameCore.logic
{
    internal class IdleForestOutputComponent : OutputComponent
    {
        protected int autoOutputProgress = 0;

        public IdleForestOutputComponent(BaseConstruction construction) : base(construction)
        {
        }

        public override void onSubLogicFrame()
        {
            autoOutputProgress++;
            int outputFrameCountMax = this.autoOutputSecondCountMax * construction.gameplayContext.LOGIC_FRAME_PER_SECOND;
            if (autoOutputProgress >= outputFrameCountMax)
            {
                autoOutputProgress = 0;
                tryAutoOutputOnce();
            }
        }

        private void tryAutoOutputOnce()
        {
            if (!this.canOutput())
            {
                //gameContext.frontend.log(this.id, "canOutput");
                return;
            }
            //gameContext.frontend.log(this.id, "AutoOutput");
            this.doOutput();
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
