using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3_лаба_Дерево
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            graphOfStrotestsWays = graph;
        }
        public static int countDejkstra = 0;
        public static int countFloyd = 0;
        public static int N = 8;
        public double[,] graph = new double[8, 8]
            {
                { 0, I, I, I, I, I, I, 2 },
                { I, 0, I, 3, I, 6, I, 2 },
                { I, I, 0, 5, I, I, 4, I },
                { I, 3, 5, 0, 2, 3, I, I },
                { I, I, I, 2, 0, 5, I, I },
                { I, 6, I, 3, 5, 0, I, 5 },
                { I, I, 4, I, I, I, 0, 5 },
                { 2, 2, I, I, I, 5, 5, 0 }
             };

        public double[,] graphOfStrotestsWays;
        public double[,] graphOfIntermediateVertices = new double[N, N];
        public static double I = double.PositiveInfinity;
        public static int[,] visitedVertices = new int[N, N];

        public static Dictionary<string, double> DijkstraAlgorithm(Dictionary<string, Dictionary<string, int>> graph, string start)
        {
            var distances = new Dictionary<string, double>();
            foreach (var vertex in graph.Keys)
            {
                distances[vertex] = double.PositiveInfinity;
            }

            distances[start] = 0;
            Tuple<double, string> start_point = new Tuple<double, string>(0, start);
            countDejkstra++;
            List<Tuple<double, string>> priorityQueue = new List<Tuple<double, string>>() { start_point };
            while (priorityQueue.Count > 0)
            {
                Tuple<double, string> lowest_vertex = priorityQueue[0];
                for (int i = 1; i < priorityQueue.Count; i++)
                {
                    if (priorityQueue[i].Item1 < lowest_vertex.Item1)
                    {
                        lowest_vertex = priorityQueue[i];
                    }
                }
                priorityQueue.Remove(lowest_vertex);
                var (currentDistance, currentVertex) = lowest_vertex;

                if (currentDistance > distances[currentVertex])
                {
                    continue;
                }

                foreach (var neighbor in graph[currentVertex])
                {
                    var weight = neighbor.Value;
                    var distance = currentDistance + weight;

                    if (distance < distances[neighbor.Key])
                    {
                        countDejkstra++;
                        distances[neighbor.Key] = distance;
                        priorityQueue.Add(new Tuple<double, string>(distance, neighbor.Key));
                    }
                }
            }
            return distances;
        }

        public List<List<int>> Permutations(List<int> lst, int k)
        {
            if (k == 0)
            {
                return new List<List<int>> { new List<int>() };
            }
            List<List<int>> result = new List<List<int>>();
            for (int i = 0; i < lst.Count; i++)
            {
                List<int> temp_lst = new List<int>(lst);
                temp_lst.RemoveAt(i);
                List<List<int>> temp_permutations = Permutations(temp_lst, k - 1);
                foreach (var perm in temp_permutations)
                {
                    perm.Insert(0, lst[i]);
                    result.Add(perm);
                }
            }
            return result;
        }
        public static void GenerateCombination(int[] nums, int[] temp, int index, int start)
        {
            if (index >= temp.Length)
            {
                ;
            }
            else
            {
                for (int i = start; i < nums.Length; i++)
                {
                    temp[index] = nums[i];
                    GenerateCombination(nums, temp, index + 1, i);
                }
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            var graph = new Dictionary<string, Dictionary<string, int>>
            {
                { "1", new Dictionary<string, int> { { "2", 4 }, { "8", 3 } } },
                { "2", new Dictionary<string, int> { { "1", 4 }, { "4", 3 }, { "6", 6 }, { "8", 2 } } },
                { "3", new Dictionary<string, int> { { "4", 5 }, { "7", 4 } } },
                { "4", new Dictionary<string, int> { { "2", 3 }, { "3", 5 }, { "5", 2 }, { "6", 3 } } },
                { "5", new Dictionary<string, int> { { "4", 2 }, { "6", 5 } } },
                { "6", new Dictionary<string, int> { { "2", 6 }, { "4", 3 }, { "5", 5 }, { "8", 2 } } },
                { "7", new Dictionary<string, int> { { "3", 4 }, { "8", 5 } } },
                { "8", new Dictionary<string, int> { { "1", 3 }, { "2", 2 }, { "6", 2 }, { "7", 5 } } }
            };
            Dictionary<string, double> result = DijkstraAlgorithm(graph, "1");

            foreach (var person in result)
            {
                listBox1.Items.Add($"Путь до {person.Key} вершины: {person.Value}");
            }

            label1.Visible = true;
            label1.Text = label1.Text + countDejkstra;
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            listBox2.Items.Clear();

            var graph = new Dictionary<string, Dictionary<string, int>>
            {
                { "1", new Dictionary<string, int> { { "2", 4 }, { "8", 3 } } },
                { "2", new Dictionary<string, int> { { "1", 4 }, { "4", 3 }, { "6", 6 }, { "8", 2 } } },
                { "3", new Dictionary<string, int> { { "4", 5 }, { "7", 4 } } },
                { "4", new Dictionary<string, int> { { "2", 3 }, { "3", 5 }, { "5", 2 }, { "6", 3 } } },
                { "5", new Dictionary<string, int> { { "4", 2 }, { "6", 5 } } },
                { "6", new Dictionary<string, int> { { "2", 6 }, { "4", 3 }, { "5", 5 }, { "8", 2 } } },
                { "7", new Dictionary<string, int> { { "3", 4 }, { "8", 5 } } },
                { "8", new Dictionary<string, int> { { "1", 3 }, { "2", 2 }, { "6", 2 }, { "7", 5 } } }
            };
            Dictionary<string, double> result = DijkstraAlgorithm(graph, "1");

            foreach (var person in result)
            {
                listBox2.Items.Add($"Путь до {person.Key} вершины: {person.Value}");
            }

            label2.Visible = true;
            label2.Text = label2.Text+ 512.ToString();


            for (int u = 0; u < N; u++)
            {
                for (int v = 0; v < N; v++)
                {
                    graphOfIntermediateVertices[u, v] = v;
                }
            }

            for (int k = 0; k < N; k++)
            {
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        double d = graphOfStrotestsWays[i, k] + graphOfStrotestsWays[k, j];
                        if (graphOfStrotestsWays[i, j] > d && visitedVertices[i, k] < 2 && visitedVertices[k, j] < 2)
                        {
                            graphOfStrotestsWays[i, j] = d;
                            graphOfIntermediateVertices[i, j] = k;
                            visitedVertices[i, k]++;
                            visitedVertices[k, j]++;
                        }
                        countFloyd++;
                    }
                }
            }

        }


        private void button3_Click_1(object sender, EventArgs e)
        {
            List<int> friends = new List<int>();
            for (int i = 1; i < N; i++)
            {
                friends.Add(i);
            }
            List<List<int>> permutations = new List<List<int>>();
            foreach (var p in Permutations(friends, N - 1))
            {
                List<int> list = new List<int>() { 0 };
                list.AddRange(p);
                list.Add(0);
                permutations.Add(list);
            }
          
            double min_len_path = double.PositiveInfinity;
            List<int> min_path = new List<int>();
            foreach (var permutation in permutations)
            {
                List<int> path = new List<int>() { 0 };
                permutation.RemoveAt(0);
                foreach (var vertex in permutation)
                {
                    var next = Convert.ToInt32(graphOfIntermediateVertices[path[path.Count() - 1], vertex]);
                    if (graph[path[path.Count() - 1], next] == I)
                    {
                        var row = new List<double>();
                        for (int i = 0; i < N; i++)
                        {
                            row.Add(graph[path[path.Count() - 1], i]);
                        }
                        for (int i = 0; i < row.Count; i++)
                        {
                            if (row[i] != I && row[i] != 0)
                            {
                                next = i;
                            }
                        }
                    }
                    path.Add(next);
                    while (vertex != next)
                    {
                        next = Convert.ToInt32(graphOfIntermediateVertices[path[path.Count() - 1], vertex]);
                        path.Add(next);
                    }
                }
                double current_len_path = 0;
                for (int vertex = 0; vertex < path.Count - 1; vertex++)
                {
                    int current_vertex = path[vertex];
                    int next_vertex = path[vertex + 1];
                    current_len_path += graphOfStrotestsWays[current_vertex, next_vertex];
                }
                if (current_len_path < min_len_path)
                {
                    min_len_path = current_len_path;
                    min_path = path;
                }
            }
            string rez = "";
            for (int i = 0; i < min_path.Count; i++)
            {
                rez += (min_path[i] + 1).ToString();
            }
            MessageBox.Show($"Минимальный путь = {min_len_path}\nПуть = {rez}");

        }
    }
}
