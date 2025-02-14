#pragma once
#include "algorithm"
#include "vector"
#include "wrapper.h"

namespace MyRange{
    template <typename Container, typename = void>
    struct IsAssociative : std::false_type {};

    template <typename Container>
    struct IsAssociative<Container, std::void_t<typename Container::key_type>> : std::true_type {};

    template<class Iterator>
    constexpr bool IsBidirectional(){
        return std::is_base_of_v<std::bidirectional_iterator_tag, typename Iterator::iterator_category>;
    }

    template<class Comparator>
    class filter {
    public:
        constexpr filter(const Comparator& cmp): func_(cmp){
            static_assert(std::is_convertible<Comparator, bool>::value, "Comparator must converteble to bool");
        }

        int type = 3;

        Comparator& get_func_(){
            return this->func_;
        }
    private:
        Comparator func_;
    };

    template<class Comparator>
    class transform {
    public:
        constexpr transform(const Comparator& cmp): func_(cmp){
            static_assert(std::is_convertible<Comparator, bool>::value, "Comparator must converteble to bool");
        };

        int type = 2;

        Comparator& get_func_(){
            return this->func_;
        }

    private:
        Comparator func_;
    };

    class reverse {
    public:
        int type = 4;

        auto get_func_(){
            return [](int i){return i * i;};
        }
    };

    class keys{
    public:
        int type = 5;
    };

    class values{
    public:
        int type = 6;
    };

    class take{
    public:
        take(size_t input_range):range(input_range) {}

        size_t& get_range() {
            return this->range;
        }

        int type = 1;

        auto get_func_(){
            return [](int i){return i * i;};
        }
    private:
        size_t range;
    };

    class drop{
    public:
        drop(size_t input_range):range(input_range) {}

        int type = 1;

        size_t& get_range() {
            return this->range;
        }

        auto get_func_(){
            return [](int i){return i * i;};
        }
    private:
        size_t range;
    };

    template<class Container>
    auto operator|(Container&& container, drop drop) {
        auto sup = container.begin();
        static_assert(IsBidirectional<decltype(container.begin())>(), "Iterator is not bidirectional");
        RangesIterator it(container.begin(), container.end(), drop, begin__);

        if(drop.get_range() > container.size()) {
            RangeContainer output(container.begin(), sup, container, drop, it);

            return std::move(output);
        }

        for(size_t i = 0; i < drop.get_range(); ++i) {
            ++sup;
        }

        RangeContainer output(sup, container.end(), container, drop, it);

        return std::move(output);
    }

    template<class Container>
    auto operator|(Container&& container, take take) {
        auto sup = container.end();
        static_assert(IsBidirectional<decltype(container.begin())>(), "Iterator is not bidirectional");
        RangesIterator it(container.begin(), container.end(), take, begin__);

        if(take.get_range() > container.size()) {
            std::move(RangeContainer(container.begin(), container.end(), container, take, it));
        }

        for(size_t i = 0; i < container.size() - take.get_range(); ++i) {
            --sup;
        }

       RangeContainer output(container.begin(), sup, container, take, it);

        return std::move(output);
    }

    template<class Container, class Predicate>
    auto operator|(Container&& container, transform<Predicate> transform) {
        static_assert(IsBidirectional<decltype(container.begin())>(), "Iterator is not bidirectional");
        TransformIterator it(container.begin(), container.end(), transform, begin__);
        RangeContainer output(container.begin(), container.end(), container, transform, it);

        return std::move(output);
    }

    template<class Container, class Predicate>
    auto operator|(Container&& container, filter<Predicate> filter) {
        static_assert(IsBidirectional<decltype(container.begin())>(), "Iterator is not bidirectional");
        auto it = RangesIterator(container.begin(), container.end(), filter, begin__);
        RangeContainer output(container.begin(), container.end(), container, filter, it);

        return std::move(output);
    }

    template<class Container>
    auto operator|(Container&& container, reverse reverse) {
        auto iter = container.begin();
        static_assert(IsBidirectional<decltype(iter)>(), "Iterator is not bidirectional");
        RangesIterator it(container.begin(), container.end(), reverse, begin__);
        RangeContainer output(--container.end(), --container.begin(), container, reverse, it);
        return std::move(output);
    }

    template<class Container>
    auto operator|(Container&& container, keys key) {
        static_assert(IsAssociative<std::remove_reference_t<Container>>::value, "Is not a Associative Container");
        AssociativeKeyIterator it(container.begin(), container.end(), key, begin__);
        RangeContainer output(container.begin(), container.end(), container, key, it);

        return std::move(output);
    }

    template<class Container>
    auto operator|(Container&& container, values value) {
        static_assert(IsAssociative<std::remove_reference_t<Container>>::value, "Is not a Associative Container");
        AssociativeValueIterator it(container.begin(), container.end(), value, begin__);
        RangeContainer output(container.begin(), container.end(), container, value, it);

        return std::move(output);
    }


}