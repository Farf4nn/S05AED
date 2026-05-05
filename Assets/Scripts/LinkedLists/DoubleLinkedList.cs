using System;
using UnityEngine;

public class DoubleLinkedList<T> //: MonoBehaviour
{
    public Node<T> Pivot;
    public Node<T> head = null;
    public Node<T> tail = null;
    public int Count;

    //->O(1)
    public virtual void Add(T value)
    {
        Node<T> newNode = new(value);

        if (head == null)
        {
            head = newNode;
            tail = newNode;
            Pivot = newNode;
        }
        else
        {
            tail.SetNext(newNode);
            newNode.SetPrev(tail);
            tail = newNode;

            Pivot = newNode;
        }

        Count++;
    }


    //->O(1)
    public void RemoveLast()
    {

        //Node<T> Evaluator = head;

        if (Count == 0)
        {
            Debug.Log("La lista esta vacia");
            return;
        }
        else if (Count == 1)
        {
            head = null;
            tail = null;
            Count--;
        }
        else if (Count >= 2)
        {
            Node<T> Evaluator = tail.Prev;
            tail.SetPrev(null);
            Evaluator.SetNext(null);
            tail = Evaluator;


            Count--;
        }
       

    }
    //-> O(1)
    public void RemoveFirst()
    {

        if (Count <= 1)
        {
            head = null;
            tail = null;
            Count--;
            return;
        }

        Node<T> Evaluator = head.Next;
        head.SetNext(null);
        head = Evaluator;
        Count--;


    }

    public void TraverseInOrder(Action<Node<T>> action)
    {
        Node<T> Evaluator = head;
        while (Evaluator != null)
        {
            //  Debug.Log(Evaluator.Value);
            action(Evaluator);

            Evaluator = Evaluator.Next;
        }
    }
    public void TraverseInReverse(Action<Node<T>> action)
    {
        Node<T> Evaluator = tail;
        while (Evaluator != null)
        {
            //  Debug.Log(Evaluator.Value);
            action(Evaluator);

            Evaluator = Evaluator.Prev;
        }
    }

    public void MoveNext()
    {
        if (Pivot != null && Pivot.Next != null)
        {
            Pivot = Pivot.Next;
        }
    }

    public void MovePrev()
    {
        if (Pivot != null && Pivot.Prev != null)
        {
            Pivot = Pivot.Prev;
        }
    }

    public void RemoveFuture()
    {
        if (Pivot == null) return;

        Node<T> current = Pivot.Next;

        while (current != null)
        {
            Node<T> temp = current;
            current = current.Next;

            temp.SetPrev(null);
            temp.SetNext(null);
        }

        Pivot.SetNext(null);
        tail = Pivot;
    }
}
