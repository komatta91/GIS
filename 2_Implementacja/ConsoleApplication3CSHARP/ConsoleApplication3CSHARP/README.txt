INSTRUKCJA OBSŁUGI PROGRAMU:

1. Program wczytuje dane z pliku .txt
2. Domyślnie brany jest plik o nazwie "GraphData.txt" w tym samym folderze co plik .exe
3. Jako parametr wywołania pliku .exe można przekazac nazwę/ścieżkę do innego pliku
4. Plik zawiera dane o grafie w postaci listy sąsiedztwa
5. Każda linijka pliku odpowiada liście sąsiedztwa jednego dla jednego wierzchołka
6. Wierzchołki na liście są oddzielone przecinkami
7. Pierwsza liczba w danej linijce to numer wierzchołka dla którego podajemy sąsiadów, kolejne to jego sąsiedzi
8. Wierzchołki muszą być oznaczane kolejnymi cyframi poczynając od zera

############################################################################################
Przykładowa zawartość pliku wejściowego dla prostego grafu o 4 wierzchołkach i 4 krawędziach

0,1
1,2
2,3
3,0

Graf taki wygląda następująco:

	1 --------> 2
	/\	    |
	|	    |
	|	    \/
	0 <-------- 3