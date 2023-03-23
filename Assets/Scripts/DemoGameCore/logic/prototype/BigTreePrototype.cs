using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.DemoGameCore.logic
{
    public class BigTreePrototype : AbstractConstructionPrototype
    {
        protected static ProficiencySpeedCalculator BIG_TREE_PROFICIENCY_SPEED_CALCULATOR = (thiz) =>
        {
            return 1;
        };

        public BigTreePrototype(Language language) : base(ConstructionPrototypeId.BIG_TREE, language, null)
        {

        }

        public override BaseConstruction getInstance(GridPosition position)
        {
            String id = prototypeId + "_" + System.Guid.NewGuid().ToString();
            AutoProficiencyConstruction construction = new AutoProficiencyConstruction(prototypeId, id, position, language);
            construction.proficiencySpeedCalculator = BIG_TREE_PROFICIENCY_SPEED_CALCULATOR;
            construction.destoryCostPack = DemoBuiltinConstructionsLoader.toPack(new Dictionary<string, int>());
            construction.destoryGainPack = DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.WOOD, 2000
                    ));

            construction.outputComponent.outputGainPack = (DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.WOOD, 1
                    )));

            construction.upgradeComponent.upgradeCostPack = (DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.COIN, 25
                    )));

            // FIXME for debug
            construction.saveData.proficiency = 47;
            return construction;
        }
    }
}
