using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;
using LINQtoCSV;

namespace LinqPractice001
{
    [Serializable]
    public class Product
    {
        [CsvColumn(Name="商品編號", FieldIndex=1)]
        public string Id { get; set; }
        [CsvColumn(Name = "商品名稱", FieldIndex = 2)]
        public string Name { get; set; }
        [CsvColumn(Name = "商品數量", FieldIndex = 3)]
        public int Quantity { get; set; }
        [CsvColumn(Name = "價格", FieldIndex = 4)]
        public int Price { get; set; }
        [CsvColumn(Name = "商品類別", FieldIndex = 5)]
        public string Category { get; set; }


    }
}
