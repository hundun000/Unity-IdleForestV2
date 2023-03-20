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

namespace hundun.idleshare.enginecore
{
    public class ConstructionPrototypeControlNodeVM<T_GAME, T_SAVE> : MonoBehaviour where T_GAME : BaseIdleGame<T_GAME, T_SAVE>
    {
        BaseIdlePlayScreen<T_GAME, T_SAVE> parent;
        AbstractConstructionControlBoardVM<T_GAME, T_SAVE> constructionControlBoardVM;
        AbstractConstructionPrototype model;

        Text constructionNameLabel;

        TextButton clickEffectButton;
        Image background;





        void Awake()
        {
            this.background = this.transform.Find("background").GetComponent<Image>();
            this.constructionNameLabel = this.transform.Find("constructionNameLabel").GetComponent<Text>();
            this.clickEffectButton = this.transform.Find("clickEffectButton").GetComponent<TextButton>();
        }

        public void postPrefabInitialization(BaseIdlePlayScreen<T_GAME, T_SAVE> parent, int index, AbstractConstructionControlBoardVM<T_GAME, T_SAVE> constructionControlBoardVM)
        {

            this.parent = parent;
            this.constructionControlBoardVM = constructionControlBoardVM;

            clickEffectButton.button.onClick.AddListener(() => {
            
                parent.game.frontend.log(this.getClass().getSimpleName(), "clicked");
                parent.game.idleGameplayExport.constructionPrototypeOnClick(model.prototypeId);
                constructionControlBoardVM.onConstructionInstancesChange(parent.area);
            });





            background.sprite = parent.game.textureManager.defaultBoardNinePatchTexture;
        }



        public void setModel(AbstractConstructionPrototype constructionExportData)
        {
            this.model = constructionExportData;
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
            constructionNameLabel.text = parent.game.idleGameplayExport.gameDictionary.constructionPrototypeIdToShowName(parent.game.idleGameplayExport.language, model.prototypeId);
            clickEffectButton.label.text = "TODO";

            // ------ update clickable-state ------
            Boolean canClickEffect = true;
            //clickEffectButton.setTouchable(clickable ? Touchable.enabled : Touchable.disabled);


            if (canClickEffect)
            {
                clickEffectButton.button.interactable = (true);
                //clickEffectButton.SetColor(Color.white);
            }
            else
            {
                clickEffectButton.button.interactable = (false);
                //clickEffectButton.SetColor(Color.red);
            }


        }
    }
}
