using hundun.idleshare.gamelib;
using hundun.unitygame.adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityEditor.Progress;

namespace hundun.idleshare.gamelib
{
    public class ConstructionManager
    {
        IdleGameplayContext gameContext;

        

        public ConstructionManager(IdleGameplayContext gameContext)
        {
            this.gameContext = gameContext;
        }


        /**
         * 运行中的设施集合。key: constructionId
         */
        Dictionary<String, BaseConstruction> runningConstructionModelMap = new Dictionary<String, BaseConstruction>();

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

        internal void createInstanceOfPrototype(string prototypeId)
        {
            BaseConstruction construction = gameContext.constructionFactory.getInstanceOfPrototype(prototypeId);
            runningConstructionModelMap.put(construction.id, construction);
        }



    }
}
