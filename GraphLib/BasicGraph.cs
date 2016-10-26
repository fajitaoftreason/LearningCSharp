using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLib
{
    public class BasicGraph<T>
    {
        public List<GraphNode<T>> AllNodes { get; internal set; } = new List<GraphNode<T>>();

        public BasicGraph(GraphNode<T> initialNode)
        {
            initialNode.MemberOf = this;
        }
        
    }

    public class GraphNode<T>
    {
        private Boolean isNew = true;
        public T Data { get; set; }
        public List<GraphEdge<T>> Connections { get; private set; } = new List<GraphEdge<T>>();
        private BasicGraph<T> memberOf;
        public BasicGraph<T> MemberOf
        {
            get { return memberOf; }
            internal set
            {
                if (memberOf == null && value != null)
                {
                    value.AllNodes.Add(this);
                }

                memberOf = value;
            }
        }

        public GraphNode()
        {
            MemberOf = null;
        }

        public GraphNode(T data)
        {
            Data = data;
            MemberOf = null;
        }

        public void addConnection(GraphNode<T> connectingNode, int weight)
        {
            Connections.Add(new GraphEdge<T>(connectingNode, weight));
            connectingNode.Connections.Add(new GraphEdge<T>(this, weight));
            

            

            if (isNew)
            {
                isNew = false;
                this.MemberOf = connectingNode.MemberOf;
            }
                
        }

        public List<GraphNode<T>> pathTo(GraphNode<T> dest)
        {
            List<GraphNode<T>> fullGraph = this.MemberOf.AllNodes;
            int totalNumNodes = fullGraph.Count;
            int startingNodeIndex = fullGraph.IndexOf(this);
            int destNodeIndex = fullGraph.IndexOf(dest);

            int[] dists = new int[totalNumNodes];
            int[] prev = new int[totalNumNodes];
            bool[] visited = new bool[totalNumNodes];

            List<GraphNode<T>> path = new List<GraphNode<T>>();

            for (int i = 0; i < totalNumNodes; i++)
            {
                dists[i] = int.MaxValue;
            }

            
            dists[startingNodeIndex] = 0;

            int currNodeIndex = startingNodeIndex;

            while (currNodeIndex != destNodeIndex)
            {
                currNodeIndex = closestNode(dists, visited);

                foreach (GraphEdge<T> currConn in fullGraph[currNodeIndex].Connections)
                {
                    int indexOfConn = fullGraph.IndexOf(currConn.ConnectingNode);
                    int distThroughCurr = currConn.Weight + dists[currNodeIndex];
                    if (dists[indexOfConn] > distThroughCurr)
                    {
                        dists[indexOfConn] = distThroughCurr;
                        prev[indexOfConn] = currNodeIndex;
                    }
                }
                visited[currNodeIndex] = true;
            }

            int cursor = destNodeIndex;
            path.Add(dest);

            while ( cursor != startingNodeIndex)
            {
                cursor = prev[cursor];
                path.Add(fullGraph[cursor]);
            }

            return path;
        }

        private int closestNode (int[] distances, bool[] visited )
        {
            int min = int.MaxValue;
            int minIndex = 0;
            for (int i = 0; i < distances.Length; i++)
            {
                if (distances[i] < min && !visited[i])
                {
                    minIndex = i;
                }
            }
            return minIndex;
        }

    }

    public class GraphEdge<T>
    {
        public GraphNode<T> ConnectingNode {get; set;}
        public int Weight { get; set; }

        public GraphEdge(GraphNode<T> connectingNode, int weight)
        {
            ConnectingNode = connectingNode;
            Weight = weight;
        }
    }

}
