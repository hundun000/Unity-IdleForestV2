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
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.DemoGameCore.ui.sub
{
    public class StageSelectMaskBoardVM : MonoBehaviour
    {
        DemoMenuScreen parent;


        DemoTextButton backTextButton;

        DemoTextButton stage1TextButton;
        DemoTextButton stage2TextButton;
        DemoTextButton stage3TextButton;

        void Awake()
        {
            this.backTextButton = this.transform.Find("backTextButton").GetComponent<DemoTextButton>();
            this.stage1TextButton = this.transform.Find("board/childrenRoot/stage1Board/textButton").GetComponent<DemoTextButton>();
            this.stage2TextButton = this.transform.Find("board/childrenRoot/stage2Board/textButton").GetComponent<DemoTextButton>();
            this.stage3TextButton = this.transform.Find("board/childrenRoot/stage3Board/textButton").GetComponent<DemoTextButton>();
        }

        public void postPrefabInitialization(DemoMenuScreen parent)
        {
            this.parent = parent;
            var texts = parent.game.idleGameplayExport.gameDictionary.getStageSelectMaskBoardTexts(parent.game.idleGameplayExport.language);

            

            this.backTextButton.label.text = texts[0];
            this.backTextButton.button.onClick.AddListener(() => {
                parent.hideStageSelectBoard();
            });

            this.stage1TextButton.button.onClick.AddListener(() => {
                parent.game.saveHandler.gameplayLoadOrStarter(0);
                SceneManager.LoadScene(DemoMenuScreen.START_PLAY_SCREEN);
            });
            this.stage1TextButton.label.text = texts[1];

            this.stage2TextButton.button.onClick.AddListener(() => {
                parent.game.saveHandler.gameplayLoadOrStarter(1);
                SceneManager.LoadScene(DemoMenuScreen.START_PLAY_SCREEN);
            });
            this.stage2TextButton.label.text = texts[2];

            this.stage3TextButton.button.onClick.AddListener(() => {
                parent.game.saveHandler.gameplayLoadOrStarter(2);
                SceneManager.LoadScene(DemoMenuScreen.START_PLAY_SCREEN);
            });
            this.stage3TextButton.label.text = texts[3];
        }

    }
}
