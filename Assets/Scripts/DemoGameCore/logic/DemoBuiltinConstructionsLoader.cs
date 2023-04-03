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
        public static DescriptionPackage descriptionPackageEN = new DescriptionPackageBuilder()
            .button("升级")
            .output("自动消耗", "自动产出")
            .upgrade("升级费用", "(已达到最大等级)", DescriptionPackageFactory.ONLY_LEVEL_IMP)
            .destroy("摧毁", "摧毁产出", "摧毁费用")
            .transfer("转职", "转职费用", "可以转职")
            .proficiency(DescriptionPackageFactory.EN_PROFICIENCY_IMP)
            .build();


        public static DescriptionPackage descriptionPackageCN = new DescriptionPackageBuilder()
            .button("升级")
            .output("自动消耗", "自动产出")
            .upgrade("升级费用", "(已达到最大等级)", DescriptionPackageFactory.CN_ONLY_LEVEL_IMP)
            .destroy("摧毁", "摧毁产出", "摧毁费用")
            .transfer("转职", "转职费用", "可以转职")
            .proficiency(DescriptionPackageFactory.CN_PROFICIENCY_IMP)
            .build();

        public Dictionary<String, AbstractConstructionPrototype> getProviderMap(Language language)
        {
            Dictionary < String, AbstractConstructionPrototype > map = new Dictionary<string, AbstractConstructionPrototype>();
            map.Add(ConstructionPrototypeId.SMALL_TREE, (AbstractConstructionPrototype)new SmallTreePrototype(language));
            map.Add(ConstructionPrototypeId.BIG_TREE, (AbstractConstructionPrototype)new BigTreePrototype(language));
            map.Add(ConstructionPrototypeId.SMALL_FACTORY, (AbstractConstructionPrototype)new SmallFactoryPrototype(language));
            map.Add(ConstructionPrototypeId.BIG_FACTORY, (AbstractConstructionPrototype)new BigFactoryPrototype(language));
            map.Add(ConstructionPrototypeId.DESERT, (AbstractConstructionPrototype)new DesertPrototype(language));
            map.Add(ConstructionPrototypeId.DIRT, (AbstractConstructionPrototype)new DirtPrototype(language));
            map.Add(ConstructionPrototypeId.LAKE, (AbstractConstructionPrototype)new LakePrototype(language));
            map.Add(ConstructionPrototypeId.RUBBISH, (AbstractConstructionPrototype)new RubbishPrototype(language));
            map.Add(ConstructionPrototypeId.GOVERNMENT, (AbstractConstructionPrototype)new GovernmentPrototype(language));
            return map;
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
