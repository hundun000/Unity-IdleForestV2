using Assets.Scripts.DemoGameCore.ui.screen;
using hundun.idleshare.enginecore;
using hundun.idleshare.gamelib;
using hundun.unitygame.adapters;
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

    public class CellDetailInnerBoardVM : MonoBehaviour
    {
        DemoPlayScreen parent;

        Image background;
        GameObject childrenRoot;
        GameObject mainBoardContainer;

        Text detailDescroptionConstPartTextTemplate;
        GameObject onePackTemplate;
        ResourceAmountPairNode resourceAmountPairNodeTemplate;
        GameObject maxLevelGroupTemplate;
        DemoConstructionControlNodeVM constructionControlNodePrefab;
        DemoConstructionPrototypeControlNodeVM constructionPrototypeControlNodePrefab;

        GridPosition position;
        void Awake()
        {
            this.background = this.transform.Find("background").GetComponent<Image>();
            this.childrenRoot = this.transform.Find("childrenRoot").gameObject;
            this.mainBoardContainer = this.transform.Find("mainBoardContainer").gameObject;

            this.detailDescroptionConstPartTextTemplate = this.transform.Find("_templates/detailDescroptionConstPartTextTemplate").GetComponent<Text>();
            this.onePackTemplate = this.transform.Find("_templates/onePackTemplate").gameObject;
            this.resourceAmountPairNodeTemplate = this.transform.Find("_templates/resourceAmountPairNodeTemplate").GetComponent<ResourceAmountPairNode>();
            this.maxLevelGroupTemplate = this.transform.Find("_templates/maxLevelGroupTemplate").gameObject;

            this.constructionControlNodePrefab = this.transform.Find("_mainBoardTemplates/constructionControlNodePrefab").GetComponent<DemoConstructionControlNodeVM>();
            this.constructionPrototypeControlNodePrefab = this.transform.Find("_mainBoardTemplates/constructionPrototypeControlNodePrefab").GetComponent<DemoConstructionPrototypeControlNodeVM>();

        }

        public void postPrefabInitialization(DemoPlayScreen parent)
        {
            //super("GUIDE_TEXT", parent.game.getButtonSkin());
            this.parent = parent;
            this.background.sprite = parent.game.textureManager.defaultBoardNinePatchTexture;
        }


        private void updateAsConstruction(BaseConstruction model)
        {
            // ------ main part ------
            mainBoardContainer.transform.AsTableClear();

            DemoConstructionControlNodeVM mainBoard = mainBoardContainer.transform.AsTableAdd<DemoConstructionControlNodeVM>(constructionControlNodePrefab.gameObject);
            mainBoard.postPrefabInitialization(parent);
            mainBoard.setModel(model);
            mainBoard.update();

            // ------ details part ------
            childrenRoot.transform.AsTableClear();

            Text detailDescroptionConstPartText = childrenRoot.transform.AsTableAdd<Text>(detailDescroptionConstPartTextTemplate.gameObject);
            detailDescroptionConstPartText.text = model.detailDescroptionConstPart;


            buildOnePack(model.outputComponent.outputCostPack);

            buildOnePack(model.outputComponent.outputGainPack);

            if (model.upgradeComponent.upgradeState == UpgradeState.HAS_NEXT_UPGRADE)
            {
                buildOnePack(model.upgradeComponent.upgradeCostPack);
            }
            else if (model.upgradeComponent.upgradeState == UpgradeState.REACHED_MAX_UPGRADE)
            {
                GameObject maxLevelGroup = childrenRoot.transform.AsTableAddGameobject(maxLevelGroupTemplate.gameObject);
                Text maxLevelGroupLabel_0 = maxLevelGroup.transform.Find("label_0").GetComponent<Text>();
                Text maxLevelGroupLabel_1 = maxLevelGroup.transform.Find("label_1").GetComponent<Text>();

                maxLevelGroupLabel_0.text = model.upgradeComponent.upgradeCostPack.descriptionStart;
                maxLevelGroupLabel_1.text = model.descriptionPackage.upgradeMaxLevelDescription;

                buildOnePack(model.upgradeComponent.transferCostPack);
            }

            buildOnePack(model.destoryCostPack);
            buildOnePack(model.destoryGainPack);


        }

        private void updateAsConstructionPrototype(AbstractConstructionPrototype constructionPrototype, GridPosition position)
        {
            // ------ main part ------
            mainBoardContainer.transform.AsTableClear();

            DemoConstructionPrototypeControlNodeVM mainBoard = mainBoardContainer.transform.AsTableAdd<DemoConstructionPrototypeControlNodeVM>(constructionPrototypeControlNodePrefab.gameObject);
            mainBoard.postPrefabInitialization(parent);
            mainBoard.setModel(constructionPrototype, position);
            mainBoard.update();

            // ------ details part ------
            childrenRoot.transform.AsTableClear();

            Text detailDescroptionConstPartText = childrenRoot.transform.AsTableAdd<Text>(detailDescroptionConstPartTextTemplate.gameObject);
            detailDescroptionConstPartText.text = "";

            buildOnePack(constructionPrototype.buyInstanceCostPack);

        }

        private void buildOnePack(ResourcePack pack)
        {
            if (pack != null && pack.modifiedValues != null)
            {
                GameObject onepackVM = childrenRoot.transform.AsTableAddGameobject(onePackTemplate.gameObject);
                Text onepackVMLabel = onepackVM.transform.Find("label").GetComponent<Text>();
                GameObject onepackVMNodesRoot = onepackVM.transform.Find("nodes").gameObject;


                onepackVMLabel.text = pack.descriptionStart;
                foreach (ResourcePair entry in pack.modifiedValues)
                {
                    ResourceAmountPairNode resourceAmountPairNode = onepackVMNodesRoot.transform.AsTableAdd<ResourceAmountPairNode>(resourceAmountPairNodeTemplate.gameObject);
                    resourceAmountPairNode.postPrefabInitialization(parent.game.textureManager, entry.type);
                    resourceAmountPairNode.update(entry.amount);
                }
            }
        }


        public void update(object data, GridPosition position)
        {
            if (data is BaseConstruction construction)
            {
                updateAsConstruction(construction);
            }
            else if (data is AbstractConstructionPrototype constructionPrototype)
            {
                updateAsConstructionPrototype(constructionPrototype, position);
            }
            
        }
    }
}