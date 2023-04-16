using Assets.Scripts.DemoGameCore;
using Assets.Scripts.DemoGameCore.logic;
using Assets.Scripts.DemoGameCore.ui.screen;
using hundun.idleshare.enginecore;
using hundun.idleshare.gamelib;
using hundun.unitygame.adapters;
using hundun.unitygame.gamelib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.DemoGameCore.ui.sub
{

    public class DemoStorageInfoBoardVM : MonoBehaviour, IOneFrameResourceChangeListener
    {
        private Image background;
        protected GameObject nodesRoot;
        protected GameObject nodePrefab;


        List<String> shownOrders;
        HashSet<String> shownTypes = new HashSet<String>();
        DemoPlayScreen parent;

        Dictionary<StorageInfoBoardResourceAmountPairNode, List<long>> nodeToDeltaHistoryMap = new();



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
            nodeToDeltaHistoryMap.Clear();

            for (int i = 0; i < shownOrders.size(); i++)
            {
                String resourceType = shownOrders.get(i);
                if (shownTypes.Contains(resourceType))
                {
                    StorageInfoBoardResourceAmountPairNode node = nodesRoot.transform.AsTableAdd<StorageInfoBoardResourceAmountPairNode>(nodePrefab);
                    node.postPrefabInitialization(parent.game.textureManager, resourceType);
                    nodeToDeltaHistoryMap.Add(node, new());
                    shownTypes.Add(resourceType);
                }
            }

        }



        private void updateViewData(Dictionary<string, long> changeMap)
        {
            Boolean needRebuildCells = !shownTypes.SetEquals(parent.game.idleGameplayExport.getUnlockedResourceTypes());
            if (needRebuildCells)
            {
                shownTypes.Clear();
                shownTypes.AddRange(parent.game.idleGameplayExport.getUnlockedResourceTypes());
                rebuildCells();
            }

            nodeToDeltaHistoryMap.ToList().ForEach(entry => {
                var node = entry.Key;
                entry.Value.Insert(entry.Value.Count, changeMap.getOrDefault(node.getResourceType(), 0));
                while (entry.Value.Count > DemoIdleGame.LOGIC_FRAME_PER_SECOND)
                {
                    entry.Value.RemoveAt(entry.Value.Count - 1);
                }
                long historySum = entry.Value.Sum();
                node.update(historySum, parent.game.idleGameplayExport.getResourceNumOrZero(node.getResourceType()));
            });


        }

        public void onResourceChange(Dictionary<string, long> changeMap)
        {
            updateViewData(changeMap);
        }
    }
}