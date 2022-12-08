using System;

public class Day17 : Day
{
    public Day17(string inputPath) : base(inputPath)
    {
    }

    public override string FirstTask()
    {
        var input = _inputLoader.LoadStringListInput(_inputPath);

        var cycles = 6;
        var inputSize = input[0].Length;
        var spaceSize = cycles * 4 + inputSize;
        var startingIndex = spaceSize / 2 - 1;
        var space = new char[spaceSize, spaceSize, spaceSize];
        var temp = new char[spaceSize, spaceSize, spaceSize];
        for(int i = 0; i < spaceSize; i++){
            for(int j = 0; j < spaceSize; j++){
                for(int a = 0; a < spaceSize; a++){
                    space[i, j, a] = '.';
                }
            }
        }

        for(int i = startingIndex; i < startingIndex + inputSize; i++){
            for(int j = startingIndex; j < startingIndex + inputSize; j++){
                space[startingIndex, i, j] = input[i - startingIndex][j - startingIndex];
                temp[startingIndex, i, j] = input[i - startingIndex][j - startingIndex];
            }
        }

        for(int c = 0; c < cycles; c++){
            RunACycle(space, temp, spaceSize);
            Copy3DCharrArray(space, temp, spaceSize);
        }

        var counter = 0;
        for(int i = 0; i < spaceSize; i++){
            for(int j = 0; j < spaceSize; j++){
                for(int a = 0; a < spaceSize; a++){
                    if(space[i, j, a] == '#'){
                        counter++;
                    }
                }
            }
        }

        return counter.ToString();
    }

    private void RunACycle(char[,,] space, char[,,] temp, int spaceSize)
    {
        for(int x = 0; x < spaceSize; x++){
            for(int y = 0; y < spaceSize; y++){
                for(int z = 0; z < spaceSize; z++){
                    ChangeState(space, temp, spaceSize, x, y, z);
                }
            }
        }
    }

    private void ChangeState(char[,,] space, char[,,] temp, int spaceSize, int x, int y, int z)
    {
        var activeNeighboursCounter = 0;
        for(int i = -1; i < 2; i++){
            for(int j = -1; j < 2; j++){
                for(int a = -1; a < 2; a++){
                    if(i != 0 || j != 0 || a != 0){
                        activeNeighboursCounter += CheckNeighbour(temp, spaceSize, x + i, y + j, a + z);
                    }
                }
            }
        }

        if(temp[x, y, z] == '#'){
            space[x, y, z] = activeNeighboursCounter == 2 || activeNeighboursCounter == 3 ? '#' : '.';
        }
        else{
            space[x, y, z] = activeNeighboursCounter == 3 ? '#' : '.';
        }
    }

    private int CheckNeighbour(char[,,] space, int spaceSize, int v1, int v2, int v3)
    {
        if(v1 < 0 || v1 >= spaceSize){
            return 0;
        }

        if(v2 < 0 || v2 >= spaceSize){
            return 0;
        }

        if(v3 < 0 || v3 >= spaceSize){
            return 0;
        }

        return space[v1, v2, v3] == '#' ? 1 : 0;
    }

    private void Copy3DCharrArray(char[,,] source, char[,,] dest, int spaceSize){
        for(int i = 0; i < spaceSize; i++){
            for(int j = 0; j < spaceSize; j++){
                for(int a = 0; a < spaceSize; a++){
                    dest[i, j, a] = source[i, j, a];
                }
            }
        }
    }

    public override string SecondTask()
    {
        var input = _inputLoader.LoadStringListInput(_inputPath);

        var cycles = 6;
        var inputSize = input[0].Length;
        var spaceSize = cycles * 4 + inputSize;
        var startingIndex = spaceSize / 2 - 1;
        var space = new char[spaceSize, spaceSize, spaceSize, spaceSize];
        var temp = new char[spaceSize, spaceSize, spaceSize, spaceSize];
        for(int i = 0; i < spaceSize; i++){
            for(int j = 0; j < spaceSize; j++){
                for(int a = 0; a < spaceSize; a++){
                    for(int b = 0; b < spaceSize; b++){
                        space[i, j, a, b] = '.';
                    }
                }
            }
        }

        for(int i = startingIndex; i < startingIndex + inputSize; i++){
            for(int j = startingIndex; j < startingIndex + inputSize; j++){
                space[startingIndex, startingIndex, i, j] = input[i - startingIndex][j - startingIndex];
                temp[startingIndex, startingIndex, i, j] = input[i - startingIndex][j - startingIndex];
            }
        }

        for(int c = 0; c < cycles; c++){
            RunACycle4(space, temp, spaceSize);
            Copy4DCharrArray(space, temp, spaceSize);
        }

        var counter = 0;
        for(int i = 0; i < spaceSize; i++){
            for(int j = 0; j < spaceSize; j++){
                for(int a = 0; a < spaceSize; a++){
                    for(int b = 0; b < spaceSize; b++){                  
                        if(space[i, j, a, b] == '#'){
                            counter++;
                        }
                    }
                }
            }
        }

        return counter.ToString();
    }

    private void Copy4DCharrArray(char[,,,] source, char[,,,] dest, int spaceSize)
    {
        for(int i = 0; i < spaceSize; i++){
            for(int j = 0; j < spaceSize; j++){
                for(int a = 0; a < spaceSize; a++){
                    for(int b = 0; b < spaceSize; b++){
                        dest[i, j, a, b] = source[i, j, a, b];
                    }
                }
            }
        }
    }

    private void RunACycle4(char[,,,] space, char[,,,] temp, int spaceSize)
    {
        for(int x = 0; x < spaceSize; x++){
            for(int y = 0; y < spaceSize; y++){
                for(int z = 0; z < spaceSize; z++){
                    for(int k = 0; k < spaceSize; k++){
                        ChangeState4(space, temp, spaceSize, x, y, z, k);
                    }
                }
            }
        }
    }

    private void ChangeState4(char[,,,] space, char[,,,] temp, int spaceSize, int x, int y, int z, int k)
    {
        var activeNeighboursCounter = 0;
        for(int i = -1; i < 2; i++){
            for(int j = -1; j < 2; j++){
                for(int a = -1; a < 2; a++){
                    for(int b = -1; b < 2; b++){
                        if(i != 0 || j != 0 || a != 0 || b != 0){
                            activeNeighboursCounter += CheckNeighbour4(temp, spaceSize, x + i, y + j, a + z, k + b);
                        }
                    }
                }
            }
        }

        if(temp[x, y, z, k] == '#'){
            space[x, y, z, k] = activeNeighboursCounter == 2 || activeNeighboursCounter == 3 ? '#' : '.';
        }
        else{
            space[x, y, z, k] = activeNeighboursCounter == 3 ? '#' : '.';
        }
    }

    private int CheckNeighbour4(char[,,,] space, int spaceSize, int v1, int v2, int v3, int v4)
    {

        if(v1 < 0 || v1 >= spaceSize){
            return 0;
        }

        if(v2 < 0 || v2 >= spaceSize){
            return 0;
        }

        if(v3 < 0 || v3 >= spaceSize){
            return 0;
        }

        if(v4 < 0 || v4 >= spaceSize){
            return 0;
        }

        return space[v1, v2, v3, v4] == '#' ? 1 : 0;
    }
}