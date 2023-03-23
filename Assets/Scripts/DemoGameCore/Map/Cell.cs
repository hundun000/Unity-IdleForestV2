using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Map
{
    public class Cell : MonoBehaviour
    {
        // 格子状态. 一块格子不可能同时拥有多种状态
        public enum CellState
        {
            None,          // 关卡不涉及的土地
            IdleField,     // 已经解锁但未使用的土地
            lockedAccess,  // 下一步将解锁的土地
            NoAccess,      // 以后才会解锁的土地
            Waters,        // 水域，用于装饰镂空地块
            Factory,       // 工厂
            Forest,        // 森林
            //Wasteland,     // 受工厂污染的土地
            //occupiedLand   // 被森林树桩占用的土地
        };
        public CellState cellState;   // 格位状态
        public Vector2 location;      // 格位坐标
        public int level;             // 工厂或森林的等级
        public bool isRunning;        // 工厂和森林的运营状态，关停或砍伐中则为false
        
        private Cell[] neighbor;      // 记录六个方向的相邻格位；0为正右方，顺时针依次编号
        private int weight;           // 格位权重，根据相邻格位中运行着的工厂数量得出

        public SpriteRenderer fieldRenderer;  // 土地贴图渲染器
        public SpriteRenderer upperRenderer;  // 工厂/森林渲染器
        public Canvas cellCanvas;             // 每个格位自己的画布
        public GameObject coordinateText;     // 测试时用的坐标显示器

        public void StateChangeTo(CellState stateTo)
        {
            switch (stateTo)
            {
                case CellState.None:
                    // 不属于可用格位，不加载任何内容
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
