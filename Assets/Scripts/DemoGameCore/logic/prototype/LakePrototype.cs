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
            .proficiency("绿化度", DescriptionPackageFactory.EN_PROFICIENCY_IMP)
            .build();
        private static DescriptionPackage descriptionPackageCN = new DescriptionPackageBuilder()
            .proficiency("绿化度", DescriptionPackageFactory.CN_PROFICIENCY_IMP)
            .build();

        static ProficiencySpeedCalculator LAKE_PROFICIENCY_SPEED_CALCULATOR = (thiz) =>
        {
            int neighborTreeCount = thiz.neighbors.Values.ToList()
                .Where(it => it != null && it.saveData.prototypeId.Equals(ConstructionPrototypeId.SMALL_TREE))
                .Count()
                ;
            int neighborFactoryCount = thiz.neighbors.Values.ToList()
                .Where(it => it != null && it.saveData.prototypeId.Equals(ConstructionPrototypeId.SMALL_TREE))
                .Count()
                ;
            return neighborTreeCount - neighborFactoryCount;
        };

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
            AutoProficiencyConstruction construction = new AutoProficiencyConstruction(prototypeId, id, position, descriptionPackage);
            construction.proficiencySpeedCalculator = LAKE_PROFICIENCY_SPEED_CALCULATOR;

            construction.proficiencyComponent.demoteConstructionPrototypeId = ConstructionPrototypeId.DIRT;

            return construction;
        }
    }
}
