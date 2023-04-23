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
        public const String AREA_WORLD = "ENUM_AREA@AREA_WORLD";
        public const String AREA_BEE = "ENUM_AREA@AREA_BEE";

        public static readonly List<String> values = JavaFeatureExtension.ArraysAsList(AREA_WORLD, AREA_BEE);
    }
}
