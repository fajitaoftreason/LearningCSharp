using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeLib
{
    public class MyTree<T>
    {
        TreeNode<T> head;

        public MyTree(T data)
        {
            head = new TreeNode<T>(data);
        }
    }
    class TreeNode<T>
    {
        T data;
        TreeNode<T> left;
        TreeNode<T> right;

        public TreeNode(T data)
        {
            this.data = data;
            left = right = null;
        }
    }
}
