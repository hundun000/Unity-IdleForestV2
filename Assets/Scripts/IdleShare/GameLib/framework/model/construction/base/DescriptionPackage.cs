using hundun.idleshare.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace hundun.idleshare.gamelib
{
    public class DescriptionPackage
    {
        public String outputCostDescriptionStart;
        public String outputGainDescriptionStart;
        public String upgradeCostDescriptionStart;
        public String upgradeMaxLevelDescription = "(max level)";
        public String buttonDescroption;
        public String destoryGainDescriptionStart;
        public ILevelDescroptionProvider levelDescroptionProvider;
        public IProficiencyDescroptionProvider proficiencyDescroptionProvider;
        public String destoryButtonDescroption = "DESTORY";

        public DescriptionPackage(string outputCostDescriptionStart, 
            string outputGainDescriptionStart, 
            string upgradeCostDescriptionStart, 
            string upgradeMaxLevelDescription, 
            string buttonDescroption,
            String destoryGainDescriptionStart,
            ILevelDescroptionProvider levelDescroptionProvider,
            IProficiencyDescroptionProvider proficiencyDescroptionProvider
            )
        {
            this.outputCostDescriptionStart = outputCostDescriptionStart;
            this.outputGainDescriptionStart = outputGainDescriptionStart;
            this.upgradeCostDescriptionStart = upgradeCostDescriptionStart;
            this.upgradeMaxLevelDescription = upgradeMaxLevelDescription;
            this.buttonDescroption = buttonDescroption;
            this.destoryGainDescriptionStart = destoryGainDescriptionStart;
            this.levelDescroptionProvider = levelDescroptionProvider;
            this.proficiencyDescroptionProvider = proficiencyDescroptionProvider;
        }
    }

    public delegate String ILevelDescroptionProvider(int level, int workingLevel, Boolean reachMaxLevel);
    public delegate String IProficiencyDescroptionProvider(int proficiency, Boolean reachMaxProficiency);





}
