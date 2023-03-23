using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                        case ConstructionPrototypeId.WIN_PROVIDER:
                            return "奖杯购买处";
                        default:
                            return "口口";
                    }
                default:
                    switch (prototypeId)
                    {
                        case ConstructionPrototypeId.WIN_PROVIDER:
                            return "win";
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
                            return "自动获得少量木头";
                        case ConstructionPrototypeId.BIG_TREE:
                            return "自动获得大量木头";
                        case ConstructionPrototypeId.SMALL_FACTORY:
                            return "自动获得少量金钱";
                        case ConstructionPrototypeId.BIG_FACTORY:
                            return "自动获得大量金钱";
                        case ConstructionPrototypeId.DIRT:
                            return "可以建设";
                        case ConstructionPrototypeId.DESERT:
                            return "无法建设，可因绿化变为土地";
                        case ConstructionPrototypeId.LAKE:
                            return "无法建设，可因污染变为土地";
                        case ConstructionPrototypeId.RUBBISH:
                            return "无法建设，可摧毁之变为土地";
                        case ConstructionPrototypeId.WIN_PROVIDER:
                            return "购买一个奖杯以赢得胜利";
                        default:
                            return "[dic lost]";
                    }
                default:
                    switch (prototypeId)
                    {
                        case ConstructionPrototypeId.WIN_PROVIDER:
                            return "Get a trophy and win the game";
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
    }
}
