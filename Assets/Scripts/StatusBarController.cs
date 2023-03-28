using Assets.Scripts.DemoGameCore.logic;
using Assets.Scripts.DemoGameCore.ui.screen;
using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBarController : MonoBehaviour, ILogicFrameListener
{
    private GameObject fill;
    private Image fillImage;
    private GameObject lines;
    private GameObject[] line=new GameObject[4];

    private float width;
    private float fillAmount;

    [SerializeField]
    float maxValue,minValue; //碳量最大最小值
    [SerializeField]
    Color[] fillAreaColor=new Color[3]; //各个段的颜色
    [SerializeField]
    Color[] lineColor=new Color[4]; //各条线的颜色
    [SerializeField]
    float[] linePosition=new float[4]; //各条线的位置
    [SerializeField]
    float[] lineWidth=new float[4]; //各条线的宽度（比例）

    DemoPlayScreen parent;      // 通过代码绑定

    private void Start()
    {
        fill=transform.GetChild(0).gameObject;
        fillImage=fill.GetComponent<Image>();
        lines=transform.GetChild(1).gameObject;
        width=GetComponent<RectTransform>().rect.width;

        //为每条线设定其位置，宽度和颜色
        for(int i=0;i<4;i++)
        {
            line[i]=lines.transform.GetChild(i).gameObject;
            line[i].GetComponent<Image>().color=lineColor[i];
            line[i].GetComponent<RectTransform>().offsetMin=new Vector2(width*linePosition[i]-lineWidth[i]*width/2,0);
            line[i].GetComponent<RectTransform>().offsetMax=new Vector2(-width*(1-linePosition[i])+lineWidth[i]*width/2,0);
        }
    }

    public void postPrefabInitialization(DemoPlayScreen playScreen)
    {
        // 通过代码绑定引用
        this.parent = playScreen;
    }

    //设置当前碳量
    public void SetValue(float value)
    {
        fillAmount=value/(maxValue-minValue);
        fill.GetComponent<Image>().fillAmount=fillAmount;

        //更新当前条的颜色
        if(fillAmount<linePosition[0])
        {
            fillImage.color=fillAreaColor[0];
        }
        else if(fillAmount>linePosition[0] && fillAmount<linePosition[1])
        {
            fillImage.color=fillAreaColor[1];
        }
        else if(fillAmount>linePosition[1])
        {
            fillImage.color=fillAreaColor[2];
        }
    }

    private float amountToRate(long amount)
    {
        return (float)(5.0 * Math.Log(amount * 1.0f));
    }

    void ILogicFrameListener.onLogicFrame()
    {
        // 后端逻辑帧到达，说明后端数据可能有变，让this更新使用最新数据
        long amount = parent.game.idleGameplayExport.getResourceNumOrZero(ResourceType.CARBON);
        SetValue(amountToRate(amount));
    }
}
