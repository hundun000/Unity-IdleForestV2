using Assets.Scripts.DemoGameCore.logic;
using hundun.idleshare.enginecore;
using hundun.idleshare.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DemoGameCore.ui.sub
{
    public class DemoFixedConstructionControlBoardVM : FixedConstructionControlBoardVM<DemoIdleGame, RootSaveData>
    {


        override protected List<BaseConstruction> filterConstructions(List<BaseConstruction> constructions)
        {
            return constructions
                .Where(it => !SpecialConstructionControlBoardVM.specialConstructionPrototypeIds.Contains(it.saveData.prototypeId))
                .ToList();
        }
    }
}
