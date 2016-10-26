using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLib
{
    public static class TSP
    {
        public static Solution branchandBound(BasicGraph<int> graph)
        {
            int?[,] distMatrix = generateDistMatrix(graph);
            int size = graph.AllNodes.Count;
            var possibleSolutions = new List<Solution>();
            int? upperBound = null;
            int lowerBound = reduce(distMatrix, size);

            Solution solutionRoot = new Solution(0, distMatrix);
            solutionRoot.minCost = lowerBound;
            solutionRoot.size = size;
            solutionRoot.isLeaf = false;


            var solutions = new LinkedList<Solution>();
            upperBound = findSolution(solutionRoot, solutions, size);

            while (solutions.Count > 1)
            {
                
                   int? nextSolution = findSolution(nextBest(solutions, upperBound), solutions, size);
                if (nextSolution < upperBound)
                    upperBound = nextSolution;

            }

            return solutions.First.Value;
            

        }

        private static Solution nextBest(LinkedList<Solution> solutions, int? upperBound)
        {
            LinkedListNode<Solution> currentNode = solutions.First;

            while (currentNode!= null)
            {
                Solution current = currentNode.Value;
                LinkedListNode<Solution> nextNode = currentNode.Next;
                if (current.minCost > upperBound)
                {
                    solutions.Remove(currentNode);
                }
                else if (!current.isLeaf || !current.isComplete && current.minCost == upperBound)
                {
                    solutions.Remove(currentNode);
                }
                currentNode = nextNode;
            }
            return solutions.Min();
        }

        private static int? findSolution(Solution currentSolution, LinkedList<Solution> solutions, int size)
        {
            
            if(currentSolution.depth == size)
            {
                currentSolution.minCost += currentSolution.distanceMatrix[currentSolution.path[currentSolution.depth - 1], currentSolution.path[0]];
                var temp = currentSolution.path;
                currentSolution.path = new int[currentSolution.depth+1];
                Array.Copy(temp, currentSolution.path, currentSolution.depth);
                currentSolution.path[currentSolution.depth] = currentSolution.path[0];
                currentSolution.isComplete = true;
                currentSolution.isLeaf = true;
                return currentSolution.minCost;
            }

            int[] viableNodes = new int[size - currentSolution.depth];
            int possibleNode = 0;
            for (int i = 0; i < size - currentSolution.depth; i++)
            {
                while (currentSolution.path.Contains(possibleNode))
                    possibleNode++;
                viableNodes[i] = possibleNode++;
            }

            foreach (int nextNode in viableNodes)
            {
                solutions.AddFirst(new Solution(nextNode, currentSolution));
            }
            int? bestlowerBound = int.MaxValue;
            Solution bestAtLevel = null;
            foreach (Solution current in solutions)
            {
                if (current.depth < currentSolution.depth + 1)
                    break;

                if (current.minCost < bestlowerBound)
                {
                    bestAtLevel = current;
                    bestlowerBound = current.minCost;
                }
            }

            if (bestAtLevel != null)
            {
                bestAtLevel.isLeaf = false;
                return findSolution(bestAtLevel, solutions, size);

            }


            return 0;

        }


        public static int reduce(int?[,] distMatrix, int size)
        {
            int totalReduction = 0;

            //reduce by row (i)
            for (int i = 0; i < size; i++)
            {
                int min = int.MaxValue;

                //find row min
                for (int j = 0; j < size; j++)
                {
                    if (distMatrix[i,j].HasValue && (distMatrix[i,j] < min))
                    {
                        min = (int)distMatrix[i, j];
                    }
                }
                if (min == int.MaxValue)
                    min = 0;

                //reduce by row min
                for (int j = 0; j < size; j++)
                {
                    distMatrix[i, j] -= min;
                }
                totalReduction += min;

            }

            //reduce by column
            for (int j = 0; j < size; j++)
            {
                int min = int.MaxValue;
                //find column min
                for (int i = 0; i < size; i++)
                {
                    if (distMatrix[i, j].HasValue && distMatrix[i, j] < min)
                        min = (int)distMatrix[i, j];
                }

                if (min == int.MaxValue)
                    min = 0;

                //reduce by column min
                for (int i = 0; i < size; i++)
                {
                    distMatrix[i, j] -= min;
                }
                totalReduction += min;

            }
            return totalReduction;
        }

        static int?[,] generateDistMatrix(BasicGraph<int> graph)
        {
            int graphSize = graph.AllNodes.Count;
            var distMatrix = new int?[graphSize, graphSize];

            //set all values to null
            for (int i = 0; i < graphSize; i++)
                for (int j = 0; j < graphSize; j++)
                    distMatrix[i, j] = null;

            //create distance matrix
            for (int i = 0; i < graphSize; i++)
            {
                foreach(GraphEdge<int> edge in graph.AllNodes[i].Connections)
                {
                    distMatrix[i, graph.AllNodes.IndexOf(edge.ConnectingNode)] = edge.Weight;
                }
                
            }
            return distMatrix;
        }

    }

    public class Solution : IComparable<Solution>
    {
        public bool isLeaf = true;
        public bool isComplete = false;
        public int depth;
        public int? minCost;
        public int[] path;
        public int size;
        public int?[,] distanceMatrix;

        public int CompareTo(Solution other)
        {
            if (this.minCost == null && other.minCost == null)
                return 0;
            if (this.minCost == null)
                return 1;
            if (other.minCost == null)
                return -1;
            return this.minCost - other.minCost ?? 0;
        }

        public Solution(int nextNode, Solution parentSolution)
        {
            size = parentSolution.size;
            depth = parentSolution.depth + 1;
            distanceMatrix = new int?[size, size];
            Array.Copy(parentSolution.distanceMatrix, distanceMatrix, distanceMatrix.Length);

            path = new int[depth];
            
            Array.Copy(parentSolution.path, path, parentSolution.depth);
            path[depth - 1] = nextNode;

            for (int j = 0; j < size; j++)
            {
                distanceMatrix[path[depth - 2], j] = null;
            }

            for (int i = 0; i <size; i++)
            {
                distanceMatrix[i, path[depth - 1]] = null;
            }


            minCost = parentSolution.minCost + parentSolution.distanceMatrix[path[depth - 2], path[depth - 1]] + TSP.reduce(distanceMatrix, size);

        }

        public Solution(int nodeNum, int?[,] initialMatrix)
        {
            depth = 1;
            path = new int[]{ 0};
            distanceMatrix = initialMatrix;

        }
    }
}
