#pragma once

#include <iterator>
#include "memory"
#include "iostream"

namespace std_dlc {
    template<typename T>
    struct Node {
        T key;
        Node* right;
        Node* left;
        Node* parrent;
    };


    template<typename T, typename Allocator  = std::allocator<T>>
    class BST;

    template<typename T>
    class InOrderIterator : public std::iterator<std::bidirectional_iterator_tag, T> {
    public:
        friend class BST<T>;

        typedef InOrderIterator<T> Iterator;
        typedef std::bidirectional_iterator_tag iterator_category;
        using size_type = typename BST<T>::size_type;
        using value_type = Node<T>;
        using reference = value_type&;
        using pointer = value_type*;
        using key_type = typename BST<T>::key_type;

        InOrderIterator(const InOrderIterator &iter) {
            *this = iter;
        }

        InOrderIterator() {
            info = nullptr;
        }

        bool operator==(const InOrderIterator<T> &iter) {
            return this->info->key == iter.info->key;
        }

        bool operator!=(const InOrderIterator<T> &iter) {
            return this->info->key != iter.info->key;
        }

        InOrderIterator<T> &operator++() {
            if (info->left == nullptr) {
                if (info->right == nullptr) {
                    if (info->parrent->left == info) {
                        return *this = InOrderIterator(info->parrent, true);
                    }
                    while (info->parrent->right == info) {
                        info = info->parrent;
                    }
                    return *this = InOrderIterator(info->parrent, true);
                }
            }
            if(info->right == nullptr) {
                if (info->parrent->left == info) {
                    return *this = InOrderIterator(info->parrent, true);
                }
                while (info->parrent->right == info) {
                    info = info->parrent;
                }
                return *this = InOrderIterator(info->parrent, true);
            }
            info = info->right;
            while (info->left != nullptr) {
                info = info->left;
            }
            return *this = InOrderIterator(info);
        }

        InOrderIterator<T> &operator--() {
            if (info->right == nullptr) {
                if (info->left == nullptr) {
                    if (info->parrent->right == info) {
                        return *this = InOrderIterator(info->parrent, true);
                    }
                    while (info->parrent->left == info) {
                        info = info->parrent;
                    }
                    return *this = InOrderIterator(info->parrent, true);
                }
            }
            info = info->left;
            while (info->right != nullptr) {
                info = info->right;
            }
            return *this = InOrderIterator(info);
        }

        T& operator*() const {
            return this->info->key;
        }

        InOrderIterator(Node<T> *other) {
            info = other;
        }

        InOrderIterator(Node<T> *other, bool type_iter) {
            info = other;
            is_from_under = type_iter;
        }
        Node<T> *info;

    private:

        bool is_from_under = false;
    };

    template<typename T>
    class PostOrderIterator : public std::iterator<std::bidirectional_iterator_tag, T> {
        friend class BST<T>;
    public:
        typedef PostOrderIterator<T> Iterator;
        typedef std::bidirectional_iterator_tag iterator_category;
        using size_type = typename  std::allocator_traits<std::allocator<T>>::size_type;
        using value_type = Node<T>;
        using reference = value_type&;
        using pointer = value_type*;
        using key_type = typename BST<T>::key_type;

        PostOrderIterator() {
            info = nullptr;
        }

        PostOrderIterator(const PostOrderIterator &iter) {
            *this = iter;
        }

        bool operator==(const PostOrderIterator<T> &iter) {
            return this->info->key == iter.info->key;
        }

        bool operator!=(const PostOrderIterator<T> &iter) {
            return this->info->key != iter.info->key;
        }

        PostOrderIterator<T>& operator++() {
            if (info->left == nullptr || is_from_under) {
                if (info->right == nullptr || is_from_under) {
                    if (info->parrent == nullptr) {

                        return *this = PostOrderIterator<T>(info);
                    }
                    if ((info->parrent->right == nullptr
                         || info->parrent->right == info)) {
                        info = info->parrent;

                        return *this = PostOrderIterator<T>(info, true);
                    }
                    info = info->parrent->right;
                    while (info->left != nullptr) {
                        info = info->left;
                    }

                    return *this = PostOrderIterator<T>(info);
                }

                return *this = PostOrderIterator<T>(info->right);;
            }

            return *this = PostOrderIterator<T>(info->left);;
        }

        PostOrderIterator<T>& operator--() {

        }

        T& operator*() {
            return this->info->key;
        }

        PostOrderIterator(Node<T> *other) {
            info = other;
        }

        PostOrderIterator(Node<T> *other, bool type_iter) {
            info = other;
            is_from_under = type_iter;
        }
    private:
        Node<T> *info;
        bool is_from_under = false;
    };

    template<typename T>
    class PreOrderIterator : public std::iterator<std::bidirectional_iterator_tag, T>{
        friend class BST<T>;
    public:
        typedef PreOrderIterator<T> Iterator;
        typedef std::bidirectional_iterator_tag iterator_category;
        using size_type = typename  std::allocator_traits<std::allocator<T>>::size_type;
        using value_type = Node<T>;
        using reference = value_type&;
        using pointer = value_type*;
        using key_type = T;

        PreOrderIterator(const PreOrderIterator& iter) {
            this->info = iter.info;
        }

        PreOrderIterator() {
            info = nullptr;
        }

        PreOrderIterator(value_type* other) {
            info = other;
        }

        bool operator==(const PreOrderIterator<key_type > &iter) const {
            return this->info->key == iter.info->key;
        }

        bool operator!=(const PreOrderIterator<key_type> &iter) const {
            return this->info->key != iter.info->key;
        }

        PreOrderIterator<key_type>& operator++() {
            if (info->left == nullptr) {
                if (info->right == nullptr) {
                    while (info->parrent->right == nullptr || info->parrent->right == info) {
                        info = info->parrent;
                        if (info->parrent == nullptr) {

                            return *this = PreOrderIterator(info);
                        }
                    }

                    return *this = PreOrderIterator(info->parrent->right);
                }

                return *this = PreOrderIterator(info->right);
            }

            return *this = PreOrderIterator(info->left);
        }

        PreOrderIterator<T>& operator--() {
            if (info->parrent == nullptr) {
                info = info->left;
                while (info->right != nullptr) {
                    info = info->right;
                }

                return *this = PreOrderIterator(info);
            }
            if (info->parrent->left == nullptr || info->parrent->left == info) {

                return *this = PreOrderIterator(info->parrent);
            }
            info = info->parrent->left;
            while (info->right != nullptr) {
                info = info->right;
            }

            return *this = PreOrderIterator(info);
        }

        key_type& operator*() {
            return this->info->key;
        }
    private:
        value_type* info;
    };

    template<typename T, typename Allocator>
    class BST{
    public:
        using key_type = T;
        using value_type = Node<key_type>;
        using reference = value_type&;
        using reverse_iterator = std::reverse_iterator<PreOrderIterator<key_type>>;
        using const_reverse_iterator = const reverse_iterator;
        using const_iterator = const PreOrderIterator<key_type>;
        using allocator = typename  Allocator::template rebind<Node<key_type>>::other;
        using size_type = typename std::allocator_traits<allocator>::size_type;
        using alloc = typename  std::allocator_traits<allocator>;

        PostOrderIterator<key_type> find(PostOrderIterator<key_type>, const key_type& key) {
            Node<key_type>* node = root->left;

            while(node!= nullptr) {
                if(node->key > key) {
                    node = node->left;
                } else if(node->key < key) {
                    node = node->right;
                } else if(node->key == key) {
                    return PostOrderIterator<key_type>(node);
                }
            }
            return end(PostOrderIterator<key_type>());
        }

        InOrderIterator<key_type> find(const key_type& key) {
            Node<key_type>* node = root->left;

            while(node!= nullptr) {
                if(node->key > key) {
                    node = node->left;
                } else if(node->key < key) {
                    node = node->right;
                } else if(node->key == key) {
                    return InOrderIterator<key_type>(node);
                }
            }
            return end(InOrderIterator<key_type>());
        }

        std::pair<PreOrderIterator<key_type>, bool> emplace (const key_type& key) {
            return insert(key);
        };

        void clear() {
            PostOrderIterator<key_type> iter = begin(PostOrderIterator<key_type>());
            while(iter != end(PostOrderIterator<key_type>())){
                iter = erase(*iter).first;
            }
        };

        std::pair<PostOrderIterator<key_type>, bool> erase(const key_type& key) {
            PostOrderIterator<key_type> iter = find(PostOrderIterator<key_type>(), key);

            Node<T>* target = iter.info;

            if(iter != end(PostOrderIterator<key_type>())) {
                if (target->left && target->right) {
                    Node<T>* localMax = target->left;
                    while (localMax->right != nullptr) {
                        localMax = localMax->right;
                    }

                    target->key = localMax->key;

                    if(localMax->parrent->right == localMax) {
                        localMax->parrent->right = nullptr;
                    } else {
                        localMax->parrent->left = nullptr;
                    }

                    alloc::deallocate(alloc_, localMax, 1);
                    --size_;

                    return {PostOrderIterator<key_type>(target), true};
                } else if (target->left != nullptr) {
                    if (target == target->parrent->left) {
                        target->parrent->left = target->left;
                    } else {
                        target->parrent->right = target->left;
                    }
                } else if (target->right != nullptr) {
                    if (target == target->parrent->right) {
                        target->parrent->right = target->right;
                    } else {
                        target->parrent->left = target->right;
                    }
                } else {
                    return {PostOrderIterator<key_type>(target), false};
                }
            }
        }

        bool contains(const key_type& key) {
            return find(PostOrderIterator<key_type>(), key) != end(PostOrderIterator<key_type>());
        }

        void merge(BST<key_type>& other) {
            PreOrderIterator<key_type> iter = other.begin();
            for(int i = 0; i < other.size(); ++i) {
                insert(*iter);
                ++iter;
            }
        }

        size_t count(key_type key) {
            if(contains(key)) {
                return 1;
            }
        }

        std::pair<PreOrderIterator<key_type>, bool> insert(const key_type& data) {
            value_type* input = alloc::allocate(alloc_, kNodeNeed);

            alloc::construct(alloc_, input);

            value_type *node = root->left;
            ++size_;

            input->key = data;
            if (root->left == nullptr) {
                root->left = input;
                input->parrent = root;

                return {PreOrderIterator(input), true};
            }

            while (node != nullptr) {
                if (input->key > node->key) {
                    if (node->right != nullptr) {
                        node = node->right;
                    } else {
                        input->parrent = node;
                        node->right = input;
                        break;
                    }
                } else if (input->key < node->key) {
                    if (node->left != nullptr) {
                        node = node->left;
                    } else {
                        input->parrent = node;
                        node->left = input;
                        break;
                    }
                } else if (input->key == node->key) {
                    --size_;
                    alloc::deallocate(alloc_, input, 1);
                    return {PreOrderIterator(input), false};
                }
            }

            return {PreOrderIterator(input), true};
        }

        bool empty() {
            return size_ == 0;
        }

        size_t size() const noexcept {
            return size_;
        }

        ~BST() {
            this->clear();
        }

        BST() {
            root = alloc::allocate(alloc_, 1);
            (*root) = Node<key_type>();
        };

        BST(PreOrderIterator<key_type> iter, PreOrderIterator<key_type> end){
            root = alloc::allocate(alloc_, 1);
            (*root) = Node<key_type>();
            while(iter != end){
                insert(*iter);
                ++iter;
            }
        }

        BST(BST<key_type>& other) {
            this->alloc_ = alloc::propagate_on_container_copy_construction(alloc_);
            root = alloc::allocate(alloc_, 1);
            (*root) = Node<key_type>();
            PreOrderIterator<key_type> iter = other.begin(PreOrderIterator<int>());
            for (long long count = 0; count < other.size(); ++count) {
                (*this).insert(*iter);
                ++iter;
            }
        }

        key_type* traverse(PreOrderIterator<key_type>) {
            key_type *out = new key_type[this->size()];
            long long point = 0;

            PreOrderIterator<key_type> iter = begin();

            for (; point < size_; ++point) {
                out[point] = *iter;
                ++iter;
            }

            return out;
        }

        InOrderIterator<key_type> lower_bound(const key_type key) {
            return --find(key);
        }

        InOrderIterator<key_type> upper_bound(const key_type key) {
            return ++find(key);
        }

        key_type* reverse_traverse() {
            key_type *out = new key_type[this->size()];
            long long point = 0;

            PreOrderIterator<key_type> iter = end(PreOrderIterator<key_type>());

            for (; point < size_; ++point) {
                --iter;
                out[point] = iter.info->key;
            }

            return out;
        }

        key_type* traverse(PostOrderIterator<key_type>) {
            key_type *out = new key_type[this->size()];
            long long point = 0;

            PostOrderIterator<key_type> iter = begin(PostOrderIterator<key_type>());

            for (; point < size_; ++point) {
                out[point] = iter.info->key;

                ++iter;
            }

            return out;
        }

        key_type* traverse(InOrderIterator<key_type>) {
            key_type *out = new key_type[this->size()];
            long long point = 0;

            InOrderIterator<key_type> iter = begin(InOrderIterator<key_type>());

            for (; point < size_; ++point) {
                out[point] = iter.info->key;
                ++iter;
            }

            return out;
        }

        const_iterator cbegin() const noexcept {
            if(root->left != nullptr) {
                return PreOrderIterator<T>(root->left);
            }

            return PreOrderIterator<T>(root);
        }

        PreOrderIterator<T> begin() noexcept{
            if(root->left != nullptr) {
                return PreOrderIterator<T>(root->left);
            }

            return PreOrderIterator<T>(root);
        };

        reverse_iterator rbegin() noexcept {
            return reverse_iterator(this->end());
        };

        reverse_iterator rend() noexcept {
            return reverse_iterator(this->begin());
        }

        const_reverse_iterator crbegin() const noexcept {
            return const_reverse_iterator(root);
        };

        const_reverse_iterator crend() const noexcept {
            return const_reverse_iterator((*this).cbegin());
        };

        PreOrderIterator<key_type> begin(PreOrderIterator<key_type>) noexcept {
            if(root->left != nullptr) {
                return PreOrderIterator<key_type>(root->left);
            }

            return PreOrderIterator<key_type>(root);
        };

        PreOrderIterator<key_type> end() noexcept {
            return root;
        }

        PreOrderIterator<key_type> end(PreOrderIterator<key_type>) noexcept {
            return root;
        }

        const_iterator cend() const noexcept {
            return root;
        }

        InOrderIterator<key_type> begin(InOrderIterator<key_type>) noexcept {
            if(root->left == nullptr){
                return InOrderIterator<key_type>(root);
            }
            Node<key_type> *sup = root->left;
            while (sup->left != nullptr) {
                sup = sup->left;
            }

            return InOrderIterator<key_type>(sup);
        };

        InOrderIterator<key_type> end(InOrderIterator<key_type>) noexcept {
            return root;
        }

        InOrderIterator<key_type> cend(InOrderIterator<key_type>) const noexcept {
            return root;
        }

        PostOrderIterator<key_type> begin(PostOrderIterator<key_type>) noexcept {
            if(root->left == nullptr){
                return PostOrderIterator<key_type>(root);
            }

            Node<key_type> *sup = root->left;
            while (sup->left != nullptr) {
                sup = sup->left;
            }

            return PostOrderIterator<key_type>(sup);
        };

        PostOrderIterator<key_type> cbegin(PostOrderIterator<key_type>) const noexcept {
            if(root->left == nullptr){
                return PostOrderIterator<key_type>(root);
            }

            Node<key_type> *sup = root->left;
            while (sup->left != nullptr) {
                sup = sup->left;
            }

            return PostOrderIterator<key_type>(sup);
        };

        PostOrderIterator<key_type> end(PostOrderIterator<key_type>) noexcept {
            return root;
        }

        PostOrderIterator<key_type> cend(PostOrderIterator<key_type>) const noexcept {
            return root;
        }

        size_t max_size() const{
            return alloc::max_size;
        }

    private:
        size_t size_ = 0;

        allocator alloc_;

        static const size_t kNodeNeed = 1;

        Node<key_type>* root = nullptr;
    };
}