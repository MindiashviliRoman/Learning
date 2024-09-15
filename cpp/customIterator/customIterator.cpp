#include <vector>
#include <list>
#include <iterator>

#include <iostream>

#include <cassert>
#include <string>
//extern int n = 0;

template<class T>
class VectorList
{
private:
    using VectT = std::vector<T>;
    using ListT = std::list<VectT>;

public:
    using value_type = const T;

    VectorList() = default;
    VectorList(VectorList const&) = default;
    VectorList(VectorList&&) = default;

    VectorList& operator=(VectorList&&) = default;
    VectorList& operator=(VectorList const&) = default;

    // метод, который будет использоваться для заполнения VectorList
    // гарантирует, что в списке не будет пустых массивов
    template<class It>
    void append(It p, It q) // определена снаружи
    {
        if (p != q)
            data_.push_back(VectT(p, q));
    }

    bool empty() const 
    { 
        return data_.begin() == data_.end(); 
    }

    // определите метод size
    size_t size() const
    {
        size_t sz = 0;
        for (auto const& v : data_)
        {
            sz += v.size();
        }
        return sz;
    }


    // определите const_iterator
    struct const_iterator : std::iterator<
        std::bidirectional_iterator_tag, //iterator_category
        const T,                               //value_type
        std::ptrdiff_t,                       //difference_type
        const T*,                              //pointer
        const T&                         //reference
    >
    {
        typedef typename std::vector<T>::const_iterator V_iterator;
        typedef typename std::list<std::vector<T>>::const_iterator L_iterator;

        const_iterator() = default;

        const_iterator(const ListT* data, const L_iterator& l_it, const V_iterator& v_it)
            :
            _data(data)
        {
            It_L = l_it;
            It_V = v_it;
        }

        const T& operator *() const
        {
            if(_data->size() == 0)
                return *It_V;

            for (auto i = It_L->cbegin(); i != It_L->cend(); ++i)
            {
                if(i == It_V)
                {
                    return *i;
                }
            }
            return *It_V;
        }

        const T* operator->() const
        {
            if (_data->size() == 0)
                return &(*It_V);

            for (auto i = It_L->cbegin(); i != It_L->cend(); ++i)
            {
                if (i == It_V)
                {
                    return &(*i);
                }
            }
            return &(*It_V);
        }

        const_iterator& operator++()
        {
            if (It_V == It_L->cend())
                return *this;

            if (++It_V == It_L->cend())
            {
                if (It_L != (--_data->cend()))
                {
                    ++It_L;
                    It_V = It_L->cbegin();
                }
            }
            return *this;
        }

        const_iterator operator++(int)
        {
            const_iterator copy = *this;
            ++(*this);
            return copy;
        }

        const_iterator& operator--()
        {
            if (It_V != It_L->cbegin())
            {
                --It_V;
            }
            else
            {
                if (It_L != _data->cbegin())
                {
                    --It_L;
                    It_V = --(It_L->cend());
                }
            }
            return *this;
        }

        const_iterator operator--(int)
        {
            const_iterator copy = *this;
            --(*this);
            return copy;
        }

        bool operator == (const const_iterator& b) const
        {
            //Если коллекция пуста, то begin() == end() !!!!!
            if (_data == nullptr && b._data == nullptr)
                return true;

            return (this->It_L == b.It_L) && this->It_V == b.It_V;
        }

        bool operator != (const const_iterator& b) const
        {
            if (_data == nullptr)
                return false;

            auto end = this->_data->cend();
            if (this->It_L == end && b.It_L == end)
                return false;

            if (this->It_L != b.It_L)
                return true;

            return this->It_V != b.It_V;
        }

    private:
        const ListT* _data;
        L_iterator It_L;
        V_iterator It_V;
    };

    // определите методы begin / end
    const_iterator begin() const
    {
        if(empty())
            return const_iterator();

        return const_iterator(&data_, data_.cbegin(), data_.cbegin()->cbegin());
    }
    const_iterator end() const
    {
        if (empty())
            return const_iterator();

            auto prevL = --data_.cend();
            return const_iterator(&data_, prevL, prevL->cend());
    }

    // определите const_reverse_iterator
    using const_reverse_iterator = std::reverse_iterator<const_iterator>;

    // определите методы rbegin / rend
    const_reverse_iterator rbegin() const
    {
        auto it = end();
        return const_reverse_iterator{ it };
    }
    const_reverse_iterator rend() const
    {
        auto it = begin();
        return const_reverse_iterator{ it };
    }

private:
    ListT data_;
};

int main()
{
    const VectorList<int> check_const_compile;
    auto check_const_equals_compile = check_const_compile.begin();
    check_const_equals_compile == check_const_equals_compile;

    VectorList<std::vector<int>> check_arrow_vl;
    std::vector<int> vint1 = { 1, 2, 3 };
    std::vector<int> vint2 = { 4, 5, 6 };
    std::vector<std::vector<int>> vecs;
    vecs.push_back(vint1);
    vecs.push_back(vint2);
    check_arrow_vl.append(vecs.begin(), vecs.end());
    auto it_f = check_arrow_vl.begin();
    assert(it_f->size() == 3);

    VectorList<char> vec_list;

    std::vector<char> v1;
    v1.push_back('A');
    v1.push_back('B');
    v1.push_back('C');
    vec_list.append(v1.begin(), v1.end());

    std::vector<char> v2;
    v2.push_back('D');
    v2.push_back('E');
    v2.push_back('F');
    v2.push_back('G');
    vec_list.append(v2.begin(), v2.end());

    assert(vec_list.size() == 7);
    assert(std::distance(vec_list.begin(), vec_list.end()) == 7);

    auto it = vec_list.begin();
    assert(*it == 'A'); ++it;
    assert(*it == 'B'); ++it;
    assert(*it == 'C'); ++it;
    assert(*it == 'D'); ++it;
    assert(*it == 'E'); ++it;
    assert(*it == 'F'); ++it;
    assert(*it == 'G'); ++it;
    assert(it == vec_list.end());

    it = vec_list.begin();
    assert(*it == 'A'); it++;
    assert(*it == 'B'); it++;
    assert(*it == 'C'); it++;
    assert(*it == 'D'); it++;
    assert(*it == 'E'); it++;
    assert(*it == 'F'); it++;
    assert(*it == 'G'); it++;

    it = vec_list.end(); it--;
    assert(*it == 'G'); it--;
    assert(*it == 'F'); it--;
    assert(*it == 'E'); it--;
    assert(*it == 'D'); it--;
    assert(*it == 'C'); it--;
    assert(*it == 'B'); it--;
    assert(*it == 'A');

    it = vec_list.end(); --it;
    assert(*it == 'G'); --it;
    assert(*it == 'F'); --it;
    assert(*it == 'E'); --it;
    assert(*it == 'D'); --it;
    assert(*it == 'C'); --it;
    assert(*it == 'B'); --it;
    assert(*it == 'A');

    auto r_it = vec_list.rbegin();
    assert(*r_it == 'G'); r_it++;
    assert(*r_it == 'F'); r_it++;
    assert(*r_it == 'E'); r_it++;
    assert(*r_it == 'D'); r_it++;
    assert(*r_it == 'C'); r_it++;
    assert(*r_it == 'B'); r_it++;
    assert(*r_it == 'A'); r_it++;
    assert(r_it == vec_list.rend());

    r_it = vec_list.rbegin();
    assert(*r_it == 'G'); ++r_it;
    assert(*r_it == 'F'); ++r_it;
    assert(*r_it == 'E'); ++r_it;
    assert(*r_it == 'D'); ++r_it;
    assert(*r_it == 'C'); ++r_it;
    assert(*r_it == 'B'); ++r_it;
    assert(*r_it == 'A'); ++r_it;
    assert(r_it == vec_list.rend());

    r_it = vec_list.rend(); --r_it;
    assert(*r_it == 'A'); --r_it;
    assert(*r_it == 'B'); --r_it;
    assert(*r_it == 'C'); --r_it;
    assert(*r_it == 'D'); --r_it;
    assert(*r_it == 'E'); --r_it;
    assert(*r_it == 'F'); --r_it;
    assert(*r_it == 'G');

    r_it = vec_list.rend(); r_it--;
    assert(*r_it == 'A'); r_it--;
    assert(*r_it == 'B'); r_it--;
    assert(*r_it == 'C'); r_it--;
    assert(*r_it == 'D'); r_it--;
    assert(*r_it == 'E'); r_it--;
    assert(*r_it == 'F'); r_it--;
    assert(*r_it == 'G');

    VectorList<int> check_empty;
    assert(check_empty.size() == 0);
    assert(check_empty.begin() == check_empty.end());
    assert(check_empty.rbegin() == check_empty.rend());

    return 0;
}


/*
int main()
{
    VectorList<int> vlist;    

    std::vector<int> expectedItems = { 1,2,3,4,5,6,7 };

    std::vector<int> v6;
    v6.push_back(1);
    v6.push_back(2);
    v6.push_back(3);

    std::vector<int> v2;
    v2.push_back(4);
    v2.push_back(5);
    v2.push_back(6);
    v2.push_back(7);
    vlist.append(v6.begin(), v6.end());
    vlist.append(v2.begin(), v2.end());
    
    //Initial test group
    assert(vlist.size() == 7);
    assert(*vlist.begin() == 1);
    auto dist = std::distance(vlist.begin(), vlist.end());
    assert(std::distance(vlist.begin(), vlist.end()) == 7);

    assert(std::equal(vlist.begin(), vlist.end(), expectedItems.begin()));

    VectorList<int> vlistEmpty;
    assert(std::distance(vlistEmpty.begin(), vlistEmpty.end()) == 0);


    const VectorList<int>::const_iterator ci_list = vlist.begin();
    VectorList<int>::const_iterator i_list = vlist.begin();

    if (ci_list == i_list)
    {
        std::cout << "ci_list == i_list, const iterator comparison works fine!\n";
    }
    else
    {
        std::cout << "ci_list == i_list, const iterator comparison not works!\n";
    }

    typename VectorList<int>::const_iterator itt1 = vlist.begin();
    typename VectorList<int>::const_reverse_iterator ritt(itt1);
    typename VectorList<int>::const_iterator itt2 = ritt.base();
    if (itt1 == itt2)
        std::cout << "base() works fine" << std::endl;
    else
        std::cout << "base() doesn't work as expected" << std::endl;

    std::cout << "Test i++" << std::endl;
    for (auto i = vlist.begin(); i != vlist.end(); i++)
        std::cout << *i << " ";

    std::cout << "Test ++i" << std::endl;
    for (auto i = vlist.begin(); i != vlist.end(); ++i)
        std::cout << *i << " ";

    std::cout << std::endl;
    std::cout << std::endl;

    std::cout << "Test --i" << std::endl;
    for (auto i = vlist.end(); i != vlist.begin();)
        std::cout << *--i << " ";
    std::cout << std::endl;

    std::cout << "Test i--" << std::endl;
    for (auto i = vlist.end(); i != vlist.begin();)
    {
        i--;
        std::cout << *i << " ";
    }
    std::cout << std::endl;


    std::cout << std::endl;
    auto j = vlist.rbegin();
    std::cout << "rbegin is " << *j << std::endl;
    j = --vlist.rend();
    std::cout << "--rend is " << *j << std::endl;

    std::cout << "Test reverse_const_iterator ++" << std::endl;
    for (j = vlist.rbegin(); j != vlist.rend(); ++j)
        std::cout << *j << " ";
    std::cout << std::endl;

    auto it1 = vlist.begin();
    for (; it1 != vlist.end(); ++it1)
        std::cout << *it1 << ' ';

    std::cout << std::endl;
    std::cout << *--it1 << ' ';
    std::cout << *--it1 << ' ';
    std::cout << *--it1 << ' ';
    std::cout << *--it1 << ' ';

    std::cout << "One element test" << std::endl;
    VectorList<int> vlistOneElement;
    std::vector<int> vOne;
    vOne.push_back(1);
    vlistOneElement.append(vOne.begin(), vOne.end());

    auto it3 = vlistOneElement.begin();
    for (; it3 != vlistOneElement.end(); ++it3)
        std::cout << *it3 << ' ';

    VectorList<int> vListEmpty;
    auto it4 = vListEmpty.begin();
    for (; it4 != vListEmpty.end(); ++it4)
        std::cout << *it4 << ' ';

    {
        VectorList<int> vListSingleItem;
        std::vector<int> vOneelemtn;
        vOneelemtn.push_back(1);
        vOneelemtn.push_back(2);
        vListSingleItem.append(vOneelemtn.begin(), vOneelemtn.end());
        vListSingleItem.append(vOneelemtn.begin(), vOneelemtn.end());

        std::vector<int> tmp;


        auto itcontaint = vListSingleItem.begin();
        for (; itcontaint != vListSingleItem.end(); ++itcontaint)
            std::cout << *itcontaint << ' ';

        --itcontaint;
        std::cout << *itcontaint << ' ';
        --itcontaint;
        std::cout << *itcontaint << ' ';
        --itcontaint;
        std::cout << *itcontaint << ' ';
        --itcontaint;
        std::cout << *itcontaint << ' ';
        --itcontaint;
        std::cout << *itcontaint << ' ';

    }

    {


        std::vector<std::string> v1 = { "one", "two", "three" };
        std::vector<std::string> v2 = { "four", "five", "six", "seven", "eight" };
        std::vector<std::string> v3 = { "nine", "ten", "eleven", "twelve" };
        std::vector<std::string> v4 = {};
        for (int k = 13; k <= 30; ++k)
        {
            v4.push_back(std::to_string(k) + "-th");
        }
        VectorList<std::string> vl;
        std::cout << std::endl;
        std::cout << "empty distance = " << std::distance(vl.rbegin(), vl.rend()) << std::endl;


        vl.append(v1.begin(), v1.end());

        vl.append(v2.begin(), v2.end());

        vl.append(v3.begin(), v3.end());

        vl.append(v4.begin(), v4.end());
        VectorList<std::string>::const_iterator it = vl.begin();
        for (; it != vl.end(); ++it)
        {
            std::string a = *it;
            std::cout << a << " ";
        }
        std::cout << std::endl;
        std::cout << "distance = " << std::distance(vl.begin(), vl.end()) << std::endl;

        VectorList<std::string>::const_iterator eit = vl.end();
        for (; eit != vl.begin();)
        {
            std::string a = *(--eit);
            std::cout << a << " ";
        }std::cout << std::endl;
        VectorList<std::string>::const_reverse_iterator rit = vl.rend();
        for (; rit != vl.rend(); ++rit)
        {
            std::string a = *rit;   std::cout << a << " ";
        }
        std::cout << std::endl; std::cout << "reverse distance = " << std::distance(vl.rbegin(), vl.rend()) << std::endl;
        VectorList<std::string>::const_reverse_iterator erit = vl.rend();
        for (; erit != vl.rbegin();)
        {
            std::string a = *(--erit);   std::cout << a << " ";
        }
        std::cout << std::endl;
        VectorList<std::string>::const_iterator i = vl.rend().base();
        for (; i != vl.end(); ++i)
        {
            std::string a = *i;
            std::cout << a << " ";
        }
        std::cout << std::endl;
    }
    return 0;
}
*/

/*
int main()
{
    VectorList<char> vlist;

    std::vector<char> v1;
    //v1.push_back('A');
    //v1.push_back('B');
    //v1.push_back('C');
    v1.begin();
    v1.end();
    std::vector<char> v2;
    v2.push_back('D');
    v2.push_back('E');
    v2.push_back('F');
    v2.push_back('G');
    vlist.append(v1.begin(), v1.end());
    vlist.append(v2.begin(), v2.end());

    std::cout << "size: " << vlist.size() << std::endl;

    auto i = vlist.begin();

    //std::cout << "Size is " << vlist.size() << std::endl;
    //std::cout << "begin is " << *i << std::endl;
    //std::cout << "std::distance(begin,end)﻿ " << (std::distance(vlist.begin(), vlist.end())) << std::endl;
    //std::cout << "*(++begin) == 'B'? " << (*++vlist.begin() == 'B') << std::endl;
    //std::cout << "*(++begin) == 'A'? " << (*++vlist.begin() == 'A') << std::endl;
    //std::cout << std::endl;


    std::cout << "Test ++i" << std::endl;

    //for (auto i : vlist)
    //    std::cout << i << " ";

    for (i = vlist.begin(); i != vlist.end(); ++i)
        std::cout << *i << " ";
    std::cout << std::endl;
    std::cout << std::endl;

    std::cout << "Test i++" << std::endl;
    for (i = vlist.begin(); i != vlist.end(); i++)
        std::cout << *i << " ";
    std::cout << std::endl;
    std::cout << std::endl;

    std::cout << "Test --i" << std::endl;
    for (i = vlist.end(); i != vlist.begin();)
        std::cout << *--i << " ";
    std::cout << std::endl;
    std::cout << std::endl;

    std::cout << "Test i--" << std::endl;
    for (i = vlist.end(); i != vlist.begin();) {
        i--;
        std::cout << *i << " ";
    }
    std::cout << std::endl;
    std::cout << std::endl;

    std::vector<int> tmp;
    auto t = tmp.rbegin();

    std::cout << "TEST is " << *t << std::endl;

    std::cout << std::endl;
    auto j = vlist.rbegin();
    std::cout << "rbegin is " << *j << std::endl;
    j = --vlist.rend();
    std::cout << "--rend is " << *j << std::endl;

    std::cout << "Test reverse_const_iterator ++" << std::endl;
    for (j = vlist.rbegin(); j != vlist.rend(); ++j)
        std::cout << *j << " ";
    std::cout << std::endl;

    system("pause");
    return 0;
}
*/