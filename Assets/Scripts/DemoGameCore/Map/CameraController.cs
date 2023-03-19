using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class CameraController : MonoBehaviour
    {
        public Camera mainCamera;
        private Transform cameraTrans;

        private int xAxisCoefficient = 1;
        private int yAsixCoefficient = 1;

        private string _mouseScrollWheel = "Mouse ScrollWheel";

        public float cameraSizeMin = 2.0f;
        public float cameraSizeMax = 10.0f;
        public float keyMoveSpeed = 8;
        public float sensitivityDrag = 1;
        public float sensitivetyMouseWheel = 30;

        private bool isDragging;
        private Vector3 departurePos;

        #region 面板修改该选项后要重新启动
        public bool xAxisinversion = false;
        public bool yAsixinversion = false;
        #endregion

        private void Start()
        {
            cameraTrans = mainCamera.transform;
            isDragging = false;

            xAxisCoefficient = xAxisinversion ? -1 : 1;
            yAsixCoefficient = yAsixinversion ? -1 : 1;
        }

        private void Update()
        {
            if (cameraTrans == null) return;

            KeyBoardControl();

            if (!isDragging && Input.GetMouseButton(0))
            {
                isDragging = true;
                departurePos = Input.mousePosition;
                StartCoroutine(nameof(MouseLeftDragControl));
            }

            MouseScrollwheelScale();
        }

        /// <summary>
        /// 键盘移动摄像机
        /// </summary>
        private void KeyBoardControl()
        {
            if (Input.GetKey(KeyCode.W))
            {
                cameraTrans.Translate(Vector3.up * Time.deltaTime * keyMoveSpeed);
            }

            if (Input.GetKey(KeyCode.S))
            {
                cameraTrans.Translate(Vector3.down * Time.deltaTime * keyMoveSpeed);
            }

            if (Input.GetKey(KeyCode.A))
            {
                cameraTrans.Translate(Vector3.left * Time.deltaTime * keyMoveSpeed);
            }

            if (Input.GetKey(KeyCode.D))
            {
                cameraTrans.Translate(Vector3.right * Time.deltaTime * keyMoveSpeed);
            }
        }

        /// <summary>
        /// 鼠标左键拖拽摄像机
        /// </summary>
        private IEnumerator MouseLeftDragControl()
        {
            while (Input.GetMouseButton(0))
            {
                //Debug.Log("鼠标：" + departurePos + "  >>>>>  " + Input.mousePosition);
                cameraTrans.position -= (Input.mousePosition - departurePos) * sensitivityDrag * 0.01f;
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
            Debug.Log(mainCamera.orthographicSize);
        }
    }
}