using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.DemoGameCore.logic
{
    public class LakePrototype : AbstractConstructionPrototype
    {
        private static DescriptionPackage descriptionPackageEN = new DescriptionPackageBuilder()
            .proficiency((proficiency, reachMaxProficiency) => {
                return "dryness: " + proficiency;
            })
            .build();
        private static DescriptionPackage descriptionPackageCN = new DescriptionPackageBuilder()
            .proficiency((proficiency, reachMaxProficiency) => {
                return "干涸进度" + proficiency;
            })
            .build();

        public LakePrototype(Language language) : base(ConstructionPrototypeId.LAKE, language, null)
        {
            // override descriptionPackage
            switch (language)
            {
                case Language.CN:
                    this.descriptionPackage = LakePrototype.descriptionPackageCN;
                    break;
                default:
                    this.descriptionPackage = LakePrototype.descriptionPackageEN;
                    break;
            }
        }

        public override BaseConstruction getInstance(GridPosition position)
        {
            String id = prototypeId + "_" + System.Guid.NewGuid().ToString();
            BaseIdleForestConstruction construction = BaseIdleForestConstruction.typeAuto(prototypeId, id, position, descriptionPackage);

            construction.proficiencyComponent.promoteConstructionPrototypeId = ConstructionPrototypeId.DIRT;

            return construction;
        }
    }
}
