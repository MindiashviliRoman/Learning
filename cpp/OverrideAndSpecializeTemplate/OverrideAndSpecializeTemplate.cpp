
#include <iostream>

using namespace std;
template<class T>
void SomeMethod(T a, T b) //Some Method 1
{
    cout << "some method" << endl;
}

template<class T, class D>
void SomeMethod(T a, D b) //Some Method 2
{
    cout << "overrided some method" << endl;
}

template<> //full specialize of template (Some Method 2)
void SomeMethod<int, int>(int a, int b)
{
    cout << "Some method with both int parameters" << endl;
}

int main()
{
    //Called Some method 1. Couse in the first was selected overriding of methods and after - specializing.
    //After overriding method - was selected some method 1 and it have not specializings.
    SomeMethod(1, 2);

    return 0;
}