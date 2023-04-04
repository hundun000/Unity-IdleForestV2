using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Assets.Scripts.DemoGameCore.logic
{
    public class DesertPrototype : AbstractConstructionPrototype
    {
        private static DescriptionPackage descriptionPackageEN = new DescriptionPackageBuilder()
            .proficiency((proficiency, reachMaxProficiency) => {
                return "reclamation: " + proficiency;
            })
            .build();
        private static DescriptionPackage descriptionPackageCN = new DescriptionPackageBuilder()
            .proficiency((proficiency, reachMaxProficiency) => {
                return "土壤化进度" + proficiency;
            })
            .build();


        public DesertPrototype(Language language) : base(ConstructionPrototypeId.DESERT, language, null)
        {
            // override descriptionPackage
            switch (language)
            {
                case Language.CN:
                    this.descriptionPackage = DesertPrototype.descriptionPackageCN;
                    break;
                default:
                    this.descriptionPackage = DesertPrototype.descriptionPackageEN;
                    break;
            }
        }

        public override BaseConstruction getInstance(GridPosition position)
        {
            String id = prototypeId + "_" + System.Guid.NewGuid().ToString();
            AutoProficiencyConstruction construction = new AutoProficiencyConstruction(prototypeId, id, position, descriptionPackage);

            construction.proficiencyComponent.promoteConstructionPrototypeId = ConstructionPrototypeId.DIRT;

            return construction;
        }
    }
}
