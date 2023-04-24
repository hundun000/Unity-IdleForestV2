using Assets.Scripts.DemoGameCore.ui.screen;
using hundun.idleshare.enginecore;
using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.DemoGameCore.ui.sub
{
    public class FirstLockedAchievementBoardVM : MonoBehaviour, IGameAreaChangeListener
    {
        WorldPlayScreen parent;

        Image background;
        Text nameStartLabel;
        Text nameValueLabel;
        Text descriptionLabel;
        Text countStartLabel;
        Text countValueLabel;

        void Awake()
        {
            this.background = this.transform.Find("background").GetComponent<Image>();
            this.nameStartLabel = this.transform.Find("nameStartLabel").GetComponent<Text>();
            this.nameValueLabel = this.transform.Find("nameValueLabel").GetComponent<Text>();
            this.descriptionLabel = this.transform.Find("descriptionLabel").GetComponent<Text>();
            this.countStartLabel = this.transform.Find("countStartLabel").GetComponent<Text>();
            this.countValueLabel = this.transform.Find("countValueLabel").GetComponent<Text>();
        }

        public void postPrefabInitialization(WorldPlayScreen parent)
        {
            this.parent = parent;
            this.background.sprite = parent.game.textureManager.defaultBoardNinePatchTexture;

            nameStartLabel.text = parent.game.idleGameplayExport.gameDictionary.getAchievementTexts(parent.game.idleGameplayExport.language)[0];
            countStartLabel.text = parent.game.idleGameplayExport.gameDictionary.getAchievementTexts(parent.game.idleGameplayExport.language)[1];

            updateData();
        }

        internal void updateData()
        {
            AchievementInfoPackage data = parent.game.idleGameplayExport.gameplayContext.achievementManager.getAchievementInfoPackage();
            if (data.firstLockedAchievement != null)
            {
                nameValueLabel.text = data.firstLockedAchievement.name;
                descriptionLabel.text = data.firstLockedAchievement.description;
            } 
            else
            {
                nameValueLabel.text = "已完成";
                descriptionLabel.text = "无";
            }

            countValueLabel.text = " " + data.unLockedSize + "/" + data.total;


        }

        public void onGameAreaChange(string last, string current)
        {
            updateData();
        }
    }
}
