
using System;
using System.Security.Principal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Security;

namespace Questify.Builder.UnitTests.Questify.Builder.Service.Cache
{
	[TestClass]
	public class ResourceEntityCacheTest : EntityCacheTestBase<ResourceEntity>
	{
		[TestMethod, TestCategory("Cache")]
		[ExpectedException(typeof(ArgumentException))]
		public void PutAndGetEntity_GenericPrincipal()
		{
			System.Threading.Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity("SomeName"), new string[] { });
            base.PutAndGetEntity_SlidedTest();
		}

		[TestMethod, TestCategory("Cache")]
		[ExpectedException(typeof(ArgumentException))]
		public void PutAndRemoveAndGetEntity_GenericPrincipal()
		{
			System.Threading.Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity("SomeName"), new string[] { });
            base.PutAndRemoveAndGetEntity_SlidedTest();
		}

        [TestMethod, TestCategory("Cache")]
        public new void PutAndGetEntity_SlidedTest()
        {
            System.Threading.Thread.CurrentPrincipal = new TestBuilderPrincipal(new TestBuilderIdentity(1, "administrator", "default"));
            base.PutAndGetEntity_SlidedTest();
        }

		[TestMethod, TestCategory("Cache")]
        public new void PutAndGetEntity_NonSlidedTest()
		{
			System.Threading.Thread.CurrentPrincipal = new TestBuilderPrincipal(new TestBuilderIdentity(1, "administrator", "default"));
			base.PutAndGetEntity_NonSlidedTest();
		}

		[TestMethod, TestCategory("Cache")]
        public new void PutAndRemoveAndGetEntity_SlidedTest()
		{
			System.Threading.Thread.CurrentPrincipal = new TestBuilderPrincipal(new TestBuilderIdentity(1, "administrator", "default"));
			base.PutAndRemoveAndGetEntity_SlidedTest();
		}

		[TestMethod, TestCategory("Cache")]
        public new void PutAndRemoveAndGetEntity_NonSlidedTest()
		{
			System.Threading.Thread.CurrentPrincipal = new TestBuilderPrincipal(new TestBuilderIdentity(1, "administrator", "default"));
			base.PutAndRemoveAndGetEntity_NonSlidedTest();
		}

		[TestMethod, TestCategory("Cache")]
        public new void CacheExpired_SlidedTest()
		{
			System.Threading.Thread.CurrentPrincipal = new TestBuilderPrincipal(new TestBuilderIdentity(1, "administrator", "default"));
			base.CacheExpired_SlidedTest();
		}

		[TestMethod, TestCategory("Cache")]
        public new void CacheExpired_NonSlidedTest()
		{
			System.Threading.Thread.CurrentPrincipal = new TestBuilderPrincipal(new TestBuilderIdentity(1, "administrator", "default"));
			base.CacheExpired_NonSlidedTest();
		}

		[TestMethod, TestCategory("Cache")]
        public new void CacheNotExpired_SlidedTest()
		{
			System.Threading.Thread.CurrentPrincipal = new TestBuilderPrincipal(new TestBuilderIdentity(1, "administrator", "default"));
			base.CacheNotExpired_SlidedTest();
		}

		[TestMethod, TestCategory("Cache")]
        public new void CacheNotExpired_AccessedMultipleTimesWithinSlidingWindow_SliderTest()
		{
			System.Threading.Thread.CurrentPrincipal = new TestBuilderPrincipal(new TestBuilderIdentity(1, "administrator", "default"));
			base.CacheNotExpired_AccessedMultipleTimesWithinSlidingWindow_SliderTest();
		}

		[TestMethod, TestCategory("Cache")]
        public new void CacheNotExpired_AccessedMultipleTimesWithinSlidingWindow_NonSliderTest()
		{
			System.Threading.Thread.CurrentPrincipal = new TestBuilderPrincipal(new TestBuilderIdentity(1, "administrator", "default"));
			base.CacheNotExpired_AccessedMultipleTimesWithinSlidingWindow_NonSliderTest();
		}

		[TestMethod, TestCategory("Cache")]
		public new void CacheNotExpired_NonSlidedTest()
		{
			System.Threading.Thread.CurrentPrincipal = new TestBuilderPrincipal(new TestBuilderIdentity(1, "administrator", "default"));
			base.CacheNotExpired_NonSlidedTest();
		}
	}
}
