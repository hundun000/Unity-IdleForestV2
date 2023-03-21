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
        public String promoteConstructionPrototypeId;
        public String demoteConstructionPrototypeId;

        public ProficiencyComponent(BaseConstruction construction)
        {
            this.construction = construction;
        }

        public String getProficiencyDescroption()
        {
            Boolean reachMaxLevel = construction.saveData.proficiency >= 100;
            return construction.descriptionPackage.proficiencyDescroptionProvider.Invoke(construction.saveData.proficiency, reachMaxLevel);
        }

        public Boolean canPromote()
        {
            return (construction.saveData.proficiency >= 100) && promoteConstructionPrototypeId != null;
        }

        public Boolean canDemote()
        {
            return (construction.saveData.proficiency < 0) && demoteConstructionPrototypeId != null;
        }

        public void changeProficiency(int delta)
        {

            construction.saveData.proficiency = (construction.saveData.proficiency + delta);
            construction.updateModifiedValues();
            construction.gameContext.frontend.log(construction.name, "changeProficiency delta = " + delta + ", success to " + construction.saveData.proficiency);


        }
    }
}

