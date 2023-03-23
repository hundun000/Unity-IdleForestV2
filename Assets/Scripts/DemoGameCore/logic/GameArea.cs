using hundun.unitygame.adapters;
using hundun.unitygame.gamelib;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DemoGameCore.logic
{
    internal class GameArea
    {
        public const String AREA_SINGLE = "ENUM_AREA@AREA_SINGLE";
    
        public static readonly List<String> values = JavaFeatureExtension.ArraysAsList(AREA_SINGLE);
    }
}
