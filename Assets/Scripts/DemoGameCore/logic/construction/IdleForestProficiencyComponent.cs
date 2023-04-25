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

    internal class IdleForestProficiencyComponent : BaseAutoProficiencyComponent
    {
        public ProficiencySpeedCalculator proficiencySpeedCalculator;

        public IdleForestProficiencyComponent(BaseIdleForestConstruction construction, int? second) : base(construction, 50, null)
        {
        }

        protected override void tryProficiencyOnce()
        {
            if (this.proficiencySpeedCalculator != null)
            {
                this.changeProficiency(proficiencySpeedCalculator.Invoke((BaseIdleForestConstruction)construction));
            }
        }
    }
}
