using Questify.Builder.Logic.Service.Cache.CacheEntities.BaseClass;
using Questify.Builder.Security;

namespace Questify.Builder.Logic.Service.Cache.CacheEntities
{
    public class PermissionCache : CachePerUserBase
    {

        public PermissionCache(int cacheExpiryInSeconds, bool sliding)
    : base("PermitionCache", cacheExpiryInSeconds, sliding) { }

        public bool? Get(TestBuilderPermissionAccess access, TestBuilderPermissionTarget permissionTarget, TestBuilderPermissionNamedTask targettedNamedTask, int bankId, int entityInstanceId)
        {
            var key = string.Format("{0}-{1}-{2}-{3}-{4}", access, permissionTarget, targettedNamedTask, bankId, entityInstanceId);
            return (bool?)GetPerUser(key);
        }

        public bool? Get(TestBuilderPermissionAccess access, TestBuilderPermissionTarget permissionTarget, int bankId)
        {
            var key = string.Format("{0}-{1}-{2}", access, permissionTarget, bankId);
            return (bool?)GetPerUser(key);
        }

        public void Put(TestBuilderPermissionAccess access, TestBuilderPermissionTarget permissionTarget, int bankId, bool isPermitted)
        {
            var key = string.Format("{0}-{1}-{2}", access, permissionTarget, bankId);
            PutPerUser(key, isPermitted);
        }

        public void Put(TestBuilderPermissionAccess access, TestBuilderPermissionTarget permissionTarget, TestBuilderPermissionNamedTask targettedNamedTask, int bankId, int entityInstanceId, bool isPermitted)
        {
            var key = string.Format("{0}-{1}-{2}-{3}-{4}", access, permissionTarget, targettedNamedTask, bankId, entityInstanceId);
            PutPerUser(key, isPermitted);
        }

        public bool IsCached(TestBuilderPermissionAccess access, TestBuilderPermissionTarget permissionTarget, TestBuilderPermissionNamedTask targettedNamedTask, int bankId, int entityInstanceId)
        {
            var key = string.Format("{0}-{1}-{2}-{3}-{4}", access, permissionTarget, targettedNamedTask, bankId, entityInstanceId);
            return IsCachedPerUser(key);
        }

        public bool IsCached(TestBuilderPermissionAccess access, TestBuilderPermissionTarget permissionTarget, int bankId)
        {
            string key = string.Format("{0}-{1}-{2}", access, permissionTarget, bankId);
            return IsCachedPerUser(key);
        }

        public void RemovePermission(int bankId)
        {
            foreach (var tbPermissionAccessEnumName in System.Enum.GetNames(typeof(TestBuilderPermissionAccess)))
            {
                {
                    var key = string.Format("{0}-{1}-{2}", tbPermissionAccessEnumName, TestBuilderPermissionTarget.BankEntity, bankId);
                    RemovePerUser(key);
                }
            }
        }
    }
}
