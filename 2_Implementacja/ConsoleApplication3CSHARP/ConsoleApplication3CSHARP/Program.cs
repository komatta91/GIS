using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3CSHARP
{

    
    /// <summary>
    /// Class representing oriented graph and its neccessary methods,
    /// as well as Fleury-algorithm related methods
    /// </summary>
    class Graph
    {
        /// <summary>
        /// Helper field to keep track of number of visited edges for visualization purposes
        /// </summary>
        public int totalEdgesVisited = 0;

        /// <summary>
        /// Helper field to keep track of number of edges to be visited for visualization purposes
        /// </summary>
        public int totalEdgesToVisit = 0;

        /// <summary>
        /// Graph adjacency list
        /// </summary>
        public List<List<int>> adj;

        /// <summary>
        /// Class constructor
        /// Initializes graph adjacency list based on number of vertices
        /// </summary>
        /// <param name="verticesNumber">Number of graph vertices</param>
        public Graph(int verticesNumber)
        {

            adj = new List<List<int>>(verticesNumber);

            for (int i = 0; i < verticesNumber; i++)
            {
                adj.Add(new List<int>());
            }

        }


        /// <summary>
        /// Adds method to graph in form of proper entries in adjacency list
        /// Increaces number of edges to be visited
        /// </summary>
        /// <param name="u">origin vertex of edge in adjacency list</param>
        /// <param name="v">target vertex of edge in adjacency list</param>
        public void addEdge(int u, int v)
        {
            adj[u].Add(v);
            this.totalEdgesToVisit++;
        }

        /// <summary>
        /// Marks edge as removed by setting value representing edge betwenn two vertices to -1
        /// </summary>
        /// <param name="u">origin vertex of edge in adjacency list</param>
        /// <param name="v">target vertex of edge in adjacency list</param>
        public void removeEdge(int u, int v)
        {
            for (int i = 0; i < adj[u].Count; i++)
            {
                if (adj[u][i] == v)
                {
                    adj[u][i] = -1;
                    break;
                }
            }
            
        }
        
        
        /// <summary>
        /// Depth-First-Search method for finding bridges in graph
        /// For each edge for specified vertex in adjacency list, visits all yet unvisited possibilities 
        /// and does recurrent call to self in each visited vertex
        /// </summary>
        /// <param name="v">Index of currently visited vertex</param>
        /// <param name="visited">List with visited/unvisited vertices</param>
        /// <returns>Number of visitable vertices</returns>
        public int DFSCount(int v, List<bool> visited)
        {
            visited[v] = true;
            int count = 1;

            foreach (int vertex in adj[v])
            {
                if (vertex != -1 && visited[vertex] == false)
                    count += DFSCount(vertex, visited);
            }

            return count;
        }


        /// <summary>
        /// Bridge finding function implementation
        /// For edge represented as pair of vertices, checks number of vertices 
        /// possible to visit before and after edge removal
        /// </summary>
        /// <param name="u">origin vertex of edge in adjacency list</param>
        /// <param name="v">target vertex of edge in adjacency list</param>
        /// <param name="c1">number of visitable vertices before edge removal</param>
        /// <param name="c2">number of visitable vertices after edge removal</param>
        /// <returns>False if edge is a bridge, True otherwise</returns>
        bool isValidNextEdge(int u, int v, out int c1, out int c2)
        {
            int count = 0;

            foreach (int vertex in adj[u])
                if (vertex != -1)
                    count++;

            if (count == 1)
            {
                c1 = -1; c2 = -1;
                return true;
            }

            List<bool> visited = new List<bool>(adj.Count);

            for (int i = 0; i < visited.Capacity; i++)
            {
                visited.Add(false);
            }

            int count1 = DFSCount(u, visited);
            c1 = count1;

            for (int i = 0; i < visited.Count; i++)
            {
                visited[i] = false;
            }

            int count2 = DFSCount(v, visited);
            c2 = count2;
            

            return (count1 > count2) ? false : true;

        }

        /// <summary>
        /// Fleury algorith init function
        /// Prints Euler Cycle, keeps track of visited edges number
        /// </summary>
        /// <param name="u">Starting vertex index</param>
        public void printEulerUtil(int u)
        {
            int c1, c2;
            Dictionary<int, int> bridgesHierarchy = new Dictionary<int, int>();

            bool enterBridge = false;

            for (int i = 0; i < adj[u].Count; i++)
            {
                int v = adj[u][i];

                if (v != -1)
                {
                    enterBridge = true;

                    if (isValidNextEdge(u, v, out c1, out c2) == true)
                    {
                        enterBridge = false;

                        Console.Write(u.ToString() + "-" + v.ToString() + " ");
                        totalEdgesVisited++;
                        removeEdge(u, v);
                        printEulerUtil(v);
                    }
                    else
                        bridgesHierarchy.Add(v, c2);
                }
            }

            if (enterBridge == false)
                return;

            KeyValuePair<int, int> bridgeToCross = bridgesHierarchy.Where(x => x.Value == bridgesHierarchy.Max(p => p.Value)).FirstOrDefault();

            Console.Write(u.ToString() + "-" + bridgeToCross.Key.ToString() + " ");
            totalEdgesVisited++;
            removeEdge(u, bridgeToCross.Key);
            printEulerUtil(bridgeToCross.Key);

        }

    }



    /// <summary>
    /// Console application class
    /// </summary>
    class Program
    {

        /// <summary>
        /// Application entry point
        /// Reads optional argument - name of file containing graph data
        /// Creates graph object instance based on graph data file
        /// Calls fleury algorithm init function
        /// </summary>
        /// <param name="args">Optional filename argument</param>
        static void Main(string[] args)
        {
            string filePath = "GraphData.txt";

            if(args.Length > 0)
            {
                filePath = args[0];
            }

            List<string> lines = new List<string>();
            string line;
            using (StreamReader reader = new StreamReader(filePath))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            Graph g = new Graph(lines.Count);

            foreach(string singleLine in lines)
            {
                string[] vertices = singleLine.Split(',');

                int vIndex = Convert.ToInt32(vertices[0]);

                for (int i = 1; i < vertices.Length; i++)
                {
                    g.addEdge(vIndex, Convert.ToInt32(vertices[i]));
                }
            }

            g.printEulerUtil(0);
            Console.Write(" --- VISITED: " + g.totalEdgesVisited.ToString() + " OF " + g.totalEdgesToVisit.ToString() + " EDGES --- ");
            Console.WriteLine("Finished");

            //Graph g1 = new Graph(4);
            //g1.addEdge(0, 1);
            //g1.addEdge(0, 2);
            //g1.addEdge(1, 2);
            //g1.addEdge(2, 3);
            //g1.printEulerUtil(2);
            //Console.WriteLine("Finished");

            //Graph g2 = new Graph(3);
            //g2.addEdge(0, 1);
            //g2.addEdge(1, 2);
            //g2.addEdge(2, 0);
            //g2.printEulerUtil(0);
            //Console.WriteLine("Finished");

            //Graph g3 = new Graph(5);
            //g3.addEdge(1, 0);
            //g3.addEdge(0, 2);
            //g3.addEdge(2, 1);

            //g3.addEdge(3, 1);
            //g3.addEdge(2, 4);
            //g3.addEdge(0, 3);
            //g3.addEdge(3, 4);
            //g3.addEdge(3, 2);
            //g3.printEulerUtil(0);
            //Console.WriteLine("Finished");

            //Graph gX = new Graph(8);
            //gX.addEdge(1, 0);
            //gX.addEdge(0, 1);
            //gX.addEdge(1, 2);
            //gX.addEdge(2, 3);
            //gX.addEdge(3, 4);
            //gX.addEdge(4, 1);
            //gX.addEdge(7, 0);
            //gX.addEdge(6, 7);
            //gX.addEdge(5, 6);
            //gX.addEdge(0, 5);
            //gX.printEulerUtil(1);
            //Console.Write(" --- VISITED: " + gX.totalEdgesVisited.ToString() + " OF " + gX.totalEdgesToVisit.ToString() + " EDGES --- ");
            //Console.WriteLine("Finished");

            //Graph gZ = new Graph(8);
            //gZ.addEdge(1, 0);
            //gZ.addEdge(0, 1);
            //gZ.addEdge(1, 2);
            //gZ.addEdge(2, 3);
            //gZ.addEdge(3, 4);
            //gZ.addEdge(4, 1);
            //gZ.addEdge(2, 1);
            //gZ.addEdge(3, 2);
            //gZ.addEdge(4, 3);
            //gZ.addEdge(1, 4);
            //gZ.addEdge(7, 0);
            //gZ.addEdge(6, 7);
            //gZ.addEdge(5, 6);
            //gZ.addEdge(0, 5);
            //gZ.addEdge(0, 7);
            //gZ.addEdge(7, 6);
            //gZ.addEdge(6, 5);
            //gZ.addEdge(5, 0);
            //gZ.printEulerUtil(1);
            //Console.Write(" --- VISITED: " + gZ.totalEdgesVisited.ToString() + " OF " + gZ.totalEdgesToVisit.ToString() + " EDGES --- ");
            //Console.WriteLine("Finished");

            //Console.WriteLine();
            //Graph gY = new Graph(19);
            //gY.addEdge(1, 0);
            //gY.addEdge(0, 2);
            //gY.addEdge(2, 1);
            //gY.addEdge(1, 3);
            //gY.addEdge(3, 4);
            //gY.addEdge(4, 5);
            //gY.addEdge(5, 1);
            //gY.addEdge(0, 9);
            //gY.addEdge(9, 10);
            //gY.addEdge(10, 11);
            //gY.addEdge(11, 0);
            //gY.addEdge(2, 7);
            //gY.addEdge(7, 8);
            //gY.addEdge(8, 6);
            //gY.addEdge(6, 2);
            //gY.addEdge(6, 12);
            //gY.addEdge(12, 6);
            //gY.addEdge(13, 12);
            //gY.addEdge(12, 13);
            //gY.addEdge(14, 12);
            //gY.addEdge(12, 14);
            //gY.addEdge(13, 15);
            //gY.addEdge(15, 13);
            //gY.addEdge(14, 15);
            //gY.addEdge(15, 14);
            //gY.addEdge(13, 16);
            //gY.addEdge(16, 17);
            //gY.addEdge(17, 18);
            //gY.addEdge(18, 13);
            //gY.addEdge(14, 15);
            //gY.addEdge(15, 14);
            //gY.addEdge(13, 15);
            //gY.addEdge(15, 13);

            //gY.printEulerUtil(15);
            //Console.Write(" --- VISITED: " + gY.totalEdgesVisited.ToString() + " OF " + gY.totalEdgesToVisit.ToString() + " EDGES --- ");
            //Console.WriteLine("Finished");

            Console.ReadKey();
        }
    }
}
