using Microsoft.OpenApi;
using Swashbuckle.Swagger;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Trivial.Tasks;

namespace JWT.Server.Services
{
    public class GenericList<T> : IEnumerable<T>
    {
        protected Node head;

        protected Node current = null;
        protected class Node
        {
            public Node next;
            private T data;

            public Node(T t)
            {
                next = null;
                data = t;
            }

            public Node Next
            {
                get { return next; }
                set { next = value; }
            }

            public T Data
            {
                get { return data; }
                set { data = value; }
            }
        }

        public GenericList()
        {
            head = null;
        }

        public void AddHead(T t)
        {
            Node n = new Node(t);
            n.Next = head;
            head = n;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class SortedList<T> : GenericList<T> where T : IComparable<T>
    {
        public void BubbleSort()
        {
            if (null == head || null == head.Next)
            {
                return;
            }
            bool swapped;

            do
            {
                Node previous = null;
                Node current = head;
                swapped = false;

                while (current.next != null)
                {
                    if (current.Data.CompareTo(current.next.Data) > 0)
                    {
                        Node tmp = current.next;
                        current.next = current.next.next;
                        tmp.next = current;

                        if (previous == null)
                        {
                            head = tmp;
                        }
                        else
                        {
                            previous.next = tmp;
                        }
                        previous = tmp;
                        swapped = true;
                    }
                    else
                    {
                        previous = current;
                        current = current.next;
                    }
                }
            } while (swapped);
        }
    }

    public class Person : IComparable<Person>
    {
        string name;
        int age;

        public int Age
        {
            get { return age; }
            set
            {
                if (value < 0 || value > 125)
                {
                    throw new ArgumentOutOfRangeException("The value is put of range.");
                }
                else
                {
                
                }

            }
        }

        public Person(string s, int i)
        {
            name = s;
            age = i;
        }

        public int CompareTo(Person p)
        {
            return age - p.age;
        }

        public override string ToString()
        {
            return name + ":" + age;
        }

        public bool Equals(Person p)
        {
            return this.age == p.age;
        }
    }
}
