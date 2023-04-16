using hundun.unitygame.enginecorelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace hundun.unitygame.adapters
{
    


    public class DemoTextButton : TextButton
    {
        override protected void postAwake() 
        {
            this.background.sprite = Resources.Load<Sprite>("button2_rounded_CC.9");
        }

    }

    


}
