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
        public GameObject focusCircle;       // 选中格位时显示的聚焦圈
        //public Cell[,] mapLayout;      // 地图布局
        private Dictionary<GridPosition, Cell> constructionControlNodes = new();  // Cell即为一种ConstructionControlNode——控制一个设施的UI

        WorldPlayScreen parent;      // 通过代码绑定
        private bool firstCome;     // 判断是否是初次加载地图


        private void Awake()
        {
            cellRoot.GetComponent<SpriteLoader>().SpriteLoad();
            firstCome = true;
            //gameObject.GetComponent<SpriteLoader>().SpriteLoad();
            //gameObject.GetComponent<LevelInfo>().readMap();
            //BuildBoard(gameObject.GetComponent<LevelInfo>());

        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Collider2D[] col = Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                //Debug.Log(col.Length);
                if (col.Length != 0)
                {
                    if (col[0].gameObject.layer == 7)
                    focusAppear(col[0].transform.position);
                }
                else focusDisappear();
            }
        }

        // 将地图打印到屏幕上
        private void BuildBoard(BackendLevelInfo levelInfo)
        {
            // 判断是否是初次加载地图
            if (firstCome)
            {
                // 清理旧有地图
                foreach (Transform child in cellRoot.transform)
                {
                    Destroy(child.gameObject);
                }
                constructionControlNodes.Clear();

                // 初始化地图
                levelInfo.constructions.ForEach(construction => {
                    var cell = Instantiate(cellPrefab, cellRoot.transform);
                    cell.StateChangeTo(parent, construction);
                    cell.transform.position = CalculatePosition(construction.saveData.position.x, construction.saveData.position.y);
                    constructionControlNodes.Add(construction.saveData.position, cell);
                });

                // 初始化相机属性
                sceneCamera.orthographicSize = cameraSize;
                GridPosition cameraPosVector2 = constructionControlNodes.Count > 0 ? constructionControlNodes.First().Key : new GridPosition(0, 0);
                Vector3 cameraPosVector = CalculatePosition(cameraPosVector2.x, cameraPosVector2.y);
                GetComponent<CameraController>().Initialize(cameraPosVector, cameraDistance);
                firstCome = false;
            }
            else
            {
                levelInfo.constructions.ForEach(construction => {
                    Cell cell = constructionControlNodes[construction.position];
                    if (cell.construction != construction)
                    {
                        cell.StateChangeTo(parent, construction);
                    }
                });
            }


            constructionControlNodes.Values.ToList().ForEach(cell => {
                cell.updateBackendData();
            });

        }

        // 换算：预设的二维坐标系坐标 -> 六边形坐标系在屏幕空间的映射
        private Vector3 CalculatePosition(int gridX, int gridY)
        {
            Vector3 newposition = new Vector3(0, 0, 0);
            newposition.y += 0.75f * gridY;
            newposition.x += Mathf.Sqrt(3) / 2  * (gridX - (Math.Abs(gridY) % 2) / 2.0f);
            return newposition;
        }

        public void postPrefabInitialization(WorldPlayScreen playScreen)
        {
            // 通过代码绑定引用
            this.parent = playScreen;
        }

        
        public void onLogicFrame()
        {
            // 后端逻辑帧到达，说明后端数据可能有变，让constructionControlNode更新使用最新数据
            constructionControlNodes.Values.ToList().ForEach(item => item.updateBackendData());
        }

        public void onGameAreaChange(string last, string current)
        {
            // GameArea变化（包括加载后首次进入GameArea），说明设施集合可能有变，使用最新设施集合重建constructionControlNodes
            onConstructionCollectionChange();
        }

        public void onConstructionCollectionChange()
        {
            List<BaseConstruction> constructions = parent.game.idleGameplayExport.gameplayContext.constructionManager.getAreaControlableConstructionsOrEmpty(parent.area);
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


        // 在指定位置打印聚焦圈
        public void focusAppear(Vector3 destPos)
        {
            focusCircle.transform.position = destPos;
            focusCircle.SetActive(true);
        }

        // 在鼠标点击空位置时隐藏聚焦圈
        private void focusDisappear()
        {
            focusCircle.SetActive(false);
            parent.cellDetailBoardVM.updateDetail(null);
        }
    }


}