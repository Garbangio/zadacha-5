using System;

namespace BinCucha
{
    public class MaxBinHeap
    {
        private int[] elements;
        private int count;

        public MaxBinHeap(int[] array)
        {
            if (array == null) throw new ArgumentNullException("Массив не может быть пустым");
            elements = new int[array.Length];
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
        public int GetMax()
        {

            if (count == 0) throw new Exception("Куча пуста");
            return elements[0];
        }
        public int RemoveMax()
        {
            if (count == 0) throw new Exception("Куча пуста");
            int max = elements[0];
            elements[0] = elements[count - 1];
            count--;
            HeapifyDown(0);
            return max;
        }
        public void Add(int value)
        {
            if (count == elements.Length)
            {
                int[] newElements = new int[elements.Length * 2];
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
        public void ChangeValue(int index, int newValue)
        {
            if (index < 0 || index >= count) throw new Exception("Неверный индекс");
            int oldValue = elements[index];
            elements[index] = newValue;
            if (newValue > oldValue)
            {
                HeapifyUp(index);
            }
            else
            {
                HeapifyDown(index);
            }
        }
        public MaxBinHeap Merge(MaxBinHeap other)
        {

            int[] newArray = new int[count + other.count];


            for (int i = 0; i < count; i++)
            {
                newArray[i] = elements[i];
            }


            for (int i = 0; i < other.count; i++)
            {
                newArray[count + i] = other.elements[i];
            }

            return new MaxBinHeap(newArray);
        }
        private void HeapifyUp(int index)
        {

            while (index > 0)
            {
                int parentIndex = (index - 1) / 2;
                if (elements[index] > elements[parentIndex])
                {
                    int tmp = elements[index];

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
                if (leftChild < count && elements[leftChild] > elements[largest])
                {
                    largest = leftChild;
                }
                if (rightChild < count && elements[rightChild] > elements[largest])
                {
                    largest = rightChild;
                }
                if (largest != index)
                {
                    int tmp = elements[index];
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

            MaxBinHeap heap = new MaxBinHeap(mas);
            int count = heap.GetCount();
            int max = heap.GetMax();

            Console.WriteLine($"Количество элементов: {count}");
            Console.WriteLine($"Максимальный элемент в массиве: {max}");
            heap.Print();

            Console.WriteLine("1. Add(50) и Add(99):");
            heap.Add(50);
            heap.Add(99);
            heap.Print();

            Console.WriteLine("2. Новый максимум после добавления: " + heap.GetMax());

            Console.WriteLine("\n3. RemoveMax(): " + heap.RemoveMax());
            heap.Print();

            Console.WriteLine("\n4. ChangeValue(index: 2, value: 150):");
            heap.ChangeValue(2, 150);
            heap.Print();

            Console.WriteLine("\n5. Создаём вторую кучу и делаем Merge:");
            MaxBinHeap heap2 = new MaxBinHeap(new int[] { 200, 100, 300 }); 
            Console.Write("   Вторая куча: "); heap2.Print();

            MaxBinHeap merged = heap.Merge(heap2); 
            Console.Write("   После слияния: "); merged.Print();

            Console.WriteLine($"\nФинал: {merged.GetMax()}");


            Console.ReadKey();
        }
    }
}