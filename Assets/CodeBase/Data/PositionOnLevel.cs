using System;

namespace Assets.CodeBase.Data
{
    [Serializable]
    public class PositionOnLevel
    {
        public string _level;
        public Vector3Data _position;

        public PositionOnLevel(string initialLevel)
        {
            _level = initialLevel;
        }

        public PositionOnLevel(string level, Vector3Data position)
        {
            _level = level;
            _position = position;
        }
    }
}
