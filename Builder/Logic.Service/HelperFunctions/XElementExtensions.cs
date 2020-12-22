using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Questify.Builder.Logic.Service.HelperFunctions
{
    public static class XElementExtensions
    {
        public static List<List<T>> SplitList<T>(this List<T> locations, int nSize = 30)
        {
            var list = new List<List<T>>();
            var i = 0;
            while (i < locations.Count)
            {
                list.Add(locations.GetRange(i, Math.Min(nSize, locations.Count - i)));
                i += nSize;
            }
            return list;
        }

        public static string OuterXml(this XElement x)
        {
            var reader = x.CreateReader();
            reader.MoveToContent();
            return reader.ReadOuterXml();
        }

        public static string InnerXml(this XElement x)
        {
            var reader = x.CreateReader();
            reader.MoveToContent();
            return reader.ReadInnerXml();
        }
    }
}
