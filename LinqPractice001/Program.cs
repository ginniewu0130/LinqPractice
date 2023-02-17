using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LINQtoCSV;
using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace LinqPractice001
{
    internal class Program
    {
        static void Main(string[] args)
        {

            ReadCsvFile();
            Console.ReadLine();

        }
        private static void ReadCsvFile()
        {
            var csvFileDescription = new CsvFileDescription
            {
                FirstLineHasColumnNames = true,
                IgnoreUnknownColumns = true,
                SeparatorChar = ',',
                UseFieldIndexForReadingData = false,//
            };

            var csvContext = new LINQtoCSV.CsvContext();
            //var products = csvContext.Read<Product>("product.csv",csvFileDescription);
            IEnumerable<Product> productsList = csvContext.Read<Product>("product.csv", csvFileDescription);

            //1.計算所有商品的總價格
            var totalPrice = (from x in productsList select x.Price).Sum();
            Console.WriteLine($"所有商品的總價格為{totalPrice}元");
            //2.計算所有商品的平均價格
            Console.WriteLine("---------------------");
            var averagePrice = productsList.Average((x) => x.Price);
            Console.WriteLine($"所有商品的平均價格為{averagePrice}元");
            //3.計算商品的總數量
            Console.WriteLine("---------------------");
            var totalQuantity = productsList.Sum((x) => x.Quantity);
            Console.WriteLine($"商品的總數量為{totalQuantity}個");
            //4.計算商品的平均數量
            Console.WriteLine("---------------------");
            var averageQuantity = productsList.Average((x) => x.Quantity);
            Console.WriteLine($"商品的平均數量為{averageQuantity}個");
            //5.找出哪一項商品最貴
            Console.WriteLine("---------------------");
            var maxPrice = productsList.Max((x) => x.Price);
            var maxName = productsList.First((x) => x.Price == maxPrice);
            Console.WriteLine($"最貴的商品為{maxName.Name}，要{maxPrice}元");
            //6.找出哪一項商品最便宜
            Console.WriteLine("---------------------");
            var minPrice = productsList.Min((x) => x.Price);
            var minName = productsList.First((x) => x.Price == minPrice);
            Console.WriteLine($"最便宜的商品為{minName.Name}，要{minPrice}元");
            //7.計算產品類別為3C的商品總價
            Console.WriteLine("---------------------");
            var priceOf3c = productsList.Where(x => x.Category == "3C").Sum(x => x.Price);
            Console.WriteLine($"商品類別為3C的總價格為{priceOf3c}元");
            //8.計算產品類別為飲料及食品的商品價格
            Console.WriteLine("---------------------");
            var priceOfdrinkandfood = productsList.Where((x) => x.Category == "飲料" && x.Category == "食品").Sum((x) => x.Price);
            Console.WriteLine($"產品類別為飲料及食品總共{priceOfdrinkandfood}元");
            //9.找出所有商品類別為食品，而且商品數量大於100的商品
            Console.WriteLine("---------商品類別為食品，而且商品數量大於100------------");
            var food = productsList.Where(x => x.Category == "食品");
            var foodQG100 = food.Where((x) => x.Quantity > 100);
            foreach (var product in foodQG100)
            {
                Console.WriteLine($"商品名稱:{product.Name}商品數量:{product.Quantity}");
            }
            //10.找出各個商品類別底下有哪些商品的價格是大於1000的商品
            Console.WriteLine("---------各個商品類別底下有哪些商品的價格是大於1000的商品------------");
            var result = productsList.Where(p => p.Price > 1000).GroupBy(p => p.Category);
            foreach (var group in result)
            {
                Console.WriteLine($"產品類別為{group.Key}，共有 {group.Count()} 項產品");
                var productsInCategory = group.Select(p => p.Name);
                Console.WriteLine($"產品清單：{string.Join(", ", productsInCategory)}");
            }
            //11.呈上題，請計算該類別底下所有商品的平均價格
            Console.WriteLine("---呈上題，計算平均價格---");
            var categoryAvgPrice = productsList.GroupBy(x => x.Category);
            foreach (var item in result)
            {
                var productsAvgPrice = item.Select(x => x.Price).Average();
                Console.WriteLine($"平均價格為{productsAvgPrice} 元");
            }
            //12.依照商品價格由高到低排序
            Console.WriteLine("-----------商品價格由高到低排序-------------");
            var sortedList = productsList.OrderByDescending(x => x.Price);
            foreach (var product in sortedList)
            {
                Console.WriteLine($"商品編號:{product.Id}，商品名稱：{product.Name}，價格：{product.Price}");
            }
            //13.依照商品數量由低到高排序
            Console.WriteLine("-----------商品數量由低到高排序-------------");
            var sortedListQ = productsList.OrderBy(x => x.Quantity);
            foreach (var product in sortedListQ)
            {
                Console.WriteLine($"商品編號:{product.Id}，商品名稱：{product.Name}，數量：{product.Quantity}");
            }
            //14.找出各商品類別底下，最貴的商品
            Console.WriteLine("--------找出各商品類別底下，最貴的商品------------");
            var resultInCategory = productsList.GroupBy(x => x.Category)
                .Select(y => new{
                Category = y.Key,
                MostExpensiveProduct = y.OrderByDescending(x => x.Price).FirstOrDefault()});
            foreach (var item in resultInCategory)
            {
                Console.WriteLine($"產品類別：{item.Category}，最貴的商品是{item.MostExpensiveProduct.Name}，價格為{item.MostExpensiveProduct.Price}元");
            }
            //15.找出各商品類別底下，最便宜的商品
            Console.WriteLine("--------找出各商品類別底下，最便宜的商品------------");
            var cheapestProductsByCategory = productsList.GroupBy(x => x.Category)
                .Select(y => y.OrderBy(x => x.Price).First());
            foreach (var product in cheapestProductsByCategory)
            {
                Console.WriteLine($"產品類別：{product.Category}，最便宜的商品是{product.Name}，價格為{product.Price}元");
            }
            //16.找出價格小於等於10000的商品
            Console.WriteLine("--------找出價格小於等於10000的商品------------");
            var priceLess10000 = productsList.Where(x => x.Price <= 10000);
            foreach (var product in priceLess10000)
            {
                Console.WriteLine($"{product.Name}的價格小於10000元");
            }
            //17.製作一頁4筆總共5頁的分頁選擇器 //?????
            int items = 4;
            int pages = 4;//查詢分頁，要手動更改
            // 計算總共有幾頁            
            int totalItems = productsList.Count();//計算有幾項
            int totalPages = (int)Math.Ceiling((double)totalItems / items);

            // 顯示分頁選擇器
            for (int i = 1; i <= totalPages; i++)
            {
                Console.Write($"第{i}頁 ");
            }
            Console.WriteLine();

            // 取得目前頁數的商品資料
            var currentPageProducts = productsList.Skip((pages - 1) * items).Take(items);

            // 顯示目前頁數的商品資料
            foreach (var product in currentPageProducts)
            {
                Console.WriteLine(product.Name);
            }

        }
    }
}