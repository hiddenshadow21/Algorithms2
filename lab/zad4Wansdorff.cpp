#include "zad4Wansdorff.h"

#include <iomanip>
#include <iostream>
#include <cstdlib>

using namespace std;

void zad4_wansdorff::add_value_to_neighbours(int x, int y, int value)
{
    for (int i = 0; i < 8; ++i)
    {
        if (neighbours_count[y + move_y[i]][x + move_x[i]] != INT_MAX)
            neighbours_count[y + move_y[i]][x + move_x[i]] += value;
    }
}

int zad4_wansdorff::get_lowest_neighbour(int x, int y)
{
    int lowestIndex = -1;
    int lowestCount = 9;
    int random = rand();
    for (int i = 0; i < 8; ++i)
    {
        int index = (i + random) % 8;
        int count = neighbours_count[y + move_y[index]][x + move_x[index]];
        if (board[y + move_y[index]][x + move_x[index]] == 0 && lowestCount > count)
        {
            lowestCount = count;
            lowestIndex = index;
        }
    }

    return lowestIndex;
}

int zad4_wansdorff::solve(int x, int y)
{
    int current_move = 1;
    board[y][x] = current_move;
    add_value_to_neighbours(x, y, -1);
    current_move++;

    while (current_move <= ORIG_SIZE * ORIG_SIZE)
    {
        int lowest_neighbour = get_lowest_neighbour(x, y);
        if (lowest_neighbour == -1)
            return 0;

        x = x + move_x[lowest_neighbour];
        y = y + move_y[lowest_neighbour];
        board[y][x] = current_move;
        add_value_to_neighbours(x, y, -1);
        current_move++;
    }

    return 1;
}

int zad4_wansdorff::get_available_moves(int x, int y)
{
    int count = 0;

    for (int i = 0; i < 8; ++i)
    {
        if (board[y + move_y[i]][x + move_x[i]] >= 0)
            count++;
    }

    return count;
}

void zad4_wansdorff::reset_board()
{
    for (int k = 0; k < FAKE_SIZE; ++k)
    {
        for (int l = 0; l < FAKE_SIZE; ++l)
        {
            if (k < shift || k >= FAKE_SIZE - shift || l < shift || l >= FAKE_SIZE - shift)
            {
                board[k][l] = -1;
                neighbours_count[k][l] = INT_MAX;
            }
            else
            {
                neighbours_count[k][l] = get_available_moves(l, k);
                board[k][l] = 0;
            }
        }
    }
}

void zad4_wansdorff::print_board()
{
    for (int x = 0; x < FAKE_SIZE; x++)
    {
        for (int y = 0; y < FAKE_SIZE; y++)
            cout << setw(2) << board[x][y] << " ";
        cout << endl;
    }
}

zad4_wansdorff::zad4_wansdorff()
{
    srand(time(NULL));

    for (int i = 0; i < ORIG_SIZE; ++i)
    {
        for (int j = 0; j < ORIG_SIZE; ++j)
        {
            reset_board();

            if (solve(j + shift, i + shift) == 1)
            {
                print_board();
                return;
            }

            cout << "Solution not found starting from " << i << "," << j << endl;
        }
    }
}
