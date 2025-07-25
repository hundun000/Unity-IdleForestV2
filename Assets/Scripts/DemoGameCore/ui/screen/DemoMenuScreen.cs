﻿using Assets.Scripts.DemoGameCore.logic;
using Assets.Scripts.DemoGameCore.ui.sub;
using hundun.idleshare.enginecore;
using hundun.idleshare.gamelib;
using hundun.unitygame.adapters;
using hundun.unitygame.enginecorelib;
using hundun.unitygame.gamelib;
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
    public class DemoMenuScreen : BaseHundunScreen<DemoIdleGame, RootSaveData>
    {
        public const String SCENE_NAME = "MenuScene";
        public const String START_PLAY_SCREEN = WorldPlayScreen.SCENE_NAME;

        private JRunable buttonContinueGameInputListener;
        private JRunable buttonNewGameInputListener;


        protected GameObject UiRoot { get; private set; }
        protected GameObject PopupRoot { get; private set; }
        protected AudioSource audioSource;

        protected Text title;
        protected DemoTextButton buttonContinueGame;
        protected DemoTextButton buttonNewGame;
        protected DemoLanguageSwitchBoardVM languageSwitchBoardVM;
        protected StageSelectMaskBoardVM stageSelectMaskBoardVM;


        override public void postMonoBehaviourInitialization(DemoIdleGame game)
        {
            base.postMonoBehaviourInitialization(game);

            foreach (Transform child in this.PopupRoot.transform)
            {
                // temp Active make awak() called
                child.gameObject.SetActive(true);
                child.gameObject.SetActive(false);
            }

            stageSelectMaskBoardVM.postPrefabInitialization(this);

            this.buttonContinueGameInputListener = () =>
            {
                game.saveHandler.gameplayLoadOrStarter(-1);
                SceneManager.LoadScene(DemoMenuScreen.START_PLAY_SCREEN);
            };
            this.buttonNewGameInputListener = () =>
            {
                showStageSelectBoard();
            };
        }

        void Awake()
        {
            UiRoot = gameObject.transform.Find("_uiRoot").gameObject;
            PopupRoot = this.transform.Find("_popupRoot").gameObject;
            audioSource = this.transform.Find("_audioSource").GetComponent<AudioSource>();

            this.title = this.UiRoot.transform.Find("title").gameObject.GetComponent<Text>();
            this.buttonContinueGame = this.UiRoot.transform.Find("buttonContinueGame").gameObject.GetComponent<DemoTextButton>();
            this.buttonNewGame = this.UiRoot.transform.Find("buttonNewGame").gameObject.GetComponent<DemoTextButton>();
            this.languageSwitchBoardVM = this.UiRoot.transform.Find("languageSwitchBoardVM").gameObject.GetComponent<DemoLanguageSwitchBoardVM>();
            this.stageSelectMaskBoardVM = this.PopupRoot.transform.Find("stageSelectMaskBoardVM").gameObject.GetComponent<StageSelectMaskBoardVM>();
        }

        override public void show()
        {
            this.postMonoBehaviourInitialization(DemoIdleGameContainer.Game);

            List<String> memuScreenTexts = game.idleGameplayExport.gameDictionary.getMemuScreenTexts(game.idleGameplayExport.language);

            title.text = JavaFeatureForGwt.stringFormat("[     %s     ]", memuScreenTexts[0]);

            buttonContinueGame.label.text = memuScreenTexts[2];
            buttonContinueGame.button.onClick.AddListener(buttonContinueGameInputListener.Invoke);

            buttonNewGame.label.text = memuScreenTexts[1];
            buttonNewGame.button.onClick.AddListener(buttonNewGameInputListener.Invoke);

            if (!game.saveHandler.hasContinuedGameplaySave())
            {
                buttonContinueGame.gameObject.SetActive(false);
            }
            else
            {
                buttonContinueGame.gameObject.SetActive(true);
            }

            languageSwitchBoardVM.postPrefabInitialization(
                this,
                Enum.GetValues(typeof(Language)).OfType<Language>().ToArray(),
                game.idleGameplayExport.language,
                memuScreenTexts.get(3),
                memuScreenTexts.get(4),
                it => { 
                    game.idleGameplayExport.language = it;
                    game.saveHandler.gameSaveCurrent();
                }
                );

            // unity adapter
            game.audioPlayManager.intoScreen(audioSource, this.getClass().getSimpleName());
        }

        internal void hideStageSelectBoard()
        {
            stageSelectMaskBoardVM.gameObject.SetActive(false);
        }

        internal void showStageSelectBoard()
        {
            stageSelectMaskBoardVM.gameObject.SetActive(true);
        }
    }
}

