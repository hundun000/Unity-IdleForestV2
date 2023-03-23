using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Map
{
    public class Cell : MonoBehaviour
    {
        // ����״̬. һ����Ӳ�����ͬʱӵ�ж���״̬
        public enum CellState
        {
            None,          // �ؿ����漰������
            IdleField,     // �Ѿ�������δʹ�õ�����
            lockedAccess,  // ��һ��������������
            NoAccess,      // �Ժ�Ż����������
            Waters,        // ˮ������װ���οյؿ�
            Factory,       // ����
            Forest,        // ɭ��
            //Wasteland,     // �ܹ�����Ⱦ������
            //occupiedLand   // ��ɭ����׮ռ�õ�����
        };
        public CellState cellState;   // ��λ״̬
        public Vector2 location;      // ��λ����
        public int level;             // ������ɭ�ֵĵȼ�
        public bool isRunning;        // ������ɭ�ֵ���Ӫ״̬����ͣ�򿳷�����Ϊfalse
        
        private Cell[] neighbor;      // ��¼������������ڸ�λ��0Ϊ���ҷ���˳ʱ�����α��
        private int weight;           // ��λȨ�أ��������ڸ�λ�������ŵĹ��������ó�

        public SpriteRenderer fieldRenderer;  // ������ͼ��Ⱦ��
        public SpriteRenderer upperRenderer;  // ����/ɭ����Ⱦ��
        public Canvas cellCanvas;             // ÿ����λ�Լ��Ļ���
        public GameObject coordinateText;     // ����ʱ�õ�������ʾ��

        public void StateChangeTo(CellState stateTo)
        {
            switch (stateTo)
            {
                case CellState.None:
                    // �����ڿ��ø�λ���������κ�����
                    break;
                case CellState.IdleField:
                    //upperRenderer.sprite = SpriteLoader.field[0];
                    break;
                case CellState.lockedAccess:
                    //upperRenderer.sprite = SpriteLoader.field[1];
                    upperRenderer.color = new Color(0.5f, 0.5f, 0.5f);
                    break;
                case CellState.NoAccess:
                    //upperRenderer.sprite = SpriteLoader.field[2];
                    upperRenderer.color = new Color(0.75f, 0f, 0f);
                    break;
                case CellState.Waters:
                    //upperRenderer.sprite = SpriteLoader.field[3];
                    upperRenderer.color = new Color(0f, 0.5f, 1f);
                    break;
                case CellState.Factory:
                    upperRenderer.color = new Color(0f, 0f, 0.5f);
                    break;
                case CellState.Forest:
                    upperRenderer.color = new Color(0f, 0.75f, 0f);
                    break;
            }
        }
        
        public void testDataPrint(Camera _camera)
        {
            cellCanvas.worldCamera = _camera;
            coordinateText.transform.position = gameObject.transform.position;
            coordinateText.GetComponent<Text>().text = "(" + location.x + ", " + location.y + ")";
            coordinateText.SetActive(true);
        }
    }
}
