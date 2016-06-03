//
// Created by Komatta on 2016-06-01.
//

#ifndef GRAPHGENERATOR_GRAPH_H
#define GRAPHGENERATOR_GRAPH_H
#include <string>
#include <vector>
#include <Vertex.h>
#include <map>

class Graph {
    std::vector<Vertex> vertexList;
    std::map<int, std::vector<Vertex>> edges;
public:
    Graph() {}
    void saveToFile(std::string filename);
    std::vector<Vertex> addVertices(int numVertices);
    void addEdge(Vertex from, Vertex to);
};


#endif //GRAPHGENERATOR_GRAPH_H
