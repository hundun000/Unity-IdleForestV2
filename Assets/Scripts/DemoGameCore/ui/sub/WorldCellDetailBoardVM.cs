using Assets.Scripts.DemoGameCore.logic;
using Assets.Scripts.DemoGameCore.ui.screen;
using hundun.idleshare.enginecore;
using hundun.idleshare.gamelib;
using hundun.unitygame.adapters;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.DemoGameCore.ui.sub
{
    public class WorldCellDetailBoardVM : BaseCellDetailBoardVM, IConstructionCollectionListener
    {



        override public void updateDetail(BaseConstruction construction)
        {
            this.data = construction;
            if (construction == null)
            {
                updateAsEmpty();
                return;
            }

            switch (construction.prototypeId)
            {
                case ConstructionPrototypeId.DIRT:
                    updateAsConstructionPrototypeDetail(construction);
                    break;
                default:
                    updateAsConstructionDetail(construction);
                    break;
            }
        }
        private void updateAsEmpty()
        {
            nodesRoot.transform.AsTableClear();
            contents.Clear();
        }

        private void updateAsConstructionDetail(BaseConstruction construction)
        {
            nodesRoot.transform.AsTableClear();
            contents.Clear();

            CellDetailInnerBoardVM innerBoardVM = nodesRoot.transform.AsTableAdd<CellDetailInnerBoardVM>(innerBoardVMPrefab.gameObject);
            innerBoardVM.postPrefabInitialization(parent);
            innerBoardVM.update(construction, construction.saveData.position);
            contents.Add(innerBoardVM);

        }

        private void updateAsConstructionPrototypeDetail(BaseConstruction construction)
        {
            nodesRoot.transform.AsTableClear();
            contents.Clear();

            List<AbstractConstructionPrototype> constructionPrototypes = parent.game.idleGameplayExport.getAreaShownConstructionPrototypesOrEmpty(parent.area);

            constructionPrototypes.ForEach(constructionPrototype => {
                CellDetailInnerBoardVM innerBoardVM = nodesRoot.transform.AsTableAdd<CellDetailInnerBoardVM>(innerBoardVMPrefab.gameObject);
                innerBoardVM.postPrefabInitialization(parent);
                innerBoardVM.update(constructionPrototype, construction.saveData.position);
                contents.Add(innerBoardVM);
            });

        }

        public void onConstructionCollectionChange()
        {
            if (data != null)
            {
                data = parent.game.idleGameplayExport.getConstructionAt(data.position);
                updateDetail(data);
            }
            
        }
    }
}
