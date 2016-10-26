using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeLib
{
    public class MyTree<T>
    {
        public TreeNode<T> head;

        public MyTree(T data)
        {
            head = new TreeNode<T>(data);
        }
    }
    public class TreeNode<T>
    {
        public T data;
        public TreeNode<T> left;
        public TreeNode<T> right;

        public TreeNode(T data)
        {
            this.data = data;
            left = null;
            right = null;
        }

        public void setRight(TreeNode<T> newright)
        {
            this.right = newright;
        }

        public void setLeft(TreeNode<T> newleft)
        {
            this.left = newleft;
        }


    }
}
