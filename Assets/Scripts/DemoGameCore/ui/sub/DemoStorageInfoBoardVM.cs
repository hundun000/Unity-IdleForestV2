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
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.DemoGameCore.ui.sub
{

    public class DemoStorageInfoBoardVM : MonoBehaviour, IOneFrameResourceChangeListener, IGameAreaChangeListener
    {
        private Image background;
        protected GameObject nodesRoot;
        protected GameObject nodePrefab;


        List<String> shownOrders;
        HashSet<String> shownTypes = new HashSet<String>();
        BaseIdleForestPlayScreen parent;

        List<StorageInfoBoardResourceAmountPairNode> nodeMap = new();



        //Label mainLabel;
        private void Awake()
        {
            this.background = this.transform.Find("background").GetComponent<Image>();
            this.nodesRoot = this.transform.Find("_nodesRoot").gameObject;
            this.nodePrefab = this.transform.Find("_templates/nodePrefab").gameObject;
        }

        public void postPrefabInitialization(BaseIdleForestPlayScreen parent, List<String> shownOrders)
        {
            this.parent = parent;
            background.sprite = (parent.game.textureManager.defaultBoardNinePatchTexture);

            this.shownOrders = shownOrders;
        }



        private void rebuildCells()
        {
            nodesRoot.transform.AsTableClear();
            nodeMap.Clear();

            for (int i = 0; i < shownOrders.size(); i++)
            {
                String resourceType = shownOrders.get(i);
                if (shownTypes.Contains(resourceType))
                {
                    StorageInfoBoardResourceAmountPairNode node = nodesRoot.transform.AsTableAdd<StorageInfoBoardResourceAmountPairNode>(nodePrefab);
                    node.postPrefabInitialization(parent.game.textureManager, resourceType);
                    nodeMap.Add(node);
                    shownTypes.Add(resourceType);
                }
            }

        }



        private void updateViewData(Dictionary<string, long> changeMap, Dictionary<string, List<long>> deltaHistoryMap)
        {
            Boolean needRebuildCells = !shownTypes.SetEquals(
                new HashSet<string>(
                    parent.game.idleGameplayExport.gameplayContext.storageManager.unlockedResourceTypes
                    .Where(it => shownOrders.Contains(it))
                    .ToList())
                );
            if (needRebuildCells)
            {
                shownTypes.Clear();
                shownTypes.AddRange(parent.game.idleGameplayExport.gameplayContext.storageManager.unlockedResourceTypes);
                rebuildCells();
            }

            nodeMap.ForEach(node => {
                long historySum;
                if (deltaHistoryMap.ContainsKey(node.getResourceType()))
                {
                    historySum = deltaHistoryMap.get(node.getResourceType()).TakeLast(DemoIdleGame.LOGIC_FRAME_PER_SECOND).Sum();
                } 
                else
                {
                    historySum = 0;
                }

                node.update(historySum, parent.game.idleGameplayExport.gameplayContext.storageManager.getResourceNumOrZero(node.getResourceType()));
            });


        }


        void IGameAreaChangeListener.onGameAreaChange(string last, string current)
        {
            updateViewData(new(), new());
        }

        void IOneFrameResourceChangeListener.onResourceChange(Dictionary<string, long> changeMap, Dictionary<string, List<long>> deltaHistoryMap)
        {
            updateViewData(changeMap, deltaHistoryMap);
        }
    }
}