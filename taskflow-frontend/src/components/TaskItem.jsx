function TaskItem({ task, onToggle }) {
    return (
        <li className={task.completed ? 'task task--done' : 'task'}>
            <label>
                <input
                    type="checkbox"
                    checked={task.completed}
                    onChange={() => onToggle(task.id)}
                />
                <span>{task.title}</span>
            </label>
        </li>
    );
}

export default TaskItem;