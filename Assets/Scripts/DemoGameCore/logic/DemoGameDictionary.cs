using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Assertions;

namespace Assets.Scripts.DemoGameCore.logic
{
    public class DemoGameDictionary : IGameDictionary
    {

        public String constructionPrototypeIdToShowName(Language language, String prototypeId)
        {
            switch (language)
            {
                case Language.CN:
                    switch (prototypeId)
                    {
                        
                        case ConstructionPrototypeId.SMALL_TREE:
                            return "小树";
                        case ConstructionPrototypeId.BIG_TREE:
                            return "大树";
                        case ConstructionPrototypeId.SMALL_FACTORY:
                            return "小工厂";
                        case ConstructionPrototypeId.MID_FACTORY:
                            return "中工厂";
                        case ConstructionPrototypeId.BIG_FACTORY:
                            return "大工厂";
                        case ConstructionPrototypeId.DESERT:
                            return "沙漠";
                        case ConstructionPrototypeId.DIRT:
                            return "土";
                        case ConstructionPrototypeId.LAKE:
                            return "湖";
                        case ConstructionPrototypeId.RUBBISH:
                            return "垃圾堆";
                        case ConstructionPrototypeId.GOVERNMENT:
                            return "政府";
                        default:
                            return "口口";
                    }
                default:
                    switch (prototypeId)
                    {
                        case ConstructionPrototypeId.SMALL_TREE:
                            return "forest";
                        case ConstructionPrototypeId.BIG_TREE:
                            return "big forest";
                        case ConstructionPrototypeId.SMALL_FACTORY:
                            return "small factory";
                        case ConstructionPrototypeId.MID_FACTORY:
                            return "medium factory";
                        case ConstructionPrototypeId.BIG_FACTORY:
                            return "large factory";
                        case ConstructionPrototypeId.DESERT:
                            return "desert";
                        case ConstructionPrototypeId.DIRT:
                            return "construction land";
                        case ConstructionPrototypeId.LAKE:
                            return "lake";
                        case ConstructionPrototypeId.RUBBISH:
                            return "wasteland";
                        case ConstructionPrototypeId.GOVERNMENT:
                            return "government";
                        default:
                            return "[dic lost]";
                    }
            }


        }

        public String constructionPrototypeIdToDetailDescroptionConstPart(Language language, String prototypeId)
        {
            switch (language)
            {
                case Language.CN:
                    switch (prototypeId)
                    {
                        
                        case ConstructionPrototypeId.SMALL_TREE:
                        case ConstructionPrototypeId.BIG_TREE:
                            return "消耗二氧化碳，产出木头。相邻的森林能更快提高工作效率。";
                        case ConstructionPrototypeId.SMALL_FACTORY:
                        case ConstructionPrototypeId.MID_FACTORY:
                        case ConstructionPrototypeId.BIG_FACTORY:
                            return "产出金钱和二氧化碳。相邻的工厂能更快提高工作效率。";
                        case ConstructionPrototypeId.DIRT:
                            return "可以建设森林或工厂。";
                        case ConstructionPrototypeId.DESERT:
                            return "自身土壤化进度受周围森林和工厂影响，土壤化进度满时变为建设用地。";
                        case ConstructionPrototypeId.LAKE:
                            return "能提高周围森林和工厂的工作效率。自身干涸度受周围森林和工厂影响，干涸度满时变为荒地。";
                        case ConstructionPrototypeId.RUBBISH:
                            return "清理后变为建设用地。";
                        case ConstructionPrototypeId.GOVERNMENT:
                            return "TODO";
                        default:
                            return "[dic lost]";
                    }
                default:
                    switch (prototypeId)
                    {
                        case ConstructionPrototypeId.SMALL_TREE:
                        case ConstructionPrototypeId.BIG_TREE:
                            return "The forest consumes carbon dioxide and produce wood. Adjacent forests can increase work efficiency faster.";
                        case ConstructionPrototypeId.SMALL_FACTORY:
                        case ConstructionPrototypeId.MID_FACTORY:
                        case ConstructionPrototypeId.BIG_FACTORY:
                            return "The factory produces money and carbon dioxide. Adjacent factories can increase work efficiency faster.";
                        case ConstructionPrototypeId.DIRT:
                            return "Forests or factories can be built.";
                        case ConstructionPrototypeId.DESERT:
                            return "The reclamation progress is affected by the surrounding constructions, and when the progress is full, it becomes a construction land.";
                        case ConstructionPrototypeId.LAKE:
                            return "Can improve the working efficiency of adjacent constructions. The dryness is affected by the surrounding constructions, and when the dryness is full, it becomes a wasteland.";
                        case ConstructionPrototypeId.RUBBISH:
                            return "Turned into construction land after clearing.";
                        case ConstructionPrototypeId.GOVERNMENT:
                            return "TODO";
                        default:
                            return "[dic lost]";
                    }
            }


        }

        public List<String> getMemuScreenTexts(Language language)
        {
            switch (language)
            {
                case Language.CN:
                    return JavaFeatureForGwt.arraysAsList("Idle样例", "新游戏", "继续游戏", "语言", "重启后生效");
                default:
                    return JavaFeatureForGwt.arraysAsList("IdleDemo", "New Game", "Continue", "Language", "Take effect after restart");
            }
        }

        public Dictionary<Language, string> getLanguageShowNameMap()
        {
            return JavaFeatureForGwt.mapOf(
                Language.CN, "中文",
                Language.EN, "English"
                );
        }

        public List<string> getAchievementTexts(Language language)
        {
            switch (language)
            {
                case Language.CN:
                    return JavaFeatureForGwt.arraysAsList("当前任务：", "已完成：", "回到游戏");
                default:
                    return JavaFeatureForGwt.arraysAsList("Quest: ", "Completed: ", "back");
            }
        }

        public List<string> getPlayScreenTexts(Language language)
        {
            switch (language)
            {
                case Language.CN:
                    return JavaFeatureForGwt.arraysAsList("购买", "购买费用");
                default:
                    return JavaFeatureForGwt.arraysAsList("Build", "Build cost");
            }
        }
    }
}
