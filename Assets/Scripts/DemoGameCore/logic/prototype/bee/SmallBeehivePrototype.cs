using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.DemoGameCore.logic
{
    public class SmallBeehivePrototype : AbstractConstructionPrototype
    {

        public SmallBeehivePrototype(Language language) : base(ConstructionPrototypeId.SMALL_BEEHIVE, language, null)
        {
            // override descriptionPackage
            switch (language)
            {
                case Language.CN:
                    this.descriptionPackage = SmallFactoryPrototype.descriptionPackageCN;
                    break;
                default:
                    this.descriptionPackage = SmallFactoryPrototype.descriptionPackageEN;
                    break;
            }
        }

        public override BaseConstruction getInstance(GridPosition position)
        {
            String id = prototypeId + "_" + System.Guid.NewGuid().ToString();
            BaseIdleForestConstruction construction = BaseIdleForestConstruction.typeNoOutputConstProficiency(prototypeId, id, position, descriptionPackage);
            //construction.allowAnyProficiencyDestory = false;

            construction.outputComponent.outputCostPack = DemoBuiltinConstructionsLoader.toPack(new());
            construction.outputComponent.outputGainPack = DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.HONEY, 2
                    ));
            construction.upgradeComponent.upgradeCostPack = DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.COIN, 40
                    ));


            return construction;
        }
    }
}
