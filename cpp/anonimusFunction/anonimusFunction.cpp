#include <iostream>
#include <cmath>

template<class F>
int * find_if(int * p, int * q, F f)
{
    for ( ; p != q; ++p )
        if (f(*p))
            return p;
    return q;
}

auto gen_finder = [](int * p, int * q) { return [&p, &q](int const & k) -> bool { 
                                                                                int * startP = p;
                                                                                for(; p != q; ++p)
                                                                                {
                                                                                    if(*p == k)
                                                                                    {
                                                                                        return true;
                                                                                    }
                                                                                }
                                                                                p = startP;
                                                                                return false;
                                                                            }; };

int main( )
{
    int primes[5] = {2,3,5,7,11};

    int m[10] = {0,0,1,1,4,6,7,8,9,10};

    // first_prime будет указывать на число 7
    int * first_prime = find_if(m, m + 10, gen_finder(primes, primes + 5));
    std::cout << "Result: " << *first_prime << std::endl;

    return 0;
}
