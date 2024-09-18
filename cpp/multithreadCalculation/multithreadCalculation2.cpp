#include<future>
#include<thread>
#include<list>
#include<vector>
#include<iostream>
#include <cassert>

// заголовок <future> уже подключён.
// заново подключать не нужно


template<typename T, typename It, typename Func1, typename Func2>
void map_reduceForOneThread(std::vector<T> & results, It p, It q, Func1 f1, Func2 f2)
{
    using result2_type = decltype(f2(f1(*p), f1(*p)));
    auto res = f1(*p);
    while (++p != q)
        res = f2(res, f1(*p));

    results.push_back(std::move(res));
}

// реализация функции mapreduce
template<typename It, typename Func1, typename Func2>
auto map_reduce(It p, It q, Func1 f1, Func2 f2, size_t thCount) -> decltype(f2(f1(*p), f1(*p)))
{
    using result2_type = decltype(f2(f1(*p), f1(*p)));

    auto n = 0;
    for (auto i = p; i != q; ++i)
    {
        ++n;
    }

    auto loadForThread = n / thCount;
    auto mod = n % thCount;

    std::vector<std::thread> calculatingThreads;
    std::vector<result2_type> results;// (thCount);
    bool flg = false;
    auto startP = p;
    for (auto i = 0; i < thCount; ++i)
    {
        auto curLast = startP;
        if (i + 1 < thCount)
        {
            for (auto j = 0; j < loadForThread; ++j)
            {
                ++curLast;
            }
        }
        else
        {
            curLast = q;
        }
         
        std::thread t([&results, startP, curLast, f1, f2]() { map_reduceForOneThread<result2_type>(results, startP, curLast, f1, f2); });

        calculatingThreads.push_back(std::move(t));
        startP = curLast;

    }
    calculatingThreads[0].join();
    auto res = results[0];
    for (auto i = 1; i < calculatingThreads.size(); ++i)
    {
        calculatingThreads[i].join();
        res = f2(res, results[i]);
    }
    return res;
}

//template<typename It, typename Func1, typename Func2>
//auto map_reduce(It p, It q, Func1 f1, Func2 f2, size_t thCount) -> decltype(f2(f1(*p), f1(*p)))
//{
//    using result2_type = decltype(f2(f1(*p), f1(*p)));
//
//    auto n = 0;
//    for (auto i = p; i != q; ++i)
//    {
//        ++n;
//    }
//
//    auto loadForThread = n / thCount;
//    auto mod = n % thCount;
//
//    std::vector<std::future<result2_type>> v;
//    bool flg = false;
//    auto startP = p;
//    for (auto i = 0; i < thCount; ++i)
//    {
//        auto curLast = startP;
//        if (i + 1 < thCount)
//        {
//            for (auto j = 0; j < loadForThread; ++j)
//            {
//                ++curLast;
//            }
//        }
//        else
//        {
//            curLast = q;
//        }
//        auto funcForOneThread = [startP, curLast, &f1, &f2]() { return map_reduceForOneThread(startP, curLast, f1, f2); };
//        v.push_back(std::async(std::launch::async, funcForOneThread));
//        startP = curLast;
//    }
//    auto res = v[0].get();
//    for (auto i = 1; i < v.size(); ++i)
//    {
//        res = f2(res, v[i].get());
//    }
//    return res;
//}

int main()
{

    std::list< int > const l1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    // параллельное суммирование в 3 потока
    auto sum = map_reduce(l1.begin(), 
                          l1.end(),
                          [](int i) { return i; },
                          std::plus<int>(), 
                          3);

    std::cout << sum << std::endl;
    assert(sum == 55);

    // проверка наличия чётных чисел в четыре потока
    auto has_even = map_reduce(l1.begin(), 
                               l1.end(),
                               [](int i) { return i % 2 == 0; },
                               std::logical_or<bool>(), 
                               4);

    std::cout << has_even << std::endl;
    assert(has_even == true);

    std::vector< std::string > const v1 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11" };
    for (std::size_t j = 1; j <= v1.size(); ++j)
    {
        auto ssum = map_reduce(v1.begin(), 
                               v1.end(), 
                               [](std::string s) { return s; }, 
                               std::plus< std::string >(), 
                               j);
        std::cout << ssum << std::endl;
        assert(ssum == "1234567891011");
    }

    std::list< int > const l2 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };
    for (int i = 1; i < 20; ++i)
    {
        auto ssum = map_reduce( l2.begin(), 
                               l2.end(),
                               [](int j) { return j; },
                               std::plus< int >(), 
                               i);

        std::cout << ssum << std::endl;
        assert(ssum == 190);
    }
    std::vector< std::string > const v2 = { "multithread", "and", "async", "in", "C++", "is", "total", "sh!t" };

    for (std::size_t i = 1; i <= v2.size(); ++i)
    {
        auto size_sum = map_reduce(v2.begin(), 
                                   v2.end(), 
                                   [](std::string s) { return s.size(); }, 
                                   std::plus<std::size_t>(), 
                                   i);
        std::cout << size_sum << std::endl;
        assert(size_sum == 35);

    }

    return 0;
}