using Assets.Scripts.DemoGameCore.logic;
using Assets.Scripts.DemoGameCore.ui.sub;
using hundun.idleshare.enginecore;
using hundun.idleshare.gamelib;
using hundun.unitygame.adapters;
using hundun.unitygame.enginecorelib;
using hundun.unitygame.gamelib;
using Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.DemoGameCore.ui.screen
{
    public class BeePlayScreen : BaseIdleForestPlayScreen
    {


        public const String SCENE_NAME = "BeePlayScene";

        [HideInInspector]
        public BeeCellDetailBoardVM cellDetailBoardVM;
        //protected SpecialConstructionControlBoardVM specialConstructionControlBoardVM;
        // ------ bind by editer ------


        override protected void Awake()
        {
            base.Awake();

            //this.specialConstructionControlBoardVM = this.UiRoot.transform.Find("cell_4/SpecialConstructionControlBoardVM").GetComponent<SpecialConstructionControlBoardVM>();
            this.cellDetailBoardVM = this.UiRoot.transform.Find("cellDetailBoardVM").GetComponent<BeeCellDetailBoardVM>();

        }


        public override void show()
        {
            this.postMonoBehaviourInitialization(DemoIdleGameContainer.Game);
            base.show();
        }

        override public void postMonoBehaviourInitialization(DemoIdleGame game)
        {
            base.postMonoBehaviourInitialization(game, GameArea.AREA_BEE, DemoIdleGame.LOGIC_FRAME_PER_SECOND);
        }

        protected override void lazyInitBackUiAndPopupUiContent()
        {
            foreach (Transform child in this.PopupRoot.transform)
            {
                // temp Active make awak() called
                child.gameObject.SetActive(true);
                child.gameObject.SetActive(false);
            }

            //screenBackgroundVM.postPrefabInitialization(this.game.textureManager);

            popupInfoBoardVM.postPrefabInitialization(this);
            achievementMaskBoard.postPrefabInitialization(this);
            notificationMaskBoard.postPrefabInitialization(this);

        }



        protected override void lazyInitUiRootContext()
        {
            base.lazyInitUiRootContext();

            cellDetailBoardVM.postPrefabInitialization(this);


        }

        override protected void lazyInitLogicContext()
        {
            base.lazyInitLogicContext();

            //logicFrameListeners.Add(specialConstructionControlBoardVM);
            //logicFrameListeners.Add(statusBarController);

            gameAreaChangeListeners.Add(cellDetailBoardVM);

            //this.game.idleGameplayExport.gameplayContext.eventManager.registerListener(specialConstructionControlBoardVM);

            

        }

        protected override void dispose()
        {
            base.dispose();
        }
    }
}
