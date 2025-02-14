#pragma once
#include "iostream"

enum types {drop = 1, trans = 2, filt = 3, rev = 4, keys = 5, values = 6};

enum iter_types {begin__ = 1, end__ = 2};



template<typename Tag, class Iterator>
class RangesIterator;

template<class Tag, class Iterator>
class TransformIterator;

template<typename Iterator, typename RangeIt, typename Container, typename Tag>
class RangeContainer {
public:
    constexpr RangeContainer(Iterator input_begin, Iterator input_end, Container& input_container, Tag input_tag_, RangeIt):
            begin_(input_begin), end_(input_end),
            container_(input_container), tag_(input_tag_){
    };

    using size_type = decltype(std::declval<Container>().size());
    using iterator = RangesIterator<Tag, Iterator>;

    size_t size() {
        return container_.size();
    }

    auto begin() {
        return RangeIt(begin_, end_, tag_, begin__);
    }

    auto end() {
        return RangeIt(begin_, end_, tag_, end__);
    }

private:
    Iterator begin_;
    Iterator end_;
    Container& container_;
    Tag tag_;
};

template<class Tag, class Iterator>
class RangesIterator{
public:
    using iterator_category = std::bidirectional_iterator_tag;
    RangesIterator(Iterator input_begin, Iterator input_end, Tag input_tag_, int type):
        begin_(input_begin), end_(input_end),  tag_(input_tag_), it_(input_begin){
        if(type == begin__) {
            it_ = begin_;
        } else {
            it_ = end_;
        }
    }

    RangesIterator& operator++(){
        if(tag_.type == filt) {
            return FilterIncrement();
        } else if(tag_.type == rev) {
            if(it_ != end_) {
                --it_;
            }
            return *this;
        } else {
            if(it_ != end_) {
                ++it_;
            }
        }
        return *this;
    }

    auto operator*() {
        return *it_;
    }

    RangesIterator& operator--(){
        if(tag_.type == filt) {
            return FilterDecrement();
        } else if(tag_.type == rev) {
            if(it_ != begin_) {
                ++it_;
            }
            return *this;
        } else {
            if(it_ != begin_) {
                --it_;
            }
            return *this;
        }
    }

    bool operator==(RangesIterator& other) {
        return other.it_ == this->it_;
    }

    bool operator!=(RangesIterator& other) {
        return !(other == *this);
    }
private:
    Iterator begin_;
    Iterator end_;
    Iterator it_;
    Tag tag_;

    RangesIterator& FilterDecrement(){
        do {
            --it_;
        } while(!tag_.get_func_()(*it_) && it_ != begin_);

        return *this;
    }

    RangesIterator& FilterIncrement(){
        do {
            ++it_;
        } while(!tag_.get_func_()(*it_) && it_ != end_);
        return *this;
    }
};

template<class Tag, class Iterator>
class TransformIterator{
public:
    using iterator_category = std::bidirectional_iterator_tag;
    TransformIterator(Iterator input_begin, Iterator input_end, Tag input_tag_, int type):
            begin_(input_begin), end_(input_end),  tag_(input_tag_), it_(input_begin) {
        if(type == begin__) {
            it_ = begin_;
        } else {
            it_ = end_;
        }
    }

    TransformIterator& operator++() {
        if(it_ != end_) {
            ++it_;
        }
        return *this;
    }

    auto operator*() {
        return tag_.get_func_()(*it_);
    }

    TransformIterator& operator--() {
        if(it_ != begin_) {
            --it_;
        }
        return *this;

    }

    bool operator==(TransformIterator& other) {
        return other.it_ == this->it_;
    }

    bool operator!=(TransformIterator& other) {
        return !(other == *this);
    }
private:
    Iterator begin_;
    Iterator end_;
    Iterator it_;
    Tag tag_;
};

template<class Tag, class Iterator>
class AssociativeKeyIterator {
public:
    using iterator_category = std::bidirectional_iterator_tag;
    AssociativeKeyIterator(Iterator input_begin, Iterator input_end, Tag input_tag_, int type) :
            begin_(input_begin), end_(input_end), tag_(input_tag_), it_(input_begin) {
        if (type == begin__) {
            it_ = begin_;
        } else {
            it_ = end_;
        }
    }

    AssociativeKeyIterator& operator++() {
        if (it_ != end_) {
            ++it_;
        }
        return *this;
    }

    auto constexpr operator*() {
        return it_->first;
    }

    AssociativeKeyIterator& operator--() {
        if (it_ != begin_) {
            --it_;
        }
        return *this;

    }

    bool operator==(AssociativeKeyIterator& other) {
        return other.it_ == this->it_;
    }

    bool operator!=(AssociativeKeyIterator& other) {
        return !(other == *this);
    }

private:
    Iterator begin_;
    Iterator end_;
    Iterator it_;
    Tag tag_;
};


template<class Tag, class Iterator>
class AssociativeValueIterator {
public:
    using iterator_category = std::bidirectional_iterator_tag;
    AssociativeValueIterator(Iterator input_begin, Iterator input_end, Tag input_tag_, int type) :
            begin_(input_begin), end_(input_end), tag_(input_tag_), it_(input_begin) {
        if (type == begin__) {
            it_ = begin_;
        } else {
            it_ = end_;
        }
    }

    AssociativeValueIterator& operator++() {
        if (it_ != end_) {
            ++it_;
        }
        return *this;
    }

    auto constexpr operator*() {
        return it_->second;
    }

    AssociativeValueIterator& operator--() {
        if (it_ != begin_) {
            --it_;
        }
        return *this;

    }

    bool operator==(AssociativeValueIterator& other) {
        return other.it_ == this->it_;
    }

    bool operator!=(AssociativeValueIterator& other) {
        return !(other == *this);
    }

private:
    Iterator begin_;
    Iterator end_;
    Iterator it_;
    Tag tag_;
};