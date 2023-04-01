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
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.DemoGameCore.ui.sub
{
    public class DemoConstructionControlNodeVM : MonoBehaviour
    {
        DemoPlayScreen parent;
        BaseConstruction model;

        Text constructionNameLabel;
        Text workingLevelLabel;
        Text proficiencyLabel;

        TextButton clickEffectButton;
        TextButton destoryButton;

        Image background;

        void Awake()
        {
            this.background = this.transform.Find("background").GetComponent<Image>();
            this.constructionNameLabel = this.transform.Find("constructionNameLabel").GetComponent<Text>();
            this.workingLevelLabel = this.transform.Find("workingLevelLabel").GetComponent<Text>();
            this.proficiencyLabel = this.transform.Find("proficiencyLabel").GetComponent<Text>();

            this.clickEffectButton = this.transform.Find("clickEffectButton").GetComponent<TextButton>();

            this.destoryButton = this.transform.Find("destoryButton").GetComponent<TextButton>();
        }

        public void postPrefabInitialization(DemoPlayScreen parent)
        {

            this.parent = parent;


            clickEffectButton.button.onClick.AddListener(() => {

                if(model.upgradeComponent.canTransfer()) 
                {
                    parent.game.frontend.log(this.getClass().getSimpleName(), "transferButton clicked");
                    parent.game.idleGameplayExport.transferConstruction(model.id);
                }
                else
                {
                    parent.game.frontend.log(this.getClass().getSimpleName(), "clickEffectButton clicked");
                    parent.game.idleGameplayExport.constructionOnClick(model.id);
                }

            });
            destoryButton.button.onClick.AddListener(() => {

                parent.game.frontend.log(this.getClass().getSimpleName(), "destoryButton clicked");
                parent.game.idleGameplayExport.destoryConstruction(model.id);

            });



            background.sprite = parent.game.textureManager.defaultBoardNinePatchTexture;
        }


        private void initAsNormalStyle()
        {


            //changeWorkingLevelGroup.setVisible(false);

            //this.debug();
        }


        private void initAsChangeWorkingLevelStyle()
        {
            //clearStyle();

            //changeWorkingLevelGroup.setVisible(true);




        }

        public void setModel(BaseConstruction constructionExportData)
        {
            this.model = constructionExportData;
            if (constructionExportData != null)
            {
                if (constructionExportData.levelComponent.workingLevelChangable)
                {
                    initAsChangeWorkingLevelStyle();
                }
                else
                {
                    initAsNormalStyle();
                }
            }
            update();
        }

        public void update()
        {
            // ------ update show-state ------
            if (model == null)
            {
                this.gameObject.SetActive(false);
                //textButton.setVisible(false);
                //Gdx.app.log("ConstructionView", this.hashCode() + " no model");
                return;
            }
            else
            {
                this.gameObject.SetActive(true);
                //textButton.setVisible(true);
                //Gdx.app.log("ConstructionView", model.getName() + " set to its view");
            }
            // ------ update text ------
            constructionNameLabel.text = (model.name);
            
            workingLevelLabel.text = (model.levelComponent.getWorkingLevelDescroption());
            proficiencyLabel.text = (model.proficiencyComponent.getProficiencyDescroption());
            destoryButton.label.text = (model.descriptionPackage.destoryButtonDescroption);

            // ------ update clickable-state ------
            if (model.canClickEffect())
            {
                clickEffectButton.button.interactable = (true);
                clickEffectButton.label.text = (model.descriptionPackage.buttonDescroption);
            }
            else if (model.upgradeComponent.canTransfer())
            {
                clickEffectButton.button.interactable = (true);
                clickEffectButton.label.text = (model.descriptionPackage.transferButtonDescroption);
            }
            else
            {
                clickEffectButton.button.interactable = (false);
                clickEffectButton.label.text = (model.descriptionPackage.buttonDescroption);
            }

            if (model.canDestory())
            {
                destoryButton.button.interactable = (true);
            }
            else
            {
                destoryButton.button.interactable = (false);
            }
        }
    }
}
