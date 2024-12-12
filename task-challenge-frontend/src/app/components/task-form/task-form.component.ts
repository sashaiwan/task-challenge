import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { TaskService } from '../../services/task.service';
import { DateValidators } from '../../validators/date.validators';

@Component({
  selector: 'app-task-form',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './task-form.component.html',
})
export class TaskFormComponent {
  taskForm: FormGroup;
  submitted = false;
  error: string | null = null;
  success: string | null = null;
  minDate: string;

  constructor(private fb: FormBuilder, private taskService: TaskService) {
    this.minDate = new Date().toISOString().split('T')[0];

    this.taskForm = this.fb.group({
      title: ['', [Validators.required, Validators.maxLength(255)]],
      description: ['', [Validators.maxLength(255)]],
      dueDate: ['', Validators.required, DateValidators.futureDateOnly],
    });
  }

  onSubmit(): void {
    this.submitted = true;
    this.error = null;
    this.success = null;

    if (this.taskForm.valid) {
      const task = {
        ...this.taskForm.value,
        isCompleted: false,
      };

      this.taskService.createTask(task).subscribe({
        next: () => {
          this.success = 'Task created successfully';
          this.taskForm.reset();
          this.submitted = false;
          window.dispatchEvent(new CustomEvent('task-created'));

          // Cleanup UI states
          setTimeout(() => (this.success = null), 2000);
        },
        error: (error) => {
          this.error = 'Failed to create task';
          console.error('Error creating task:', error);
        },
      });
    }
  }
}
