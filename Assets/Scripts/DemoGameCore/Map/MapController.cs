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

        public List<BaseConstruction> constructions;     // ������Ϣ

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

        public Camera sceneCamera;           // ���������
        public float cameraSize = 5;         // ����ߴ�
        public float cameraDistance = 10;    // �����ʼ����Ļλ��
        public GameObject cellRoot;          // ��λ�����壬����ȫ����λ
        public Cell cellPrefab;              // ��λԤ����
        public GameObject focusCircle;       // ѡ�и�λʱ��ʾ�ľ۽�Ȧ
        //public Cell[,] mapLayout;      // ��ͼ����
        private Dictionary<GridPosition, Cell> constructionControlNodes = new();  // Cell��Ϊһ��ConstructionControlNode��������һ����ʩ��UI

        WorldPlayScreen parent;      // ͨ�������
        private bool firstCome;     // �ж��Ƿ��ǳ��μ��ص�ͼ


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

        // ����ͼ��ӡ����Ļ��
        private void BuildBoard(BackendLevelInfo levelInfo)
        {
            // �ж��Ƿ��ǳ��μ��ص�ͼ
            if (firstCome)
            {
                // ������е�ͼ
                foreach (Transform child in cellRoot.transform)
                {
                    Destroy(child.gameObject);
                }
                constructionControlNodes.Clear();

                // ��ʼ����ͼ
                levelInfo.constructions.ForEach(construction => {
                    var cell = Instantiate(cellPrefab, cellRoot.transform);
                    cell.StateChangeTo(parent, construction);
                    cell.transform.position = CalculatePosition(construction.saveData.position.x, construction.saveData.position.y);
                    constructionControlNodes.Add(construction.saveData.position, cell);
                });

                // ��ʼ���������
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

        // ���㣺Ԥ��Ķ�ά����ϵ���� -> ����������ϵ����Ļ�ռ��ӳ��
        private Vector3 CalculatePosition(int gridX, int gridY)
        {
            Vector3 newposition = new Vector3(0, 0, 0);
            newposition.y += 0.75f * gridY;
            newposition.x += Mathf.Sqrt(3) / 2  * (gridX - (Math.Abs(gridY) % 2) / 2.0f);
            return newposition;
        }

        public void postPrefabInitialization(WorldPlayScreen playScreen)
        {
            // ͨ�����������
            this.parent = playScreen;
        }

        
        public void onLogicFrame()
        {
            // ����߼�֡���˵��������ݿ����б䣬��constructionControlNode����ʹ����������
            constructionControlNodes.Values.ToList().ForEach(item => item.updateBackendData());
        }

        public void onGameAreaChange(string last, string current)
        {
            // GameArea�仯���������غ��״ν���GameArea����˵����ʩ���Ͽ����б䣬ʹ��������ʩ�����ؽ�constructionControlNodes
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
            // �����Ƴ�������ʩ��ֻ������ͨ��ʩ
            return constructions
                .Where(it => !SpecialConstructionControlBoardVM.specialConstructionPrototypeIds.Contains(it.saveData.prototypeId))
                .ToList();
        }


        // ��ָ��λ�ô�ӡ�۽�Ȧ
        public void focusAppear(Vector3 destPos)
        {
            focusCircle.transform.position = destPos;
            focusCircle.SetActive(true);
        }

        // ���������λ��ʱ���ؾ۽�Ȧ
        private void focusDisappear()
        {
            focusCircle.SetActive(false);
            parent.cellDetailBoardVM.updateDetail(null);
        }
    }


}