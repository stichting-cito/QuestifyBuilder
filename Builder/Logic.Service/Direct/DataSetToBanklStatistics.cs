using System;
using System.Data;
using CustomClasses;

namespace Questify.Builder.Logic.Service.Direct
{
    internal class DataSetToBanklStatistics
    {

        public BankStatistics Convert(DataSet ds)
        {
            var ret = new BankStatistics
            {
                TotalNumberOfItems = GetValue(ds, resultSetDatatableName.totalNumberOfItems, 0),
                NumberOfItemsCreatedByMe = GetValue(ds, resultSetDatatableName.numberOfItemsCreatedByMe, 0),
                NumberOfUnusedItems = GetValue(ds, resultSetDatatableName.numberOfUnusedItems, 0),
                TotalNumberOfTest = GetValue(ds, resultSetDatatableName.totalNumberOfTest, 0),
                NumberOfTestCreatedByMe = GetValue(ds, resultSetDatatableName.numberOfTestCreatedByMe, 0),
                TotalNumberOfMedia = GetValue(ds, resultSetDatatableName.totalNumberOfMedia, 0),
                NumberOfUnusedMedia = GetValue(ds, resultSetDatatableName.numberOfUnusedMedia, 0),
                TotalNumberOfItemTemplates = GetValue(ds, resultSetDatatableName.totalNumberOfItemTemplates, 0),
                NumberOfUnusedItemsTemplates = GetValue(ds, resultSetDatatableName.numberOfUnusedItemsTemplates, 0),
                NumberOfItemTemplatesCreatedByMe = GetValue(ds, resultSetDatatableName.numberOfItemTemplatesCreatedByMe,
                    0),
                TotalNumberOfTestTemplates = GetValue(ds, resultSetDatatableName.totalNumberOfTestTemplates, 0),
                NumberOfTestTemplatesCreatedByMe = GetValue(ds, resultSetDatatableName.numberOfTestTemplatesCreatedByMe,
                    0),
                TotalNumberOfControlTemplates = GetValue(ds, resultSetDatatableName.totalNumberOfControlTemplates, 0),
                NumberOfUnusedControlTemplates = GetValue(ds, resultSetDatatableName.numberOfUnusedControlTemplates, 0),
                NumberOfControlTemplatesCreatedByMe = GetValue(ds, resultSetDatatableName.numberOfControlTemplates, 0)
            };

            ret.LastModifiedItems = GetLastModifiedItems(ds);
            ret.ItemStatus = GetItemStatus(ds);

            return ret;
        }


        private T GetValue<T>(DataSet ds, resultSetDatatableName resultSetDatatableName, T valueWhenEmpty)
        {
            var ret = valueWhenEmpty;
            var dt = ds.Tables[(int)resultSetDatatableName];
            if (dt != null && dt.Rows.Count != 0) ret = (T)dt.Rows[0][0];
            return ret;
        }

        private ModifiedItemsList GetLastModifiedItems(DataSet ds)
        {
            var ret = new ModifiedItemsList();
            var dt = ds.Tables[(int)resultSetDatatableName.topModifiedItems];
            if (dt == null)
            {
                return ret;
            }

            foreach (DataRow r in dt.Rows)
                ret.Add(new ModifiedItems
                {
                    resourceId = GetValue<Guid>(r, 0),
                    Name = GetValue<string>(r, 1),
                    fullName = GetValue<string>(r, 4),
                    ModifiedDate = GetValue<DateTime>(r, 3)
                });
            return ret;
        }

        private ItemStatusList GetItemStatus(DataSet ds)
        {
            var ret = new ItemStatusList();
            var dt = ds.Tables[(int)resultSetDatatableName.itemStateInformation];
            if (dt == null)
            {
                return ret;
            }

            foreach (DataRow r in dt.Rows)
                ret.Add(new ItemStatus
                {
                    numberOfItem = GetValue<int>(r, 0),
                    name = GetValue<string>(r, 1)
                });

            return ret;
        }

        private T GetValue<T>(DataRow r, int index)
        {
            return (T)r[index];
        }


        private enum resultSetDatatableName
        {
            totalNumberOfItems = 0,
            numberOfItemsCreatedByMe = 1,
            numberOfUnusedItems = 2,
            topModifiedItems = 3,
            totalNumberOfTest = 4,
            numberOfTestCreatedByMe = 5,
            totalNumberOfMedia = 6,
            numberOfUnusedMedia = 7,
            totalNumberOfItemTemplates = 8,
            numberOfUnusedItemsTemplates = 9,
            numberOfItemTemplatesCreatedByMe = 10,
            totalNumberOfTestTemplates = 11,
            numberOfTestTemplatesCreatedByMe = 12,
            totalNumberOfControlTemplates = 13,
            numberOfUnusedControlTemplates = 14,
            numberOfControlTemplates = 15,
            itemStateInformation = 16
        }



    }
}