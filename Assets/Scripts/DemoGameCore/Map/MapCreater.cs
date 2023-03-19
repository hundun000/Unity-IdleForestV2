using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MapCreater : MonoBehaviour
    {
        public List<NestMapInfo> colMapInfo;
    }

    [Serializable]
    public class NestMapInfo
    {
        public Cell.CellState[] rowMapInfo;
    }
}
