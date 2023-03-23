using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MapController : MonoBehaviour
    {
        public bool isTest = false;

        public Camera sceneCamera;     // ���������
        public GameObject cellRoot;    // ��λ�����壬����ȫ����λ
        public Cell cellPrefab;        // ��λԤ����
        public Vector3 zeroPos;        // ����ϵԭ��
        public Cell[,] mapLayout;      // ��ͼ����

        private void Awake()
        {
            //gameObject.GetComponent<SpriteLoader>().SpriteLoad();
            gameObject.GetComponent<LevelInfo>().readMap();
            BuildBoard(gameObject.GetComponent<LevelInfo>());

        }


        
        // ����ͼ��ӡ����Ļ�ϣ��ڶ�ȡ��ͼ��ִ��
        private void BuildBoard(LevelInfo levelInfo)
        {
            // ����
            foreach (Transform child in cellRoot.transform)
            {
                Destroy(child.gameObject);
            }

            // ��������
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

            // �������
            sceneCamera.orthographicSize = levelInfo.cameraSize;
            sceneCamera.transform.position = levelInfo.cameraPosition;

            // ��ֵ����
        }

        // ���㣺Ԥ��Ķ�ά����ϵ���� -> ����������ϵ����Ļ�ռ��ӳ��
        private Vector3 CalculatePosition(Vector2 pos)
        {
            Vector3 newposition = zeroPos;
            newposition.y += 0.75f * pos.y;
            newposition.x += Mathf.Sqrt(3) / 2  * (pos.x - (pos.y % 2) / 2);
            return newposition;
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