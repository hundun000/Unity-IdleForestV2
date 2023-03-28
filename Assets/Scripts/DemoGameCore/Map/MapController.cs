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
        //public Cell[,] mapLayout;      // ��ͼ����
        private List<Cell> constructionControlNodes = new List<Cell>();  // Cell��Ϊһ��ConstructionControlNode��������һ����ʩ��UI

        DemoPlayScreen parent;      // ͨ�������

        /*
        private void Awake()
        {
            gameObject.GetComponent<SpriteLoader>().SpriteLoad();
            gameObject.GetComponent<LevelInfo>().readMap();
            BuildBoard(gameObject.GetComponent<LevelInfo>());

        }
        */

        
        // ����ͼ��ӡ����Ļ��
        private void BuildBoard(BackendLevelInfo levelInfo)
        {
            // ����
            foreach (Transform child in cellRoot.transform)
            {
                Destroy(child.gameObject);
            }
            constructionControlNodes.Clear();

            // ��������

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


            // �������
            sceneCamera.orthographicSize = cameraSize;
            Vector2 cameraPosVector2 = constructionControlNodes.First().transform.position;
            sceneCamera.transform.position = new Vector3(cameraPosVector2.x, cameraPosVector2.y, -cameraDistance);

        }

        // ���㣺Ԥ��Ķ�ά����ϵ���� -> ����������ϵ����Ļ�ռ��ӳ��
        private Vector3 CalculatePosition(int gridX, int gridY)
        {
            Vector3 newposition = new Vector3(0, 0, 0);
            newposition.y += 0.75f * gridY;
            newposition.x += Mathf.Sqrt(3) / 2  * (gridX - (gridY % 2) / 2.0f);
            return newposition;
        }

        public void postPrefabInitialization(DemoPlayScreen playScreen)
        {
            // ͨ�����������
            this.parent = playScreen;
        }

        
        public void onLogicFrame()
        {
            // ����߼�֡���˵��������ݿ����б䣬��constructionControlNode����ʹ����������
            constructionControlNodes.ForEach(item => item.updateBackendData());
        }

        public void onGameAreaChange(string last, string current)
        {
            // GameArea�仯���������غ��״ν���GameArea����˵����ʩ���Ͽ����б䣬ʹ��������ʩ�����ؽ�constructionControlNodes
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
            // �����Ƴ�������ʩ��ֻ������ͨ��ʩ
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
    /// ������ͼ��������
    /// </summary>
    public class SpriteLoader
    {
        public static Sprite[] field;         // ��������
        public static Sprite[] factory;       // ��������
        public static Sprite[] forest;        // ɭ������

        public void SpriteLoad()
        {
            
        }
    }
}