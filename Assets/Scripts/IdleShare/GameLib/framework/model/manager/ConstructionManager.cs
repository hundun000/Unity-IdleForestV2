using hundun.idleshare.gamelib;
using hundun.unitygame.adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

namespace hundun.idleshare.gamelib
{
    public class ConstructionManager : ITileNodeMap<BaseConstruction>
    {
        IdleGameplayContext gameContext;

        

        public ConstructionManager(IdleGameplayContext gameContext)
        {
            this.gameContext = gameContext;
        }


        /**
         * 运行中的设施集合。key: constructionId
         */
        public Dictionary<String, BaseConstruction> runningConstructionModelMap = new Dictionary<String, BaseConstruction>();

        /**
         * 根据GameArea显示不同的Construction集合
         */
        Dictionary<String, List<String>> areaControlableConstructionPrototypeIds;


        public void lazyInit(Dictionary<String, List<String>> areaControlableConstructionPrototypeIds)
        {
            this.areaControlableConstructionPrototypeIds = areaControlableConstructionPrototypeIds;
            //if (areaControlableConstructionPrototypeIds != null)
            //{
            //    foreach (KeyValuePair<String, List<String>> entry in areaControlableConstructionPrototypeIds)
            //    {
            //        areaControlableConstructions.Add(
            //                entry.Key,
            //                entry.Value
            //                    .SelectMany(id => gameContext.constructionFactory.getConstructionsOfPrototype(id))
            //                    .ToList()
            //        );
            //    }
            //}

            //foreach (KeyValuePair<String, List<BaseConstruction>> entry in areaControlableConstructions)
            //{
            //    var items = entry.Value;
            //    items.ForEach(item => runningConstructionModelMap.TryAdd(item.id, item));
            //}

                
        }
        public void onSubLogicFrame()
        {
            foreach (KeyValuePair<String, BaseConstruction> entry in runningConstructionModelMap)
            {
                var item = entry.Value;
                item.onLogicFrame();
            }
        }

        public List<BaseConstruction> getAreaShownConstructionsOrEmpty(String gameArea)
        {
            return runningConstructionModelMap.Values
                .Where(it => areaControlableConstructionPrototypeIds.ContainsKey(gameArea) && 
                        areaControlableConstructionPrototypeIds.get(gameArea).Contains(it.prototypeId))
                .ToList();
        }

        public List<AbstractConstructionPrototype> getAreaShownConstructionPrototypesOrEmpty(String gameArea)
        {
            return areaControlableConstructionPrototypeIds.get(gameArea)
                .Select(it => gameContext.constructionFactory.getPrototype(it))
                .ToList();
        }

        public BaseConstruction getConstruction(String id)
        {
            BaseConstruction result = runningConstructionModelMap[id];
            if (result == null)
            {
                throw new SystemException("getConstruction " + id + " not found");
            }
            return result;
        }

        public List<BaseConstruction> getConstructions()
        {
            return runningConstructionModelMap.Values.ToList();
        }

        internal List<BaseConstruction> getConstructionsOfPrototype(string prototypeId)
        {
            return runningConstructionModelMap.Values
                .Where(it => it.prototypeId.Equals(prototypeId))
                .ToList();
        }

        internal void promoteInstance(String id)
        {
            BaseConstruction construction = runningConstructionModelMap[id];
            removeInstance(construction);
            createInstanceOfPrototype(construction.proficiencyComponent.promoteConstructionPrototypeId, construction.position);
        }

        internal void destoryInstance(String id)
        {
            BaseConstruction construction = runningConstructionModelMap[id];
            removeInstance(construction);
            if (construction.destoryGainPack != null)
            {
                gameContext.storageManager.modifyAllResourceNum(construction.destoryGainPack.modifiedValues, true);
            }
            gameContext.eventManager.notifyConstructionCollectionChange();
        }

        private void removeInstance(BaseConstruction construction)
        {
            runningConstructionModelMap.Remove(construction.id);
            TileNodeUtils.updateNeighbors(construction, this);
            construction.neighbors.Values.ToList()
                .Where(it => it != null)
                .ToList()
                .ForEach(it => TileNodeUtils.updateNeighbors(it, this));
        }

        internal void loadInstance(ConstructionSaveData saveData)
        {
            string prototypeId = saveData.prototypeId;
            GridPosition position = saveData.position;
            BaseConstruction construction = gameContext.constructionFactory.getInstanceOfPrototype(prototypeId, position);
            construction.saveData = saveData;
            construction.updateModifiedValues();

            runningConstructionModelMap.put(construction.id, construction);
            TileNodeUtils.updateNeighbors(construction, this);
            construction.neighbors.Values.ToList()
                .Where(it => it != null)
                .ToList()
                .ForEach(it => TileNodeUtils.updateNeighbors(it, this));
        }
        internal void createInstanceOfPrototype(string prototypeId, GridPosition position)
        {
            BaseConstruction construction = gameContext.constructionFactory.getInstanceOfPrototype(prototypeId, position);
            
            runningConstructionModelMap.put(construction.id, construction);
            TileNodeUtils.updateNeighbors(construction, this);
            construction.neighbors.Values.ToList()
                .Where(it => it != null)
                .ToList()
                .ForEach(it => TileNodeUtils.updateNeighbors(it, this));
            gameContext.eventManager.notifyConstructionCollectionChange();
        }

        

        BaseConstruction ITileNodeMap<BaseConstruction>.getValidNodeOrNull(GridPosition position)
        {
            return runningConstructionModelMap.Values
                .Where(it => it.saveData.position.x == position.x && it.saveData.position.y == position.y)
                .FirstOrDefault()
                ;
        }
    }
}
