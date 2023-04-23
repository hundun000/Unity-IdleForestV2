using Assets.Scripts.DemoGameCore.logic;
using Assets.Scripts.DemoGameCore.ui.sub;
using hundun.idleshare.enginecore;
using hundun.unitygame.adapters;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.DemoGameCore.ui.screen
{
    public abstract class BaseIdleForestPlayScreen : BaseIdlePlayScreen<DemoIdleGame, RootSaveData>, IGameAreaChangeListener
    {
        protected DemoGameAreaControlBoardVM gameAreaControlBoardVM;
        protected DemoStorageInfoBoardVM storageInfoBoardVM;


        override protected void Awake()
        {
            base.Awake();

            this.gameAreaControlBoardVM = this.UiRoot.transform.Find("gameAreaControlBoardVM").gameObject.GetComponent<DemoGameAreaControlBoardVM>();
            this.storageInfoBoardVM = this.UiRoot.transform.Find("storageInfoBoardVM").gameObject.GetComponent<DemoStorageInfoBoardVM>();

        }

        override protected void lazyInitLogicContext()
        {
            base.lazyInitLogicContext();

            gameAreaChangeListeners.Add(gameAreaControlBoardVM);
            gameAreaChangeListeners.Add(this);
            gameAreaChangeListeners.Add(storageInfoBoardVM);

            this.game.idleGameplayExport.eventManagerRegisterListener(storageInfoBoardVM);
        }

        override protected void dispose()
        {
            base.dispose();
            this.game.idleGameplayExport.eventManagerUnregisterListener(storageInfoBoardVM);
        }

        override protected void lazyInitUiRootContext()
        {
            //base.lazyInitUiRootContext();

            gameAreaControlBoardVM.postPrefabInitialization(this, GameArea.values);
            storageInfoBoardVM.postPrefabInitialization(this, ResourceType.VALUES_FOR_SHOW_ORDER);
        }

        Dictionary<String, String> areaToScreenIdMap = JavaFeatureForGwt.mapOf(
            GameArea.AREA_WORLD, WorldPlayScreen.SCENE_NAME,
            GameArea.AREA_BEE, BeePlayScreen.SCENE_NAME
            );

        void IGameAreaChangeListener.onGameAreaChange(string last, string current)
        {
            String lastScreen = areaToScreenIdMap.get(last);
            String currentScreen = areaToScreenIdMap.get(current);

            if (lastScreen != null && !currentScreen.Equals(lastScreen))
            {
                game.frontend.log(this.getClass().getSimpleName(), "will LoadScene for currentScreen = " + current);
                this.dispose();
                SceneManager.LoadScene(currentScreen);
            }
        }

        
    }
}
