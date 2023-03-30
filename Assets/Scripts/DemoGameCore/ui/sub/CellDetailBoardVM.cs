using Assets.Scripts.DemoGameCore.logic;
using Assets.Scripts.DemoGameCore.ui.screen;
using hundun.idleshare.enginecore;
using hundun.idleshare.gamelib;
using hundun.unitygame.adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.DemoGameCore.ui.sub
{
    public class CellDetailBoardVM : MonoBehaviour, IConstructionCollectionListener
    {
        DemoPlayScreen parent;

        GameObject nodesRoot;
        Image background;
        Text posLabel;

        DemoConstructionControlNodeVM constructionControlNodePrefab;
        DemoConstructionPrototypeControlNodeVM constructionPrototypeControlNodePrefab;
        CellDetailInnerBoardVM innerBoardVMPrefab;

        public BaseConstruction data;
        private List<object> contents = new List<object>();

        void Awake()
        {
            this.background = this.transform.Find("background").GetComponent<Image>();
            this.posLabel = this.transform.Find("posLabel").GetComponent<Text>();
            this.nodesRoot = this.transform.Find("_nodesRoot").gameObject;
            this.constructionControlNodePrefab = this.transform.Find("_templates/constructionControlNodePrefab").GetComponent<DemoConstructionControlNodeVM>();
            this.constructionPrototypeControlNodePrefab = this.transform.Find("_templates/constructionPrototypeControlNodePrefab").GetComponent<DemoConstructionPrototypeControlNodeVM>();
            this.innerBoardVMPrefab = this.transform.Find("_templates/innerBoardVMPrefab").GetComponent<CellDetailInnerBoardVM>();
        }

        void Update()
        {
            contents.ForEach(it => {
                if (it is DemoConstructionControlNodeVM constructionControlNodeVM)
                {
                    constructionControlNodeVM.update();
                }
                else if (it is DemoConstructionPrototypeControlNodeVM constructionPrototypeControlNodeVM)
                {
                    constructionPrototypeControlNodeVM.update();
                }
            });
        }

        public void postPrefabInitialization(DemoPlayScreen parent)
        {
            //super("GUIDE_TEXT", parent.game.getButtonSkin());
            this.parent = parent;
            this.background.sprite = parent.game.textureManager.defaultBoardNinePatchTexture;

            updateDetail(null);
        }


        public void updateDetail(BaseConstruction construction)
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

            posLabel.text = "点击一个地块可查看详情";
        }

        private void updateAsConstructionDetail(BaseConstruction construction)
        {
            nodesRoot.transform.AsTableClear();
            contents.Clear();

            DemoConstructionControlNodeVM constructionControlNodeVM = nodesRoot.transform.AsTableAdd<DemoConstructionControlNodeVM>(constructionControlNodePrefab.gameObject);
            constructionControlNodeVM.postPrefabInitialization(parent);
            constructionControlNodeVM.setModel(construction);
            constructionControlNodeVM.update();
            contents.Add(constructionControlNodeVM);

            CellDetailInnerBoardVM innerBoardVM = nodesRoot.transform.AsTableAdd<CellDetailInnerBoardVM>(innerBoardVMPrefab.gameObject);
            innerBoardVM.postPrefabInitialization(parent);
            innerBoardVM.update(construction);
            contents.Add(innerBoardVM);

            posLabel.text = construction.name + "(" +
                construction.saveData.position.x + ", " +
                construction.saveData.position.y + ")"; ;
        }

        private void updateAsConstructionPrototypeDetail(BaseConstruction construction)
        {
            nodesRoot.transform.AsTableClear();
            contents.Clear();

            List<AbstractConstructionPrototype> constructionPrototypes = parent.game.idleGameplayExport.getAreaShownConstructionPrototypesOrEmpty(parent.area);

            constructionPrototypes.ForEach(constructionPrototype => {
                DemoConstructionPrototypeControlNodeVM content = nodesRoot.transform.AsTableAdd<DemoConstructionPrototypeControlNodeVM>(constructionPrototypeControlNodePrefab.gameObject);
                content.postPrefabInitialization(parent);
                content.setModel(constructionPrototype);
                content.update();
                contents.Add(content);

                CellDetailInnerBoardVM innerBoardVM = nodesRoot.transform.AsTableAdd<CellDetailInnerBoardVM>(innerBoardVMPrefab.gameObject);
                innerBoardVM.postPrefabInitialization(parent);
                innerBoardVM.update(constructionPrototype);
                contents.Add(innerBoardVM);
            });

            posLabel.text = construction.name + "(" +
                construction.saveData.position.x + ", " +
                construction.saveData.position.y + ")"; ;
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
