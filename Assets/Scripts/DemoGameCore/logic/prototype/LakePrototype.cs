using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.DemoGameCore.logic
{
    public class LakePrototype : AbstractConstructionPrototype
    {
        static ProficiencySpeedCalculator LAKE_PROFICIENCY_SPEED_CALCULATOR = (thiz) =>
        {
            int neighborTreeCount = thiz.neighbors.Values.ToList()
                .Where(it => it != null && it.saveData.prototypeId.Equals(ConstructionPrototypeId.SMALL_TREE))
                .Count()
                ;
            int neighborFactoryCount = thiz.neighbors.Values.ToList()
                .Where(it => it != null && it.saveData.prototypeId.Equals(ConstructionPrototypeId.SMALL_TREE))
                .Count()
                ;
            return neighborTreeCount - neighborFactoryCount;
        };

        public LakePrototype(Language language) : base(ConstructionPrototypeId.LAKE, language, null)
        {
           
        }

        public override BaseConstruction getInstance(GridPosition position)
        {
            String id = prototypeId + "_" + System.Guid.NewGuid().ToString();
            AutoProficiencyConstruction construction = new AutoProficiencyConstruction(prototypeId, id, position, language);
            construction.proficiencySpeedCalculator = LAKE_PROFICIENCY_SPEED_CALCULATOR;

            construction.proficiencyComponent.demoteConstructionPrototypeId = ConstructionPrototypeId.DIRT;

            return construction;
        }
    }
}
