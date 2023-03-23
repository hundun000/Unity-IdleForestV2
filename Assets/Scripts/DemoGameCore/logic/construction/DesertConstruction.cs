using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;
using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DemoGameCore.logic
{
    public class DesertConstruction : AutoProficiencyConstruction
    {

        public DesertConstruction(
            String prototypeId, 
            String id, 
            GridPosition position, 
            Language language) : base(prototypeId, id, position, language, null)
        {

        }


        override protected void tryProficiencyOnce()
        {
            int neighborTreeCount = neighbors.Values.ToList()
                .Where(it => it != null && it.saveData.prototypeId.Equals(ConstructionPrototypeId.COOKIE_TREE))
                .Count()
                ;
            proficiencyComponent.changeProficiency(neighborTreeCount);
        }
    }
}
