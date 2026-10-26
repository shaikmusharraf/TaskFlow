import { useState } from 'react';

function TaskForm({ onAddTask }) {
    const [title, setTitle] = useState('');

    function handleSubmit(event) {
        event.preventDefault();

        const trimmedTitle = title.trim();
        if (!trimmedTitle) return;

        onAddTask(trimmedTitle);
        setTitle('');
    }

    return (
        <form className="task-form" onSubmit={handleSubmit}>
            <label htmlFor="task-title">New task</label>
            <div className="task-form__row">
                <input
                    id="task-title"
                    value={title}
                    onChange={(event) => setTitle(event.target.value)}
                    placeholder="For example, revise useState"
                />
                <button type="submit">Add task</button>
            </div>
        </form>
    );
}

export default TaskForm;
