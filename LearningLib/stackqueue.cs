using System;

namespace LearningLib
{
    public class Stack<T>
    {
        public int Size { get; private set; }
        private class Node
        {
            private T data;
            public T Data
            {
                get { return data; }
                set { data = value; }
            }
            public Node Next { get; set; }

            public Node(T data)
            {
                Data = data;
            }
        }

        Node head;

        public bool isEmpty()
        {
            if (head == null)
            {
                return true;
            }
            return false;
        }

        public void push(T element)
        {
            Node addMe = new Node(element);
            addMe.Next = head;
            head = addMe;
            Size++;
        }

        public T pop()
        {
            if (head != null)
            {
                T result = head.Data;
                head = head.Next;
                Size--;
                return result;

            }
            throw new NullReferenceException("The stack is empty. Use isEmpty() to check.");
        }


    }

    public class Queue<T>
    {
        private MyLinkedList<T> data;

        public Queue()
        {
            data = new MyLinkedList<T>();
        }

        public bool isEmpty()
        {
            if (data.head == null)
            {
                return true;
            }
            return false;
        }

        public void enqueue(T element)
        {
            data.Append(element);
        }

        public T dequeue()
        {
            if (data.head != null)
            {
                T result = data.head.Data;
                data.DeleteHead();
                return result;
            }
            throw new NullReferenceException("The queue is empty. Use isEmpty() to check.");
        }

    }
}
