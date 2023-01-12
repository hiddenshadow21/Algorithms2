#include "zad3.h"

#include <iomanip>
#include <iostream>

using namespace std;

bool zad3::is_available(int x, int y)
{
    return row[y] == 0 && nw[x - y + size] == 0 && ne[x + y] == 0;
}

int zad3::recursive_solve(int current_move)
{
    if (current_move == size)
        return 1;

    for (int i = 0; i < size; ++i)
    {
        if (is_available(current_move, i))
        {
            col[current_move] = i;
            row[i] = 1;
            nw[current_move - i + size] = 1;
            ne[current_move + i] = 1;

            if (recursive_solve(current_move + 1) == 1)
                return 1;

            col[current_move] = -1;
            row[i] = 0;
            nw[current_move - i + size] = 0;
            ne[current_move + i] = 0;
        }
    }
    return 0;
}

void zad3::print_solution()
{
    for (int i = 0; i < size; i++)
    {
        auto it = find(col.begin(), col.end(), i);
        int index = it - col.begin();
        cout << setfill('0') << setw(index + 1) << "X" << right << setw(8 - index) << "" << endl;
    }
}

zad3::zad3(int board_size)
{
    size = board_size;
    col = vector<int>(size);
    row = vector<int>(size);
    nw = vector<int>(2 * size - 1);
    ne = vector<int>(2 * size - 1);

    if (recursive_solve(0) == 1)
        print_solution();
    else
        cout << "Solution does not exist" << endl;
}
