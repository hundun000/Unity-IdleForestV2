using Assets.Scripts.DemoGameCore;
using Assets.Scripts.DemoGameCore.logic;
using Assets.Scripts.DemoGameCore.ui.screen;
using hundun.idleshare.enginecore;
using hundun.unitygame.adapters;
using hundun.unitygame.gamelib;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DemoStorageInfoBoardVM : MonoBehaviour, ILogicFrameListener
{
    private Image background;
    protected GameObject nodesRoot;
    protected GameObject nodePrefab;


    List<String> shownOrders;
    HashSet<String> shownTypes = new HashSet<String>();
    DemoPlayScreen parent;

    List<ResourceAmountPairNode> nodes = new List<ResourceAmountPairNode>();



    //Label mainLabel;
    private void Awake()
    {
        this.background = this.transform.Find("background").GetComponent<Image>();
        this.nodesRoot = this.transform.Find("_nodesRoot").gameObject;
        this.nodePrefab = this.transform.Find("_templates/nodePrefab").gameObject;
    }

    public void postPrefabInitialization(DemoPlayScreen parent, List<String> shownOrders)
    {
        this.parent = parent;
        background.sprite = (parent.game.textureManager.defaultBoardNinePatchTexture);

        this.shownOrders = shownOrders;
    }



    private void rebuildCells()
    {
        nodesRoot.transform.AsTableClear();
        nodes.Clear();

        for (int i = 0; i < shownOrders.size(); i++)
        {
            String resourceType = shownOrders.get(i);
            if (shownTypes.Contains(resourceType))
            {
                ResourceAmountPairNode node = nodesRoot.transform.AsTableAdd<ResourceAmountPairNode>(nodePrefab);
                node.postPrefabInitialization(parent.game.textureManager, resourceType);
                nodes.Add(node);
                shownTypes.Add(resourceType);
            }
        }

    }



    private void updateViewData()
    {
        Boolean needRebuildCells = !shownTypes.Equals(parent.game.idleGameplayExport.getUnlockedResourceTypes());
        if (needRebuildCells)
        {
            shownTypes.Clear();
            shownTypes.AddRange(parent.game.idleGameplayExport.getUnlockedResourceTypes());
            rebuildCells();
        }
        nodes.ForEach(
            node => node.update(parent.game.idleGameplayExport.getResourceNumOrZero(node.getResourceType()))
            );
    }

    public void onLogicFrame()
    {
        updateViewData();
    }
}
