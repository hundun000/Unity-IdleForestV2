using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.DemoGameCore.logic
{
    public class SmallFactoryPrototype : AbstractConstructionPrototype
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
            .destroy("摧毁", "摧毁产出", "摧毁费用")
            .transform("转职", "转职费用", "可以转职")
            .proficiency(DescriptionPackageFactory.CN_PROFICIENCY_IMP)
            .build();

        public SmallFactoryPrototype(Language language) : base(ConstructionPrototypeId.SMALL_FACTORY, language,
            DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.COIN, 50,
                    ResourceType.WOOD, 50
                    ))
            )
        {
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
            BaseIdleForestConstruction construction = BaseIdleForestConstruction.typeAutoProficiency(prototypeId, id, position, descriptionPackage);

            construction.existenceComponent.destoryCostPack = DemoBuiltinConstructionsLoader.toPack(new Dictionary<string, int>());
            construction.existenceComponent.destoryGainPack = DemoBuiltinConstructionsLoader.toPack(new Dictionary<string, int>());

            construction.outputComponent.outputGainPack = (DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.COIN, 5,
                    ResourceType.CARBON, 5
                    )));

            construction.upgradeComponent.upgradeCostPack = (DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.COIN, 50,
                    ResourceType.WOOD, 50
                    )));
            construction.upgradeComponent.transformCostPack = (DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.COIN, 500,
                    ResourceType.WOOD, 500
                    )));
            construction.upgradeComponent.transformConstructionPrototypeId = ConstructionPrototypeId.MID_FACTORY;

            return construction;
        }
    }
}
