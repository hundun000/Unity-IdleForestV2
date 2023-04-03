using hundun.idleshare.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace hundun.idleshare.gamelib
{

    public class DescriptionPackageBuilder
    {
        private String buttonDescroption;

        private String outputCostDescriptionStart;
        private String outputGainDescriptionStart;

        private String upgradeCostDescriptionStart;
        private String upgradeMaxLevelNoTransferDescription;

        private String transferButtonDescroption;
        private String transferCostDescriptionStart;
        private String upgradeMaxLevelHasTransferDescription;

        private String destroyButtonDescroption;
        private String destroyGainDescriptionStart;
        private String destroyCostDescriptionStart;

        private ILevelDescroptionProvider levelDescroptionProvider;

        private String proficiencyShowName;
        private IProficiencyDescroptionProvider proficiencyDescroptionProvider;

        public DescriptionPackageBuilder button(String buttonDescroption)
        {
            this.buttonDescroption = buttonDescroption;
            return this;
        }

        public DescriptionPackageBuilder output(String outputCostDescriptionStart, String outputGainDescriptionStart)
        {
            this.outputCostDescriptionStart = outputCostDescriptionStart;
            this.outputGainDescriptionStart = outputGainDescriptionStart;
            return this;
        }

        public DescriptionPackageBuilder upgrade(String upgradeCostDescriptionStart, String upgradeMaxLevelNoTransferDescription, ILevelDescroptionProvider levelDescroptionProvider)
        {
            this.upgradeCostDescriptionStart = upgradeCostDescriptionStart;
            this.upgradeMaxLevelNoTransferDescription = upgradeMaxLevelNoTransferDescription;
            this.levelDescroptionProvider = levelDescroptionProvider;
            return this;
        }

        public DescriptionPackageBuilder transfer(String transferButtonDescroption,
                String transferCostDescriptionStart,
                String upgradeMaxLevelHasTransferDescription)
        {
            this.transferButtonDescroption = transferButtonDescroption;
            this.transferCostDescriptionStart = transferCostDescriptionStart;
            this.upgradeMaxLevelHasTransferDescription = upgradeMaxLevelHasTransferDescription;
            return this;
        }

        public DescriptionPackageBuilder destroy(String destroyButtonDescroption,
                String destroyGainDescriptionStart,
                String destroyCostDescriptionStart)
        {
            this.destroyButtonDescroption = destroyButtonDescroption;
            this.destroyGainDescriptionStart = destroyGainDescriptionStart;
            this.destroyCostDescriptionStart = destroyCostDescriptionStart;
            return this;
        }

        public DescriptionPackageBuilder proficiency(String proficiencyShowName,
            IProficiencyDescroptionProvider proficiencyDescroptionProvider)
        {
            this.proficiencyShowName = proficiencyShowName;
            this.proficiencyDescroptionProvider = proficiencyDescroptionProvider;
            return this;
        }

        public DescriptionPackage build()
        {
            return new DescriptionPackage(
                    buttonDescroption,

                    outputCostDescriptionStart,
                    outputGainDescriptionStart,

                    upgradeCostDescriptionStart,
                    upgradeMaxLevelNoTransferDescription,

                    transferButtonDescroption,
                    transferCostDescriptionStart,
                    upgradeMaxLevelHasTransferDescription,

                    destroyButtonDescroption,
                    destroyGainDescriptionStart,
                    destroyCostDescriptionStart,

                    levelDescroptionProvider,

                    proficiencyShowName,
                    proficiencyDescroptionProvider
                );
        }

    }
    public class DescriptionPackage
    {
        public String buttonDescroption;

        public String outputCostDescriptionStart;
        public String outputGainDescriptionStart;

        public String upgradeCostDescriptionStart;
        public String upgradeMaxLevelNoTransferDescription;

        public String transferButtonDescroption;
        public String transferCostDescriptionStart;
        public String upgradeMaxLevelHasTransferDescription;

        public String destroyButtonDescroption;
        public String destroyGainDescriptionStart;
        public String destroyCostDescriptionStart;

        public ILevelDescroptionProvider levelDescroptionProvider;

        public String proficiencyShowName;
        public IProficiencyDescroptionProvider proficiencyDescroptionProvider;

        public DescriptionPackage(
            string buttonDescroption, 
            string outputCostDescriptionStart, 
            string outputGainDescriptionStart, 
            string upgradeCostDescriptionStart, 
            string upgradeMaxLevelNoTransferDescription, 
            string transferButtonDescroption, 
            string transferCostDescriptionStart, 
            string upgradeMaxLevelHasTransferDescription, 
            string destroyButtonDescroption, 
            string destroyGainDescriptionStart, 
            string destroyCostDescriptionStart, 
            ILevelDescroptionProvider levelDescroptionProvider,
            String proficiencyShowName,
            IProficiencyDescroptionProvider proficiencyDescroptionProvider)
        {
            this.buttonDescroption = buttonDescroption;
            this.outputCostDescriptionStart = outputCostDescriptionStart;
            this.outputGainDescriptionStart = outputGainDescriptionStart;
            this.upgradeCostDescriptionStart = upgradeCostDescriptionStart;
            this.upgradeMaxLevelNoTransferDescription = upgradeMaxLevelNoTransferDescription;
            this.transferButtonDescroption = transferButtonDescroption;
            this.transferCostDescriptionStart = transferCostDescriptionStart;
            this.upgradeMaxLevelHasTransferDescription = upgradeMaxLevelHasTransferDescription;
            this.destroyButtonDescroption = destroyButtonDescroption;
            this.destroyGainDescriptionStart = destroyGainDescriptionStart;
            this.destroyCostDescriptionStart = destroyCostDescriptionStart;
            this.levelDescroptionProvider = levelDescroptionProvider;
            this.proficiencyShowName = proficiencyShowName;
            this.proficiencyDescroptionProvider = proficiencyDescroptionProvider;
        }
    }

    public delegate String ILevelDescroptionProvider(int level, int workingLevel, Boolean reachMaxLevel);
    public delegate String IProficiencyDescroptionProvider(int proficiency, Boolean reachMaxProficiency);





}
