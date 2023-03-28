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
        DemoPlayScreen parent;

        Image background;
        Text nameLabel;
        Text descriptionLabel;

        void Awake()
        {
            this.background = this.transform.Find("background").GetComponent<Image>();
            this.nameLabel = this.transform.Find("nameLabel").GetComponent<Text>();
            this.descriptionLabel = this.transform.Find("descriptionLabel").GetComponent<Text>();
        }

        public void postPrefabInitialization(DemoPlayScreen parent)
        {
            this.parent = parent;
            this.background.sprite = parent.game.textureManager.defaultBoardNinePatchTexture;
            updateData();
        }

        internal void updateData()
        {
            AchievementInfoPackage data = parent.game.idleGameplayExport.getAchievementInfoPackage();
            if (data.firstLockedAchievement != null)
            {
                nameLabel.text = data.firstLockedAchievement.name;
                descriptionLabel.text = data.firstLockedAchievement.description;
            } 
            else
            {
                nameLabel.text = "已完成";
                descriptionLabel.text = "无";
            }

            nameLabel.text += " " + data.unLockedSize + "/" + data.total;


        }

        public void onGameAreaChange(string last, string current)
        {
            updateData();
        }
    }
}
