//
// Created by Komatta on 2016-06-01.
//

#ifndef GRAPHGENERATOR_GRAPHGENERATOR_H
#define GRAPHGENERATOR_GRAPHGENERATOR_H
#include <Graph.h>

class GraphGenerator {
    int numEdges;
    int numVertices;
public:
    GraphGenerator(int numEdges, int numVertices = 0) : numEdges(numEdges), numVertices(numVertices) {}
    Graph generate();

private:
    bool validate();
};


#endif //GRAPHGENERATOR_GRAPHGENERATOR_H
