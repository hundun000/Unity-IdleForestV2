using hundun.unitygame.gamelib;
using Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Unity.VisualScripting.Icons;

namespace hundun.idleshare.gamelib
{
    public abstract class BaseConstruction : ILogicFrameListener, IBuffChangeListener, ITileNode<BaseConstruction>
    {

        public static readonly int DEFAULT_MAX_LEVEL = 99;
        public int maxLevel = DEFAULT_MAX_LEVEL;

        public static readonly int DEFAULT_MAX_DRAW_NUM = 5;
        public int maxDrawNum = DEFAULT_MAX_DRAW_NUM;

        public static readonly int DEFAULT_MIN_WORKING_LEVEL = 0;
        public int minWorkingLevel = DEFAULT_MIN_WORKING_LEVEL;

        public static readonly int DEFAULT_MAX_PROFICIENCY = 100;
        public int maxProficiency = DEFAULT_MAX_PROFICIENCY;

        protected Random random = new Random();

        public IdleGameplayContext gameContext;

        /**
         * NotNull
         */
        public ConstructionSaveData saveData;

        public String name;

        public String id;

        public String prototypeId;

        public String detailDescroptionConstPart;

        public DescriptionPackage descriptionPackage;


        /**
         * NotNull
         */
        public UpgradeComponent upgradeComponent;


        /**
         * NotNull
         */
        public OutputComponent outputComponent;

        /**
         * NotNull
         */
        public LevelComponent levelComponent;

        /**
         * NotNull
         */
        public ProficiencyComponent proficiencyComponent;

        private Dictionary<TileNeighborDirection, BaseConstruction> _neighbors;

        public GridPosition position { get => this.saveData.position; set => this.saveData.position = value; }
        public Dictionary<TileNeighborDirection, BaseConstruction> neighbors { get => _neighbors; set => _neighbors = value; }

        public void lazyInitDescription(IdleGameplayContext gameContext, Language language)
        {
            this.gameContext = gameContext;

            this.name = gameContext.gameDictionary.constructionPrototypeIdToShowName(language, prototypeId);
            this.detailDescroptionConstPart = gameContext.gameDictionary.constructionPrototypeIdToDetailDescroptionConstPart(language, prototypeId);

            outputComponent.lazyInitDescription();
            upgradeComponent.lazyInitDescription();

            updateModifiedValues();
        }

        public BaseConstruction(String prototypeId, String id)
        {

            this.saveData = new ConstructionSaveData();
            this.id = id;
            this.prototypeId = prototypeId;
            this.proficiencyComponent = new ProficiencyComponent(this);
        }

        public abstract void onClick();

        public abstract Boolean canClickEffect();

        public String getButtonDescroption()
        {
            return descriptionPackage.buttonDescroption;
        }

        //protected abstract long calculateModifiedUpgradeCost(long baseValue, int level);
        public abstract long calculateModifiedOutput(long baseValue, int level, int proficiency);
        public abstract long calculateModifiedOutputCost(long baseValue, int level, int proficiency);



        /**
         * 重新计算各个数值的加成后的结果
         */
        public void updateModifiedValues()
        {
            //gameContext.frontend.log(this.name, "updateCurrentCache called");
            // --------------
            Boolean reachMaxLevel = this.saveData.level == this.maxLevel;
            upgradeComponent.updateModifiedValues(reachMaxLevel);
            outputComponent.updateModifiedValues();

        }

       
        public void onBuffChange()
        {
            updateModifiedValues();
        }


        virtual protected void printDebugInfoAfterConstructed()
        {
            // default do nothing
        }

        protected Boolean canOutput()
        {
            return outputComponent.canOutput();
        }


        protected Boolean canUpgrade()
        {
            return upgradeComponent.canUpgrade();
        }

        public String getSaveDataKey()
        {
            return id;
        }

        public abstract void onLogicFrame();
    }
}
