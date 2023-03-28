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

        public BaseConstruction data;

        void Awake()
        {
            this.background = this.transform.Find("background").GetComponent<Image>();
            this.posLabel = this.transform.Find("posLabel").GetComponent<Text>();
            this.nodesRoot = this.transform.Find("_nodesRoot").gameObject;
            this.constructionControlNodePrefab = this.transform.Find("_templates/constructionControlNodePrefab").GetComponent<DemoConstructionControlNodeVM>();
            this.constructionPrototypeControlNodePrefab = this.transform.Find("_templates/constructionPrototypeControlNodePrefab").GetComponent<DemoConstructionPrototypeControlNodeVM>();
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

            posLabel.text = "点击一个地块可查看详情";
        }

        private void updateAsConstructionDetail(BaseConstruction construction)
        {
            nodesRoot.transform.AsTableClear();

            DemoConstructionControlNodeVM content = nodesRoot.transform.AsTableAdd<DemoConstructionControlNodeVM>(constructionControlNodePrefab.gameObject);
            content.postPrefabInitialization(parent);
            content.setModel(construction);
            content.update();

            posLabel.text = construction.name + "(" +
                construction.saveData.position.x + ", " +
                construction.saveData.position.y + ")"; ;
        }

        private void updateAsConstructionPrototypeDetail(BaseConstruction construction)
        {
            nodesRoot.transform.AsTableClear();

            List<AbstractConstructionPrototype> constructionPrototypes = parent.game.idleGameplayExport.getAreaShownConstructionPrototypesOrEmpty(parent.area);

            constructionPrototypes.ForEach(constructionPrototype => {
                DemoConstructionPrototypeControlNodeVM content = nodesRoot.transform.AsTableAdd<DemoConstructionPrototypeControlNodeVM>(constructionPrototypeControlNodePrefab.gameObject);
                content.postPrefabInitialization(parent);
                content.setModel(constructionPrototype);
                content.update();
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
