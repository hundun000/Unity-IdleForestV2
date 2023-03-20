using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hundun.idleshare.gamelib
{
    public class ProficiencyComponent
    {
        private readonly BaseConstruction construction;

        public ProficiencyComponent(BaseConstruction construction)
        {
            this.construction = construction;
        }

        public String getProficiencyDescroption()
        {
            Boolean reachMaxLevel = construction.saveData.proficiency == construction.maxProficiency;
            return construction.descriptionPackage.proficiencyDescroptionProvider.Invoke(construction.saveData.proficiency, reachMaxLevel);
        }

        public Boolean canChangeProficiency(int delta)
        {
            int next = construction.saveData.proficiency + delta;
            if (next > construction.maxProficiency || next < 0)
            {
                return false;
            }
            return true;
        }

        public void changeProficiency(int delta)
        {
            if (canChangeProficiency(delta))
            {
                construction.saveData.proficiency = (construction.saveData.proficiency + delta);
                construction.updateModifiedValues();
                construction.gameContext.frontend.log(construction.name, "changeProficiency delta = " + delta + ", success to " + construction.saveData.proficiency);
            }
            else
            {
                construction.gameContext.frontend.log(construction.name, "canChangeProficiency delta = " + delta + ", but cannot!");
            }

        }
    }
}

