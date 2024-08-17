using System;

namespace WpfSqlAny.Logic
{
    public interface IShowWithStringCallback
    {
        void ShowWithCallback(Action<string> callback);
    }
}

