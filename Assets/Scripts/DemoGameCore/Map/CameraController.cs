using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class CameraController : MonoBehaviour
    {
        public Camera mainCamera;                // �ű����Ƶ����
        public Vector3 zeroPos;                  // ��ͼ����ԭ��
        public Vector3 cameraPoint;              // ������ٵ�
        public Vector2 pointSkew;                // ���ٵ������������ĵ�ƫ����
        private Transform cameraTrans;           // �����ʱ��transfrom

        private int xAxisCoefficient = 1;     // X�ᷴת
        private int yAsixCoefficient = 1;     // Y�ᷴת

        private string _mouseScrollWheel = "Mouse ScrollWheel";

        public float cameraSizeMin = 2.0f;        // �����С����
        public float cameraSizeMax = 10.0f;       // ����������
        public float keyMoveSpeed = 8;            // �����ƶ�������
        public float sensitivityDrag = 1;         // ����϶�������
        public float sensitivetyMouseWheel = 30;  // ����������������

        private Vector3 frameMovement;      // ÿ֡���˶�����
        private bool isDragging;            // ����Ƿ��ڱ����ǣ��
        private Vector3 departurePos;       // ����ڱ�ǣ��ʱÿ֡��Ŀ���

        #region ����޸ĸ�ѡ���Ҫ��������
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
            // �����ӽ��£����������ͶӰ���ȥƫ������Ϊ������ٵ��xy����
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
        /// �����ƶ������
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
        /// ����Ҽ���ק�����
        /// </summary>
        private IEnumerator MouseLeftDragControl()
        {
            while (Input.GetMouseButton(1))
            {
                //Debug.Log("��꣺" + departurePos + "  >>>>>  " + Input.mousePosition);
                frameMovement -= (Input.mousePosition - departurePos) * sensitivityDrag * 0.01f;
                departurePos = Input.mousePosition;
                yield return new WaitForFixedUpdate();
            }
                isDragging = false;
                yield break;
        }

        /// <summary>
        /// ����������
        /// </summary>
        private void MouseScrollwheelScale()
        {
            if (Input.GetAxis(_mouseScrollWheel) == 0) return;

            mainCamera.orthographicSize = mainCamera.orthographicSize - Input.GetAxis(_mouseScrollWheel) * sensitivetyMouseWheel;
            mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, cameraSizeMin, cameraSizeMax);
        }

        /// <summary>
        /// ����ƶ��Ƿ�ʹ��ͼ�����뿪��ͼ��������ִ���ƶ�
        /// </summary>
        private void leavingCheck()
        {
            Collider2D[] col = Physics2D.OverlapPointAll(cameraPoint + frameMovement);
            if(col.Length != 0) cameraTrans.Translate(frameMovement);
            frameMovement = Vector3.zero;
        }
    }
}