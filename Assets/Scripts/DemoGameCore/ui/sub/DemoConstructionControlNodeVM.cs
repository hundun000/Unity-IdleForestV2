using Assets.Scripts.DemoGameCore.logic;
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
    public class DemoConstructionControlNodeVM : MonoBehaviour
    {
        DemoPlayScreen parent;
        BaseIdleForestConstruction model;

        Text constructionNameLabel;
        Text workingLevelLabel;
        Text proficiencyLabel;

        TextButton clickEffectButton;
        TextButton destoryButton;
        TextButton transformButton;

        Image background;

        void Awake()
        {
            this.background = this.transform.Find("background").GetComponent<Image>();
            this.constructionNameLabel = this.transform.Find("constructionNameLabel").GetComponent<Text>();
            this.workingLevelLabel = this.transform.Find("workingLevelLabel").GetComponent<Text>();
            this.proficiencyLabel = this.transform.Find("proficiencyLabel").GetComponent<Text>();

            this.clickEffectButton = this.transform.Find("clickEffectButton").GetComponent<TextButton>();

            this.destoryButton = this.transform.Find("destoryButton").GetComponent<TextButton>();
            this.transformButton = this.transform.Find("transformButton").GetComponent<TextButton>();
        }

        public void postPrefabInitialization(DemoPlayScreen parent)
        {

            this.parent = parent;


            clickEffectButton.button.onClick.AddListener(() => {
                parent.game.frontend.log(this.getClass().getSimpleName(), "clickEffectButton clicked");
                parent.game.idleGameplayExport.constructionOnClick(model.id);
            });
            transformButton.button.onClick.AddListener(() => {
                parent.game.frontend.log(this.getClass().getSimpleName(), "transformButton clicked");
                parent.game.idleGameplayExport.transformConstruction(model.id);
            });
            destoryButton.button.onClick.AddListener(() => {
                parent.game.frontend.log(this.getClass().getSimpleName(), "destoryButton clicked");
                parent.game.idleGameplayExport.destoryConstruction(model.id, ConstructionPrototypeId.DIRT);
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

        public void setModel(BaseIdleForestConstruction constructionExportData)
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
            
            

            if (model.upgradeComponent.upgradeState != UpgradeState.NO_UPGRADE)
            {
                workingLevelLabel.text = (model.levelComponent.getWorkingLevelDescroption());
            }
            else
            {
                workingLevelLabel.text = "";
            }


            if (model.descriptionPackage.proficiencyDescroptionProvider != null)
            {
                proficiencyLabel.text = (model.proficiencyComponent.getProficiencyDescroption());
            }
            else
            {
                proficiencyLabel.text = "";
            }

            

            // ------ update clickable-state ------
            if (model.descriptionPackage.buttonDescroption == null)
            {
                clickEffectButton.gameObject.SetActive(false);
            }
            else if (model.canClickEffect())
            {
                clickEffectButton.gameObject.SetActive(true);
                clickEffectButton.button.interactable = (true);
                clickEffectButton.label.text = (model.descriptionPackage.buttonDescroption);
            }
            else
            {
                clickEffectButton.gameObject.SetActive(true);
                clickEffectButton.button.interactable = (false);
                clickEffectButton.label.text = (model.descriptionPackage.buttonDescroption);
            }

            if (model.descriptionPackage.transformButtonDescroption == null)
            {
                transformButton.gameObject.SetActive(false);
            }
            else if (model.canTransfer())
            {
                transformButton.gameObject.SetActive(true);
                transformButton.button.interactable = (true);
                transformButton.label.text = (model.descriptionPackage.transformButtonDescroption);
            }
            else
            {
                transformButton.gameObject.SetActive(true);
                transformButton.button.interactable = (false);
                transformButton.label.text = (model.descriptionPackage.transformButtonDescroption);
            }


            if (model.descriptionPackage.destroyButtonDescroption == null)
            {
                destoryButton.gameObject.SetActive(false);
            } 
            else if(model.canDestory())
            {
                destoryButton.gameObject.SetActive(true);
                destoryButton.button.interactable = (true);
                destoryButton.label.text = (model.descriptionPackage.destroyButtonDescroption);
            }
            else
            {
                destoryButton.gameObject.SetActive(true);
                destoryButton.button.interactable = (false);
                destoryButton.label.text = (model.descriptionPackage.destroyButtonDescroption);
            }
        }
    }
}
