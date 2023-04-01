using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.DemoGameCore.logic
{
    public class SmallTreePrototype : AbstractConstructionPrototype
    {
        public static DescriptionPackage descriptionPackageEN = new DescriptionPackage(
                    "TODO", "TODO", "TODO", "TODO", "TODO", "TODO", "TODO", "TODO", "TODO",
                    DescriptionPackageFactory.ONLY_LEVEL_IMP,
                    DescriptionPackageFactory.EN_PROFICIENCY_IMP);
        public static DescriptionPackage descriptionPackageCN = new DescriptionPackage(
                    "自动消耗", "自动产出", "升级费用", "(已达到最大等级)", "升级", "砍伐产出", "砍伐费用", "摧毁", "转职",
                    DescriptionPackageFactory.CN_ONLY_LEVEL_IMP,
                    DescriptionPackageFactory.CN_PROFICIENCY_IMP);

        protected static ProficiencySpeedCalculator TREE_PROFICIENCY_SPEED_CALCULATOR = (thiz) =>
        {
            return 1;
        };
        public SmallTreePrototype(Language language) : base(ConstructionPrototypeId.SMALL_TREE, language,
            DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.COIN, 100
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
            construction.proficiencySpeedCalculator = TREE_PROFICIENCY_SPEED_CALCULATOR;
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
            construction.upgradeComponent.transferConstructionPrototypeId = ConstructionPrototypeId.BIG_TREE;

            // FIXME for debug
            construction.saveData.proficiency = 47 + UnityEngine.Random.Range(0, 2) * 50;

            return construction;
        }
    }
}
