using System;
using System.Collections.Generic;
using UnityEngine.Rendering;

namespace PathFinder.PriorityQueue
{
    public class PriorityQueue<TElement, TPriority> where TPriority : IComparable<TPriority>
    {
        // ヒープの実体
        private readonly List<(TElement Element, TPriority Priority)> heap;

        /* プロパティ */
        public int Count => heap.Count;

        public PriorityQueue()
        {
            heap = new List<(TElement, TPriority)>();
        }

        // @brief キューに追加
        // @param element 追加する要素, priority 要素の優先度
        public void Enqueue(TElement element, TPriority priority)
        {
            heap.Add((element, priority));
            SiftUp(Count - 1);
        }

        // @brief キューから要素を取り出す
        // @return 取り出した要素
        public TElement Dequeue()
        {
            if (Count == 0)
                throw new InvalidOperationException("Queue is empty.");

            var result = heap[0].Element;

            heap[0] = heap[Count - 1];
            heap.RemoveAt(Count - 1);

            if (Count > 0)
                SiftDown(0);

            return result;
        }
        // @brief キューの要素を上にシフト
        // @param element シフトする要素のインデックス
        private void SiftUp(int index)
        {
            if (index == 0)
                return;

            var parentIndex = (index - 1) / 2;

            if (heap[index].Priority.CompareTo(heap[parentIndex].Priority) < 0)
            {
                Swap(index, parentIndex);
                SiftUp(parentIndex);
            }
        }

        // @brief キューの要素を下にシフト
        // @param index シフトする要素のインデックス
        private void SiftDown(int index)
        {
            var leftChildIndex = 2 * index + 1;
            var rightChildIndex = 2 * index + 2;
            var smallestChildIndex = index;

            if(leftChildIndex < Count && heap[leftChildIndex].Priority.CompareTo(heap[smallestChildIndex].Priority) < 0)
                smallestChildIndex = leftChildIndex;

            if(rightChildIndex < Count && heap[rightChildIndex].Priority.CompareTo(heap[smallestChildIndex].Priority) < 0)
                smallestChildIndex = rightChildIndex;

            if (smallestChildIndex != index)
            {
                Swap(index, smallestChildIndex);
                SiftDown(smallestChildIndex);
            }
        }

        // @brief ヒープの要素を入れ替える
        private void Swap(int a, int b)
        {
            (heap[a], heap[b]) = (heap[b], heap[a]);
        }
    }
}