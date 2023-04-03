using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DemoGameCore.logic
{
    public class OwnConstructionAchievement : AbstractAchievement
    {

        public Dictionary<String, KeyValuePair<int, int>> requireds;


        public OwnConstructionAchievement(String id, string name, string description, string congratulationText,
            Dictionary<String, KeyValuePair<int, int>> requireds
            )
                : base(id, name, description, congratulationText)
        {
            this.requireds = requireds;
        }


        override public bool checkUnloack()
        {
            var allConstructions = gameplayContext.constructionManager.getConstructions();

            foreach (var requiredEntry in requireds)
            {
                int requiredAmount = requiredEntry.Value.Key;
                int requiredLevel = requiredEntry.Value.Value;
                bool matched = allConstructions
                        .Where(it => it.prototypeId.Equals(requiredEntry.Key))
                        .Where(it => it.saveData.level >= requiredLevel)
                        .Count() >= requiredAmount;
                if (!matched)
                {
                    return false;
                }
            }

            return true;
        }
    }

    public class IdleForestAchievementLoader : IBuiltinAchievementsLoader
    {
        public Dictionary<String, AbstractAchievement> getProviderMap(Language language)
        {
            Dictionary<String, AbstractAchievement> map = new Dictionary<string, AbstractAchievement>();
            // FIXEME swich case language
            map.Add(IdleForestAchievementId.STEP_1, new OwnConstructionAchievement(
                    IdleForestAchievementId.STEP_1, 
                    "植树第一步", "拥有两个至少1级森林", "你完成了植树第一步。感谢你为环保做出的贡献！",
                    JavaFeatureForGwt.mapOf(
                        ConstructionPrototypeId.SMALL_TREE, new KeyValuePair<int, int>(2, 1)
                        )
                    ));
            return map;
        }
    }
}
