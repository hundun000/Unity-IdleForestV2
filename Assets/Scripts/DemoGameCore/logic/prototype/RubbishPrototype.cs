using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.DemoGameCore.logic
{
    public class RubbishPrototype : AbstractConstructionPrototype
    {
        private static DescriptionPackage descriptionPackageEN = new DescriptionPackageBuilder()
            .destroy("Clear", null, "Clear cost")
            .build();
        private static DescriptionPackage descriptionPackageCN = new DescriptionPackageBuilder()
            .destroy("清理", null, "清理费用")
            .build();


        public RubbishPrototype(Language language) : base(ConstructionPrototypeId.RUBBISH, language, null)
        {
            // override descriptionPackage
            switch (language)
            {
                case Language.CN:
                    this.descriptionPackage = RubbishPrototype.descriptionPackageCN;
                    break;
                default:
                    this.descriptionPackage = RubbishPrototype.descriptionPackageEN;
                    break;
            }
        }

        public override BaseConstruction getInstance(GridPosition position)
        {
            String id = prototypeId + "_" + System.Guid.NewGuid().ToString();
            BaseIdleForestConstruction construction = BaseIdleForestConstruction.typeNoProficiency(prototypeId, id, position, descriptionPackage);

            construction.existenceComponent.destoryCostPack = DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.COIN, 1000
                    ));
            construction.existenceComponent.destoryGainPack = DemoBuiltinConstructionsLoader.toPack(new Dictionary<string, int>()); 

            return construction;
        }
    }
}
