using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class CameraController : MonoBehaviour
    {
        public Camera mainCamera;                // 脚本控制的相机
        public Vector3 zeroPos;                  // 地图坐标原点
        public Vector3 cameraPoint;              // 相机跟踪点
        public Vector2 pointSkew;                // 跟踪点相对于相机中心的偏移量
        private Transform cameraTrans;           // 相机此时的transfrom

        private int xAxisCoefficient = 1;     // X轴反转
        private int yAsixCoefficient = 1;     // Y轴反转

        private string _mouseScrollWheel = "Mouse ScrollWheel";

        public float cameraSizeMin = 2.0f;        // 相机最小缩放
        public float cameraSizeMax = 10.0f;       // 相机最大缩放
        public float keyMoveSpeed = 8;            // 键盘移动灵敏度
        public float sensitivityDrag = 1;         // 鼠标拖动灵敏度
        public float sensitivetyMouseWheel = 30;  // 鼠标滚轮缩放灵敏度

        private Vector3 frameMovement;      // 每帧的运动距离
        private bool isDragging;            // 相机是否在被鼠标牵引
        private Vector3 departurePos;       // 相机在被牵引时每帧的目标点

        #region 面板修改该选项后要重新启动
        public bool xAxisinversion = false;
        public bool yAsixinversion = false;
        #endregion

        private void Start()
        {
            cameraTrans = mainCamera.transform;
            cameraTrans.position = new(zeroPos.x + pointSkew.x, zeroPos.y + pointSkew.y, cameraTrans.position.z);
            isDragging = false;

            xAxisCoefficient = xAxisinversion ? -1 : 1;
            yAsixCoefficient = yAsixinversion ? -1 : 1;
        }

        private void Update()
        {
            if (cameraTrans == null) return;
            // 正交视角下，相机的中心投影点减去偏移量就为相机跟踪点的xy坐标
            cameraPoint = new(cameraTrans.position.x - pointSkew.x, cameraTrans.position.y - pointSkew.y, 0);

            KeyBoardControl();

            if (!isDragging && Input.GetMouseButton(1))
            {
                isDragging = true;
                departurePos = Input.mousePosition;
                StartCoroutine(nameof(MouseLeftDragControl));
            }
            leavingCheck();

            MouseScrollwheelScale();

        }

        /// <summary>
        /// 键盘移动摄像机
        /// </summary>
        private void KeyBoardControl()
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                frameMovement += Vector3.up * Time.deltaTime * keyMoveSpeed;
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                frameMovement += Vector3.down * Time.deltaTime * keyMoveSpeed;
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                frameMovement += Vector3.left * Time.deltaTime * keyMoveSpeed;
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
               frameMovement += Vector3.right * Time.deltaTime * keyMoveSpeed;
            }
        }

        /// <summary>
        /// 鼠标右键拖拽摄像机
        /// </summary>
        private IEnumerator MouseLeftDragControl()
        {
            while (Input.GetMouseButton(1))
            {
                //Debug.Log("鼠标：" + departurePos + "  >>>>>  " + Input.mousePosition);
                frameMovement -= (Input.mousePosition - departurePos) * sensitivityDrag * 0.01f;
                departurePos = Input.mousePosition;
                yield return new WaitForFixedUpdate();
            }
                isDragging = false;
                yield break;
        }

        /// <summary>
        /// 鼠标滚轮缩放
        /// </summary>
        private void MouseScrollwheelScale()
        {
            if (Input.GetAxis(_mouseScrollWheel) == 0) return;

            mainCamera.orthographicSize = mainCamera.orthographicSize - Input.GetAxis(_mouseScrollWheel) * sensitivetyMouseWheel;
            mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, cameraSizeMin, cameraSizeMax);
        }

        /// <summary>
        /// 监测移动是否将使视图中心离开地图，若否，则执行移动
        /// </summary>
        private void leavingCheck()
        {
            Collider2D[] col = Physics2D.OverlapPointAll(cameraPoint + frameMovement);
            if(col.Length != 0) cameraTrans.Translate(frameMovement);
            frameMovement = Vector3.zero;
        }
    }
}