using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            //int[] arr = { 0, 3, 5, 7, 9, 1, 2, 10, 4, 8, 6, 13, 12, 11, 19, 18, 15, 14, 16, 17 };
            SortTools.SortList sortList = new SortTools.SortList();
            Console.Write("排序前的数组：");
            SortTools.Print(sortList);
            Console.WriteLine("--------------------------------");

            Console.WriteLine("冒泡排序：");
            DateTime start = DateTime.Now;
            SortTools.BubbleSort(sortList);
            SortTools.Print(sortList);
            Console.WriteLine("用时为：{0}ms",DateTime.Now.Millisecond - start.Millisecond);
            Console.WriteLine("--------------------------------");

            sortList = new SortTools.SortList();
            Console.WriteLine("选择排序：");
            start = DateTime.Now;
            SortTools.SelectSort(sortList);
            SortTools.Print(sortList);
            Console.WriteLine("用时为：{0}ms", DateTime.Now.Millisecond - start.Millisecond);
            Console.WriteLine("--------------------------------");

            sortList = new SortTools.SortList();
            Console.WriteLine("插入排序：");
            start = DateTime.Now;
            SortTools.InsertionSort(sortList);
            SortTools.Print(sortList);
            Console.WriteLine("用时为：{0}ms", DateTime.Now.Millisecond - start.Millisecond);
            Console.WriteLine("--------------------------------");

            sortList = new SortTools.SortList();
            Console.WriteLine("希尔排序：");
            start = DateTime.Now;
            SortTools.ShellSort(sortList);
            SortTools.Print(sortList);
            Console.WriteLine("用时为：{0}ms", DateTime.Now.Millisecond - start.Millisecond);
            Console.WriteLine("--------------------------------");

            sortList = new SortTools.SortList();
            Console.WriteLine("堆排序：");
            start = DateTime.Now;
            SortTools.HeapSort(sortList);
            SortTools.Print(sortList);
            Console.WriteLine("用时为：{0}ms", DateTime.Now.Millisecond - start.Millisecond);
            Console.WriteLine("--------------------------------");

            sortList = new SortTools.SortList();
            Console.WriteLine("快速排序：");
            start = DateTime.Now;
            SortTools.QuickSort(sortList);
            SortTools.Print(sortList);
            Console.WriteLine("用时为：{0}ms", DateTime.Now.Millisecond - start.Millisecond);
            Console.WriteLine("--------------------------------");

            Console.ReadLine();
        }
    }
}
