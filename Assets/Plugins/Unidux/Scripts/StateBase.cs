﻿using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Unidux
{
    [Serializable]
    public class StateBase<TValue> : IState, IStateClone<TValue>
    {
        public virtual TValue Clone()
        {
            TValue result;
            BinaryFormatter formatter = new BinaryFormatter();

            using (MemoryStream stream = new MemoryStream())
            {
                try
                {
                    formatter.Serialize(stream, this);
                    stream.Position = 0;
                    result = (TValue) formatter.Deserialize(stream);
                }
                finally
                {
                    stream.Close();
                }
            }

            return result;
        }
    }
}