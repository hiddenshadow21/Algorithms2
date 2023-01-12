#pragma once

#define ORIG_SIZE 5
#define FAKE_SIZE (ORIG_SIZE + 4)

class zad2
{
private:
    int move_x[8] = {  1, 2, 2, 1, -1, -2, -2, -1 };
    int move_y[8] = { -2, -1, 1, 2, 2, 1, -1, -2 };
    int board[FAKE_SIZE][FAKE_SIZE] = {{0}};
    int recursive_solve(int x, int y, int current_move);
    void start(int start_x, int start_y);
    void print_board();

public:
    zad2(int start_x, int start_y);
};
