using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MapCreater : MonoBehaviour
    {
        public Vector3 zeroPos;                                      // ����������ϵԭ�㣨���½ǣ�����Ļ�е�λ��
        public float cameraSize = 5f;                                // ���FOV
        public Vector3 cameraPosition = new Vector3(0, 0, -10);      // ���λ��
        public List<NestMapInfo> colMapInfo;                         // ��ͼ��һ��
    }

    [Serializable]
    public class NestMapInfo
    {
        public Cell.CellState[] rowMapInfo;                          // ��ͼ��һ��
    }
}
