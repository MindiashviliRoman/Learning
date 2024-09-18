#include <iostream>
#include <vector>
#include <algorithm>

// алгоритм должен работать с forward итераторами
// и возвращать итератор, который потом будет передан
// в метод erase соответствующего контейнера
template<class FwdIt>
FwdIt remove_nth(FwdIt p, FwdIt q, size_t n)
{
    auto m = 0;
    FwdIt i2 = p; 
    for(auto i = p; i != q; ++i)
    {
        ++i2;        
        if(m >= n)
        {
            if(i2 == q)
                return i;
            
            *i = *i2;
        }   
        ++m;
    }
    return q;
}

int main()
{

    std::vector<int> v = {0,1,2,3,4,5,6,7,8,9,10};
    v.erase(remove_nth(v.begin(), v.end(), 11), v.end());

    std::cout << std::endl;
    for(auto x: v)
    {
        std::cout << x;
    }
    return 0;
}