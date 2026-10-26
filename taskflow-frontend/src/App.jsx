import { useState } from 'react';
import TaskForm from './components/TaskForm.jsx';
import TaskList from './components/TaskList.jsx';
import './App.css';

const initialTasks = [
  { id: 1, title: 'Review React components', completed: false },
  { id: 2, title: 'Practice props and state', completed: true },
];

function App() {
  const [tasks, setTasks] = useState(initialTasks);

  function addTask(title) {
    const newTask = {
      id: Date.now(),
      title,
      completed: false,
    };

    // Create a new array; do not mutate the existing state array.
    setTasks((currentTasks) => [...currentTasks, newTask]);
  }

  function toggleTask(taskId) {
    setTasks((currentTasks) =>
      currentTasks.map((task) =>
        task.id === taskId
          ? { ...task, completed: !task.completed }
          : task,
      ),
    );
  }

  return (
    <main className="app">
      <header>
        <p className="eyebrow">Day 3 practice</p>
        <h1>TaskFlow</h1>
        <p>Capture a task and track its completion.</p>
      </header>

      <TaskForm onAddTask={addTask} />
      <TaskList tasks={tasks} onToggleTask={toggleTask} />
      <p className="task-count">
        {tasks.filter((task) => !task.completed).length} open task(s)
      </p>
    </main>
  );
}

export default App;
