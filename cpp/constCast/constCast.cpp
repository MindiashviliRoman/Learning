
#include <iostream>

int main()
{
    int a = 27;
    int const b = 412;
    int * pa = &a;

    int const c = a;
    int d = b;
    int const * p1 = pa;
    int * const * p2 = &pa;
    int const ** p3 = const_cast<int const **>(&pa); //required
    int const * const * p4 = &pa;

    std::cout << c << std::endl;
    std::cout << d << std::endl;
    std::cout << p1 << std::endl;
    std::cout << p2 << std::endl; 
    std::cout << p3 << std::endl;
    std::cout << p4 << std::endl; 
    
    return 0;
}