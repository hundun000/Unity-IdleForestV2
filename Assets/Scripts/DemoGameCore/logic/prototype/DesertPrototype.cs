using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.DemoGameCore.logic
{
    public class DesertPrototype : AbstractConstructionPrototype
    {

        public DesertPrototype(Language language) : base(ConstructionPrototypeId.DESERT, language, null)
        {

        }

        public override BaseConstruction getInstance(GridPosition position)
        {
            String id = prototypeId + "_" + System.Guid.NewGuid().ToString();
            BaseConstruction construction = new DesertConstruction(prototypeId, id, position, language);

            construction.proficiencyComponent.promoteConstructionPrototypeId = ConstructionPrototypeId.COOKIE_TREE;

            return construction;
        }
    }
}
