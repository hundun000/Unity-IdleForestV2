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
    public delegate int ProficiencySpeedCalculator(BaseIdleForestConstruction thiz);

    

    public class AutoProficiencyConstruction : BaseIdleForestConstruction
    {
        
        

        protected int autoOutputProgress = 0;
        protected int autoProficiencyProgress = 0;
        protected const int AUTO_PROFICIENCY_SECOND_MAX = 2; // n秒生长一次

        public AutoProficiencyConstruction(
            String prototypeId, 
            String id, 
            GridPosition position,
            DescriptionPackage descriptionPackage
            ) : base(prototypeId, id, position, descriptionPackage)
        {
        }


        override public void onLogicFrame()
        {
            autoOutputProgress++;
            int outputFrameCountMax = outputComponent.autoOutputSecondCountMax * gameContext.LOGIC_FRAME_PER_SECOND;
            if (autoOutputProgress >= outputFrameCountMax)
            {
                autoOutputProgress = 0;
                tryAutoOutputOnce();
            }

            autoProficiencyProgress++;
            int proficiencyFrameCountMax = AUTO_PROFICIENCY_SECOND_MAX * gameContext.LOGIC_FRAME_PER_SECOND;
            if (autoProficiencyProgress >= proficiencyFrameCountMax)
            {
                autoProficiencyProgress = 0;
                tryProficiencyOnce();
            }

        }

        virtual protected void tryProficiencyOnce()
        {
            if (proficiencySpeedCalculator != null)
            {
                proficiencyComponent.changeProficiency(proficiencySpeedCalculator.Invoke(this));
            }
        }

        private void tryAutoOutputOnce()
        {
            if (!canOutput())
            {
                //gameContext.frontend.log(this.id, "canOutput");
                return;
            }
            //gameContext.frontend.log(this.id, "AutoOutput");
            doOutput();
        }

    }
}
