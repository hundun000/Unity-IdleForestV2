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

    public class CookieTreePrototype : AbstractConstructionPrototype
    {
        static DescriptionPackage descriptionPackageCN = new DescriptionPackage(
                            "自动消耗", "自动产出", "升级费用", "(已达到最大等级)", "升级",
                            DescriptionPackageFactory.CN_ONLY_LEVEL_IMP,
                            DescriptionPackageFactory.CN_PROFICIENCY_IMP);

        public CookieTreePrototype() : base(ConstructionPrototypeId.GROWING_COOKIE_AUTO_PROVIDER)
        {

        }

        public override BaseConstruction getInstance(Language language)
        {
            String id = prototypeId + "_" + System.Guid.NewGuid().ToString();
            BaseConstruction construction = new IdleForestConstruction(prototypeId, id);
            construction.descriptionPackage = descriptionPackageCN;

            OutputComponent outputComponent = new OutputComponent(construction);
            outputComponent.outputGainPack = (DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.COOKIE, 1
                    )));
            construction.outputComponent = (outputComponent);

            UpgradeComponent upgradeComponent = new UpgradeComponent(construction);
            upgradeComponent.upgradeCostPack = (DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.COOKIE, 25
                    )));
            construction.upgradeComponent = (upgradeComponent);

            LevelComponent levelComponent = new LevelComponent(construction, false);
            construction.levelComponent = (levelComponent);

            ProficiencyComponent proficiencyComponent = new ProficiencyComponent(construction);
            construction.proficiencyComponent = (proficiencyComponent);

            construction.saveData.level = 1;
            construction.saveData.workingLevel = 1;
            construction.saveData.proficiency = 47;

            return construction;
        }
    }




    public class DemoBuiltinConstructionsLoader : IBuiltinConstructionsLoader
    {

        public Dictionary<String, AbstractConstructionPrototype> getProviderMap()
        {
            return JavaFeatureForGwt.mapOf(
                ConstructionPrototypeId.GROWING_COOKIE_AUTO_PROVIDER, (AbstractConstructionPrototype)new CookieTreePrototype()
                );
        }

        public static ResourcePack toPack(Dictionary<String, int> map)
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

    }
}
