//
// Created by Komatta on 2016-06-01.
//

#include "Graph.h"
#include <fstream>

void Graph::saveToFile(std::string filename) {
    std::ofstream file(filename);
    if ( !file.fail() ) {
        for(auto it = vertexList.begin(); it != vertexList.end(); it++) {
            file << it->getId();
            for(auto jt = edges[it->getId()].begin(); jt != edges[it->getId()].end(); jt++) {
                file << "," << jt->getId();
            }
            file << std::endl;
        }
    }
}

std::vector<Vertex> Graph::addVertices(int numVertices) {
    for (int i = 0; i < numVertices; i++) {
        vertexList.push_back(Vertex(i));
    }
    return vertexList;
}

void Graph::addEdge(Vertex from, Vertex to){
    edges[from.getId()].push_back(to);
}