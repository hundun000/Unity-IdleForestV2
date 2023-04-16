using hundun.unitygame.enginecorelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace hundun.idleshare.enginecore
{
    public class StorageInfoBoardResourceAmountPairNode : MonoBehaviour
    {
        static readonly Color PLUS_COLOR = new Color(0f, 0.5f, 0f);
        static readonly Color MINUS_COLOR = new Color(0.5f, 0f, 0f);

        String resourceType;
        // ------ replace-lombok ------
        public String getResourceType()
        {
            return resourceType;
        }

        Image image;
        Text amountLabel;
        Text deltaLabel;

        void Awake()
        {
            this.image = this.transform.Find("image").GetComponent<Image>();
            this.amountLabel = this.transform.Find("amountLabel").GetComponent<Text>();
            this.deltaLabel = this.transform.Find("deltaLabel").GetComponent<Text>();
        }

        public void postPrefabInitialization(AbstractTextureManager textureManager, String resourceType)
        {

            this.resourceType = resourceType;
            Sprite textureRegion = textureManager.getResourceIcon(resourceType);
            this.image.sprite = textureRegion;
            this.amountLabel.text = "";
            this.deltaLabel.text = "";
        }

        public void update(long delta, long amout)
        {
            amountLabel.text = (
                    amout + ""
                    );
            if (delta > 0)
            {
                deltaLabel.text = "(+" + delta + ")";
                deltaLabel.color = PLUS_COLOR;
            } 
            else if(delta == 0)
            {
                deltaLabel.text = "";
            }
            else
            {
                deltaLabel.text = "(-" + Math.Abs(delta) + ")";
                deltaLabel.color = MINUS_COLOR;
            }
        }
    }
}
