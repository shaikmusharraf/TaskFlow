import TaskItem from './TaskItem.jsx';

function TaskList({ tasks, onToggleTask }) {
    if (tasks.length === 0) {
        return <p className="empty-state">No tasks yet. Add your first task above.</p>;
    }

    return (
        <ul className="task-list">
            {tasks.map((task) => (
                <TaskItem
                    key={task.id}
                    task={task}
                    onToggle={onToggleTask}
                />
            ))}
        </ul>
    );
}

export default TaskList;
