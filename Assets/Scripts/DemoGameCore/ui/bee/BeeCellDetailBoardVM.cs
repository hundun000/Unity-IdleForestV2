using Assets.Scripts.DemoGameCore.logic;
using Assets.Scripts.DemoGameCore.ui.screen;
using hundun.idleshare.enginecore;
using hundun.idleshare.gamelib;
using hundun.unitygame.adapters;
using hundun.unitygame.gamelib;
using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.DemoGameCore.ui.sub
{
    public class BeeCellDetailBoardVM : BaseCellDetailBoardVM, IGameAreaChangeListener, IConstructionCollectionListener
    {

        public void postPrefabInitialization(BeePlayScreen parent)
        {
            base.postPrefabInitialization(parent);
            updateAsEmpty();
        }


        private void updateAsEmpty()
        {
            nodesRoot.transform.AsTableClear();
            contents.Clear();
        }

        private void updateAsAreaControlableConstructions()
        {
            nodesRoot.transform.AsTableClear();
            contents.Clear();

            List<BaseConstruction> constructions = parent.game.idleGameplayExport.gameplayContext.constructionManager.getAreaControlableConstructionsOrEmpty(parent.area);

            constructions.ForEach(construction => {
                CellDetailInnerBoardVM innerBoardVM = nodesRoot.transform.AsTableAdd<CellDetailInnerBoardVM>(innerBoardVMPrefab.gameObject);
                innerBoardVM.postPrefabInitialization(parent);
                innerBoardVM.update(construction, null);
                contents.Add(innerBoardVM);
            });
        }

        void IGameAreaChangeListener.onGameAreaChange(string last, string current)
        {
            updateAsAreaControlableConstructions();
        }

        void IConstructionCollectionListener.onConstructionCollectionChange()
        {
            updateAsAreaControlableConstructions();
        }
    }
}
