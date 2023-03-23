using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.DemoGameCore.logic
{
    public class RubbishPrototype : AbstractConstructionPrototype
    {

        public RubbishPrototype(Language language) : base(ConstructionPrototypeId.RUBBISH, language, null)
        {

        }

        public override BaseConstruction getInstance(GridPosition position)
        {
            String id = prototypeId + "_" + System.Guid.NewGuid().ToString();
            BaseConstruction construction = new AutoProficiencyConstruction(prototypeId, id, position, language, 0);

            construction.destoryCostPack = DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.COIN, 1000
                    ));
            construction.destoryGainPack = DemoBuiltinConstructionsLoader.toPack(new Dictionary<string, int>()); 

            return construction;
        }
    }
}
