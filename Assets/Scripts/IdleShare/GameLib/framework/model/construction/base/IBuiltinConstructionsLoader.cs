using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hundun.idleshare.gamelib
{

    

    public abstract class AbstractConstructionPrototype {

        public readonly String prototypeId;
        public readonly Language language;

        protected AbstractConstructionPrototype(String prototypeId, Language language)
        {
            this.prototypeId = prototypeId;
            this.language = language;
        }

        public abstract BaseConstruction getInstance(GridPosition position);

    }


    public interface IBuiltinConstructionsLoader
    {
        public Dictionary<String, AbstractConstructionPrototype> getProviderMap(Language language);
    }
}
