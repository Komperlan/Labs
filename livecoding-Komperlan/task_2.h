#include "iostream"

template<class IterLhs, class IterRhs>
class MyIter {
public:
    MyIter(IterLhs lhs, IterRhs rhs): lhs_(lhs), rhs_(rhs){}

    auto operator*() {
        return std::pair(*lhs_, *rhs_);
    }

    MyIter& operator++() {
        ++lhs_;
        ++rhs_;
        return *this;
    }

    bool operator ==(MyIter<IterLhs, IterRhs>& other) {
        if(lhs_ == other.lhs_ || rhs_ == other.rhs_) {
            return true;
        }
        return false;
    }

private:
    IterLhs lhs_;
    IterRhs rhs_;
};

template<class ContainerLhs, class ContainerRhs>
class pythonzip {
private:

public:
    pythonzip(ContainerLhs& lhs, ContainerRhs& rhs): lhs_(lhs), rhs_(rhs){
        size = std::min(lhs_.size(), rhs_.size());
    }

    auto begin() {
        return MyIter(lhs_.begin(), rhs_.begin());
    }

    auto end() {
        return MyIter(lhs_.end(), rhs_.end());
    }

private:
    size_t size;
    ContainerLhs& lhs_;
    ContainerRhs& rhs_;
};