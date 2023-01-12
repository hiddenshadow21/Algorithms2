#pragma once
#include <string>

class zad1
{
private:
    static void combination_util(std::string s, int length, int number,
                                 int index, char current_combination[], int i);
    static void variation_util(std::string s, int length, int number,
                                 int index, char current_variation[]);
public:
    static void get_all_permutations(std::string& a, int permutations_made, int length);
    static void get_all_combinations(std::string& s, int length, int number);
    static void get_all_variations(std::string& s, int length, int number);
};
