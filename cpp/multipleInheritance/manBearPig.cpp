
#include <iostream>

struct Unit  
{
    explicit Unit(size_t id) 
        : id_(id) 
    {}

    size_t id() const { return id_; }

private:
    size_t id_;
};

// базовый класс для животных
struct Animal : virtual Unit
{
    // name хранит название животного
    // "bear" для медведя
    // "pig" для свиньи
    Animal(std::string const & name, size_t id) 
        : 
    name_(name),
    Unit(id)
    { }

    std::string const& name() const { return name_; }
private:
    std::string name_;
};

// класс для человека
struct Man : virtual Unit
{
    explicit Man(size_t id) 
        :
    Unit(id + 1)
    { }
    // ...
};

// класс для медведя
struct Bear : Animal
{
    explicit Bear(size_t id)
        :
    Unit(id + 2),    
    Animal("bear", id + 3)    
    { }
    // ...
};

// класс для свиньи
struct Pig : Animal
{
    explicit Pig(size_t id)
        :
    Unit(id + 4),        
    Animal("pig", id + 5)    
    { }
    // ...
};

// класс для челмедведосвина
struct ManBearPig: Man, Bear, Pig
{
    ManBearPig(size_t id)
        :
    Unit(id + 6),    
    //Animal("manBearPig", id),
    Man(id + 7),
    Bear(id + 8),
    Pig(id + 9)
    { }    
    // ...
};

int main()
{
    ManBearPig mbp(1);

    std::cout << mbp.id() << std::endl;
    std::cout << ((Unit)mbp).id() << std::endl;
    std::cout << ((Animal)((Bear)mbp)).id() << std::endl;
    std::cout << ((Man)mbp).id() << std::endl;
    std::cout << ((Bear)mbp).id() << std::endl;
    std::cout << ((Pig)mbp).id() << std::endl;
    std::cout << ((Bear)mbp).name() << std::endl;
    std::cout << ((Pig)mbp).name() << std::endl;

    return 0;
}