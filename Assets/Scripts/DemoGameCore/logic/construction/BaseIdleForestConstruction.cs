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
        public static ProficiencySpeedCalculator IDLE_FOREST_PROFICIENCY_SPEED_CALCULATOR = (thiz) =>
        {
            int neighborTreeCount = thiz.neighbors.Values.ToList()
                .Where(it => it != null && it.saveData.prototypeId.Equals(ConstructionPrototypeId.SMALL_TREE))
                .Count()
                ;
            int neighborFactoryCount = thiz.neighbors.Values.ToList()
                .Where(it => it != null)
                .Where(it => it.saveData.prototypeId.Equals(ConstructionPrototypeId.SMALL_FACTORY)
                    || it.saveData.prototypeId.Equals(ConstructionPrototypeId.MID_FACTORY)
                    || it.saveData.prototypeId.Equals(ConstructionPrototypeId.BIG_FACTORY)
                )
                .Count()
                ;
            int neighborLakeCount = thiz.neighbors.Values.ToList()
                .Where(it => it != null)
                .Where(it => it.saveData.prototypeId.Equals(ConstructionPrototypeId.LAKE))
                .Count()
                ;

            switch (thiz.prototypeId)
            { 
                case ConstructionPrototypeId.SMALL_TREE:
                case ConstructionPrototypeId.MID_TREE:
                case ConstructionPrototypeId.BIG_TREE:
                    return 1 + Math.Max(0, neighborTreeCount * 2 - 1) + neighborLakeCount;
                case ConstructionPrototypeId.SMALL_FACTORY:
                case ConstructionPrototypeId.MID_FACTORY:
                case ConstructionPrototypeId.BIG_FACTORY:
                    return 1 + Math.Max(0, neighborFactoryCount * 2 - 1) + neighborLakeCount;
                case ConstructionPrototypeId.LAKE:
                    return neighborTreeCount * -1 + neighborFactoryCount * 1;
                case ConstructionPrototypeId.DESERT:
                    return neighborTreeCount * 1 + neighborFactoryCount * -1;
                default:
                    return 0;
            }

            
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
            this.proficiencySpeedCalculator = IDLE_FOREST_PROFICIENCY_SPEED_CALCULATOR;

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

        

        

        override public long calculateModifiedOutputGain(long baseValue, int level, int proficiency)
        {
            return (long)((baseValue * level) * (0.5 + proficiency / 100.0 * 0.5));
        }

        override public long calculateModifiedOutputCost(long baseValue, int level, int proficiency)
        {
            return (long)((baseValue  * level) * (0.5 + proficiency / 100.0 * 0.5));
        }

        public override void onLogicFrame()
        {
            // base do nothing
        }
    }
}
