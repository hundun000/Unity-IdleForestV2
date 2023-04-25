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
        public Dictionary<String, AbstractConstructionPrototype> getProviderMap(Language language)
        {
            Dictionary<String, AbstractConstructionPrototype> map = new Dictionary<string, AbstractConstructionPrototype>();
            map.Add(ConstructionPrototypeId.SMALL_TREE, new SmallTreePrototype(language));
            map.Add(ConstructionPrototypeId.MID_TREE, new MidForestPrototype(language));
            map.Add(ConstructionPrototypeId.BIG_TREE, new BigTreePrototype(language));
            map.Add(ConstructionPrototypeId.SMALL_FACTORY, new SmallFactoryPrototype(language));
            map.Add(ConstructionPrototypeId.MID_FACTORY, new MidFactoryPrototype(language));
            map.Add(ConstructionPrototypeId.BIG_FACTORY, new BigFactoryPrototype(language));
            map.Add(ConstructionPrototypeId.DESERT, new DesertPrototype(language));
            map.Add(ConstructionPrototypeId.DIRT, new DirtPrototype(language));
            map.Add(ConstructionPrototypeId.LAKE, new LakePrototype(language));
            map.Add(ConstructionPrototypeId.RUBBISH, new RubbishPrototype(language));
            map.Add(ConstructionPrototypeId.SMALL_BEEHIVE, new SmallBeehivePrototype(language));
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
