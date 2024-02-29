using System;
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
        private int[,] graph = {
            { 0, 4, 0, 0, 0, 0, 0, 3 },
            { 4, 0, 0, 3, 0, 6, 0, 2 },
            { 0, 0, 0, 5, 0, 0, 4, 0 },
            { 0, 3, 5, 0, 2, 3, 0, 0 },
            { 0, 0, 0, 2, 0, 5, 0, 0 },
            { 0, 6, 0, 3, 5, 0, 0, 2 },
            { 0, 0, 4, 0, 0, 0, 0, 5 },
            { 3, 2, 0, 0, 0, 2, 5, 0 }
        };
        private const int Infinity = int.MaxValue;

        public Form1()
        {
            InitializeComponent();
        }

        private int[] Dijkstra(int[,] graph, int startVertex)
        {
            int verticesCount = graph.GetLength(0);
            int[] distances = new int[verticesCount];
            bool[] visited = new bool[verticesCount];

            // Инициализация расстояний до всех вершин как "бесконечность",
            // кроме начальной вершины, расстояние до которой равно 0
            for (int i = 0; i < verticesCount; i++)
            {
                distances[i] = Infinity;
            }
            distances[startVertex] = 0;

            for (int i = 0; i < verticesCount - 1; i++)
            {
                int minDistance = Infinity;
                int nearestVertex = -1;

                // Находим ближайшую непосещенную вершину
                for (int vertex = 0; vertex < verticesCount; vertex++)
                {
                    if (!visited[vertex] && distances[vertex] < minDistance)
                    {
                        minDistance = distances[vertex];
                        nearestVertex = vertex;
                    }
                }

                // Помечаем вершину как посещенную
                visited[nearestVertex] = true;

                // Обновляем расстояния до смежных вершин
                for (int vertex = 0; vertex < verticesCount; vertex++)
                {
                    int edgeWeight = graph[nearestVertex, vertex];
                    if (edgeWeight > 0 && minDistance + edgeWeight < distances[vertex])
                    {
                        distances[vertex] = minDistance + edgeWeight;
                    }
                }
            }

            return distances;
        }

        private int[,] Floyd(int[,] graph)
        {
            int verticesCount = graph.GetLength(0);
            int[,] distances = new int[verticesCount, verticesCount];

            // Инициализация матрицы расстояний
            for (int i = 0; i < verticesCount; i++)
            {
                for (int j = 0; j < verticesCount; j++)
                {
                    distances[i, j] = graph[i, j];
                    if (i != j && distances[i, j] == 0)
                    {
                        distances[i, j] = Infinity;
                    }
                }
            }

            // Алгоритм Флойда
            for (int k = 0; k < verticesCount; k++)
            {
                for (int i = 0; i < verticesCount; i++)
                {
                    for (int j = 0; j < verticesCount; j++)
                    {
                        if (distances[i, k] != Infinity && distances[k, j] != Infinity &&
                            distances[i, k] + distances[k, j] < distances[i, j])
                        {
                            distances[i, j] = distances[i, k] + distances[k, j];
                        }
                    }
                }
            }

            return distances;
        }

        private void DisplayResults(int[] distances)
        {
            string result = "Кратчайшие расстояния от города 1:\n";
            for (int i = 0; i < distances.Length; i++)
            {
                result += $"До города {i + 1}: {(distances[i] == Infinity ? "Недостижим" : distances[i].ToString())}\n";
            }
            MessageBox.Show(result);
        }

        private void btnFloyd_Click(object sender, EventArgs e)
        {
            int[,] distances = Floyd(graph);
            int startVertex = 0; // Начальная вершина (город Оли)

            // Получаем расстояния от начальной вершины до всех остальных
            int[] shortestDistances = new int[graph.GetLength(0)];
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                shortestDistances[i] = distances[startVertex, i];
            }

            DisplayResults(shortestDistances);
        }

        private void btnDijkstra_Click(object sender, EventArgs e)
        {
            int startVertex = 0; // Начальная вершина (город Оли)
            int[] distances = Dijkstra(graph, startVertex);

            DisplayResults(distances);
        }
    }
}
