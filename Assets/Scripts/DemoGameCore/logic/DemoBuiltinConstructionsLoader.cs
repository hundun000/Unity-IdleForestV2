using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityEngine.EventSystems.EventTrigger;

namespace Assets.Scripts.DemoGameCore.logic
{
    public class DemoBuiltinConstructionsLoader : IBuiltinConstructionsLoader
    {

        public List<BaseConstruction> provide(Language language)
        {
            List<BaseConstruction> constructions = new List<BaseConstruction>();
            // clicker-provider
            for (int i = 0; i < 2; i ++)
            {
                String prototypeId = ConstructionPrototypeId.COOKIE_CLICK_PROVIDER;
                String id = prototypeId + "_" + i;
                BaseConstruction construction = new BaseClickGatherConstruction(prototypeId, id);
                construction.descriptionPackage = DescriptionPackageFactory.getGatherDescriptionPackage(language);

                OutputComponent outputComponent = new OutputComponent(construction);
                outputComponent.outputGainPack = (toPack(JavaFeatureForGwt.mapOf(
                        ResourceType.COOKIE, 1
                        )));
                construction.outputComponent = (outputComponent);

                UpgradeComponent upgradeComponent = new UpgradeComponent(construction);
                construction.upgradeComponent = (upgradeComponent);

                LevelComponent levelComponent = new LevelComponent(construction, false);
                construction.levelComponent = (levelComponent);

                constructions.Add(construction);
            }
            // auto-provider
            for (int i = 0; i < 2; i++)
            {
                String prototypeId = ConstructionPrototypeId.COOKIE_AUTO_PROVIDER;
                String id = prototypeId + "_" + i;
                BaseConstruction construction = new BaseAutoConstruction(prototypeId, id);
                construction.descriptionPackage = DescriptionPackageFactory.getMaxLevelAutoDescriptionPackage(language);

                OutputComponent outputComponent = new OutputComponent(construction);
                outputComponent.outputGainPack = (toPack(JavaFeatureForGwt.mapOf(
                        ResourceType.COOKIE, 1
                        )));
                construction.outputComponent = (outputComponent);

                UpgradeComponent upgradeComponent = new UpgradeComponent(construction);
                upgradeComponent.upgradeCostPack = (toPack(JavaFeatureForGwt.mapOf(
                        ResourceType.COIN, 25
                        )));
                construction.upgradeComponent = (upgradeComponent);

                LevelComponent levelComponent = new LevelComponent(construction, false);
                construction.levelComponent = (levelComponent);

                construction.maxDrawNum = (9);
                constructions.Add(construction);
            }
            // growing-auto-provider
            for (int i = 0; i < 1; i++)
            {
                String prototypeId = ConstructionPrototypeId.GROWING_COOKIE_AUTO_PROVIDER;
                String id = prototypeId + "_" + i;
                BaseConstruction construction = new IdleForestConstruction(prototypeId, id);
                construction.descriptionPackage = getForestDescriptionPackage(language);

                OutputComponent outputComponent = new OutputComponent(construction);
                outputComponent.outputGainPack = (toPack(JavaFeatureForGwt.mapOf(
                        ResourceType.COOKIE, 1
                        )));
                construction.outputComponent = (outputComponent);

                UpgradeComponent upgradeComponent = new UpgradeComponent(construction);
                upgradeComponent.upgradeCostPack = (toPack(JavaFeatureForGwt.mapOf(
                        ResourceType.COIN, 25
                        )));
                construction.upgradeComponent = (upgradeComponent);

                LevelComponent levelComponent = new LevelComponent(construction, false);
                construction.levelComponent = (levelComponent);

                construction.maxDrawNum = (9);
                constructions.Add(construction);
            }
            // seller
            for (int i = 0; i < 2; i++)
            {
                String prototypeId = ConstructionPrototypeId.COOKIE_SELLER;
                String id = prototypeId + "_" + i;
                BaseConstruction construction = new BaseAutoConstruction(prototypeId, id);
                construction.descriptionPackage = DescriptionPackageFactory.getWorkingLevelAutoDescriptionPackage(language);

                OutputComponent outputComponent = new OutputComponent(construction);
                outputComponent.outputCostPack = (toPack(JavaFeatureForGwt.mapOf(
                        ResourceType.COOKIE, 1
                        )));
                outputComponent.outputGainPack = (toPack(JavaFeatureForGwt.mapOf(
                        ResourceType.COIN, 5
                        )));
                construction.outputComponent = (outputComponent);

                UpgradeComponent upgradeComponent = new UpgradeComponent(construction);
                upgradeComponent.upgradeCostPack = (toPack(JavaFeatureForGwt.mapOf(
                        ResourceType.COIN, 500
                        )));
                construction.upgradeComponent = (upgradeComponent);

                LevelComponent levelComponent = new LevelComponent(construction, true);
                construction.levelComponent = (levelComponent);

                construction.maxDrawNum = (9);
                constructions.Add(construction);
            }
            // win
            {
                String prototypeId = ConstructionPrototypeId.WIN_PROVIDER;
                String id = prototypeId + "_Singleton";
                BaseConstruction construction = new BaseBuffConstruction(prototypeId, id, BuffId.WIN);
                construction.descriptionPackage = DescriptionPackageFactory.getWinDescriptionPackage(language);

                OutputComponent outputComponent = new OutputComponent(construction);
                construction.outputComponent = (outputComponent);

                UpgradeComponent upgradeComponent = new UpgradeComponent(construction);
                upgradeComponent.upgradeCostPack = (toPack(JavaFeatureForGwt.mapOf(
                        ResourceType.COIN, 500
                        )));
                construction.upgradeComponent = (upgradeComponent);

                LevelComponent levelComponent = new LevelComponent(construction, false);
                construction.levelComponent = (levelComponent);

                construction.maxLevel = (1);
                constructions.Add(construction);
            }
            return constructions;
        }

        private static ResourcePack toPack(Dictionary<String, int> map)
        {
            ResourcePack pack = new ResourcePack();
            List<ResourcePair> pairs = new List<ResourcePair>(map.Count);
            foreach (KeyValuePair<String, int> entry in map)
            {
                pairs.Add(new ResourcePair(entry.Key, (long)entry.Value));
            }
            pack.baseValues = (pairs);
            return pack;
        }


        public static DescriptionPackage getForestDescriptionPackage(Language language)
        {
            switch (language)
            {
                default:// TODO
                case Language.CN:
                    return new DescriptionPackage(
                            "自动消耗", "自动产出", "升级费用", "(已达到最大等级)", "升级",
                            DescriptionPackageFactory.CN_ONLY_LEVEL_IMP,
                            DescriptionPackageFactory.CN_PROFICIENCY_IMP);
            }
        }

    }
}
