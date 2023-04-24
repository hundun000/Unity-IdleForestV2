using Assets.Scripts.DemoGameCore.logic;
using Assets.Scripts.DemoGameCore.ui.sub;
using hundun.idleshare.enginecore;
using hundun.idleshare.gamelib;
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
    public class WorldPlayScreen : BaseIdleForestPlayScreen
    {


        public const String SCENE_NAME = "WorldPlayScene";

        //protected SpecialConstructionControlBoardVM specialConstructionControlBoardVM;
        [HideInInspector]
        public WorldCellDetailBoardVM cellDetailBoardVM;
        protected FirstLockedAchievementBoardVM firstLockedAchievementBoardVM;
        
        // ------ bind by editer ------
        public MapController mapController;
        //public StatusBarController statusBarController;

        override protected void Awake()
        {
            base.Awake();

            //this.specialConstructionControlBoardVM = this.UiRoot.transform.Find("cell_4/SpecialConstructionControlBoardVM").GetComponent<SpecialConstructionControlBoardVM>();
            this.cellDetailBoardVM = this.UiRoot.transform.Find("cellDetailBoardVM").GetComponent<WorldCellDetailBoardVM>();
            this.firstLockedAchievementBoardVM = this.UiRoot.transform.Find("firstLockedAchievementBoardVM").GetComponent<FirstLockedAchievementBoardVM>();
            
        }


        public override void show()
        {
            this.postMonoBehaviourInitialization(DemoIdleGameContainer.Game);
            base.show();
        }

        override public void postMonoBehaviourInitialization(DemoIdleGame game)
        {
            base.postMonoBehaviourInitialization(game, GameArea.AREA_WORLD, DemoIdleGame.LOGIC_FRAME_PER_SECOND);
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
            cellDetailBoardVM.postPrefabInitialization(this);
            firstLockedAchievementBoardVM.postPrefabInitialization(this);
        }



        protected override void lazyInitUiRootContext()
        {
            base.lazyInitUiRootContext();

            storageInfoBoardVM.postPrefabInitialization(this, ResourceType.VALUES_FOR_SHOW_ORDER);

            //specialConstructionControlBoardVM.postPrefabInitialization(this);
            mapController.postPrefabInitialization(this);
            //statusBarController.postPrefabInitialization(this);
        }

        override protected void lazyInitLogicContext()
        {
            base.lazyInitLogicContext();

            //logicFrameListeners.Add(specialConstructionControlBoardVM);
            //logicFrameListeners.Add(statusBarController);
            logicFrameListeners.Add(mapController);
            logicFrameListeners.Add(cellDetailBoardVM);
            //gameAreaChangeListeners.Add(specialConstructionControlBoardVM);
            gameAreaChangeListeners.Add(mapController);
            gameAreaChangeListeners.Add(firstLockedAchievementBoardVM);
            //this.game.idleGameplayExport.gameplayContext.eventManager.registerListener(specialConstructionControlBoardVM);
            this.game.idleGameplayExport.gameplayContext.eventManager.registerListener(mapController);
            this.game.idleGameplayExport.gameplayContext.eventManager.registerListener(storageInfoBoardVM);
            this.game.idleGameplayExport.gameplayContext.eventManager.registerListener(cellDetailBoardVM);
        }

        protected override void dispose()
        {
            base.dispose();
            this.game.idleGameplayExport.gameplayContext.eventManager.unregisterListener(mapController);
            this.game.idleGameplayExport.gameplayContext.eventManager.unregisterListener(storageInfoBoardVM);
            this.game.idleGameplayExport.gameplayContext.eventManager.unregisterListener(cellDetailBoardVM);
        }

        override public void showAchievementMaskBoard(AbstractAchievement prototype)
        {
            base.showAchievementMaskBoard(prototype);
            firstLockedAchievementBoardVM.updateData();
        }

    }
}
