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
        public String destoryCostDescriptionStart;
        public ILevelDescroptionProvider levelDescroptionProvider;
        public IProficiencyDescroptionProvider proficiencyDescroptionProvider;
        public String destoryButtonDescroption = "DESTORY";

        /**
         * for idleShare
         */
        public DescriptionPackage(
            string outputCostDescriptionStart,
            string outputGainDescriptionStart,
            string upgradeCostDescriptionStart,
            string upgradeMaxLevelDescription,
            string buttonDescroption,
            ILevelDescroptionProvider levelDescroptionProvider,
            IProficiencyDescroptionProvider proficiencyDescroptionProvider
            ) : this(
                outputCostDescriptionStart,
                outputGainDescriptionStart,
                upgradeCostDescriptionStart,
                upgradeMaxLevelDescription,
                buttonDescroption,
                null,
                null,
                levelDescroptionProvider,
                proficiencyDescroptionProvider
                )
        {
        }

        /**
         * for idleForest
         */
        public DescriptionPackage(
            string outputCostDescriptionStart, 
            string outputGainDescriptionStart, 
            string upgradeCostDescriptionStart, 
            string upgradeMaxLevelDescription, 
            string buttonDescroption,
            String destoryGainDescriptionStart,
            String destoryCostDescriptionStart,
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
            this.destoryCostDescriptionStart = destoryCostDescriptionStart;
            this.levelDescroptionProvider = levelDescroptionProvider;
            this.proficiencyDescroptionProvider = proficiencyDescroptionProvider;
        }
    }

    public delegate String ILevelDescroptionProvider(int level, int workingLevel, Boolean reachMaxLevel);
    public delegate String IProficiencyDescroptionProvider(int proficiency, Boolean reachMaxProficiency);





}
