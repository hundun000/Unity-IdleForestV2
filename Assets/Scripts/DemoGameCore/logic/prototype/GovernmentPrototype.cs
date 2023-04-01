using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.DemoGameCore.logic
{
    public class GovernmentPrototype : AbstractConstructionPrototype
    {

        public GovernmentPrototype(Language language) : base(ConstructionPrototypeId.GOVERNMENT, language, null)
        {
           
        }

        public override BaseConstruction getInstance(GridPosition position)
        {
            String id = prototypeId + "_" + System.Guid.NewGuid().ToString();
            BaseConstruction construction = new GovernmentConstruction(prototypeId, id, position, descriptionPackage);

            return construction;
        }
    }
}
