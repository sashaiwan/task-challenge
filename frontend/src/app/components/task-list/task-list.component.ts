import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Task } from '../../models/task.model';
import { TaskService } from '../../services/task.service';

@Component({
  selector: 'app-task-list',
  imports: [CommonModule],
  templateUrl: './task-list.component.html',
})
export class TaskListComponent implements OnInit, OnDestroy {
  tasks: Task[] = [];
  loading = false;
  success: string | null = null;
  error: string | null = null;
  filterCompleted: boolean | undefined = undefined;
  private taskCreatedListener: any;

  constructor(private taskService: TaskService) {}

  ngOnInit(): void {
    this.loadTasks();
    this.taskCreatedListener = () => this.loadTasks();
    window.addEventListener('task-created', this.taskCreatedListener);
  }

  ngOnDestroy(): void {
    window.removeEventListener('task-created', this.taskCreatedListener);
  }

  loadTasks(): void {
    this.loading = true;
    this.error = null;

    this.taskService.getTasks(this.filterCompleted).subscribe({
      next: (tasks) => {
        this.tasks = tasks;
        this.loading = false;
      },
      error: (error) => {
        this.error = 'Failed to load tasks';
        this.loading = false;
        console.error('Error loading tasks:', error);
      },
    });
  }

  onTaskComplete(task: Task): void {
    if (task.id) {
      const updatedTask = { ...task, isCompleted: !task.isCompleted };
      this.taskService.updateTask(task.id, updatedTask).subscribe({
        next: (response) => {
          const index = this.tasks.findIndex((t) => t.id === task.id);
          if (index !== -1) {
            this.tasks[index] = response;
          }
        },
        error: (error) => {
          this.error = 'Failed to update task';
          console.error('Error updating task:', error);
        },
      });
    }
  }

  deleteTask(task: Task): void {
    if (task.id && confirm('Are you sure you want to delete this task?')) {
      this.taskService.deleteTask(task.id).subscribe({
        next: () => {
          this.tasks = this.tasks.filter((t) => t.id !== task.id);
          this.handleSuccess('Task deleted successfully');
        },
        error: (error) => {
          this.error = 'Failed to delete task';
          console.error('Error deleting task:', error);
        },
      });
    }
  }

  private handleSuccess(message: string) {
    this.success = message;
    setTimeout(() => (this.success = null), 2000);
  }
}
