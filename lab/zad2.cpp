#include "zad2.h"

#include <iomanip>
#include <iostream>

using namespace std;

int zad2::recursive_solve(int x, int y, int current_move)
{
    if (current_move == ORIG_SIZE * ORIG_SIZE + 1)
        return 1;

    if (current_move == 1)
    {
        board[y][x] = 1;
        current_move++;
    }
    for (int k = 0; k < 8; k++)
    {
        int next_x = x + move_x[k];
        int next_y = y + move_y[k];
        if (board[next_y][next_x] == 0)
        {
            board[next_y][next_x] = current_move;
            if (recursive_solve(next_x, next_y, current_move + 1) == 1)
                return 1;

            // backtracking
            board[next_y][next_x] = 0;
        }
    }
    return 0;
}

void zad2::start(int start_x, int start_y)
{
    if (recursive_solve(start_x, start_y, 1) == 0)
    {
        cout << "Solution does not exist";
    }
    else
        print_board();
}

void zad2::print_board()
{
    for (int x = 0; x < FAKE_SIZE; x++)
    {
        for (int y = 0; y < FAKE_SIZE; y++)
            cout << setw(2) << board[x][y] << " ";
        cout << endl;
    }
}

zad2::zad2(int start_x, int start_y)
{
    int diff = 2;
    for (int i = 0; i < FAKE_SIZE; ++i)
    {
        for (int j = 0; j < FAKE_SIZE; ++j)
        {
            if (i < diff || i >= FAKE_SIZE - diff || j < diff || j >= FAKE_SIZE - diff)
                board[i][j] = -1;
        }
    }

    start(start_x + diff, start_y + diff);
}
