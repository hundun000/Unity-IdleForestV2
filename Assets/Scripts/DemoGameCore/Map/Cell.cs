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
    // ����״̬. һ����Ӳ�����ͬʱӵ�ж���״̬
    public enum CellState
    {
        None,          // �ؿ����漰������
        IdleField,     // �Ѿ�������δʹ�õ�����
        VirginField,   // δ�����޷�ʹ�õ�����
        NoAccess,      // �Ժ�Ż����������
        Factory,       // ����
        Forest,        // ɭ��
        Waters,        // ˮ��
        Desert,        // ɳĮ
    };

    public class Cell : MonoBehaviour
    {
        public int level;             // ������ɭ�ֵĵȼ�
        public bool isRunning;        // ������ɭ�ֵ���Ӫ״̬����ͣ�򿳷�����Ϊfalse
        
        public SpriteRenderer fieldRenderer;  // ������ͼ��Ⱦ��
        public SpriteRenderer upperRenderer;  // ����/ɭ����Ⱦ��
        public Canvas cellCanvas;             // ÿ����λ�Լ��Ļ���
        public GameObject coordinateText;     // ����ʱ�õ�������ʾ��

        public DemoPlayScreen parent; // ���Screen����
        public BaseConstruction construction; // �����������

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
