using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hundun.idleshare.gamelib
{

    

    public abstract class AbstractConstructionPrototype {

        public readonly String prototypeId;

        protected AbstractConstructionPrototype(String prototypeId)
        {
            this.prototypeId = prototypeId;
        }

        public abstract BaseConstruction getInstance(Language language);

    }


    public interface IBuiltinConstructionsLoader
    {
        public Dictionary<String, AbstractConstructionPrototype> getProviderMap();
    }
}
