using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.DemoGameCore.logic
{
    public class SuperCookieTreePrototype : AbstractConstructionPrototype
    {
        

        public SuperCookieTreePrototype(Language language) : base(ConstructionPrototypeId.SUPPER_COOKIE_TREE, language, null)
        {

        }

        public override BaseConstruction getInstance(GridPosition position)
        {
            String id = prototypeId + "_" + System.Guid.NewGuid().ToString();
            BaseConstruction construction = new AutoProficiencyConstruction(prototypeId, id, position, language, 1);
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
