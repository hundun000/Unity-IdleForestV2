using Assets.Scripts.DemoGameCore.logic;
using Assets.Scripts.DemoGameCore.ui.screen;
using hundun.idleshare.enginecore;
using hundun.idleshare.gamelib;
using hundun.unitygame.adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DemoGameCore
{
    public class DemoIdleGame : BaseIdleGame<DemoIdleGame, RootSaveData>
    {
        public const int LOGIC_FRAME_PER_SECOND = 10;

        public DemoIdleGame() : base(1024, 768)
        {
            //this.textFormatTool = new TextFormatTool();
            this.saveHandler = new DemoSaveHandler(frontend, new UnitySaveTool<RootSaveData>());
            this.textureManager = new DemoTextureManager();
            //this.managerContext = new ManagerContext<>(this);
            this.audioPlayManager = new AudioPlayManager<DemoIdleGame, RootSaveData>(this);
            this.childGameConfig = new DemoChildGameConfig();
        }

        override protected void createStage1()
        {
            base.createStage1();

            this.idleGameplayExport = new IdleGameplayExport(
                    frontend,
                    new DemoGameDictionary(),
                    new DemoBuiltinConstructionsLoader(),
                    new IdleForestAchievementLoader(),
                    LOGIC_FRAME_PER_SECOND,
                    childGameConfig
                    );
            this.saveHandler.registerSubHandler(idleGameplayExport);
            saveHandler.systemSettingLoadOrStarter();
        }

        override protected void createStage3()
        {
            base.createStage3();

        }
    }
}
