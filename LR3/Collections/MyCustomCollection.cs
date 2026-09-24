using System.Collections;

public class MyCustomCollection<T> : ICustomCollection<T>, IEnumerable<T>
{
    private Node<T>? head;
    private Node<T>? current;
    private int count;

    public int Count
    {
        get { return count; }
    }

    public void Add(T item)
    {
        Node<T> newNode = new Node<T>(item);

        if (head == null)
        {
            head = newNode;
        }
        else
        {
            Node<T> currentNode = head;
            while (currentNode.Next != null)
            {
                currentNode = currentNode.Next;
            }
            currentNode.Next = newNode;
        }

        count++;
    }

    public void Reset()
    {
        current = head;
    }

    public void MoveNext()
    {
        if (current != null)
        {
            current = current.Next;
        }
    }

    public T Current()
    {
        if (current == null)
        {
            throw new InvalidOperationException("Текущий элемент недоступен.");
        }

        return current.Value;
    }

    public T this[int index]
    {
        get
        {
            return GetNodeAt(index).Value;
        }
        set
        {
            GetNodeAt(index).Value = value;
        }
    }

    private Node<T> GetNodeAt(int index)
    {
        if (index < 0 || index >= count)
        {
            throw new IndexOutOfRangeException(
                $"Индекс {index} выходит за границы коллекции (элементов: {count}).");
        }

        Node<T> node = head!;

        for (int i = 0; i < index; i++)
        {
            node = node.Next!;
        }

        return node;
    }

    public void Remove(T item)
    {
        if (head == null)
        {
            throw new ItemNotFoundException("Коллекция пуста, удалять нечего.");
        }

        if (EqualityComparer<T>.Default.Equals(head.Value, item)) //сравнение
        {
            if (current == head)
            {
                current = head.Next;
            }

            head = head.Next;
            count--;
            return;
        }

        Node<T> previousNode = head;
        Node<T>? currentNode = head.Next;

        while (currentNode != null)
        {
            if (EqualityComparer<T>.Default.Equals(currentNode.Value, item))   //сравнение
            {
                if (current == currentNode)
                {
                    current = currentNode.Next;
                }

                previousNode.Next = currentNode.Next;
                count--;
                return;
            }

            previousNode = currentNode;
            currentNode = currentNode.Next;
        }

        throw new ItemNotFoundException("Элемент отсутствует в коллекции.");
    }

    public T RemoveCurrent()
    {
        if (current == null)
        {
            throw new InvalidOperationException("Текущий элемент недоступен.");
        }

        T removedValue = current.Value;
        Node<T>? nextNode = current.Next;

        if (current == head)
        {
            head = nextNode;
        }
        else
        {
            Node<T> previousNode = head!;

            while (previousNode.Next != current)
            {
                previousNode = previousNode.Next!;
            }

            previousNode.Next = nextNode;
        }

        current = nextNode;
        count--;

        return removedValue;
    }

    public IEnumerator<T> GetEnumerator()
    {
        Node<T>? node = head;

        while (node != null)
        {
            yield return node.Value;
            node = node.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
