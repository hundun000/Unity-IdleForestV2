using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Tilemaps;

namespace Assets.Scripts.DemoGameCore.logic
{
    internal class DemoSaveHandler : PairChildrenSaveHandler<RootSaveData, SystemSettingSaveData, GameplaySaveData>
    {

        public DemoSaveHandler(IFrontend frontEnd, ISaveTool<RootSaveData> saveTool) : base(frontEnd, Factory.INSTANCE, saveTool)
        {
            

        }

        override protected RootSaveData genereateStarterRootSaveData()
        {
            var starterConstructionSaveDatas = new List<ConstructionSaveData>();
            starterConstructionSaveDatas.Add(
                ConstructionSaveData.builder()
                                    .prototypeId(ConstructionPrototypeId.SMALL_TREE)
                                    .level(1)
                                    .workingLevel(1)
                                    .proficiency(48)
                                    .position(new GridPosition(0, 0))
                                    .build()
                );
            starterConstructionSaveDatas.Add(
                ConstructionSaveData.builder()
                                    .prototypeId(ConstructionPrototypeId.SMALL_FACTORY)
                                    .level(1)
                                    .workingLevel(1)
                                    .proficiency(48)
                                    .position(new GridPosition(0, -1))
                                    .build()
                );
            starterConstructionSaveDatas.Add(
                ConstructionSaveData.builder()
                                    .prototypeId(ConstructionPrototypeId.DESERT)
                                    .proficiency(97)
                                    .position(new GridPosition(0, -2))
                                    .build()
                );
            starterConstructionSaveDatas.Add(
                ConstructionSaveData.builder()
                                    .prototypeId(ConstructionPrototypeId.DIRT)
                                    .position(new GridPosition(0, 1))
                                    .build()
                );
            starterConstructionSaveDatas.Add(
                ConstructionSaveData.builder()
                                    .prototypeId(ConstructionPrototypeId.RUBBISH)
                                    .position(new GridPosition(0, 2))
                                    .build()
                );
            starterConstructionSaveDatas.Add(
                ConstructionSaveData.builder()
                                    .prototypeId(ConstructionPrototypeId.LAKE)
                                    .position(new GridPosition(0, 3))
                                    .build()
                );
            starterConstructionSaveDatas.Add(
                ConstructionSaveData.builder()
                                    .prototypeId(ConstructionPrototypeId.GOVERNMENT)
                                    .position(new GridPosition(-100, -100))
                                    .build()
                );


            var gameplaySaveData = new GameplaySaveData();
            gameplaySaveData.constructionSaveDataMap = starterConstructionSaveDatas
                .ToDictionary(
                    it => it.prototypeId + System.Guid.NewGuid().ToString(),
                    it => it
                );
            gameplaySaveData.ownResoueces = (new Dictionary<String, long>());
            gameplaySaveData.ownResoueces.Add(ResourceType.COIN, 10000);
            gameplaySaveData.unlockedResourceTypes = (new HashSet<String>());
            gameplaySaveData.unlockedResourceTypes.Add(ResourceType.COIN);
            gameplaySaveData.unlockedResourceTypes.Add(ResourceType.WOOD);
            gameplaySaveData.unlockedResourceTypes.Add(ResourceType.CARBON);
            gameplaySaveData.unlockedAchievementIds = (new HashSet<String>());

            var systemSettingSaveData = new SystemSettingSaveData();
            systemSettingSaveData.language = Language.CN;
            var rootSaveData = new RootSaveData(gameplaySaveData, systemSettingSaveData);
            return rootSaveData;

        }
    }
}
