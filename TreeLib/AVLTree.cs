using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeLib
{
    public class MyAVLTree<T> : MyBST<T> where T : IComparable
    {


        public override Boolean insert(T data)
        {
            if (!base.insert(data))
            { 
                return false;
            }
            reBalance();
            return true;
        }

        public override void delete(T data)
        {
            base.delete(data);
            reBalance();
        }



        public static TreeNode<T> rotateRight(TreeNode<T> oldroot)
        {
            //if we cant rotate right return false.  shouldnt be called in these cases in the first place though.
            if (oldroot == null || oldroot.left == null)
            {
                return null;
            }

            //now we rotate
            TreeNode<T> newroot = oldroot.left;
            oldroot.setLeft(newroot.right);
            newroot.setRight(oldroot);
            return newroot;
        }

        public static TreeNode<T> rotateLeft(TreeNode<T> oldroot)
        {
            //if we cant rotate left return false.  shouldnt be called in these cases in the first place though.
            if (oldroot == null || oldroot.right == null)
            {
                return null;
            }

            //now rotate
            TreeNode<T> newroot = oldroot.right;
            oldroot.setRight(newroot.left);
            newroot.setLeft(oldroot);
            return newroot;
        }

        public void reBalance()
        {
            head = reBalanceRecur(head);
        }

        private TreeNode<T> reBalanceRecur(TreeNode<T> root)
        {
            if (root == null)
            {
                return null;
            }

            reBalanceRecur(root.right);
            reBalanceRecur(root.left);

            //check the balance of the right child
            if (root.right != null)
            {
                int balance = getHeight(root.right.right) - getHeight(root.right.left);
                if (balance >= 2)
                {
                    root.setRight(rotateLeft(head));
                }
                else if (balance <= -2)
                {
                    root.setRight(rotateRight(head));
                }
            }
            //check balance of left child
            if (root.left != null)
            {
                int balance = getHeight(root.left.right) - getHeight(root.left.left);
                if (balance >= 2)
                {
                    root.setLeft(rotateLeft(head));
                }
                else if (balance <= -2)
                {
                    root.setLeft(rotateRight(head));
                }
            }

                //check balance if we're back to the actual root of tree
                if (root == head)
            {
                int balance = getHeight(head.right) - getHeight(head.left);
                if (balance >= 2)
                {
                    return rotateLeft(head);
                }
                else if (balance <= -2)
                {
                    return rotateRight(head);
                }
                return root;
            }



            return null;


        }
    }


}
