using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeLib
{
    //T must be comparable, it is a search tree after all.
    public class MyBST<T> where T : IComparable
    {
        public TreeNode<T> head = null;

        public MyBST(T data)
        {
            head = new TreeNode<T>(data);
        }

        public MyBST() { }

        public TreeNode<T> search(T data)
        {
            return searchrecur(head, data);
        }
        private TreeNode<T> searchrecur(TreeNode<T> current, T data)
        {
            if (current == null){
                return null;
            }
            if (current.data.Equals(data))
            {
                return current;
            }
            if (current.data.CompareTo(data) > 0)
            {
                return searchrecur(current.left, data);
            }
            if (current.data.CompareTo(data) < 0)
            {
                return searchrecur(current.right, data);
            }
            return null;
        }

        public virtual void delete(T data)
        {
            head = deleteRecur(head, data);
        }

        //returns the root of the tree, important if deleting the head.
        private TreeNode<T> deleteRecur( TreeNode<T> root, T data)
        {
            if (root == null)
            {
                return null;
            }
            if (root.data.Equals(data))
            {
                return removeThis(root);
            }
            if (root.data.CompareTo(data) > 0)
            {
                if (root.left.data.Equals(data))
                {
                    root.setLeft(removeThis(root.left));
                }
                else
                {
                    searchrecur(root.left, data);
                }
                return root;
            }
            else if (root.data.CompareTo(data) < 0)
            {
                if (root.right.data.Equals(data))
                {
                    root.setRight(removeThis(root.right));
                }
                else
                {
                    searchrecur(root.right, data);
                }
                return root;
            }
            return null;
        }

        private TreeNode<T> removeThis(TreeNode<T> current)
        {
            int rightH, leftH;
            //store these because the operations are expensive
            rightH = getHeight(current.right);
            leftH = getHeight(current.left);

            //if you die without children, nobody continues your legacy
            if (rightH == 0 && leftH == 0)
            {
                return null;
            }
            //if the right subtree is bigger or equal, bring that one up
            if (rightH >= leftH)
            {
                if (current.left != null)
                {

                    TreeNode<T> abandoned = current.right.left;
                    current.right.setLeft(current.left);

                    if (abandoned != null)
                    {
                        TreeNode<T> foster = current.left;
                        while (foster.right != null)
                        {
                            foster = foster.right;
                        }
                        foster.setRight(abandoned);
                    }
                }
                return current.right;
            }
            //if the left subtree is larger, bring that one up
            if (getHeight(current.right) < getHeight(current.left))
            {

                if (current.right != null)
                {
                    TreeNode<T> abandoned = current.left.right;
                    current.left.setRight(current.right);


                    if (abandoned != null)
                    {
                        TreeNode<T> foster = current.right;
                        while (foster.left != null)
                        {
                            foster = foster.left;
                        }
                        foster.setLeft(abandoned);
                    }
                }
                return current.left;
            }
            return null;
        }
        


        public virtual Boolean insert(T data)
        {
            TreeNode<T> current;
            if (head == null)
            {
                head = new TreeNode<T>(data);
                return true;
            }
            current = head;
            while (current != null)
            {
                //current node already contains the data to be inserted
                if (current.data.Equals(data))
                {
                    return true; // if it's already in the tree we don't add it, but it's there already. it can not contain duplicates.
                }
                
                // the current node is larger than the data to be inserted
                if (current.data.CompareTo(data) > 0)
                {
                    if (current.left == null)
                    {
                        current.left = new TreeNode<T>(data);
                        return true;
                    }
                    else
                    {
                        current = current.left;
                    }
                }
                
                //the current node is smaller than the data to be inserted
                else if (current.data.CompareTo(data) < 0)
                {
                    if (current.right == null)
                    {
                        current.right = new TreeNode<T>(data);
                        return true;
                    }
                    else
                    {
                        current = current.right;
                    }
                }

            }
            
            return false;
        }

        public static int getHeight(TreeNode<T> root)
        {
            if (root == null)
            {
                return 0;
            }
            return 1 + Math.Max(getHeight(root.left), getHeight(root.right));
        }


    }
}
