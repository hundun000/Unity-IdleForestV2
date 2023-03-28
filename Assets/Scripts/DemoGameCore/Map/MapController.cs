using Assets.Scripts.DemoGameCore.ui.screen;
using Assets.Scripts.DemoGameCore.ui.sub;
using hundun.idleshare.enginecore;
using hundun.idleshare.gamelib;
using hundun.unitygame.adapters;
using hundun.unitygame.gamelib;
using Mono.Cecil.Cil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Map
{

    public class BackendLevelInfo
    {

        public List<BaseConstruction> constructions;     // 棋盘信息

        internal static BackendLevelInfo from(List<BaseConstruction> constructions)
        {
            var result = new BackendLevelInfo();
            result.constructions = constructions;
            return result;
        }
    }


    public class MapController : MonoBehaviour, 
        ILogicFrameListener,
        IGameAreaChangeListener,
        IConstructionCollectionListener
    {
        public bool isTest = false;

        public Camera sceneCamera;           // 场景的相机
        public float cameraSize = 5;         // 相机尺寸
        public float cameraDistance = 10;    // 相机初始离屏幕位置
        public GameObject cellRoot;          // 格位根物体，下有全部格位
        public Cell cellPrefab;              // 格位预制体
        //public Cell[,] mapLayout;      // 地图布局
        private List<Cell> constructionControlNodes = new List<Cell>();  // Cell即为一种ConstructionControlNode——控制一个设施的UI

        DemoPlayScreen parent;      // 通过代码绑定

        /*
        private void Awake()
        {
            gameObject.GetComponent<SpriteLoader>().SpriteLoad();
            gameObject.GetComponent<LevelInfo>().readMap();
            BuildBoard(gameObject.GetComponent<LevelInfo>());

        }
        */

        
        // 将地图打印到屏幕上
        private void BuildBoard(BackendLevelInfo levelInfo)
        {
            // 清理
            foreach (Transform child in cellRoot.transform)
            {
                Destroy(child.gameObject);
            }
            constructionControlNodes.Clear();

            // 加载网格

            levelInfo.constructions.ForEach(construction => { 
                var cell = Instantiate(cellPrefab, cellRoot.transform);
                cell.StateChangeTo(parent, construction);
                cell.transform.position = CalculatePosition(construction.saveData.position.x, construction.saveData.position.y);
                constructionControlNodes.Add(cell);
            });


            


            constructionControlNodes.ForEach(cell => {
                cell.updateBackendData();
                if (isTest)
                {
                    cell.testDataPrint(sceneCamera);
                }
            });


            // 相机调整
            sceneCamera.orthographicSize = cameraSize;
            Vector2 cameraPosVector2 = constructionControlNodes.First().transform.position;
            sceneCamera.transform.position = new Vector3(cameraPosVector2.x, cameraPosVector2.y, -cameraDistance);

        }

        // 换算：预设的二维坐标系坐标 -> 六边形坐标系在屏幕空间的映射
        private Vector3 CalculatePosition(int gridX, int gridY)
        {
            Vector3 newposition = new Vector3(0, 0, 0);
            newposition.y += 0.75f * gridY;
            newposition.x += Mathf.Sqrt(3) / 2  * (gridX - (gridY % 2) / 2.0f);
            return newposition;
        }

        public void postPrefabInitialization(DemoPlayScreen playScreen)
        {
            // 通过代码绑定引用
            this.parent = playScreen;
        }

        
        public void onLogicFrame()
        {
            // 后端逻辑帧到达，说明后端数据可能有变，让constructionControlNode更新使用最新数据
            constructionControlNodes.ForEach(item => item.updateBackendData());
        }

        public void onGameAreaChange(string last, string current)
        {
            // GameArea变化（包括加载后首次进入GameArea），说明设施集合可能有变，使用最新设施集合重建constructionControlNodes
            onConstructionCollectionChange();
        }

        public void onConstructionCollectionChange()
        {
            List<BaseConstruction> constructions = parent.game.idleGameplayExport.getAreaShownConstructionsOrEmpty(parent.area);
            constructions = filterConstructions(constructions);

            BackendLevelInfo backendLevelInfo = BackendLevelInfo.from(constructions);

            BuildBoard(backendLevelInfo);

            parent.game.frontend.log(this.getClass().getSimpleName(), "MapController change to: " + String.Join(",",
                constructions.Select(construction => construction.name))
            );
        }

        private List<BaseConstruction> filterConstructions(List<BaseConstruction> constructions)
        {
            // 过滤移除特殊设施；只管理普通设施
            return constructions
                .Where(it => !SpecialConstructionControlBoardVM.specialConstructionPrototypeIds.Contains(it.saveData.prototypeId))
                .ToList();
        }

        private void OnMouseUpOutsideCells()
        {
            parent.cellDetailBoardVM.updateDetail(null);
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