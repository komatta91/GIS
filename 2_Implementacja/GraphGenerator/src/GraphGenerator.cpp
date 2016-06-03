//
// Created by Komatta on 2016-06-01.
//

#include "GraphGenerator.h"
#include <stdexcept>

#include <boost/random/mersenne_twister.hpp>
#include <boost/random/uniform_int_distribution.hpp>

#include <iostream>

Graph GraphGenerator::generate() {
    validate();
    Graph g;
    boost::random::mt19937_64 gen;
    boost::random::uniform_int_distribution<> dist(0, numVertices - 1);

    std::vector<Vertex> vertexList = g.addVertices(numVertices);
    Vertex first = vertexList[0];
    Vertex last = first;

    while (numEdges > 1) {
        Vertex next = vertexList[dist(gen)];
        //std ::cout << "Edge: " << last.getId() << "," << next.getId() << std::endl;
        g.addEdge(last, next);
        last = next;
        numEdges--;
    }
    //std ::cout << "Edge: " << last.getId() << "," << first.getId() << std::endl;
    g.addEdge(last, first);
    return g;
}

bool GraphGenerator::validate() {
    if (numEdges < 2) {
        throw std::invalid_argument("Insufficient number of edges");
    }
    if (numVertices < 1) {
        throw std::invalid_argument("Insufficient number of vertices");
    }
}
