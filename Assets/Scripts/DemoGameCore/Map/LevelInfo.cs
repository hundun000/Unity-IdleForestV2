using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class LevelInfo : MonoBehaviour
    {

        public Cell.CellState[,] mapInfo;     // 棋盘信息
        public Vector3 zeroPos;               // 六边形坐标系原点在屏幕中的位置
        public float cameraSize = 6f;              // 相机FOV
        public Vector3 cameraPosition = new Vector3(0, 0, -10);        // 相机位置

        public int mapCode = 0;               // 地图编号，0号为默认地图. 可以使用编号方便地切换要加载的地图
        public GameObject[] mapsToLoad;       // 存有所有挂载了MapCreater的物体，用编号可以调取其中的地图

        private List<NestMapInfo> _mapInfo;   // 临时list，存储从地图创建器中读到的地图，用于转格式



        // 从编辑器中读取地图布局并转换为二维数组，以便屏幕输出，必须被最先执行
        public void readMap()
        {
            if (mapCode >= mapsToLoad.Length)
            {
                Debug.LogError("地图编号越界！");
                return;
            }
            _mapInfo = mapsToLoad[mapCode].GetComponent<MapCreater>().colMapInfo;
            int _y = _mapInfo[0].rowMapInfo.Length;
            mapInfo = new Cell.CellState[_mapInfo.Count, _y];
            for (int i = 1; i < _mapInfo.Count; i++)
            {
                if (_mapInfo[i].rowMapInfo.Length > _y) _y = _mapInfo[i].rowMapInfo.Length;
            }
            for (int i = 0; i < _mapInfo.Count; i++)
            {
                for (int j = 0; j < _y; j++)
                {
                    if (j >= _mapInfo[i].rowMapInfo.Length) mapInfo[i, j] = Cell.CellState.None;
                    else mapInfo[i, j] = _mapInfo[i].rowMapInfo[j];
                }
            }
            Debug.Log("地图读取成功.");
        }


    }
}
