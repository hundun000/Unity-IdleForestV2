using hundun.unitygame.adapters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DemoGameCore.logic
{
    public class ResourceType
    {
        public const String COIN = "ENUM_RESC@COIN";
        public const String WOOD = "ENUM_RESC@WOOD";
        public const String CARBON = "ENUM_RESC@CARBON";
        //public static final String WIN_TROPHY = "ENUM_RESC@TROPHY";

        public static readonly List<String> VALUES_FOR_SHOW_ORDER = JavaFeatureExtension.ArraysAsList(COIN, WOOD, CARBON);

    }
}
