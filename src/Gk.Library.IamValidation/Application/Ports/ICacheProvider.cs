using System;

namespace GK.Library.IamValidation.Application.Ports
{
    public interface ICacheProvider
    {
        T Get<T>(string key);

        bool Set<T>(string key, T obj, TimeSpan expirationTime);
    }
}
