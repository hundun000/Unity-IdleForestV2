using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Assets.Scripts.DemoGameCore.logic
{
    public class DesertPrototype : AbstractConstructionPrototype
    {
        static ProficiencySpeedCalculator DESERT_PROFICIENCY_SPEED_CALCULATOR = (thiz) =>
        {
            int neighborTreeCount = thiz.neighbors.Values.ToList()
                .Where(it => it != null && it.saveData.prototypeId.Equals(ConstructionPrototypeId.SMALL_TREE))
                .Count()
                ;
            return neighborTreeCount;
        };

        public DesertPrototype(Language language) : base(ConstructionPrototypeId.DESERT, language, null)
        {
            
        }

        public override BaseConstruction getInstance(GridPosition position)
        {
            String id = prototypeId + "_" + System.Guid.NewGuid().ToString();
            AutoProficiencyConstruction construction = new AutoProficiencyConstruction(prototypeId, id, position, language);
            construction.proficiencySpeedCalculator = DESERT_PROFICIENCY_SPEED_CALCULATOR;

            construction.proficiencyComponent.promoteConstructionPrototypeId = ConstructionPrototypeId.SMALL_TREE;

            return construction;
        }
    }
}
