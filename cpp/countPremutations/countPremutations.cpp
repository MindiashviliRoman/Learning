#include <iostream>
#include <algorithm>
#include <numeric>
#include <array>
#include <vector>
#include <cassert>

/*
* Напишите шаблонную функцию count_permutations, которая принимает некоторую последовательность и 
вычисляет количество перестановок этой последовательности (равные последовательности считаются одной перестановкой), 
в которых нет подряд идущих одинаковых элементов.
*/

template<class Iterator>
size_t count_permutations(Iterator p, Iterator q)
{
    using T = typename std::iterator_traits<Iterator>::value_type;
    std::vector<T> v;
    for (auto i = p; i != q; ++i)
    {
        v.push_back(*i);
    }
    std::sort(v.begin(), v.end());

    int n = 0;
    do
    {
        if (std::adjacent_find(v.begin(), v.end()) == v.end())
        {
            ++n;
        }
    } while (std::next_permutation(v.begin(), v.end()));
    return n;
}

int main()
{
    std::array<int, 3> a1 = { 1,2,3 };
    size_t c1 = count_permutations(a1.begin(), a1.end()); // 6
    std::cout << c1 << std::endl;

    std::array<int, 5> a2 = { 1,2,3,4,4 };
    size_t c2 = count_permutations(a2.begin(), a2.end()); // 36
    std::cout << c2 << std::endl;

    {
        const std::array<int, 3> a1 = { 1,2,3 };
        const size_t c1 = count_permutations(a1.begin(), a1.end()); // 6
        assert(c1 == 6);
    }
    {
        const std::array<int, 5> a2 = { 1,2,3,4,4 };
        const size_t c2 = count_permutations(a2.begin(), a2.end()); // 36
        assert(c2 == 36);
    }
    {
        const std::array<int, 3> a3 = { 1, 2, 1 };
        const size_t c3 = count_permutations(a3.begin(), a3.end()); // 1
        assert(c3 == 1);
    }
    {
        const std::array<int, 3> a4 = { 1, 1, 1 };
        const size_t c4 = count_permutations(a4.begin(), a4.end()); // 0
        assert(c4 == 0);
    }
    {
        const std::array<int, 3> a5 = { 3, 2, 1 };
        const size_t c5 = count_permutations(a5.begin(), a5.end()); // 6
        assert(c5 == 6);
    }
    {
        const std::array<int, 3> a6 = { 2, 1, 1 };
        const size_t c6 = count_permutations(a6.begin(), a6.end()); // 1
        assert(c6 == 1);
    }
    {
        const std::array<int, 1> a7 = {};
        const size_t c7 = count_permutations(a7.begin(), a7.end()); // 1
        assert(c7 == 1);
    }
    {
        const std::array<int, 1> a8 = { 1 };
        const size_t c8 = count_permutations(a8.begin(), a8.end()); // 1
        assert(c8 == 1);
    }
    {
        const std::array<int, 2> a9 = { 1, 1 };
        const size_t c9 = count_permutations(a9.begin(), a9.end()); // 0
        assert(c9 == 0);
    }


    return 0;
}