#include "zad1.h"
#include <iostream>

using namespace std;

void zad1::get_all_permutations(string& a, int permutations_made, const int length)
{
    if (permutations_made == length)
        cout << a << endl;
    else {
        for (int i = permutations_made; i <= length; i++) {
            swap(a[permutations_made], a[i]);
            get_all_permutations(a, permutations_made + 1, length);
            swap(a[permutations_made], a[i]);
        }
    }
}

void zad1::combination_util(string s, int length, int number,
                           int index, char current_combination[], int i)
{
    if (index == number)
    {
        for (int j = 0; j < number; j++)
            cout << current_combination[j] << " ";
        cout << endl;
        return;
    }
    
    if (i >= length)
        return;
    
    current_combination[index] = s[i];
    combination_util(s, length, number, index + 1, current_combination, i + 1);
    combination_util(s, length, number, index, current_combination, i + 1);
}

void zad1::get_all_combinations(std::string& s, int length, const int number)
{
    char* current_combination = new char[number];
    combination_util(s, length, number, 0, current_combination, 0);
}

void zad1::variation_util(std::string s, int length, int number, int index, char current_variation[])
{
    if (index == number)
    {
        for (int j = 0; j < number; j++)
            cout << current_variation[j] << " ";
        cout << endl;
        return;
    }
    for (int i = 0; i < length; ++i)
    {
        current_variation[index] = s[i];
        variation_util(s, length, number, index + 1, current_variation);
    }
}

void zad1::get_all_variations(std::string& s, int length, int number)
{
    char* current_combination = new char[number];
    variation_util(s, length, number, 0, current_combination);
}
