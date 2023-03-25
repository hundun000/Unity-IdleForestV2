using Assets.Scripts.DemoGameCore.logic;
using Assets.Scripts.DemoGameCore.ui.screen;
using hundun.idleshare.enginecore;
using hundun.idleshare.gamelib;
using Mono.Cecil.Cil;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Map
{
    // 格子状态. 一块格子不可能同时拥有多种状态
    public enum CellState
    {
        None,          // 关卡不涉及的土地
        IdleField,     // 已经解锁但未使用的土地
        VirginField,   // 未开垦无法使用的土地
        NoAccess,      // 以后才会解锁的土地
        Factory,       // 工厂
        Forest,        // 森林
        Waters,        // 水域
        Desert,        // 沙漠
    };

    public class Cell : MonoBehaviour
    {
        public int level;             // 工厂或森林的等级
        public bool isRunning;        // 工厂和森林的运营状态，关停或砍伐中则为false
        
        public SpriteRenderer fieldRenderer;  // 土地贴图渲染器
        public SpriteRenderer upperRenderer;  // 工厂/森林渲染器
        public Canvas cellCanvas;             // 每个格位自己的画布
        public GameObject coordinateText;     // 测试时用的坐标显示器

        public DemoPlayScreen parent; // 后端Screen引用
        public BaseConstruction construction; // 后端数据引用

        public void StateChangeTo(BaseConstruction construction)
        {
            this.construction = construction;
            switch (construction.prototypeId)
            {
                case ConstructionPrototypeId.SMALL_FACTORY:
                case ConstructionPrototypeId.BIG_FACTORY:
                    upperRenderer.color = new Color(0.75f, 0f, 0.5f);
                    break;
                case ConstructionPrototypeId.SMALL_TREE:
                case ConstructionPrototypeId.BIG_TREE:
                    upperRenderer.color = new Color(0f, 0.75f, 0f);
                    break;
                case ConstructionPrototypeId.LAKE:
                    //upperRenderer.sprite = SpriteLoader.field[3];
                    upperRenderer.color = new Color(0f, 0.5f, 1f);
                    break;
                case ConstructionPrototypeId.DESERT:
                    //upperRenderer.sprite = SpriteLoader.field[3];
                    upperRenderer.color = new Color(0.8f, 0.8f, 0f);
                    break;
                default: 
                    break;
            }
        }
        
        public void testDataPrint(Camera _camera)
        {
            cellCanvas.worldCamera = _camera;
            coordinateText.transform.position = gameObject.transform.position;
            coordinateText.GetComponent<Text>().text = construction.name + "(" + 
                construction.saveData.position.x + ", " + 
                construction.saveData.position.y + ")";
            coordinateText.SetActive(true);
        }

        internal void updateBackendData()
        {
            this.level = construction.saveData.level;
            this.isRunning = construction.saveData.workingLevel == construction.saveData.level;
        }

        internal void fakeOnClick()
        {
            parent.cellDetailBoardVM.updateDetail(construction);
        }
    }
}
