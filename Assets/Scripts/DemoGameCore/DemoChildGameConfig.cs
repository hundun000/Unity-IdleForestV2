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
            areaControlableConstructionVMPrototypeIds.put(GameArea.AREA_COOKIE, JavaFeatureForGwt.arraysAsList(
                     ConstructionPrototypeId.COOKIE_TREE,
                     ConstructionPrototypeId.SUPPER_COOKIE_TREE,
                     ConstructionPrototypeId.DESERT,
                     ConstructionPrototypeId.DIRT,
                     ConstructionPrototypeId.RUBBISH
            ));
            areaControlableConstructionVMPrototypeIds.put(GameArea.AREA_WIN, JavaFeatureForGwt.arraysAsList(
                    ConstructionPrototypeId.WIN_PROVIDER
            ));

            this.areaControlableConstructionVMPrototypeIds = areaControlableConstructionVMPrototypeIds;

            Dictionary<String, List<String>> areaControlableConstructionPrototypeVMPrototypeIds = new Dictionary<String, List<String>>();
            areaControlableConstructionPrototypeVMPrototypeIds.put(GameArea.AREA_COOKIE, JavaFeatureForGwt.arraysAsList(
                     ConstructionPrototypeId.COOKIE_TREE
            ));
            this.areaControlableConstructionPrototypeVMPrototypeIds = areaControlableConstructionPrototypeVMPrototypeIds;

            this.areaShowEntityByOwnAmountConstructionPrototypeIds = new Dictionary<String, List<String>>();

            Dictionary<String, List<String>> areaShowEntityByOwnAmountResourceIds = new Dictionary<String, List<String>>();
            this.areaShowEntityByOwnAmountResourceIds = (areaShowEntityByOwnAmountResourceIds);

            Dictionary<String, List<String>> areaShowEntityByChangeAmountResourceIds = new Dictionary<String, List<String>>();
            areaShowEntityByChangeAmountResourceIds.put(GameArea.AREA_COOKIE, JavaFeatureForGwt.arraysAsList(
                ResourceType.COIN,
                ResourceType.WOOD,
                ResourceType.CARBON
            ));
            this.areaShowEntityByChangeAmountResourceIds = (areaShowEntityByChangeAmountResourceIds);

            Dictionary<String, String> screenIdToFilePathMap = JavaFeatureForGwt.mapOf(
                    typeof(DemoMenuScreen).Name, "audio/Loop-Menu.wav",
                    typeof(DemoPlayScreen).Name, "audio/forest.mp3"
                );
            this.screenIdToFilePathMap = (screenIdToFilePathMap);

            List<AchievementPrototype> achievementPrototypes = JavaFeatureForGwt.arraysAsList(
                    new AchievementPrototype("Game win", "You win the game!",
                            JavaFeatureForGwt.mapOf(BuffId.WIN, 1),
                            null
                            )
                    );
            this.achievementPrototypes = (achievementPrototypes);
        }
    }
}
