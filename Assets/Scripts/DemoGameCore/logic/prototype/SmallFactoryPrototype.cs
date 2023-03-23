using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.DemoGameCore.logic
{
    public class SmallFactoryPrototype : AbstractConstructionPrototype
    {
        protected static ProficiencySpeedCalculator SMALL_FACTORY_PROFICIENCY_SPEED_CALCULATOR = (thiz) =>
        {
            return 1;
        };
        public SmallFactoryPrototype(Language language) : base(ConstructionPrototypeId.SMALL_FACTORY, language,
            DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.COIN, 100
                    ))
            )
        {

        }

        public override BaseConstruction getInstance(GridPosition position)
        {
            String id = prototypeId + "_" + System.Guid.NewGuid().ToString();
            AutoProficiencyConstruction construction = new AutoProficiencyConstruction(prototypeId, id, position, language);
            construction.proficiencySpeedCalculator = SMALL_FACTORY_PROFICIENCY_SPEED_CALCULATOR;
            construction.destoryCostPack = DemoBuiltinConstructionsLoader.toPack(new Dictionary<string, int>());
            construction.destoryGainPack = DemoBuiltinConstructionsLoader.toPack(new Dictionary<string, int>());

            construction.outputComponent.outputGainPack = (DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.COIN, 1
                    )));

            construction.upgradeComponent.upgradeCostPack = (DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.COIN, 10
                    )));
            construction.upgradeComponent.transferCostPack = (DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.COIN, 50
                    )));
            construction.upgradeComponent.transferCostPack.modifiedValuesDescription = "转职费用";
            construction.upgradeComponent.transferConstructionPrototypeId = ConstructionPrototypeId.BIG_FACTORY;

            // FIXME for debug
            construction.saveData.proficiency = 47 + UnityEngine.Random.Range(0, 2) * 50;

            return construction;
        }
    }
}
