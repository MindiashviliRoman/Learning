
#include <iostream>



template<class T, int N>
int array_size(const T (&arr)[N])
{
    return N;
}

int main()
{
    int ints[] = {1, 2, 3, 4};
    int *iptr = ints;
    double doubles[] = {3.14};
    std::cout << array_size(ints) << std::endl; // вернет 4
    std::cout << array_size(doubles) << std::endl; // вернет 1
    //std::cout << array_size(iptr) << std::endl; // тут должна произойти ошибка компиляции
    return 0;
}