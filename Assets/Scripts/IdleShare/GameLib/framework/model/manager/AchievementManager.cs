using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityEditor.Progress;
using Unity.VisualScripting;
using hundun.unitygame.adapters;
using static UnityEditor.MaterialProperty;

namespace hundun.idleshare.gamelib
{
    public class AchievementManager : IBuffChangeListener, IOneFrameResourceChangeListener, IGameStartListener, IConstructionCollectionListener
    {
        IdleGameplayContext gameContext;

        Dictionary<String, AbstractAchievement> prototypes = new Dictionary<String, AbstractAchievement>();


        public HashSet<String> unlockedAchievementIds = new HashSet<String>();
        private List<String> achievementQueue = new List<string>();

        public AchievementManager(IdleGameplayContext gameContext)
        {
            this.gameContext = gameContext;
            gameContext.eventManager.registerListener(this);
        }

        public void addPrototype(AbstractAchievement prototype)
        {
            prototypes.Add(prototype.id, prototype);
            prototype.lazyInitDescription(gameContext);
        }

        public AbstractAchievement getFirstLockedAchievement()
        {
            return achievementQueue
                .Where(it => !unlockedAchievementIds.Contains(prototypes.get(it).id))
                .Select(it => prototypes.get(it))
                .FirstOrDefault();
        }

        private Boolean checkRequiredResources(Dictionary<String, int> requiredResources)
        {
            if (requiredResources == null)
            {
                return true;
            }
            foreach (KeyValuePair<String, int> entry in requiredResources)
            {
                long own = gameContext.storageManager.getResourceNumOrZero(entry.Key);
                if (own < entry.Value)
                {
                    return false;
                }
            }
            return true;
        }

        private Boolean checkRequiredBuffs(Dictionary<String, int> map)
        {
            if (map == null)
            {
                return true;
            }
            foreach (KeyValuePair<String, int> entry in map)
            {
                int own = gameContext.buffManager.getBuffAmoutOrDefault(entry.Key);
                if (own < entry.Value)
                {
                    return false;
                }
            }
            return true;
        }

        private void checkAllAchievementUnlock()
        {
            //Gdx.app.log(this.getClass().getSimpleName(), "checkAllAchievementUnlock");
            foreach (AbstractAchievement prototype in prototypes.Values)
            {
                if (unlockedAchievementIds.Contains(prototype.id))
                {
                    continue;
                }
                Boolean resourceMatched = prototype.checkUnloack();
                if (resourceMatched)
                {
                    unlockedAchievementIds.Add(prototype.id);
                    gameContext.eventManager.notifyAchievementUnlock(prototype);
                }
            }
        }



        public void onBuffChange()
        {
            checkAllAchievementUnlock();
        }

        public void lazyInit(Dictionary<string, AbstractAchievement> achievementProviderMap, List<String> achievementPrototypeIds)
        {
            achievementPrototypeIds.ForEach(it => addPrototype(achievementProviderMap.get(it)));
            this.achievementQueue = achievementPrototypeIds;
        }


        public void onResourceChange(Dictionary<String, long> changeMap)
        {
            checkAllAchievementUnlock();
        }

        public void onGameStart()
        {
            checkAllAchievementUnlock();
        }

        public void onConstructionCollectionChange()
        {
            checkAllAchievementUnlock();
        }
    }
}
