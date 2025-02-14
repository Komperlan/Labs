template<class Container, class Predicate>
auto FindNot(Container& container, Predicate predicate) {
    auto iter = container.begin();
    while(iter != container.end()) {
        if(!predicate(*iter)){
            return iter;
        }
        ++iter;
    }
}
