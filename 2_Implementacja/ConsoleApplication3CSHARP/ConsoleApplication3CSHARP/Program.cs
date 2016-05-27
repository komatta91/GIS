using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3CSHARP
{

    

    class Graph
    {
        public int totalEdgesVisited = 0;
        public int totalEdgesToVisit = 0;

        public List<List<int>> adj;

        public Graph(int verticesNumber)
        {

            adj = new List<List<int>>(verticesNumber);

            for (int i = 0; i < verticesNumber; i++)
            {
                adj.Add(new List<int>());
            }

        }

        public void addEdge(int u, int v)
        {
            adj[u].Add(v);
            this.totalEdgesToVisit++;
        }

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




    class Program
    {
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
