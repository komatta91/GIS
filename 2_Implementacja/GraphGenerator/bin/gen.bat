for /l %%v in (5, 5, 5000) do (
	@set /A edge = %%v * 5 
	GraphGenerator.exe -e %%v GraphData.txt
	copy GraphData.txt results\%%v_s.txt 
	ConsoleApplication3CSHARP.exe > results\%%v.txt
)