using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hundun.idleshare.gamelib
{
    

    public class ConstructionSaveData
    {
        public int level;
        public int workingLevel;
        public int proficiency;
        public GridPosition position;

        public ConstructionSaveData()
        {
        }

        public ConstructionSaveData(int level, int workingLevel, int _proficiency)
        {
            this.level = level;
            this.workingLevel = workingLevel;
            this.proficiency = _proficiency;
        }

        public static Builder builder()
        {
            return new Builder();
        }

        public class Builder
        {
            public int _level;
            public int _workingLevel;
            public int _proficiency;
            public GridPosition _position;

            public ConstructionSaveData build()
            {
                return new ConstructionSaveData(_level, _workingLevel, _proficiency);
            }

            public Builder level(int _level)
            {
                this._level = _level;
                return this;
            }

            public Builder workingLevel(int _workingLevel)
            {
                this._workingLevel = _workingLevel;
                return this;
            }

            public Builder proficiency(int _proficiency)
            {
                this._proficiency = _proficiency;
                return this;
            }

            public Builder position(GridPosition _position)
            {
                this._position = _position;
                return this;
            }
        }
    }
}
