﻿using System;

namespace Assets.CodeBase.Data
{
    [Serializable]
    public class State
    {
        public float CurrentHP;
        public float MaxHP;

        public void ResteHP() => 
            CurrentHP = MaxHP;
    }
}
