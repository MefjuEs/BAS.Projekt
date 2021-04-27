import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable()
export class NotificationService {

  constructor(private snackBar: MatSnackBar) { }

  showSnackBarNotification(message, buttonText, className) {
    this.snackBar.open(message, buttonText, {
      horizontalPosition: 'end',
      verticalPosition: 'top',
      duration: 10000,
      panelClass: [className]
    })
  }
}
