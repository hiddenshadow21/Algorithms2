#pragma once
#include <vector>

class zad3
{
    std::vector<int> col;
    std::vector<int> row;
    std::vector<int> nw;
    std::vector<int> ne;
    int size;
    bool is_available(int x, int y);
    int recursive_solve(int current_move);
    void print_solution();
public:
    zad3(int board_size);
};
