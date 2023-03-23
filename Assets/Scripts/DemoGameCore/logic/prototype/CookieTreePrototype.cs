using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.DemoGameCore.logic
{
    public class CookieTreePrototype : AbstractConstructionPrototype
    {

        public CookieTreePrototype(Language language) : base(ConstructionPrototypeId.COOKIE_TREE, language,
            DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.COIN, 100
                    ))
            )
        {

        }

        public override BaseConstruction getInstance(GridPosition position)
        {
            String id = prototypeId + "_" + System.Guid.NewGuid().ToString();
            BaseConstruction construction = new AutoProficiencyConstruction(prototypeId, id, position, language, 1);
            construction.destoryCostPack = DemoBuiltinConstructionsLoader.toPack(new Dictionary<string, int>());
            construction.destoryGainPack = DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.WOOD, 1000
                    ));

            construction.outputComponent.outputGainPack = (DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.WOOD, 1
                    )));

            construction.upgradeComponent.upgradeCostPack = (DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.COIN, 10
                    )));
            construction.upgradeComponent.transferCostPack = (DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.COIN, 50
                    )));
            construction.upgradeComponent.transferCostPack.modifiedValuesDescription = "转职费用";
            construction.upgradeComponent.transferConstructionPrototypeId = ConstructionPrototypeId.SUPPER_COOKIE_TREE;

            // FIXME for debug
            construction.saveData.proficiency = 47 + UnityEngine.Random.Range(0, 2) * 50;

            return construction;
        }
    }
}
