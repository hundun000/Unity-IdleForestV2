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
    public class SpecialConstructionControlBoardVM : FixedConstructionControlBoardVM<DemoIdleGame, RootSaveData>
    {
        public static readonly HashSet<String> specialConstructionPrototypeIds = new HashSet<String>() {
            ConstructionPrototypeId.WIN_PROVIDER,
            ConstructionPrototypeId.GOVERNMENT,
            ConstructionPrototypeId.ORGANIZATION
        };

        override protected List<BaseConstruction> filterConstructions(List<BaseConstruction> constructions)
        {
            return constructions
                .Where(it => SpecialConstructionControlBoardVM.specialConstructionPrototypeIds.Contains(it.saveData.prototypeId))
                .ToList();
        }
    }

    
}
