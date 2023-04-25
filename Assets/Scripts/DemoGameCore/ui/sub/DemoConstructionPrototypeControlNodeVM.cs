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
    public class DemoConstructionPrototypeControlNodeVM : MonoBehaviour
    {
        BaseIdleForestPlayScreen parent;
        public AbstractConstructionPrototype model;
        public GridPosition position;

        Text constructionNameLabel;
        Image previewImage;
        DemoTextButton clickEffectButton;





        void Awake()
        {
            this.constructionNameLabel = this.transform.Find("constructionNameLabel").GetComponent<Text>();
            this.previewImage = this.transform.Find("previewImage").GetComponent<Image>();
            this.clickEffectButton = this.transform.Find("clickEffectButton").GetComponent<DemoTextButton>();
        }

        public void postPrefabInitialization(BaseIdleForestPlayScreen parent)
        {

            this.parent = parent;

            clickEffectButton.button.onClick.AddListener(() => {

                parent.game.frontend.log(this.getClass().getSimpleName(), "clicked");
                parent.game.idleGameplayExport.gameplayContext.constructionManager.buyInstanceOfPrototype(model.prototypeId, position);
            });


            clickEffectButton.label.text = parent.game.idleGameplayExport.gameDictionary.getPlayScreenTexts(parent.game.idleGameplayExport.language)[0];
        }

        private void updateCanCreateInstance()
        {
            bool enable;
            try
            {
                enable = parent.game.idleGameplayExport.gameplayContext.constructionManager.canBuyInstanceOfPrototype(model.prototypeId, position);
            }
            catch
            {
                enable = false;
            }

            clickEffectButton.button.interactable = enable;
        }

        public void setModel(AbstractConstructionPrototype constructionExportData, GridPosition position)
        {
            this.model = constructionExportData;
            this.position = position;
            update();
        }

        public void update()
        {
            previewImage.sprite = parent.game.textureManager.getConstructionEntity(model.prototypeId);
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
            constructionNameLabel.text = parent.game.idleGameplayExport.gameDictionary.constructionPrototypeIdToShowName(parent.game.idleGameplayExport.language, model.prototypeId);

            // ------ update clickable-state ------

            updateCanCreateInstance();
        }
    }
}
