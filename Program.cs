using System;

namespace BinCucha
{
    public class MaxBinHeap<T> where T : IComparable<T>
    {
        private T[] elements;
        private int count;

        public MaxBinHeap(T[] array)
        {
            if (array == null) throw new ArgumentNullException("Массив не может быть пустым");
            elements = new T[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                elements[i] = array[i];
            }
            count = array.Length;
            for (int i = count / 2 - 1; i >= 0; i--)
            {
                HeapifyDown(i);
            }
        }
        public T GetMax()
        {
            if (count == 0) throw new Exception("Куча пуста");
            return elements[0];
        }
        public T RemoveMax()
        {
            if (count == 0) throw new Exception("Куча пуста");
            T max = elements[0];
            elements[0] = elements[count - 1];
            count--;
            HeapifyDown(0);
            return max;
        }
        public void Add(T value)
        {
            if (count == elements.Length)
            {
                T[] newElements = new T[elements.Length * 2];
                for (int i = 0; i < elements.Length; i++)
                {
                    newElements[i] = elements[i];
                }
                elements = newElements;
            }
            elements[count] = value;
            count++;
            HeapifyUp(count - 1);
        }
        public void ChangeValue(int index, T newValue)
        {
            if (index < 0 || index >= count) throw new Exception("Неверный индекс");
            T oldValue = elements[index];
            elements[index] = newValue;
            if (newValue.CompareTo(oldValue) > 0)
            {
                HeapifyUp(index);
            }
            else
            {
                HeapifyDown(index);
            }
        }
        public MaxBinHeap<T> Merge(MaxBinHeap<T> other)
        {
            T[] newArray = new T[count + other.count];
            for (int i = 0; i < count; i++)
            {
                newArray[i] = elements[i];
            }
            for (int i = 0; i < other.count; i++)
            {
                newArray[count + i] = other.elements[i];
            }
            return new MaxBinHeap<T>(newArray);
        }
        private void HeapifyUp(int index)
        {
            while (index > 0)
            {
                int parentIndex = (index - 1) / 2;
                if (elements[index].CompareTo(elements[parentIndex]) > 0)
                {
                    T tmp = elements[index];
                    elements[index] = elements[parentIndex];
                    elements[parentIndex] = tmp;
                    index = parentIndex;
                }
                else { break; }
            }
        }
        protected void HeapifyDown(int index)
        {
            while (true)
            {
                int leftChild = 2 * index + 1;
                int rightChild = 2 * index + 2;
                int largest = index;
                if (leftChild < count && elements[leftChild].CompareTo(elements[largest]) > 0)
                {
                    largest = leftChild;
                }
                if (rightChild < count && elements[rightChild].CompareTo(elements[largest]) > 0)
                {
                    largest = rightChild;
                }
                if (largest != index)
                {
                    T tmp = elements[index];
                    elements[index] = elements[largest];
                    elements[largest] = tmp;
                    index = largest;
                }
                else { break; }
            }
        }
        public void Print()
        {
            Console.Write("Куча: ");
            for (int i = 0; i < count; i++)
            {
                Console.Write(elements[i] + " ");
            }
            Console.WriteLine();
        }
        public int GetCount()
        {
            return count;
        }
    }

    class Program
    {
        public static void Main()
        {
            int[] mas = new int[7];
            for (int i = 0; i < 7; i++)
            {
                int element = int.Parse(Console.ReadLine());
                mas[i] = element;
            }

            MaxBinHeap<int> heap = new MaxBinHeap<int>(mas);
            int count = heap.GetCount();
            int max = heap.GetMax();

            Console.WriteLine($"Количество элементов: {count}");
            Console.WriteLine($"Максимальный элемент в массиве: {max}");
            heap.Print();

            Console.WriteLine("1. +50 и +99:");
            heap.Add(50);
            heap.Add(99);
            heap.Print();

            Console.WriteLine("2. Новый максимум : " + heap.GetMax());

            Console.WriteLine("\n3. RemoveMax(): " + heap.RemoveMax());
            heap.Print();

            Console.WriteLine("\n4. ChangeValue:");
            heap.ChangeValue(2, 150);
            heap.Print();

            Console.WriteLine("\n5. Создаём вторую кучу и делаем Merge:");
            MaxBinHeap<int> heap2 = new MaxBinHeap<int>(new int[] { 200, 100, 300 });
            Console.Write("Вторая куча: "); heap2.Print();

            MaxBinHeap<int> merged = heap.Merge(heap2);
            Console.Write("После слияния: "); merged.Print();

            Console.WriteLine($"\n максимум в новой куче: {merged.GetMax()}");

            Console.ReadKey();
        }
    }
}
