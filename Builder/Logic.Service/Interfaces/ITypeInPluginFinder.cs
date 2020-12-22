using System;
using System.Collections.Generic;

namespace Questify.Builder.Logic.Service.Interfaces
{
    public interface ITypeInPluginFinder
    {

        IEnumerable<Type> Find(Type type, string pluginName);

    }
}
