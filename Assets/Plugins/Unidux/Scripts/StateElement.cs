﻿using System;

namespace Unidux
{
    [Serializable]
    public class StateElement<TClass> : IState, IStateChanged
    {
        private bool _stateChanged = false;

        public bool IsStateChanged()
        {
            return _stateChanged;
        }

        public void SetStateChanged(bool changed = true)
        {
            this._stateChanged = changed;
        }
    }
}
