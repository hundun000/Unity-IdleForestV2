using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.DemoGameCore.logic
{
    public class MidForestPrototype : AbstractConstructionPrototype
    {
        public MidForestPrototype(Language language) : base(ConstructionPrototypeId.MID_TREE, language, null)
        {
            switch (language)
            {
                case Language.CN:
                    this.descriptionPackage = SmallTreePrototype.descriptionPackageCN;
                    break;
                default:
                    this.descriptionPackage = SmallTreePrototype.descriptionPackageEN;
                    break;
            }
        }

        public override BaseConstruction getInstance(GridPosition position)
        {
            String id = prototypeId + "_" + System.Guid.NewGuid().ToString();
            BaseIdleForestConstruction construction = BaseIdleForestConstruction.typeAutoProficiency(prototypeId, id, position, descriptionPackage);

            construction.existenceComponent.destoryCostPack = DemoBuiltinConstructionsLoader.toPack(new Dictionary<string, int>());
            construction.existenceComponent.destoryGainPack = DemoBuiltinConstructionsLoader.toPack(new Dictionary<string, int>());

            construction.outputComponent.outputCostPack = (DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.CARBON, 10
                    )));
            construction.outputComponent.outputGainPack = (DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.WOOD, 10
                    )));
            construction.upgradeComponent.upgradeCostPack = (DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.COIN, 200
                    )));
            construction.upgradeComponent.transformCostPack = (DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.COIN, 2000
                    )));
            construction.upgradeComponent.transformConstructionPrototypeId = ConstructionPrototypeId.BIG_TREE;

            return construction;
        }
    }
}
