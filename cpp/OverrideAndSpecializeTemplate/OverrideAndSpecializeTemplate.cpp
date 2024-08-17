
#include <iostream>

using namespace std;
template<class T>
void SomeFunc(T a, T b) //Some function 1
{
    cout << "some function" << endl;
}

template<class T, class D>
void SomeFunc(T a, D b) //Some function 2
{
    cout << "overrided some function" << endl;
}

template<> //full specialize of template (Some function 2)
void SomeFunc<int, int>(int a, int b)
{
    cout << "Some function with both int parameters" << endl;
}

int main()
{
    //Called Some function 1. Couse in the first was selected overriding of functions and after - specializing.
    //After overriding function - was selected some function 1 and it have not specializings.
    SomeFunc(1, 2);

    return 0;
}