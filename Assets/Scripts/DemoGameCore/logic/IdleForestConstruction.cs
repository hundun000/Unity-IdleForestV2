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
        protected int autoProficiencyProgress = 0;
        protected const int AUTO_PROFICIENCY_SECOND_MAX = 2; // 2秒生长一次

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
            doUpgrade();
        }

        override public Boolean canClickEffect()
        {
            return canUpgrade();
        }

        private void doUpgrade()
        {
            List<ResourcePair> upgradeCostRule = upgradeComponent.upgradeCostPack.modifiedValues;
            gameContext.storageManager.modifyAllResourceNum(upgradeCostRule, false);
            saveData.level = (saveData.level + 1);
            if (!levelComponent.workingLevelChangable)
            {
                saveData.workingLevel = (saveData.level);
            }
            updateModifiedValues();
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

            autoProficiencyProgress++;
            int proficiencyFrameCountMax = AUTO_PROFICIENCY_SECOND_MAX * gameContext.LOGIC_FRAME_PER_SECOND;
            if (autoProficiencyProgress >= proficiencyFrameCountMax)
            {
                autoProficiencyProgress = 0;
                tryProficiencyOnce();
            }
        }

        private void tryProficiencyOnce()
        {
            proficiencyComponent.changeProficiency(1);
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

        override public long calculateModifiedOutput(long baseValue, int level, int proficiency)
        {
            return (long)(baseValue * level * (proficiency / 50.0));
        }

        override public long calculateModifiedOutputCost(long baseValue, int level, int proficiency)
        {
            return (long)(baseValue * level * (proficiency / 50.0));
        }
    }
}
