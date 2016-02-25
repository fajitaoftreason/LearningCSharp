using System;

namespace LearningLib
{
    public class MyLinkedList<T>
    {

        private int size = 0;
        public ListNode<T> head;
        public ListNode<T> tail;

        public void InsertAtHead(T data)
        {
            head = new ListNode<T>(data, head);
            if (head.Next == null)
            {
                tail = head;
            }
            size++;
        }

        public void Append(T data)
        {
            if (head == null) {
                InsertAtHead(data);
            }
            else
            {
                tail.Next = new ListNode<T>(data, null);
                tail = tail.Next;
            }
            size++;
        }

        public void PrintAllNodes()
        {
            if (head != null)
            {
                ListNode<T> current = head;
                do
                {
                    Console.WriteLine(current.Data);
                    current = current.Next;
                } while (current != null);
            }
        }

        public bool DeleteHead()
        {
            if (head == null)
            {
                return false;
            }
            head = head.Next;
            size--;
            return true;
            
        }

        public void ReverseIter()
        {
            for (int i = 0; i < size; i++)
            {
                tail.Next = head;   //make circular
                tail = head;        //move tail over one
                head = head.Next;   //move head over one
                tail.Next = null;   //break circle one node over, (previous head is now at the tail)
            }
        }

        //only here so that the call is in the same format as the iterative reverse
        public void ReverseRecur()
        {
            ReverseRecurHelper(head, null);
        }

        //called by wrapper function above
        protected void ReverseRecurHelper(ListNode<T> current, ListNode<T> prev)
        {
            //upon reaching the end of the list, this is the new head
            if (current == null)
            {
                head = prev;
                return;
            }

            //recursively call function on the next node, with the current node as prev
            ReverseRecurHelper(current.Next, current);
            //set the current's next to the node before it
            current.Next = prev;
        }


    }

    public class ListNode<T>
    {
        public T Data { get; }

        public ListNode<T> Next { get; set; }

        public ListNode(T data, ListNode<T> next)
        {
            Data = data;
            Next = next;
        }

    }

}
