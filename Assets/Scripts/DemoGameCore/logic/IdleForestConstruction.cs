using hundun.idleshare.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DemoGameCore.logic
{
    public class IdleForestConstruction : BaseConstruction
    {
        protected int autoOutputProgress = 0;
        protected int autoGrowProgress = 0;
        protected const int AUTO_GROW_SECOND_COUNT_MAX = 2; // 2秒生长一次

        public IdleForestConstruction(String prototypeId, String id
                ) : base(prototypeId, id)
        {
        }

        override protected void printDebugInfoAfterConstructed()
        {
        
        }


        override public void onClick()
        {
            if (!canClickEffect())
            {
                return;
            }
        }

        override public Boolean canClickEffect()
        {
            return false;
        }


        override public void onLogicFrame()
        {
            autoOutputProgress++;
            int outputFrameCountMax = outputComponent.autoOutputSecondCountMax * gameContext.LOGIC_FRAME_PER_SECOND;
            if (autoOutputProgress >= outputFrameCountMax)
            {
                autoOutputProgress = 0;
                tryAutoOutputOnce();
            }

            autoGrowProgress++;
            int growFrameCountMax = AUTO_GROW_SECOND_COUNT_MAX * gameContext.LOGIC_FRAME_PER_SECOND;
            if (autoGrowProgress >= growFrameCountMax)
            {
                autoGrowProgress = 0;
                tryGrowOnce();
            }
        }

        private void tryGrowOnce()
        {
            if (!levelComponent.canChangeWorkingLevel(1))
            {
                //gameContext.frontend.log(this.id, "canOutput");
                return;
            }
            levelComponent.changeWorkingLevel(1);
        }

        private void tryAutoOutputOnce()
        {
            if (!canOutput())
            {
                //gameContext.frontend.log(this.id, "canOutput");
                return;
            }
            //gameContext.frontend.log(this.id, "AutoOutput");
            if (outputComponent.hasCost())
            {
                gameContext.storageManager.modifyAllResourceNum(outputComponent.outputCostPack.modifiedValues, false);
            }
            if (outputComponent.outputGainPack != null)
            {
                gameContext.storageManager.modifyAllResourceNum(outputComponent.outputGainPack.modifiedValues, true);
            }
        }

        override public long calculateModifiedOutput(long baseValue, int level)
        {
            return baseValue * level;
        }

        override public long calculateModifiedOutputCost(long baseValue, int level)
        {
            return baseValue * level;
        }
    }
}
