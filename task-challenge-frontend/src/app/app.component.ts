import { Component } from '@angular/core';
import { TaskFormComponent } from './components/task-form/task-form.component';
import { TaskListComponent } from './components/task-list/task-list.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [TaskListComponent, TaskFormComponent],
  template: `
    <div class="min-h-screen bg-gray-100">
      <div class="max-w-4xl mx-auto py-8 px-4 sm:px-6 lg:px-8">
        <header class="mb-8">
          <h1 class="text-3xl font-bold text-gray-900">Task Management</h1>
        </header>

        <div class="space-y-6">
          <app-task-list></app-task-list>
          <app-task-form></app-task-form>
        </div>
      </div>
    </div>
  `,
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'task-challenge-frontend';
}
