using hundun.idleshare.gamelib;
using hundun.unitygame.adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityEditor.Progress;

namespace hundun.idleshare.gamelib
{
    public class BaseConstructionFactory
    {
        IdleGameplayContext gameContext;
        Language language;
        Dictionary<String, AbstractConstructionPrototype> providerMap;
        



        

        public void lazyInit(IdleGameplayContext gameContext, Language language, Dictionary<String, AbstractConstructionPrototype> providerMap)
        {
            this.language = language;
            this.providerMap = providerMap;
            this.gameContext = gameContext;
        }

        internal AbstractConstructionPrototype getPrototype(string prototypeId)
        {
            return providerMap.get(prototypeId);
        }


        internal BaseConstruction getInstanceOfPrototype(string prototypeId)
        {
            AbstractConstructionPrototype prototype = providerMap.get(prototypeId);
            BaseConstruction construction = prototype.getInstance(language);
            construction.lazyInitDescription(gameContext, language);
            gameContext.eventManager.registerListener(construction);
            return construction;
        }


    }
}
