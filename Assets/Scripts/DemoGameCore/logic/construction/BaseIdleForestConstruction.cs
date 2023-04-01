using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;
using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DemoGameCore.logic
{
    public class BaseIdleForestConstruction : BaseConstruction
    {
        static ProficiencySpeedCalculator DEFAULT = (thiz) =>
        {
            return 0;
        };
        public ProficiencySpeedCalculator proficiencySpeedCalculator = null;
        public BaseIdleForestConstruction(
            String prototypeId, 
            String id, 
            GridPosition position,
            DescriptionPackage descriptionPackage) : base(prototypeId, id)
        {

            this.descriptionPackage = descriptionPackage;

            

            OutputComponent outputComponent = new OutputComponent(this);
            this.outputComponent = (outputComponent);

            UpgradeComponent upgradeComponent = new UpgradeComponent(this);
            this.upgradeComponent = (upgradeComponent);

            LevelComponent levelComponent = new LevelComponent(this, false);
            this.levelComponent = (levelComponent);

            ProficiencyComponent proficiencyComponent = new ProficiencyComponent(this);
            this.proficiencyComponent = (proficiencyComponent);

            this.saveData.position = position;
            this.saveData.level = 1;
            this.saveData.workingLevel = 1;
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

        

        

        override public long calculateModifiedOutput(long baseValue, int level, int proficiency)
        {
            return (long)(baseValue * level * (proficiency / 50.0));
        }

        override public long calculateModifiedOutputCost(long baseValue, int level, int proficiency)
        {
            return (long)(baseValue * level * (proficiency / 50.0));
        }

        public override void onLogicFrame()
        {
            // base do nothing
        }
    }
}
