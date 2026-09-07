public class MyCustomCollection<T> : ICustomCollection<T>
{
   private Node<T>? head;
   private Node<T>? current;
   private int count; 

   public void Add(T item)
   {
        Node<T> newNode = new Node<T>();
        newNode.Value = item;

        if( head == null)
        {
            head = newNode;
        }
        else
        {
            Node<T> CurrentNode = head;
            while( CurrentNode.Next != null )
            {
                CurrentNode = CurrentNode.Next;
            }
            CurrentNode.Next = newNode;
        }
        count++;
   }
   
    public int Count
    {
        get { return count; }
    }

    public void Reset()
    {
        current = head;
    }

    public void MoveNext()
    {
        if ( current != null )
        {
            current = current.Next;
        }
    }

    public T Current()
    {
        if ( current == null)
        {
            throw new InvalidOperationException("Currnet element is not avaliable");
        }
        return current.Value;
    }

    public T this[int index]
    {
        get
        {
            if ( index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException();
            }
            
            Node<T> node = head!;

            for ( int i = 0; i < index; i++ )
            {
                node = node.Next!;
            }
            return node.Value;
        }
        set
        {
            if ( index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException();
            }
            
            Node<T> node = head!;

            for ( int i = 0; i < index; i++)
            {
                node = node.Next!;
            }
            node.Value = value;
        }
    }

    public void Remove(T item)
    {
        if ( head == null)
        {
            return;
        }

        if ( head.Value.Equals(item) )
        {
            head = head.Next;
            count--;
            return;
        }

        Node<T> previousNode = head;
        Node<T> currentNode = head.Next;

        while ( currentNode != null)
        {
            if ( currentNode.Value.Equals(item) )
            {
                previousNode.Next = currentNode.Next;
                count--;
                return;
            }

            previousNode = currentNode;
            currentNode = currentNode.Next;
        }
    }

    public T RemoveCurrent()
    {
        if ( current == null )
        {
            throw new InvalidOperationException("Current element is not avaliable");
        }

        T removedValue = current.Value;

        if ( current == head)
        {
            head = head.Next;
            count--;
            return removedValue;
        }

        Node<T> previousNode = head!;

        while ( previousNode.Next != current )
        {
            previousNode = previousNode.Next!;
        }

        previousNode.Next = current.Next;
        count--;
        
        return removedValue;
    }
}