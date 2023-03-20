using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class LevelInfo : MonoBehaviour
    {

        public Cell.CellState[,] mapInfo;     // ������Ϣ
        public Vector3 zeroPos;               // ����������ϵԭ������Ļ�е�λ��
        public float cameraSize = 6f;              // ���FOV
        public Vector3 cameraPosition = new Vector3(0, 0, -10);        // ���λ��

        public int mapCode = 0;               // ��ͼ��ţ�0��ΪĬ�ϵ�ͼ. ����ʹ�ñ�ŷ�����л�Ҫ���صĵ�ͼ
        public GameObject[] mapsToLoad;       // �������й�����MapCreater�����壬�ñ�ſ��Ե�ȡ���еĵ�ͼ

        private List<NestMapInfo> _mapInfo;   // ��ʱlist���洢�ӵ�ͼ�������ж����ĵ�ͼ������ת��ʽ



        // �ӱ༭���ж�ȡ��ͼ���ֲ�ת��Ϊ��ά���飬�Ա���Ļ��������뱻����ִ��
        public void readMap()
        {
            if (mapCode >= mapsToLoad.Length)
            {
                Debug.LogError("��ͼ���Խ�磡");
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
            Debug.Log("��ͼ��ȡ�ɹ�.");
        }


    }
}
