import { AbstractControl, ValidationErrors } from '@angular/forms';
import { Observable, of } from 'rxjs';

export class DateValidators {
  static futureDateOnly(
    control: AbstractControl
  ): Observable<ValidationErrors | null> {
    if (!control.value) {
      return of({ required: true });
    }

    const today = new Date();
    today.setHours(0, 0, 0, 0);

    const inputDate = new Date(control.value);
    inputDate.setHours(0, 0, 0, 0);

    return inputDate >= today ? of(null) : of({ pastDate: true });
  }
}
