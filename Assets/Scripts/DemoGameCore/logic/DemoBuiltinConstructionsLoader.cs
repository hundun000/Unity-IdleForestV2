using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Assets.Scripts.DemoGameCore.logic
{




    public class DemoBuiltinConstructionsLoader : IBuiltinConstructionsLoader
    {
        public static DescriptionPackage descriptionPackageEN = new DescriptionPackage(
                            "TODO", "TODO", "TODO", "TODO", "TODO", "TODO", "TODO",
                            DescriptionPackageFactory.CN_ONLY_LEVEL_IMP,
                            DescriptionPackageFactory.CN_PROFICIENCY_IMP);
        public static DescriptionPackage descriptionPackageCN = new DescriptionPackage(
                            "自动消耗", "自动产出", "升级费用", "(已达到最大等级)", "升级", "摧毁产出", "摧毁费用",
                            DescriptionPackageFactory.CN_ONLY_LEVEL_IMP,
                            DescriptionPackageFactory.CN_PROFICIENCY_IMP);

        public Dictionary<String, AbstractConstructionPrototype> getProviderMap(Language language)
        {
            return JavaFeatureForGwt.mapOf(
                ConstructionPrototypeId.COOKIE_TREE, (AbstractConstructionPrototype)new CookieTreePrototype(language),
                ConstructionPrototypeId.SUPPER_COOKIE_TREE, (AbstractConstructionPrototype)new SuperCookieTreePrototype(language),
                ConstructionPrototypeId.DESERT, (AbstractConstructionPrototype)new DesertPrototype(language),
                ConstructionPrototypeId.RUBBISH, (AbstractConstructionPrototype)new RubbishPrototype(language),
                ConstructionPrototypeId.DIRT, (AbstractConstructionPrototype)new DirtPrototype(language)
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
