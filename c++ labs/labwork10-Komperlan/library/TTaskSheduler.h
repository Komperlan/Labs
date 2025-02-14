#include "vector"
#include "memory"
#include "set"
#include "map"
#include "math.h"

template<class function, typename variable_1, typename variable_2>
class Task;

template<class Function, typename Variable>
class TaskSimple;

template<typename Task_>
struct TaskInfo{
public:
    TaskInfo(Task_* task): task_ptr(task){};
    Task_* task_ptr;
};

template<typename Task_>
struct TaskInfoLeft{
    TaskInfoLeft(Task_* task): task_ptr(task){};
    Task_* task_ptr;
};

template<typename Task_>
struct TaskInfoRight{
    TaskInfoRight(Task_* task): task_ptr(task){};
    Task_* task_ptr;
};

template<typename Task_>
struct TaskInfoLeftRight{
    TaskInfoLeftRight(Task_* task): task_ptr(task){};
    Task_* task_ptr;
};

template<typename Task_>
struct TaskInfoSimple{
    TaskInfoSimple(Task_* task): task_ptr(task){};
    Task_* task_ptr;
};

template<typename Task_>
struct TaskInfoSimple_{
    TaskInfoSimple_(Task_* task): task_ptr(task){};
    Task_* task_ptr;
};

template<typename Task_, typename TypeOut>
struct FutureTaskInfo{
    FutureTaskInfo(Task_ info, TypeOut): info_(info), out_(TypeOut()){};

    Task_ info_;
    TypeOut out_;
};

template<typename Task_, typename TypeOut>
struct FutureTaskInfo_{
    FutureTaskInfo_(Task_ info, TypeOut): info_(info), out_(TypeOut()){};

    Task_ info_;
    TypeOut out_;
};



struct var{
    template <typename _Ty>
    var(_Ty src): _inner(new inner<_Ty>(std::forward<_Ty>(src))) {};

    struct inner_base{
        using ptr = std::unique_ptr<inner_base>;
    };

    template <typename _Ty> struct inner : inner_base{
        inner(_Ty newval) : _value(newval) {}

    private:
        _Ty _value;
    };
    typename inner_base::ptr _inner;
};

class TaskSheduler {
public:
    template<typename Functor, typename variable_2, typename Task_, typename TypeOut>
    auto add(Functor func, FutureTaskInfo<Task_, TypeOut> var_left, variable_2 var_right) noexcept {
        auto task = Task<Functor, decltype(var_left), variable_2>(func, var_left, var_right);
        task.id = tasks.size();
        tasks.push_back(var(task));

        return TaskInfoLeft(reinterpret_cast<Task<Functor, decltype(var_left), variable_2>*>((*tasks.rbegin())._inner.release()));
    }

    template<typename Functor, typename Task_, typename TypeOut>
    auto add(Functor func, FutureTaskInfo<Task_, TypeOut> var_left) noexcept {
        auto task = TaskSimple<Functor, decltype(var_left)>(func, var_left);
        task.id = tasks.size();
        tasks.push_back(var(task));

        return TaskInfoSimple(reinterpret_cast<TaskSimple<Functor, decltype(var_left)>*>((*tasks.rbegin())._inner.release()));
    }

    template<typename Functor, typename Variable>
    auto add(Functor func, Variable var_left) noexcept {
        auto task = TaskSimple<Functor, decltype(var_left)>(func, var_left);
        task.id = tasks.size();
        tasks.push_back(var(task));

        return TaskInfoSimple_(reinterpret_cast<TaskSimple<Functor, decltype(var_left)>*>((*tasks.rbegin())._inner.release()));
    }

    template<typename Functor, typename variable_1, typename Task_, typename TypeOut>
    auto add(Functor func,variable_1 var_left, FutureTaskInfo<Task_, TypeOut> var_right) noexcept {
        auto task = Task<Functor, variable_1, decltype(var_right)>(func, var_left, var_right);
        task.id = tasks.size();
        tasks.push_back(var(task));

        return TaskInfoRight(reinterpret_cast<Task<Functor, variable_1, decltype(var_right)>*>((*tasks.rbegin())._inner.release()));
    }

    template<typename Functor, typename TaskLhs, typename TypeOutLhs, typename TaskRhs, typename TypeOutRhs>

    auto add(Functor func, FutureTaskInfo<TaskLhs, TypeOutLhs> var_left,
             FutureTaskInfo<TaskRhs, TypeOutRhs> var_right) noexcept {
        auto task = Task<Functor, decltype(var_left), decltype(var_right)>(func, var_left, var_right);
        task.id = tasks.size();
        tasks.push_back(var(task));

        return TaskInfoLeftRight(reinterpret_cast<Task<Functor, decltype(var_left), decltype(var_right)>*>((*tasks.rbegin())._inner.release()));
    }

    template<typename Functor, typename variable_1, typename variable_2>
    auto add(Functor func, variable_1 var_left, variable_2 var_right) {
        auto task = Task<Functor, variable_1, variable_2>(func, var_left, var_right);
        task.id = tasks.size();
        tasks.push_back(var(task));

        return TaskInfo(reinterpret_cast<Task<Functor, variable_1, variable_2>*>((*tasks.rbegin())._inner.release()));
    }

    void executeALL(){
        return;
    }

    template<typename TypeOut, typename Task_>
    TypeOut getResult(TaskInfo<Task_> task) {
        if(task.task_ptr->is_calculated){
            return *(reinterpret_cast<TypeOut*>((answers.find(task.task_ptr->id)->second)._inner.get()));
        }

        auto left = task.task_ptr->lhs_;
        auto right = task.task_ptr->rhs_;
        TypeOut answ = (*task.task_ptr).get_function()(left, right);
        answers.insert({task.task_ptr->id , answ});

        task.task_ptr->is_calculated = true;
        return *(reinterpret_cast<TypeOut*>((answers.find(task.task_ptr->id)->second)._inner.get()));
    }

    template<typename TypeOut, typename Task_>
    TypeOut getResult(TaskInfoLeft<Task_> task) {
        if(task.task_ptr->is_calculated){
            return *(reinterpret_cast<TypeOut*>((answers.find(task.task_ptr->id)->second)._inner.get()));
        }

        auto left = (*this).getResult<decltype(task.task_ptr->lhs_.out_)>(task.task_ptr->lhs_.info_);
        auto right = task.task_ptr->rhs_;
        TypeOut answ = (*task.task_ptr).get_function()(left, right);
        answers.insert({task.task_ptr->id , answ});

        task.task_ptr->is_calculated = true;
        return *(reinterpret_cast<TypeOut*>((answers.find(task.task_ptr->id)->second)._inner.get()));
    }

    template<typename TypeOut, typename Task_>
    TypeOut getResult(TaskInfoRight<Task_> task) {
        if(task.task_ptr->is_calculated){
            return *(reinterpret_cast<TypeOut*>((answers.find(task.task_ptr->id)->second)._inner.get()));
        }

        auto right = (*this).getResult<decltype(task.task_ptr->rhs_.out_)>(task.task_ptr->rhs_.info_);
        auto left = task.task_ptr->lhs_;
        TypeOut answ =  (*task.task_ptr).get_function()(left, right);
        answers.insert({task.task_ptr->id, answ});

        task.task_ptr->is_calculated = true;
        return *(reinterpret_cast<TypeOut*>((answers.find(task.task_ptr->id)->second)._inner.get()));
    }

    template<typename TypeOut, typename Task_>
    TypeOut getResult(TaskInfoLeftRight<Task_> task) {
        if(task.task_ptr->is_calculated){
            return *(reinterpret_cast<TypeOut*>((answers.find(task.task_ptr->id)->second)._inner.get()));
        }

        auto right = (*this).getResult<decltype(task.task_ptr->rhs_.out_)>(task.task_ptr->rhs_.info_);
        auto left = (*this).getResult<decltype(task.task_ptr->lhs_.out_)>(task.task_ptr->lhs_.info_);
        TypeOut answ =  (*task.task_ptr).get_function()(left, right);
        answers.insert({task.task_ptr->id, answ});

        task.task_ptr->is_calculated = true;
        return *(reinterpret_cast<TypeOut*>((answers.find(task.task_ptr->id)->second)._inner.get()));
    }

    template<typename TypeOut, typename Task_>
    TypeOut getResult(TaskInfoSimple<Task_> task) {
        if(task.task_ptr->is_calculated){
            return *(reinterpret_cast<TypeOut*>((answers.find(task.task_ptr->id)->second)._inner.get()));
        }

        auto left = (*this).getResult<decltype(task.task_ptr->var_.out_)>(task.task_ptr->var_.info_);
        TypeOut answ =  (*task.task_ptr).get_function()(left);
        answers.insert({task.task_ptr->id, answ});

        task.task_ptr->is_calculated = true;
        return *(reinterpret_cast<TypeOut*>((answers.find(task.task_ptr->id)->second)._inner.get()));
    }

    template<typename TypeOut, typename Task_>
    TypeOut getResult(TaskInfoSimple_<Task_> task) {
        if(task.task_ptr->is_calculated){
            return *(reinterpret_cast<TypeOut*>((answers.find(task.task_ptr->id)->second)._inner.get()));
        }

        auto left = task.task_ptr->var_;
        TypeOut answ =  (*task.task_ptr).get_function()(left);
        answers.insert({task.task_ptr->id, answ});

        task.task_ptr->is_calculated = true;
        return *(reinterpret_cast<TypeOut*>((answers.find(task.task_ptr->id)->second)._inner.get()));
    }

    template<typename TypeOut, typename _Task>
    auto getFutureResult(TaskInfo<_Task> task){
        return FutureTaskInfo(task, TypeOut());
    }

    template<typename TypeOut, typename _Task>
    auto getFutureResult(TaskInfoLeft<_Task> task){
        return FutureTaskInfo(task, TypeOut());
    }

    template<typename TypeOut, typename _Task>
    auto getFutureResult(TaskInfoSimple<_Task> task){
        return FutureTaskInfo(task, TypeOut());
    }

    template<typename TypeOut, typename _Task>
    auto getFutureResult(TaskInfoSimple_<_Task> task){
        return FutureTaskInfo_(task, TypeOut());
    }

    template<typename TypeOut, typename _Task>
    auto getFutureResult(TaskInfoRight<_Task> task){
        return FutureTaskInfo(task, TypeOut());
    }

    template<typename TypeOut, typename _Task>
    auto getFutureResult(TaskInfoLeftRight<_Task> task){
        return FutureTaskInfo(task, TypeOut());
    }

private:
    std::map<int, var> answers;
    std::vector<var> tasks;
};

template<class Function, typename Variable>
class TaskSimple {
public:
    TaskSimple (Function& func, Variable& var): func_(func), var_(var){};

    Function& get_function(){
        return func_;
    }

    bool is_calculated = false;
    int id = 0;
    Variable var_;
private:
    Function func_;
};

template<class Function, typename Variable_1, typename Variable_2>
class Task {
public:
    Task (Function& func, Variable_1& lhs, Variable_2& rhs): func_(func), lhs_(lhs), rhs_(rhs){};

    Function& get_function(){
        return func_;
    }

    bool is_calculated = false;
    int id = 0;
    Variable_1 lhs_;
    Variable_2 rhs_;
private:
    Function func_;
};