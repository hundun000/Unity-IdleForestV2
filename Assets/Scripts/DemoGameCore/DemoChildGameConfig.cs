using Assets.Scripts.DemoGameCore.logic;
using Assets.Scripts.DemoGameCore.ui.screen;
using hundun.idleshare.gamelib;
using hundun.unitygame.adapters;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DemoGameCore
{
    public class DemoChildGameConfig : ChildGameConfig
    {
        public DemoChildGameConfig()
        {

            //        BuiltinConstructionsLoader builtinConstructionsLoader = new BuiltinConstructionsLoader(game);
            //        this.setConstructions(builtinConstructionsLoader.load());


            Dictionary<String, List<String>> areaControlableConstructionVMPrototypeIds = new Dictionary<String, List<String>>();
            areaControlableConstructionVMPrototypeIds.put(GameArea.AREA_SINGLE, JavaFeatureForGwt.arraysAsList(
                     ConstructionPrototypeId.SMALL_TREE,
                     ConstructionPrototypeId.MID_TREE,
                     ConstructionPrototypeId.BIG_TREE,
                     ConstructionPrototypeId.SMALL_FACTORY,
                     ConstructionPrototypeId.MID_FACTORY,
                     ConstructionPrototypeId.BIG_FACTORY,
                     ConstructionPrototypeId.DESERT,
                     ConstructionPrototypeId.DIRT,
                     ConstructionPrototypeId.RUBBISH,
                     ConstructionPrototypeId.LAKE,
                     ConstructionPrototypeId.GOVERNMENT
            ));


            this.areaControlableConstructionVMPrototypeIds = areaControlableConstructionVMPrototypeIds;

            Dictionary<String, List<String>> areaControlableConstructionPrototypeVMPrototypeIds = new Dictionary<String, List<String>>();
            areaControlableConstructionPrototypeVMPrototypeIds.put(GameArea.AREA_SINGLE, JavaFeatureForGwt.arraysAsList(
                     ConstructionPrototypeId.SMALL_TREE,
                     ConstructionPrototypeId.SMALL_FACTORY
            ));
            this.areaControlableConstructionPrototypeVMPrototypeIds = areaControlableConstructionPrototypeVMPrototypeIds;

            this.areaShowEntityByOwnAmountConstructionPrototypeIds = new Dictionary<String, List<String>>();

            Dictionary<String, List<String>> areaShowEntityByOwnAmountResourceIds = new Dictionary<String, List<String>>();
            this.areaShowEntityByOwnAmountResourceIds = (areaShowEntityByOwnAmountResourceIds);

            Dictionary<String, List<String>> areaShowEntityByChangeAmountResourceIds = new Dictionary<String, List<String>>();
            areaShowEntityByChangeAmountResourceIds.put(GameArea.AREA_SINGLE, JavaFeatureForGwt.arraysAsList(
                ResourceType.COIN,
                ResourceType.WOOD,
                ResourceType.CARBON
            ));
            this.areaShowEntityByChangeAmountResourceIds = (areaShowEntityByChangeAmountResourceIds);

            Dictionary<String, String> screenIdToFilePathMap = JavaFeatureForGwt.mapOf(
                    typeof(DemoMenuScreen).Name, "audio/Loop-Menu.wav",
                    typeof(DemoPlayScreen).Name, "audio/relax.wav"
                );
            this.screenIdToFilePathMap = (screenIdToFilePathMap);

            List<String> achievementPrototypeIds = JavaFeatureForGwt.arraysAsList(
                    IdleForestAchievementId.STEP_1,
                    IdleForestAchievementId.STEP_2,
                    IdleForestAchievementId.STEP_3,
                    IdleForestAchievementId.STEP_4,
                    IdleForestAchievementId.STEP_5,
                    IdleForestAchievementId.STEP_6
                    );
            this.achievementPrototypeIds = (achievementPrototypeIds);
        }
    }
}
