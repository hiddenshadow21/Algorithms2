#include "zad1.h"
#include "zad2.h"
#include "zad3.h"
#include "zad4Wansdorff.h"


int main(int argc, char* argv[])
{
    std::string s = "abcd";
    //zad1::get_all_permutations(s, 0, s.length()-1);
    //zad1::get_all_combinations(s, s.length(), 3);
    //zad1::get_all_variations(s, s.length(), 3);
    //zad2(0,0);
    //zad3(9);
    zad4_wansdorff();
    return 0;
}
