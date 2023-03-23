using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.DemoGameCore.logic
{
    public class DirtPrototype : AbstractConstructionPrototype
    {

        public DirtPrototype(Language language) : base(ConstructionPrototypeId.DIRT, language, null)
        {
           
        }

        public override BaseConstruction getInstance(GridPosition position)
        {
            String id = prototypeId + "_" + System.Guid.NewGuid().ToString();
            BaseConstruction construction = new BaseIdleForestConstruction(prototypeId, id, position, language);

            construction.allowPositionOverwrite = true;

            return construction;
        }
    }
}
