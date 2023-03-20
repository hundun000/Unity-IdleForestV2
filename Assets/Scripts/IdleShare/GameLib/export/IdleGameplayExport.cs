using Assets.Scripts.DemoGameCore.logic;
using hundun.unitygame.adapters;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;


namespace hundun.idleshare.gamelib
{

    public class IdleGameplayExport : ILogicFrameListener, ISubGameplaySaveHandler<GameplaySaveData>, ISubSystemSettingSaveHandler<SystemSettingSaveData>
    {
        private IdleGameplayContext gameplayContext;
        private IBuiltinConstructionsLoader builtinConstructionsLoader;
        private ChildGameConfig childGameConfig;
        public IGameDictionary gameDictionary;
        public Language language;

        public IdleGameplayExport(
                IFrontend frontEnd,
                IGameDictionary gameDictionary,
                IBuiltinConstructionsLoader builtinConstructionsLoader,
                int LOGIC_FRAME_PER_SECOND, ChildGameConfig childGameConfig)
        {
            this.gameDictionary = gameDictionary;
            this.childGameConfig = childGameConfig;
            this.builtinConstructionsLoader = builtinConstructionsLoader;
            this.gameplayContext = new IdleGameplayContext(frontEnd, gameDictionary, LOGIC_FRAME_PER_SECOND);
        }

        public long getResourceNumOrZero(String resourceId)
        {
            return gameplayContext.storageManager.getResourceNumOrZero(resourceId);
        }


        public List<BaseConstruction> getConstructionsOfPrototype(String prototypeId)
        {
            return gameplayContext.constructionManager.getConstructionsOfPrototype(prototypeId)
                
                ;
        }

        public void onLogicFrame()
        {
            gameplayContext.constructionManager.onSubLogicFrame();
            gameplayContext.storageManager.onSubLogicFrame();
        }

        public List<BaseConstruction> getAreaShownConstructionsOrEmpty(String current)
        {
            return gameplayContext.constructionManager.getAreaShownConstructionsOrEmpty(current)
                    ;
        }

        public List<AbstractConstructionPrototype> getAreaShownConstructionPrototypesOrEmpty(String current)
        {
            return gameplayContext.constructionManager.getAreaShownConstructionPrototypesOrEmpty(current)
                    ;
            ;
        }

        public void eventManagerRegisterListener(Object objecz)
        {
            gameplayContext.eventManager.registerListener(objecz);
        }

        public HashSet<String> getUnlockedResourceTypes()
        {
            return gameplayContext.storageManager.unlockedResourceTypes;
        }

        public void constructionChangeWorkingLevel(String id, int delta)
        {
            BaseConstruction model = gameplayContext.constructionManager.getConstruction(id);
            model.levelComponent.changeWorkingLevel(delta);
        }

        public void constructionOnClick(String id)
        {
            BaseConstruction model = gameplayContext.constructionManager.getConstruction(id);
            model.onClick();
        }

        public Boolean constructionCanClickEffect(String id)
        {
            BaseConstruction model = gameplayContext.constructionManager.getConstruction(id);
            return model.canClickEffect();
        }

        public Boolean constructionCanChangeWorkingLevel(String id, int delta)
        {
            BaseConstruction model = gameplayContext.constructionManager.getConstruction(id);
            return model.levelComponent.canChangeWorkingLevel(delta);
        }

        public void applyGameplaySaveData(GameplaySaveData gameplaySaveData)
        {
            List<BaseConstruction> constructions = gameplayContext.constructionManager.getConstructions();
            foreach (BaseConstruction construction in constructions)
            {
                if (gameplaySaveData.constructionSaveDataMap.ContainsKey(construction.id))
                {
                    construction.saveData = (gameplaySaveData.constructionSaveDataMap.get(construction.id));
                    construction.updateModifiedValues();
                }
            }
            gameplayContext.storageManager.unlockedResourceTypes = (gameplaySaveData.unlockedResourceTypes);
            gameplayContext.storageManager.ownResoueces = (gameplaySaveData.ownResoueces);
            gameplayContext.achievementManager.unlockedAchievementNames = (gameplaySaveData.unlockedAchievementNames);
        }

        public void currentSituationToGameplaySaveData(GameplaySaveData gameplaySaveData)
        {
            List<BaseConstruction> constructions = gameplayContext.constructionManager.getConstructions();
            gameplaySaveData.constructionSaveDataMap = (constructions
                    .ToDictionary(
                            it => it.id,
                            it => it.saveData
                            )
                    );
            gameplaySaveData.unlockedResourceTypes = (gameplayContext.storageManager.unlockedResourceTypes);
            gameplaySaveData.ownResoueces = (gameplayContext.storageManager.ownResoueces);
            gameplaySaveData.unlockedAchievementNames = (gameplayContext.achievementManager.unlockedAchievementNames);
        }

        public void applySystemSetting(SystemSettingSaveData systemSettingSave)
        {
            this.language = (systemSettingSave.language);
            gameplayContext.allLazyInit(
                    systemSettingSave.language,
                    childGameConfig,
                    builtinConstructionsLoader.getProviderMap(language)
                    );
            gameplayContext.frontend.log(this.getClass().getSimpleName(), "applySystemSetting done");
        }

        public void currentSituationToSystemSetting(SystemSettingSaveData systemSettingSave)
        {
            systemSettingSave.language = (this.language);
        }

        internal void constructionPrototypeOnClick(string prototypeId, GridPosition position)
        {
            gameplayContext.constructionManager.createInstanceOfPrototype(prototypeId, position);
        }

        internal GridPosition getConnectedRandonPosition()
        {
            if (gameplayContext.constructionManager.runningConstructionModelMap.Count == 0)
            {
                return new GridPosition(0, 0);
            }
            else
            {
                foreach (var construction in gameplayContext.constructionManager.runningConstructionModelMap.Values)
                {
                    foreach (var neighborEntry in construction.neighbors)
                    {
                        if (neighborEntry.Value == null)
                        {
                            return TileNodeUtils.tileNeighborPosition(construction, gameplayContext.constructionManager, neighborEntry.Key);
                        }
                    }
                }
            }
            throw new Exception("getConnectedRandonPosition fail");
        }
    }
}
