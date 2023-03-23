using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;
using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DemoGameCore.logic
{

    public class GovernmentConstruction : BaseIdleForestConstruction
    {


        protected int autoProficiencyProgress = 0;
        protected const int AUTO_PROFICIENCY_SECOND_MAX = 1; // n秒生长一次

        public GovernmentConstruction(
            String prototypeId, 
            String id, 
            GridPosition position, 
            Language language
            ) : base(prototypeId, id, position, language)
        {
        }


        override public void onLogicFrame()
        {
            autoProficiencyProgress++;
            int proficiencyFrameCountMax = AUTO_PROFICIENCY_SECOND_MAX * gameContext.LOGIC_FRAME_PER_SECOND;
            if (autoProficiencyProgress >= proficiencyFrameCountMax)
            {
                autoProficiencyProgress = 0;
                tryProficiencyOnce();
            }

        }

        private void tryProficiencyOnce()
        {
            proficiencyComponent.changeProficiency(10);

            if (saveData.proficiency == 100)
            {
                proficiencyComponent.cleanProficiency();
                gameContext.eventManager.notifyNotification("政府进行了一次奖惩");
            }

            
        }

    }
}
