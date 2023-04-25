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
        BaseIdleForestPlayScreen parent;
        public BaseIdleForestConstruction model;

        Text constructionNameLabel;
        Text workingLevelLabel;
        Text proficiencyLabel;

        DemoTextButton upgradeButton;
        DemoTextButton destoryButton;
        DemoTextButton transformButton;

        Image background;

        void Awake()
        {
            this.background = this.transform.Find("background").GetComponent<Image>();
            this.constructionNameLabel = this.transform.Find("constructionNameLabel").GetComponent<Text>();
            this.workingLevelLabel = this.transform.Find("workingLevelLabel").GetComponent<Text>();
            this.proficiencyLabel = this.transform.Find("proficiencyLabel").GetComponent<Text>();

            this.upgradeButton = this.transform.Find("clickEffectButton").GetComponent<DemoTextButton>();

            this.destoryButton = this.transform.Find("destoryButton").GetComponent<DemoTextButton>();
            this.transformButton = this.transform.Find("transformButton").GetComponent<DemoTextButton>();
        }

        public void postPrefabInitialization(BaseIdleForestPlayScreen parent)
        {

            this.parent = parent;


            upgradeButton.button.onClick.AddListener(() => {
                parent.game.frontend.log(this.getClass().getSimpleName(), "upgradeButton clicked");
                model.upgradeComponent.doUpgrade();
            });
            transformButton.button.onClick.AddListener(() => {
                parent.game.frontend.log(this.getClass().getSimpleName(), "transformButton clicked");
                parent.game.idleGameplayExport.gameplayContext.constructionManager.transformInstanceAndNotify(model.id);
            });
            destoryButton.button.onClick.AddListener(() => {
                parent.game.frontend.log(this.getClass().getSimpleName(), "destoryButton clicked");
                model.existenceComponent.destoryInstanceAndNotify(ConstructionPrototypeId.DIRT);
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
                upgradeButton.gameObject.SetActive(false);
            }
            else if (model.upgradeComponent.canUpgrade())
            {
                upgradeButton.gameObject.SetActive(true);
                upgradeButton.button.interactable = (true);
                upgradeButton.label.text = (model.descriptionPackage.buttonDescroption);
            }
            else
            {
                upgradeButton.gameObject.SetActive(true);
                upgradeButton.button.interactable = (false);
                upgradeButton.label.text = (model.descriptionPackage.buttonDescroption);
            }

            if (model.descriptionPackage.transformButtonDescroption == null)
            {
                transformButton.gameObject.SetActive(false);
            }
            else if (model.upgradeComponent.canTransfer())
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
            else if(model.existenceComponent.canDestory())
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
