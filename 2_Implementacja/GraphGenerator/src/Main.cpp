#include <boost/program_options/options_description.hpp>
#include <boost/program_options/parsers.hpp>
#include <boost/program_options/variables_map.hpp>
#include <iostream>
#include <Core.h>
#include <GraphGenerator.h>


int main(int argc, const char* argv[]){
    try {
        boost::program_options::options_description description("GraphGenerator Usage");

        description.add_options()
                ("help,h", "Display this help message")
                ("edges,e", boost::program_options::value<int>(), "Number of edges")
                ("vertices,v", boost::program_options::value<int>(), "Number of vertices")
                ("output-file", boost::program_options::value<std::string>(), "Output file name")
                ("version", "Display the version number");

        boost::program_options::positional_options_description p;
        p.add("output-file", -1);

        boost::program_options::variables_map vm;
        boost::program_options::store(
                boost::program_options::command_line_parser(argc, argv).options(description).positional(p).run(), vm);
        boost::program_options::notify(vm);

        if (vm.count("help")) {
            std::cout << description;
            return 0;
        }

        if (vm.count("version")) {
            std::cout << "GraphGenerator version " << CORE_VERSION << std::endl;
            return 0;
        }

        int edges = 0;
        if (vm.count("edges")) {
            edges = vm["edges"].as<int>();
        } else {
            throw std::invalid_argument("Number of edges not set");
        }

        int vertices = 0;
        if (vm.count("vertices")) {
            vertices = vm["vertices"].as<int>();
        } else {
            //throw std::invalid_argument("Number of vertices not set");
            vertices = edges / 5;
        }

        std::string outputFile;
        if (vm.count("output-file")) {
            outputFile = vm["output-file"].as<std::string>();
        } else {
            throw std::invalid_argument("Output filename not set");
        }

        GraphGenerator graphGenerator(edges, vertices);
        Graph graph = graphGenerator.generate();
        graph.saveToFile(outputFile);
    } catch (const std::exception& e) {
        std::cout << "Error: " << e.what() << std::endl;
        return -1;
    }
    return 0;
}