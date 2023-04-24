using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.DemoGameCore.logic
{
    public class DirtPrototype : AbstractConstructionPrototype
    {
        private static DescriptionPackage descriptionPackageEN = new DescriptionPackageBuilder()
            .build();
        private static DescriptionPackage descriptionPackageCN = new DescriptionPackageBuilder()
            .build();

        public DirtPrototype(Language language) : base(ConstructionPrototypeId.DIRT, language, null)
        {

            // override descriptionPackage
            switch (language)
            {
                case Language.CN:
                    this.descriptionPackage = DirtPrototype.descriptionPackageCN;
                    break;
                default:
                    this.descriptionPackage = DirtPrototype.descriptionPackageEN;
                    break;
            }
        }

        public override BaseConstruction getInstance(GridPosition position)
        {
            String id = prototypeId + "_" + System.Guid.NewGuid().ToString();
            BaseIdleForestConstruction construction = BaseIdleForestConstruction.typeNoProficiency(prototypeId, id, position, descriptionPackage);

            construction.allowPositionOverwrite = true;

            return construction;
        }
    }
}
