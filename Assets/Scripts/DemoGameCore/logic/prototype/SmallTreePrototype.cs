using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.DemoGameCore.logic
{
    public class SmallTreePrototype : AbstractConstructionPrototype
    {
        public static DescriptionPackage descriptionPackageEN = new DescriptionPackageBuilder()
            .output("自动消耗", "自动产出")
            .destroy("砍伐", "砍伐产出", null)
            .transfer("转职", "转职费用", "可以转职")
            .proficiency(DescriptionPackageFactory.CN_PROFICIENCY_IMP)
            .build();
        public static DescriptionPackage descriptionPackageCN = new DescriptionPackageBuilder()
            .output("自动消耗", "自动产出")
            .destroy("砍伐", "砍伐产出", null)
            .transfer("转职", "转职费用", "可以转职")
            .proficiency(DescriptionPackageFactory.CN_PROFICIENCY_IMP)
            .build();

        public SmallTreePrototype(Language language) : base(ConstructionPrototypeId.SMALL_TREE, language,
            DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.COIN, 30
                    ))
            )
        {
            // override descriptionPackage
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
            AutoProficiencyConstruction construction = new AutoProficiencyConstruction(prototypeId, id, position, descriptionPackage);
            construction.allowAnyProficiencyDestory = false;

            construction.destoryCostPack = DemoBuiltinConstructionsLoader.toPack(new Dictionary<string, int>());
            construction.destoryGainPack = DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.WOOD, 300
                    ));

            construction.outputComponent.outputCostPack = (DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.CARBON, 10
                    )));
            construction.outputComponent.outputGainPack = (DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.WOOD, 10
                    )));

            return construction;
        }
    }
}
