using System.Collections.Concurrent;
using System.Collections.Generic;
using Cito.Tester.ContentModel;
using Questify.Builder.Model.ContentModel.EntityClasses;

namespace Questify.Builder.Logic.Service.Interfaces
{
    public interface IHarmonizer
    {
        bool Harmonize(ItemResourceEntity item);
        bool Harmonize(IEnumerable<string> templates, ItemResourceEntity item);
        bool Harmonize(ConcurrentDictionary<string, ParameterSetCollection> parametersetCollections, ItemResourceEntity item);
        bool Harmonize(ItemResourceEntity item, string template);
    }
}
