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
        private void quickAddOwnConstructionAchievement(Dictionary<String, AbstractAchievement> map, String id, Dictionary<String, List<String>> textMap, Dictionary<String, KeyValuePair<int, int>> requireds)
        {
            AbstractAchievement achievement =  new OwnConstructionAchievement(
                    id,
                    textMap[id][0], textMap[id][1], textMap[id][2],
                    requireds
                    );
            map.Add(id, achievement);
        }



        public Dictionary<String, AbstractAchievement> getProviderMap(Language language)
        {
            Dictionary<String, List<String>> textMap = new Dictionary<String, List<String>>();
            switch (language) 
            {
                case Language.CN:
                    textMap.Add(IdleForestAchievementId.STEP_1, new List<string> {
                        "NO.1",
                        "拥有两个小森林。",
                        "你完成了任务NO.1。"
                    });
                    textMap.Add(IdleForestAchievementId.STEP_2, new List<string> {
                        "NO.2",
                        "拥有两个小工厂。",
                        "你完成了任务NO.2。"
                    });
                    textMap.Add(IdleForestAchievementId.STEP_3, new List<string> {
                        "NO.3",
                        "拥有一个中森林。",
                        "你完成了任务NO.3。"
                    });
                    textMap.Add(IdleForestAchievementId.STEP_4, new List<string> {
                        "NO.4",
                        "拥有一个中工厂。",
                        "你完成了任务NO.4。"
                    });
                    textMap.Add(IdleForestAchievementId.STEP_5, new List<string> {
                        "NO.5",
                        "拥有一个大森林。",
                        "你完成了任务NO.5。"
                    });
                    textMap.Add(IdleForestAchievementId.STEP_6, new List<string> {
                        "NO.6",
                       "拥有一个大工厂。",
                        "你完成了任务NO.6。"
                    });
                    break;
                default:
                    textMap.Add(IdleForestAchievementId.STEP_1, new List<string> {
                        "NO.1",
                        "Own two small forests.",
                        "You completed Quest NO.1.\nThank you for your tireless efforts to protect our planet and make it a better place for future generations."
                    });
                    textMap.Add(IdleForestAchievementId.STEP_2, new List<string> {
                        "NO.2",
                        "Own two small factories.",
                        "You completed Quest NO.2.\nWe are thrilled to see the positive impact on our local economy with more jobs and increased business opportunities. Thank you to all who have contributed to our economic success."
                    });
                    textMap.Add(IdleForestAchievementId.STEP_3, new List<string> {
                        "NO.3",
                        "Own one medium forest.",
                        "You completed Quest NO.1.\nYour environmental achievements are truly inspiring and have made a significant impact in the fight against climate change."
                    });
                    textMap.Add(IdleForestAchievementId.STEP_4, new List<string> {
                        "NO.4",
                        "Own one medium factory.",
                        "You completed Quest NO.4.\nOur community is thriving thanks to the hard work and dedication of our entrepreneurs, small business owners, and investors. Your contributions are greatly appreciated."
                    });
                    textMap.Add(IdleForestAchievementId.STEP_5, new List<string> {
                        "NO.5",
                        "Own one big forest.",
                        "You completed Quest NO.5.\nWe are grateful for the positive change you have brought to our community and the world through your dedication to environmental sustainability."
                    });
                    textMap.Add(IdleForestAchievementId.STEP_6, new List<string> {
                        "NO.6",
                        "Own one big factory.",
                        "You completed Quest NO.6.\nIt is with immense gratitude that we thank our community leaders and government officials for their support in creating a business-friendly environment and fostering economic growth."
                    });
                    break;
            }



            Dictionary<String, AbstractAchievement> map = new Dictionary<string, AbstractAchievement>();
            quickAddOwnConstructionAchievement(
                map,
                IdleForestAchievementId.STEP_1, 
                textMap,
                JavaFeatureForGwt.mapOf(
                        ConstructionPrototypeId.SMALL_TREE, new KeyValuePair<int, int>(2, 1)
                        )
            );
            quickAddOwnConstructionAchievement(
                map,
                IdleForestAchievementId.STEP_2,
                textMap,
                JavaFeatureForGwt.mapOf(
                        ConstructionPrototypeId.SMALL_FACTORY, new KeyValuePair<int, int>(2, 1)
                        )
            );
            quickAddOwnConstructionAchievement(
                map,
                IdleForestAchievementId.STEP_3,
                textMap,
                JavaFeatureForGwt.mapOf(
                        ConstructionPrototypeId.MID_TREE, new KeyValuePair<int, int>(1, 1)
                        )
            );
            quickAddOwnConstructionAchievement(
                map,
                IdleForestAchievementId.STEP_4,
                textMap,
                JavaFeatureForGwt.mapOf(
                        ConstructionPrototypeId.MID_FACTORY, new KeyValuePair<int, int>(1, 1)
                        )
            );
            quickAddOwnConstructionAchievement(
                map,
                IdleForestAchievementId.STEP_5,
                textMap,
                JavaFeatureForGwt.mapOf(
                        ConstructionPrototypeId.BIG_TREE, new KeyValuePair<int, int>(1, 1)
                        )
            );
            quickAddOwnConstructionAchievement(
                map,
                IdleForestAchievementId.STEP_6,
                textMap,
                JavaFeatureForGwt.mapOf(
                        ConstructionPrototypeId.BIG_FACTORY, new KeyValuePair<int, int>(1, 1)
                        )
            );
            return map;
        }
    }
}
