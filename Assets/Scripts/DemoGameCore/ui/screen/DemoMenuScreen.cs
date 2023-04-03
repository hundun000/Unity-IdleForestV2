using Assets.Scripts.DemoGameCore.logic;
using hundun.idleshare.enginecore;
using hundun.unitygame.adapters;
using hundun.unitygame.enginecorelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.DemoGameCore.ui.screen
{
    public class DemoMenuScreen : BaseIdleMenuScreen<DemoIdleGame, RootSaveData>
    {
        public const String SCENE_NAME = "MenuScene";

        override public void postMonoBehaviourInitialization(DemoIdleGame game) {
            base.postMonoBehaviourInitialization(
            game,
            () =>
            {
                game.saveHandler.gameplayLoadOrStarter(-1);
                SceneManager.LoadScene(DemoPlayScreen.SCENE_NAME);
            },
            () =>
            {
                int starterIndex = 0;
                game.saveHandler.gameplayLoadOrStarter(starterIndex);
                SceneManager.LoadScene(DemoPlayScreen.SCENE_NAME);
            }
            );

        }

        override public void show()
        {
            this.postMonoBehaviourInitialization(DemoIdleGameContainer.Game);

            base.show();

            
        }
    }
}

