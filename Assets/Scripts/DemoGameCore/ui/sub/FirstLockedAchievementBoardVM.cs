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
            AbstractAchievement data = parent.game.idleGameplayExport.getFirstLockedAchievement();
            if (data != null)
            {
                nameLabel.text = data.name;
                descriptionLabel.text = data.description;
            } 
            else
            {
                nameLabel.text = "无";
                descriptionLabel.text = "无";
            }
            
        }

        public void onGameAreaChange(string last, string current)
        {
            updateData();
        }
    }
}
