using System;

namespace Questify.Builder.Logic.Service.Interfaces
{
    public interface IResourceEditorService
    {


        void Edit(Guid resourceId, string mediaType);
    }
}
