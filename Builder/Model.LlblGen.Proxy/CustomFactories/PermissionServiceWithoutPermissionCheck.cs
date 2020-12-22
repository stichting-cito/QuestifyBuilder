using Questify.Builder.Security;

namespace Questify.Builder.Model.LlblGen.Proxy.CustomFactories
{
    public class PermissionServiceWithoutPermissionCheck : IPermissionService
    {
        public bool TryUserIsPermittedToNamedTask(TestBuilderPermissionAccess access, TestBuilderPermissionTarget permissionTarget,
            TestBuilderPermissionNamedTask targettedNamedTask, int bankId, int entityInstanceId)
        {
            return true;
        }
        public bool TryUserIsPermittedTo(TestBuilderPermissionAccess access, TestBuilderPermissionTarget permissionTarget, int bankId)
        {
            return true;
        }
        public void UserIsPermittedToNamedTask(TestBuilderPermissionAccess access, TestBuilderPermissionTarget permissionTarget,
            TestBuilderPermissionNamedTask targettedNamedTask, int bankId, int entityInstanceId)
        {
        }
        public bool UserIsPermittedTo(TestBuilderPermissionAccess access, TestBuilderPermissionTarget permissionTarget, int bankId)
        {
            return true;
        }
    }
}