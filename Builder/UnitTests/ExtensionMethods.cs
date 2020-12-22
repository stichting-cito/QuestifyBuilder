
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using Cito.Tester.Common;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.Model.ContentModel.EntityClasses;

namespace Questify.Builder.UnitTests
{
    public static class ExtensionMethods
    {

        public static void SetAssessmentOnItemResource(this ItemResourceEntity me, AssessmentItem a)
        {
            if (me.ResourceData == null) me.ResourceData = new ResourceDataEntity();

            using (var stream = new MemoryStream())
            {
                SerializeHelper.XmlSerializeToStream(stream, a);
                me.ResourceData.BinData = stream.ToArray();
            }
        }


        public static AssessmentItem GetAssessmentFromItemResource(this ItemResourceEntity me)
        {
            if (me.ResourceData == null) return null;
            if (me.ResourceData.BinData.Length == 0) return null;

            return (AssessmentItem)SerializeHelper.XmlDeserializeFromByteArray(me.ResourceData.BinData, typeof(AssessmentItem));
        }

        public static CombinedScoringMapKey AsCombinedScoringMap(this ScoringParameter me)
        {
            List<ScoringMapKey> keys = new List<ScoringMapKey>();
            foreach (var set in me.Value)
            {
                keys.Add(new ScoringMapKey(me, set.Id));
            }

            return CombinedScoringMapKey.Create(keys);
        }

        public static CombinedScoringMapKey AsCombinedScoringMapThatIsGroup(this ScoringParameter me, IEnumerable<int> groupIdentifiers)
        {
            List<ScoringMapKey> keys = new List<ScoringMapKey>();
            foreach (var set in me.Value)
            {
                keys.Add(new ScoringMapKey(me, set.Id));
            }

            return CombinedScoringMapKey.Create(keys, groupIdentifiers);
        }

        public static CombinedScoringMapKey AsCombinedScoringMap(this ScoringParameter me, Solution solution)
        {
            List<ScoringParameter> sp = new List<ScoringParameter>() { me };
            return new ScoringMap(sp, solution).GetMap().First();
        }

        public static CombinedScoringMapKey AsCombinedScoringMap(this ScoringParameter me, params int[] setNumbers)
        {
            List<ScoringMapKey> keys = new List<ScoringMapKey>();
            foreach (var set in me.Value)
            {
                keys.Add(new ScoringMapKey(me, set.Id));
            }
            return CombinedScoringMapKey.Create(keys, setNumbers);
        }

        public static TScorePrm AddSubParameters<TScorePrm>(this TScorePrm me, params string[] ids)
    where TScorePrm : ScoringParameter
        {
            me.Value = new ParameterSetCollection();
            foreach (var id in ids)
                me.Value.Add(new ParameterCollection() { Id = id });
            return me;
        }

        public static TScorePrm AddGapTextParameters<TScorePrm>(this TScorePrm me)
            where TScorePrm : ScoringParameter
        {
            foreach (var collection in me.Value)
            {
                collection.InnerParameters.Add(new GapTextParameter() { Value = collection.Id });
            }
            return me;
        }

        public static XElement DoSerialize<T>(this T obj)
        {
            var s = new XmlSerializer(typeof(T));
            XElement ret = null;
            using (StringWriter m = new StringWriter())
            {
                s.Serialize(m, obj);
                ret = XElement.Parse(m.ToString());
            }
            return ret;
        }

        public static T To<T>(this XElement input)
        {
            T ret = default(T);
            var s = new XmlSerializer(typeof(T));

            using (StringReader m = new StringReader(input.ToString()))
            {
                ret = (T)s.Deserialize(m);
            }

            return ret;
        }

        public static T To<T>(this byte[] input)
        {
            T ret = default(T);
            var s = new XmlSerializer(typeof(T));

            using (MemoryStream m = new MemoryStream(input))
            {
                ret = (T)s.Deserialize(m);
            }

            return ret;
        }

        public static byte[] ToBytes(this XElement input)
        {
            return Encoding.UTF8.GetBytes(input.ToString());
        }

        public static string AsString(this byte[] input)
        {
            return Encoding.UTF8.GetString(input);
        }

    }
}
