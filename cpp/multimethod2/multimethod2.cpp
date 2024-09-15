#include <iostream>

#include <typeinfo>
#include <typeindex>
#include <map>
#include <unordered_map>
#include <functional>

#include <cstdint>



// базовый класс фигуры (полиморфный)
struct Shape {
    virtual ~Shape() {}
};

// прямоугольник
struct Rectangle : Shape {


};

// треугольник
struct Triangle : Shape { };

struct Quad : Shape { };

// функция для проверки пересечения двух прямоугольников
int is_intersect_r_r(Shape* a, Shape* b)
{
    return 0;
}

// функция для проверки пересечения прямоугольника и треугольника
int is_intersect_r_t(Shape* a, Shape* b)
{
    return 1;
}

// функция для проверки пересечения прямоугольника и треугольника
int is_intersect_r_q(Shape* a, Shape* b)
{
    return 2;
}

// функция для проверки пересечения прямоугольника и треугольника
int is_intersect_q_r(Shape* a, Shape* b)
{
    return 3;
}

// функция для проверки пересечения прямоугольника и треугольника
int is_intersect_q_t(Shape* a, Shape* b)
{
    return 4;
}
// функция для проверки пересечения прямоугольника и треугольника
int is_intersect_t_t(Shape* a, Shape* b)
{
    return 5;
}

// функция для проверки пересечения прямоугольника и треугольника
int is_intersect_q_q(Shape* a, Shape* b)
{
    return 6;
}


// Base - базовый класс иерархии
// Result - тип возвращаемого значения мультиметода
// Commutative - флаг, который показывает, что
// мультиметод коммутативный (т.е. f(x,y) = f(y,x)).
template<class Base, class Result, bool Commutative>
struct Multimethod2
{
    // устанавливает реализацию мультиметода
    // для типов t1 и t2 заданных через typeid 
    // f - это функция или функциональный объект
    // принимающий два указателя на Base 
    // и возвращающий значение типа Result
    using func = std::function<Result(Base*, Base*)>;
    void addImpl(const std::type_info& t1, const std::type_info& t2, func f)
    {
        auto key1 = std::type_index(t1);
        auto key2 = std::type_index(t2);
        auto it1 = mapF.find(key1);
        if (it1 == mapF.end())
        {
            std::unordered_map<std::type_index, func> tmpMap;
            tmpMap.emplace(key2, f);
            mapF[key1] = tmpMap;
        }
        else
        {
            it1->second.emplace(key2, f);
            //it1->second[key2] = f;
            //if(it1->second.find(key2) == it1->second.end())
            //{
            //    it1->second.emplace(key2, f);
            //}
        }
    }

    // проверяет, есть ли реализация мультиметода
    // для типов объектов a и b
    bool hasImpl(Base* a, Base* b) const
    {
        // возвращает true, если реализация есть
        // если операция коммутативная, то нужно 
        // проверить есть ли реализация для b и а 

        auto key1 = std::type_index(typeid(*a));
        auto key2 = std::type_index(typeid(*b));
        auto it1 = mapF.find(key1);
        if (it1 != mapF.end())
        {
            if (it1->second.find(key2) != it1->second.end())
            {
                return true;
            }
        }

        if (Commutative)
        {
            auto it2 = mapF.find(key2);
            if (it2 != mapF.end())
            {
                if (it2->second.find(key1) != it2->second.end())
                {
                    return true;
                }
            }
        }
        return false;
    }

    // Применяет мультиметод к объектам
    // по указателям a и b
    Result call(Base* a, Base* b) const
    {
        // возвращает результат применения реализации
        // мультиметода к a и b
        auto key1 = std::type_index(typeid(*a));
        auto key2 = std::type_index(typeid(*b));
        auto it1 = mapF.find(key1);
        if (it1 != mapF.end())
        {
            auto iit1 = it1->second.find(key2);
            if (iit1 != it1->second.end())
            {
                return iit1->second(a, b);
            }
        }

        if (Commutative)
        {
            auto it2 = mapF.find(key2);
            if (it2 != mapF.end())
            {
                auto iit2 = it2->second.find(key1);
                if (iit2 != it2->second.end())
                {
                    return iit2->second(b, a);
                }
            }
        }

        throw(std::exception());
    }
private:
    std::unordered_map<std::type_index, std::unordered_map<std::type_index, func>> mapF;
};

int main()
{
    // мультиметод для наследников Shape
    // возращающий bool и являющийся коммутативным 
    Multimethod2<Shape, int, true> is_intersect;

    // добавляем реализацию мультиметода для двух прямоугольников
    //is_intersect.addImpl(typeid(Rectangle), typeid(Rectangle), is_intersect_r_r);

    // добавляем реализацию мультиметода для прямоугольника и треугольника
    is_intersect.addImpl(typeid(Triangle), typeid(Triangle), is_intersect_t_t);
    is_intersect.addImpl(typeid(Rectangle), typeid(Rectangle), is_intersect_r_r);
    is_intersect.addImpl(typeid(Quad), typeid(Quad), is_intersect_q_q);

    is_intersect.addImpl(typeid(Rectangle), typeid(Triangle), is_intersect_r_t);
    is_intersect.addImpl(typeid(Rectangle), typeid(Quad), is_intersect_r_q);
    //is_intersect.addImpl(typeid(Quad), typeid(Rectangle), is_intersect_q_r);
    is_intersect.addImpl(typeid(Quad), typeid(Triangle), is_intersect_q_t);

    // создаём две фигуры    
    Shape* s1 = new Triangle();
    Shape* s2 = new Rectangle();
    Shape* s3 = new Quad();

    std::cout << is_intersect.hasImpl(s1, s1) << std::endl; //t_t
    std::cout << is_intersect.hasImpl(s1, s2) << std::endl; //t_r
    std::cout << is_intersect.hasImpl(s1, s3) << std::endl; //q_t
    std::cout << is_intersect.hasImpl(s2, s1) << std::endl; //r_t
    std::cout << is_intersect.hasImpl(s2, s2) << std::endl; //r_r
    std::cout << is_intersect.hasImpl(s2, s3) << std::endl; //r_q
    std::cout << is_intersect.hasImpl(s3, s1) << std::endl; //q_t
    std::cout << is_intersect.hasImpl(s3, s2) << std::endl; //q_r
    std::cout << is_intersect.hasImpl(s3, s3) << std::endl; //q_q


    // проверяем, что реализация для s1 и s2 есть
    // if (is_intersect.hasImpl(s1, s1))
    // {
    //     // вызывается функция is_intersect_r_t(s2, s1)
    //     //is_intersect.call(s1, s2);
    //     //std::cout << is_intersect.call(s1, s2) << std::endl; //t_r
    //     std::cout << is_intersect.call(s2, s2) << std::endl; //r_r
    //     std::cout << is_intersect.call(s2, s1) << std::endl; //r_t
    //     std::cout << is_intersect.call(s2, s3) << std::endl; //r_q
    //     std::cout << is_intersect.call(s3, s2) << std::endl; //q_r
    //     std::cout << is_intersect.call(s3, s1) << std::endl; //q_t

    //     // Замечание: is_intersect_r_t ожидает,
    //     // что первым аргументом будет прямоугольник
    //     // а вторым треугольник, а здесь аргументы
    //     // передаются в обратном порядке. 
    //     // ваша реализация должна самостоятельно 
    //     // об этом позаботиться
    // }

    return 0;
}