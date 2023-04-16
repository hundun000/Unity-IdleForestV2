using Assets.Scripts.DemoGameCore.logic;
using hundun.idleshare.enginecore;
using hundun.idleshare.gamelib;
using hundun.unitygame.adapters;
using hundun.unitygame.enginecorelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Assets.Scripts.DemoGameCore.ui.sub
{
    public class DemoLanguageSwitchBoardVM : LanguageSwitchBoardVM<DemoIdleGame, RootSaveData>
    {

        public void postPrefabInitialization(BaseHundunScreen<DemoIdleGame, RootSaveData> parent,
                    Language[] values,
                    Language current,
                    String startText,
                    String hintText,
                    JConsumer<Language> onSelect
                    )
        {
            base.postPrefabInitialization(parent);

            selectBox.options = values
                .Select(it => new Dropdown.OptionData(languageShowNameMap.get(it)))
                .ToList();
            selectBox.value = selectBox.options.FindIndex(0, it => it.text.Equals(languageShowNameMap.get(current)));
            selectBox.onValueChanged.AddListener(it =>
            {
                restartHintLabel.gameObject.SetActive(true);
                Language language = languageShowNameMap.FirstOrDefault(x => x.Value == selectBox.options[it].text).Key;
                onSelect.Invoke(language);
            });

            this.label.text = startText;
            this.restartHintLabel.text = hintText;
        }
    }
}
