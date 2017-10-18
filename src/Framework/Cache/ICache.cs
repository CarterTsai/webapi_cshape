using System;

namespace webapi.Framework.DAL
{
    public interface ICache
    {
        string Get();
        void Set();
    }
}