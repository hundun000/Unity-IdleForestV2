using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.DemoGameCore.logic
{
    public class BigFactoryPrototype : AbstractConstructionPrototype
    {
        protected static ProficiencySpeedCalculator BIG_FACTORY_PROFICIENCY_SPEED_CALCULATOR = (thiz) =>
        {
            return 1;
        };
        public BigFactoryPrototype(Language language) : base(ConstructionPrototypeId.BIG_FACTORY, language, null)
        {

        }

        public override BaseConstruction getInstance(GridPosition position)
        {
            String id = prototypeId + "_" + System.Guid.NewGuid().ToString();
            AutoProficiencyConstruction construction = new AutoProficiencyConstruction(prototypeId, id, position, descriptionPackage);
            construction.proficiencySpeedCalculator = BIG_FACTORY_PROFICIENCY_SPEED_CALCULATOR;
            construction.destoryCostPack = DemoBuiltinConstructionsLoader.toPack(new Dictionary<string, int>());
            construction.destoryGainPack = DemoBuiltinConstructionsLoader.toPack(new Dictionary<string, int>());

            construction.outputComponent.outputGainPack = (DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.COIN, 2
                    )));

            construction.upgradeComponent.upgradeCostPack = (DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.COIN, 10
                    )));

            // FIXME for debug
            construction.saveData.proficiency = 47 + UnityEngine.Random.Range(0, 2) * 50;

            return construction;
        }
    }
}
