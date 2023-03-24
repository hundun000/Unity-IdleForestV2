using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MapCreater : MonoBehaviour
    {
        public Vector3 zeroPos;                                      // 六边形坐标系原点（左下角）在屏幕中的位置
        public float cameraSize = 5f;                                // 相机FOV
        public Vector3 cameraPosition = new Vector3(0, 0, -10);      // 相机位置
        public List<NestMapInfo> colMapInfo;                         // 地图的一列
    }

    [Serializable]
    public class NestMapInfo
    {
        public Cell.CellState[] rowMapInfo;                          // 地图的一行
    }
}
