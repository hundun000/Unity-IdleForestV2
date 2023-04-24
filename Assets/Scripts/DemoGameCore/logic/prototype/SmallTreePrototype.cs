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
            .button("Upgrade")
            .output("Consume", "Produce")
            .upgrade("Upgrade cost", "(max)", DescriptionPackageFactory.ONLY_LEVEL_IMP)
            .destroy("Destroy", null, null)
            .transform("Transform", "Transform cost", "Can be transformed")
            .proficiency(DescriptionPackageFactory.EN_PROFICIENCY_IMP)
            .build();
        public static DescriptionPackage descriptionPackageCN = new DescriptionPackageBuilder()
            .button("升级")
            .output("自动消耗", "自动产出")
            .upgrade("升级费用", "(已达到最大等级)", DescriptionPackageFactory.CN_ONLY_LEVEL_IMP)
            .destroy("砍伐", null, null)
            .transform("转职", "转职费用", "可以转职")
            .proficiency(DescriptionPackageFactory.CN_PROFICIENCY_IMP)
            .build();

        public SmallTreePrototype(Language language) : base(ConstructionPrototypeId.SMALL_TREE, language,
            DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.COIN, 40
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
            BaseIdleForestConstruction construction = BaseIdleForestConstruction.typeAutoProficiency(prototypeId, id, position, descriptionPackage);
            //construction.allowAnyProficiencyDestory = false;

            construction.existenceComponent.destoryCostPack = DemoBuiltinConstructionsLoader.toPack(new Dictionary<string, int>());
            construction.existenceComponent.destoryGainPack = DemoBuiltinConstructionsLoader.toPack(new Dictionary<string, int>());

            construction.outputComponent.outputCostPack = (DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.CARBON, 2
                    )));
            construction.outputComponent.outputGainPack = (DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.WOOD, 2
                    )));
            construction.upgradeComponent.upgradeCostPack = (DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.COIN, 40
                    )));
            construction.upgradeComponent.transformCostPack = (DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.COIN, 400
                    )));
            construction.upgradeComponent.transformConstructionPrototypeId = ConstructionPrototypeId.MID_TREE;


            return construction;
        }
    }
}
