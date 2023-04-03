using Assets.Scripts.DemoGameCore.ui.screen;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SystemMenuController: MonoBehaviour
{
    DemoPlayScreen parent;
    //the ButtonPauseMenu
    public GameObject ingameMenu;
    private PlayerInput playerInput;

    void Start()
    {
        playerInput = GameObject.FindObjectOfType<PlayerInput>();
    }

    public void postPrefabInitialization(DemoPlayScreen playScreen)
    {
        // 通过代码绑定引用
        this.parent = playScreen;
    }

    public void OnPause()//�������ͣ��ʱִ�д˷���
    {
        //playerInput.SwitchCurrentActionMap("UI");
        Time.timeScale = 0;
        ingameMenu.SetActive(true);
    }

    public void OnResume()//������ص���Ϸ��ʱִ�д˷���
    {
        //playerInput.SwitchCurrentActionMap("Player");
        //Time.timeScale = 1f;
        ingameMenu.SetActive(false);
    }

    //public void OnRestart()
    //{
    //    //playerInput.SwitchCurrentActionMap("Player");
    //    // Loading Scene0
    //    UnityEngine.SceneManagement.SceneManager.LoadScene(DemoMenuScreen.SCENE_NAME);
    //    // 保存游戏进度，这样从MenuScreen继续游戏时才是正确的数据
    //    parent.game.saveHandler.gameSaveCurrent();

    //    Time.timeScale = 1f;
    //}

    public void OnExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
    public void Onclicktheitem()
    {
        GameObject test1 = GameObject.Find("Canvas");
        GameObject test = test1.transform.Find("epinbox").gameObject;
        if (test.GetComponent<CanvasGroup>().alpha == 1)
        {
            test.GetComponent<CanvasGroup>().alpha = 0;
        }
    }
    public void _Update()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            GameObject test1 = GameObject.Find("Canvas");
            GameObject test = test1.transform.Find("item").gameObject;
            test.SetActive(!test.activeSelf);
        }
    }
}

