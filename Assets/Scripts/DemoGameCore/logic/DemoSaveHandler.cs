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

        private ConstructionSaveData quickSmallTree(int x, int y)
        {
            return ConstructionSaveData.builder()
                                    .prototypeId(ConstructionPrototypeId.SMALL_TREE)
                                    .level(1)
                                    .workingLevel(1)
                                    .proficiency(50)
                                    .position(new GridPosition(x, y))
                                    .build();
        }

        private ConstructionSaveData quickDirt(int x, int y)
        {
            return ConstructionSaveData.builder()
                                    .prototypeId(ConstructionPrototypeId.DIRT)
                                    .position(new GridPosition(x, y))
                                    .build();
        }

        private ConstructionSaveData quickRubbish(int x, int y)
        {
            return ConstructionSaveData.builder()
                                    .prototypeId(ConstructionPrototypeId.RUBBISH)
                                    .position(new GridPosition(x, y))
                                    .build();
        }

        private ConstructionSaveData quickLake(int x, int y)
        {
            return ConstructionSaveData.builder()
                                    .prototypeId(ConstructionPrototypeId.LAKE)
                                    .proficiency(50)
                                    .position(new GridPosition(x, y))
                                    .build();
        }

        private ConstructionSaveData quickDesert(int x, int y)
        {
            return ConstructionSaveData.builder()
                                    .prototypeId(ConstructionPrototypeId.DESERT)
                                    .proficiency(50)
                                    .position(new GridPosition(x, y))
                                    .build();
        }

        override protected List<RootSaveData> genereateStarterRootSaveData()
        {
            

            Dictionary<KeyValuePair<int, int>, ConstructionSaveData> posMap = new();

            // 所有 DIRT 的坐标
            (new List<KeyValuePair<int, int>>() { 
                new(0, 0), new(1, 0), new(0, 2), new(1, 2), new(0, -2), new(1, -2) 
            }).ForEach(it =>
                posMap.Add(it, quickDirt(it.Key, it.Value))
                );

            // 所有 LAKE 的坐标
            (new List<KeyValuePair<int, int>>() {
                new(1, 1), new(1, -1)
            }).ForEach(it =>
                posMap.Add(it, quickLake(it.Key, it.Value))
                );

            // 所有 DESERT 的坐标
            (new List<KeyValuePair<int, int>>() {
                new(1, 3), new(1, -3)
            }).ForEach(it =>
                posMap.Add(it, quickDesert(it.Key, it.Value))
                );

            // 所有 RUBBISH 的坐标
            for (int y = -4; y <= 4; y++)
            {
                int x_from;
                int x_to;
                if (Math.Abs(y) % 2 == 0)
                {
                    x_from = -1;
                    x_to = 2;
                }
                else
                {
                    x_from = -1;
                    x_to = 3;
                }
                for (int x = x_from; x <= x_to; x++)
                {
                    var tryPos = new KeyValuePair<int, int>(x, y);
                    if (!posMap.ContainsKey(tryPos))
                    {
                        posMap.Add(tryPos, quickRubbish(tryPos.Key, tryPos.Value));
                    }
                }
            }

            var gameplaySaveData = new GameplaySaveData();
            gameplaySaveData.constructionSaveDataMap = posMap.Values
                .ToDictionary(
                    it => it.prototypeId + System.Guid.NewGuid().ToString(),
                    it => it
                );
            gameplaySaveData.ownResoueces = (new Dictionary<String, long>());
            gameplaySaveData.ownResoueces.Add(ResourceType.COIN, 10000);
            gameplaySaveData.ownResoueces.Add(ResourceType.CARBON, 512);
            gameplaySaveData.unlockedResourceTypes = (new HashSet<String>());
            gameplaySaveData.unlockedResourceTypes.Add(ResourceType.COIN);
            gameplaySaveData.unlockedResourceTypes.Add(ResourceType.WOOD);
            gameplaySaveData.unlockedResourceTypes.Add(ResourceType.CARBON);
            gameplaySaveData.unlockedAchievementIds = (new HashSet<String>());

            var systemSettingSaveData = new SystemSettingSaveData();
            systemSettingSaveData.language = Language.CN;
            return new List<RootSaveData>() { 
                new RootSaveData(null, systemSettingSaveData),
                new RootSaveData(gameplaySaveData, null)
            };

        }
    }
}
