using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MapController : MonoBehaviour
    {
        public bool isTest = false;

        public Camera sceneCamera;     // 场景的相机
        public GameObject cellRoot;    // 格位根物体，下有全部格位
        public Cell cellPrefab;        // 格位预制体
        public Vector3 zeroPos;        // 坐标系原点
        public Cell[,] mapLayout;      // 地图布局

        private void Awake()
        {
            //gameObject.GetComponent<SpriteLoader>().SpriteLoad();
            gameObject.GetComponent<LevelInfo>().readMap();
            BuildBoard(gameObject.GetComponent<LevelInfo>());

        }


        
        // 将地图打印到屏幕上，在读取地图后执行
        private void BuildBoard(LevelInfo levelInfo)
        {
            // 清理
            foreach (Transform child in cellRoot.transform)
            {
                Destroy(child.gameObject);
            }

            // 加载网格
            zeroPos = levelInfo.zeroPos;
            mapLayout = new Cell[levelInfo.mapInfo.GetLength(0), levelInfo.mapInfo.GetLength(1)];
            for (var i = 0; i < levelInfo.mapInfo.GetLength(0); ++i)
            {
                for (var j = 0; j < levelInfo.mapInfo.GetLength(1); ++j)
                {
                    if (levelInfo.mapInfo[i, j] <= 0) continue;
                    mapLayout[i, j] = Instantiate(cellPrefab, cellRoot.transform);
                    mapLayout[i, j].StateChangeTo(levelInfo.mapInfo[i, j]);
                    mapLayout[i, j].location = new Vector2(i, j);
                    mapLayout[i, j].transform.position = CalculatePosition(mapLayout[i, j].location);
                    if (isTest) mapLayout[i, j].GetComponent<Cell>().testDataPrint(sceneCamera);
                }
            }

            // 相机调整
            sceneCamera.orthographicSize = levelInfo.cameraSize;
            sceneCamera.transform.position = levelInfo.cameraPosition;

            // 数值加载
        }

        // 换算：预设的二维坐标系坐标 -> 六边形坐标系在屏幕空间的映射
        private Vector3 CalculatePosition(Vector2 pos)
        {
            Vector3 newposition = zeroPos;
            newposition.y += 0.75f * pos.y;
            newposition.x += Mathf.Sqrt(3) / 2  * (pos.x - (pos.y % 2) / 2);
            return newposition;
        }


    }

    /// <summary>
    /// 精灵贴图加载器类
    /// </summary>
    public class SpriteLoader
    {
        public static Sprite[] field;         // 地面纹理
        public static Sprite[] factory;       // 工厂纹理
        public static Sprite[] forest;        // 森林纹理

        public void SpriteLoad()
        {
            
        }
    }
}