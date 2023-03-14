using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Unity.VisualScripting.Icons;

namespace hundun.idleshare.gamelib
{
    public interface IGameDictionary
    {
        String constructionPrototypeIdToShowName(Language language, String prototypeId);
        String constructionPrototypeIdToDetailDescroptionConstPart(Language language, String prototypeId);
        List<String> getMemuScreenTexts(Language language);

        Dictionary<Language, String> getLanguageShowNameMap();
    }
}
