#pragma once

#define ORIG_SIZE 210
#define FAKE_SIZE (ORIG_SIZE + 4)


class zad4_wansdorff
{
private:
    int shift = 2;
    int move_x[8] = {  1, 2, 2, 1, -1, -2, -2, -1 };
    int move_y[8] = { -2, -1, 1, 2, 2, 1, -1, -2 };
    int board[FAKE_SIZE][FAKE_SIZE] = {{0}};
    int neighbours_count[FAKE_SIZE][FAKE_SIZE] = {{0}};
    void add_value_to_neighbours(int x, int y, int value);
    int get_lowest_neighbour(int x, int y);
    int solve(int x, int y);
    int get_available_moves(int x, int y);
    void reset_board();
    void print_board();

public:
    zad4_wansdorff();
};
