using Assets.Scripts.DemoGameCore.logic;
using Assets.Scripts.DemoGameCore.ui.screen;
using hundun.idleshare.enginecore;
using hundun.idleshare.gamelib;
using hundun.unitygame.adapters;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.DemoGameCore.ui.sub
{
    public abstract class BaseCellDetailBoardVM : MonoBehaviour, ILogicFrameListener
    {
        protected BaseIdleForestPlayScreen parent;

        protected GameObject nodesRoot;
        protected Image background;


        protected CellDetailInnerBoardVM innerBoardVMPrefab;

        
        protected List<CellDetailInnerBoardVM> contents = new List<CellDetailInnerBoardVM>();

        virtual protected void Awake()
        {
            this.background = this.transform.Find("background").GetComponent<Image>();
            this.nodesRoot = this.transform.Find("_nodesRoot").gameObject;
            this.innerBoardVMPrefab = this.transform.Find("_templates/innerBoardVMPrefab").GetComponent<CellDetailInnerBoardVM>();
        }

        public void onLogicFrame()
        {
            contents.ForEach(it => {
                it.update();
            });
        }

        virtual public void postPrefabInitialization(BaseIdleForestPlayScreen parent)
        {
            //super("GUIDE_TEXT", parent.game.getButtonSkin());
            this.parent = parent;
            this.background.sprite = parent.game.textureManager.defaultBoardNinePatchTexture;

        }


    }
}
