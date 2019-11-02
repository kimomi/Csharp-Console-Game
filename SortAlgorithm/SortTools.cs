using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortAlgorithm
{
    class SortTools
    {
        /// <summary>
        /// 要排序的类
        /// </summary>
        public class SortList {
            //int型数据
            public int[] a;

            //构造函数
            public SortList() { 
                a = new int[]{26,0,2,19,3,27,23,28,12,5,29,14,13,7,22,15,24,16,25,8,10,9,11,21,1,0,6,30,18,20,4,17};
            }
            public SortList(int[] arr) {
                a = arr;
            }
        }

        /// <summary>
        /// 输出矩阵目前的排序情况，方便打印
        /// </summary>
        /// <param name="sort"></param>
        public static void Print(SortList sort) {
            Console.Write('[');
            int N = sort.a.Length;
            for (int i = 0;i< N - 1;i++) {
                Console.Write("{0}, ", sort.a[i]);
            }
            Console.WriteLine("{0} ]",sort.a[N-1]);

        }

        /// <summary>
        /// 交换数组的两序号对应的数据值
        /// </summary>
        /// <param name="a">要排序的数组</param>
        /// <param name="i">第一个数据的序号</param>
        /// <param name="j">第二个数据的序号</param>
        public static void Swap(int[] a, int i, int j)
        {
            int temp = a[i];
            a[i] = a[j];
            a[j] = temp;
        }

        /// <summary>
        /// 比较左右两个，左边大于右边就交换
        /// </summary>
        /// <param name="sortList"></param>
        public static void BubbleSort(SortList sortList) {
            //数组长度
            int N = sortList.a.Length;
            for (int i = 0; i < N - 1; i++) {
                bool finishFlag = true;
                for (int j = 0; j < N - i -1; j++) { 
                    if(sortList.a[j] > sortList.a[j + 1])
                    {
                        Swap(sortList.a, j, j + 1);
                        finishFlag = false;
                    }
                }
                if (finishFlag)
                    break;
            }
        }

        /// <summary>
        /// 快速排序，冒泡排序的升级版
        /// 分区，左边所有值小于中间值，右边所有值大于中间值
        /// </summary>
        /// <param name="sortList"></param>
        public static void QuickSort(SortList sortList) {
            QuickSort(sortList.a, 0, sortList.a.Length - 1);
        }

        /// <summary>
        /// 遍历数组，选择最小的放到最左边
        /// </summary>
        /// <param name="sortList"></param>
        public static void SelectSort(SortList sortList) {
            //数组长度
            int N = sortList.a.Length;
            for (int i = 0; i < N; i++) {
                int minindex = i;
                for (int j = i; j < N - 1 ; j++) {
                    if (sortList.a[j + 1] < sortList.a[minindex])
                    {
                        minindex = j + 1;
                    }
                }
                Swap(sortList.a, i, minindex);
            }
        }

        /// <summary>
        /// 堆排序（选择排序的升级版）
        /// </summary>
        /// <param name="sortList"></param>
        public static void HeapSort(SortList sortList) {
            int N = sortList.a.Length;
            //构造大顶堆
            for (int i = (N - (N % 2)) / 2 - 1; i >= 0; i--)
            {
                HeapAdjust(sortList.a, i, N-1);
            }
            //把堆顶元素移到最后一个，再堆排序
            int lastindex = N - 1;
            for(int i = 0; i < N; i++){
                Swap(sortList.a, 0, lastindex--);
                for (int j = (lastindex + (lastindex % 2)) / 2 - 1; j >= 0; j--)
                {
                    HeapAdjust(sortList.a, j, lastindex);
                }
            }
        }

        /// <summary>
        /// 从左到右一个一个排，选择当前，对比前面的已排序数
        /// 前面的大的话，前面的移到后面，再对比前面一个
        /// 前面小的话，就将当前值插入
        /// </summary>
        /// <param name="sortList"></param>
        public static void InsertionSort(SortList sortList) {
            //数组长度
            int N = sortList.a.Length;
            for (int i = 1; i < N; i++) {
                int current = sortList.a[i];
                for (int j = i; j != 0; j--) {
                    if (sortList.a[j - 1] > current) {
                        sortList.a[j] = sortList.a[j - 1];
                        sortList.a[j - 1] = current;
                    }
                    else{
                        sortList.a[j] = current;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 希尔排序，每次增量不同，相当于先按照增量分组宏观地调整一下大小
        /// 增量递减，直至为1
        /// 
        /// </summary>
        /// <param name="sortList"></param>
        public static void ShellSort(SortList sortList) {
            //数组长度
            int N = sortList.a.Length;
            //每次递增为increment
            for(int increment = N / 2; increment > 0; increment /= 2){
                //对每一组插入排序，共increment组
                for (int i = 0; i < increment; i ++ )
                {
                    //针对每一组，每个元素与上个元素相差索引为increment
                    for (int j = i + increment; j < N; j += increment) {
                        int current = sortList.a[j];
                        for (int k = j; k >= increment; k -= increment) {
                            if (sortList.a[k - increment] > current)
                            {
                                sortList.a[k] = sortList.a[k - increment];
                                sortList.a[k - increment] = current;
                            }
                            else {
                                sortList.a[k] = current;
                                break;
                            }
                        }
                    }
                }
            }
        }




        /****************************** private 函数 **********************************/

        //调整堆的index节点，把索引0处变为最大的
        private static void HeapAdjust(int[] a,int index,int lastIndex) {
            int left = 2 * index + 1;
            int right = 2 * index + 2;
            int max = index;
            if (a[left] > a[max]) max = left;
            if (right <= lastIndex && a[right] > a[max]) max = right;
            if (max != index) Swap(a, index, max);            
        }

        //进行快排操作
        private static void QuickSort(int[] a, int left, int right) {
            int minddle = Partition(a, left, right);
            if (left < right && minddle > 1)
            {
                QuickSort(a, left, minddle - 1);
                QuickSort(a, minddle + 1, right);
            }
        }

        //分区操作,左边都小于a[middle],右边都大于a[middle]
        private static int Partition(int[] a, int left, int right) {
            int middle = left;
            while (left < right) {
                //等号约束，使存在相等的元素时能够比较好地处理
                while(left < right && a[right] >= a[middle])
                {
                    right--;
                }
                Swap(a, middle, right);
                middle = right;
                while (a[left] < a[middle])
                {
                    left++;
                }
                Swap(a, middle, left);
                middle = left;
            }
            return middle;
        }
    }
}
