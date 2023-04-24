using Assets.Scripts.DemoGameCore.logic;
using hundun.idleshare.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

namespace Assets.Scripts.DemoGameCore.logic
{
    public delegate int ProficiencySpeedCalculator(BaseIdleForestConstruction thiz);

    internal class IdleForestProficiencyComponent : ProficiencyComponent
    {
        private int upgradeLostProficiency = 50;
        protected int autoProficiencyProgress = 0;
        protected readonly int? AUTO_PROFICIENCY_SECOND_MAX; // n秒生长一次
        public ProficiencySpeedCalculator proficiencySpeedCalculator;

        public IdleForestProficiencyComponent(BaseIdleForestConstruction construction, int? second) : base(construction)
        {
            this.AUTO_PROFICIENCY_SECOND_MAX = second;
        }

        internal override void onSubLogicFrame()
        {
            if (AUTO_PROFICIENCY_SECOND_MAX != null)
            {
                autoProficiencyProgress++;
                int proficiencyFrameCountMax = AUTO_PROFICIENCY_SECOND_MAX.Value * construction.gameplayContext.LOGIC_FRAME_PER_SECOND;
                if (autoProficiencyProgress >= proficiencyFrameCountMax)
                {
                    autoProficiencyProgress = 0;
                    tryProficiencyOnce();
                }
            }
            
        }

        virtual protected void tryProficiencyOnce()
        {
            if (this.proficiencySpeedCalculator != null)
            {
                this.changeProficiency(proficiencySpeedCalculator.Invoke((BaseIdleForestConstruction)construction));
            }
        }

        internal override void afterUpgrade()
        {
            construction.saveData.proficiency -= this.upgradeLostProficiency;
        }
    }
}
